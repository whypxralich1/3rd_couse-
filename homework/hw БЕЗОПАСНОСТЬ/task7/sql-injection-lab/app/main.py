import asyncio
import sys
from fastapi import FastAPI, Depends, HTTPException, Path, Query
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from contextlib import asynccontextmanager

if sys.platform == "win32":
    asyncio.set_event_loop_policy(asyncio.WindowsSelectorEventLoopPolicy())

from app.db import get_session
from app.auth import get_user_by_token
from app.models import Order, Good, User, Token
from app.schemas import AuthRequest, TokenResponse
from passlib.context import CryptContext
import secrets

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")


@asynccontextmanager
async def lifespan(app: FastAPI):
    yield


app = FastAPI(title="SQLi Lab (safe edition)", lifespan=lifespan)


@app.post("/auth/token", response_model=TokenResponse)
async def auth_token(body: AuthRequest, session: AsyncSession = Depends(get_session)):
    stmt = select(User).where(User.name == body.name)
    result = await session.execute(stmt)
    user = result.scalar_one_or_none()

    if not user or not pwd_context.verify(body.password, user.password_hash):
        raise HTTPException(status_code=401, detail="Invalid credentials")

    stmt = select(Token.value).where(Token.user_id == user.id, Token.is_valid == True).limit(1)
    result = await session.execute(stmt)
    existing_token = result.scalar()

    if existing_token:
        return {"token": existing_token}

    token_value = secrets.token_urlsafe(64)
    new_token = Token(user_id=user.id, value=token_value, is_valid=True)
    session.add(new_token)
    await session.commit()

    return {"token": token_value}


@app.get("/orders")
async def list_orders(
    limit: int = Query(10, ge=1, le=100),
    offset: int = Query(0, ge=0),
    user: dict = Depends(get_user_by_token),
    session: AsyncSession = Depends(get_session),
):
    stmt = (
        select(Order)
        .where(Order.user_id == user["id"])
        .order_by(Order.created_at.desc())
        .limit(limit)
        .offset(offset)
    )
    result = await session.execute(stmt)
    orders = result.scalars().all()

    return [
        {
            "id": o.id,
            "user_id": o.user_id,
            "created_at": o.created_at.isoformat(),
        }
        for o in orders
    ]


@app.get("/orders/{order_id}")
async def order_details(
    order_id: int = Path(..., ge=1),
    user: dict = Depends(get_user_by_token),
    session: AsyncSession = Depends(get_session),
):
    stmt = select(Order).where(Order.id == order_id, Order.user_id == user["id"])
    result = await session.execute(stmt)
    order = result.scalar_one_or_none()

    if not order:
        raise HTTPException(status_code=404, detail="Order not found")

    stmt = select(Good).where(Good.order_id == order.id)
    result = await session.execute(stmt)
    goods = result.scalars().all()

    return {
        "order": {
            "id": order.id,
            "user_id": order.user_id,
            "created_at": order.created_at.isoformat(),
        },
        "goods": [
            {
                "id": g.id,
                "name": g.name,
                "count": g.count,
                "price": float(g.price),
            }
            for g in goods
        ],
    }