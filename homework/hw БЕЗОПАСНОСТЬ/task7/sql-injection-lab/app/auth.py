from fastapi import HTTPException, Header, Depends
from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.db import get_session
from app.models import Token, User


async def get_user_by_token(
    authorization: str | None = Header(None),
    session: AsyncSession = Depends(get_session),
):
    if not authorization or not authorization.lower().startswith("bearer "):
        raise HTTPException(status_code=401, detail="Invalid Authorization header")

    token_value = authorization[7:].strip()

    stmt = (
        select(User.id, User.name)
        .join(Token)
        .where(Token.value == token_value, Token.is_valid == True)
    )
    result = await session.execute(stmt)
    row = result.first()

    if not row:
        raise HTTPException(status_code=401, detail="Invalid token")

    return {"id": row.id, "name": row.name}