import hashlib
import time
from validation import validate_password
from user import User, UserStorage
from argon2 import PasswordHasher
from argon2.exceptions import VerifyMismatchError

ph = PasswordHasher()


def register_user(storage: UserStorage, username: str, email: str, password: str) -> User:
    if User.exists(storage, username):
        raise ValueError("Пользователь с таким username уже существует")

    hash = ph.hash(password)
    user = User(username=username, email=email, password_hash=hash)
    user.save(storage)
    return user


def verify_credentials(storage: UserStorage, username: str, password: str) -> bool:
    user = User.load(storage, username)
    if user is None:
        return False

    if user.is_locked:
        return False

    is_md5 = len(user.password_hash) == 32 and all(c in '0123456789abcdef' for c in user.password_hash.lower())

    if is_md5:
        md5_hex = hashlib.md5(password.encode("utf-8")).hexdigest()
        matches = md5_hex == user.password_hash
    else:
        try:
            matches = ph.verify(user.password_hash, password)
        except VerifyMismatchError:
            matches = False

    if matches:
        if is_md5:
            user.password_hash = ph.hash(password)
        user.failed_attempts = 0
        user.save(storage)
        return True
    else:
        time.sleep(1.5 ** user.failed_attempts + 1)
        user.failed_attempts += 1
        if user.failed_attempts >= 5:
            user.is_locked = True
        user.save(storage)
        return False


def is_account_locked(storage: UserStorage, username: str) -> bool:
    user = User.load(storage, username)
    if user is None:
        return False
    return user.is_locked