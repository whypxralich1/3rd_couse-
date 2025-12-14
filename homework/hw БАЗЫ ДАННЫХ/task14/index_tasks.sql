\timing on

SELECT * FROM users WHERE email = 'ivanov@example.com';

SELECT * FROM users WHERE email = 'petrova@test.com';

\timing off

CREATE INDEX idx_users_email ON users(email);

\timing on

SELECT * FROM users WHERE email = 'ivanov@example.com';

SELECT * FROM users WHERE email = 'petrova@test.com';

\timing off

EXPLAIN ANALYZE SELECT * FROM users WHERE email = 'sidorov@mail.ru';

\timing on

SELECT * FROM users WHERE bio ILIKE '%разработчик%' LIMIT 10;

SELECT COUNT(*) FROM users WHERE bio ILIKE '%данных%';

\timing off

CREATE INDEX idx_users_bio_lower ON users(lower(bio));

\timing on

SELECT * FROM users WHERE lower(bio) LIKE '%разработчик%' LIMIT 10;

SELECT COUNT(*) FROM users WHERE lower(bio) LIKE '%данных%';

\timing off

EXPLAIN ANALYZE SELECT * FROM users WHERE lower(bio) LIKE '%разработчик%' LIMIT 10;

\timing on

SELECT COUNT(*) as count_iliKE FROM users WHERE bio ILIKE '%опыт%';

SELECT COUNT(*) as count_lower_like FROM users WHERE lower(bio) LIKE '%опыт%';

\timing off

SELECT 
    indexname, 
    indexdef 
FROM pg_indexes 
WHERE tablename = 'users'
ORDER BY indexname;

SELECT 
    pg_size_pretty(pg_total_relation_size('users')) as total_size,
    pg_size_pretty(pg_relation_size('users')) as table_size,
    pg_size_pretty(pg_indexes_size('users')) as indexes_size;