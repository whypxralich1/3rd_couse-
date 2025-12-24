from pydantic import BaseModel
from datetime import datetime
from typing import List


class AuthRequest(BaseModel):
    name: str
    password: str


class TokenResponse(BaseModel):
    token: str


class GoodResponse(BaseModel):
    id: int
    name: str
    count: int
    price: float


class OrderResponse(BaseModel):
    id: int
    user_id: int
    created_at: str


class OrderDetailsResponse(BaseModel):
    order: OrderResponse
    goods: List[GoodResponse]