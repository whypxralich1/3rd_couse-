from functools import wraps

CACHE = {}

def cache_int_arg(func):
    @wraps(func)
    def wrapper(n):
        if n in CACHE:
            print(f'возвращаем результат из кеша для {n}')
            return CACHE[n]
        
        result = func(n)
        CACHE[n] = result
        return result
    
    return wrapper

@cache_int_arg
def fib(n):
    if n < 2:
        return n
    return fib(n-1) + fib(n-2)
print(fib(10))
print(fib(10))