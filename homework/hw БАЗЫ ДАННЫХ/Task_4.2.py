import time
from functools import wraps

def retry(times=3, exceptions=(Exception,), delay=0.0):
    def decorator(func):
        @wraps(func)
        def wrapper(*args, **kwargs):
            for attempt in range(times):
                try:
                    return func(*args, **kwargs)
                except exceptions as e:
                    if attempt < times - 1:
                        time.sleep(delay)
                    else:
                        raise e
        return wrapper
    return decorator

i = 0
@retry(times=4, exceptions=(ValueError,), delay=0.05)
def flaky():
    global i
    i += 1
    if i < 3:
        raise ValueError("not yet")
    return "ok"

print(flaky())