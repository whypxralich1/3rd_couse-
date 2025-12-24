import asyncio
import os
from sqlalchemy.ext.asyncio import AsyncSession
from app.db import AsyncSessionLocal, engine
from app.models import Base, User, Order, Good
from passlib.context import CryptContext

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")


async def init_db():
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)

    async with AsyncSessionLocal() as session:
        users_data = [
            ("user", "user"),
            ("admin", "admin"),
        ]

        for username, password in users_data:
            result = await session.execute(Base.metadata.tables['users'].select().where(Base.metadata.tables['users'].c.name == username))
            if result.fetchone():
                continue

            hashed = pwd_context.hash(password)
            user = User(name=username, password_hash=hashed)
            session.add(user)

        await session.commit()


if __name__ == "__main__":
    asyncio.run(init_db())