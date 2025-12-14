DROP TABLE IF EXISTS users;

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    full_name TEXT NOT NULL,
    email VARCHAR(255) NOT NULL,
    bio TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION generate_random_string(length INTEGER) 
RETURNS TEXT AS $$
DECLARE
    chars TEXT := 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    result TEXT := '';
    i INTEGER;
BEGIN
    FOR i IN 1..length LOOP
        result := result || substr(chars, floor(random() * length(chars) + 1)::INTEGER, 1);
    END LOOP;
    RETURN result;
END;
$$ LANGUAGE plpgsql;

INSERT INTO users (full_name, email, bio, created_at)
SELECT 
    generate_random_string(8) || ' ' || generate_random_string(10),

    generate_random_string(10) || '@' || 
    CASE (random() * 5)::INT
        WHEN 0 THEN 'gmail.com'
        WHEN 1 THEN 'yahoo.com'
        WHEN 2 THEN 'mail.ru'
        WHEN 3 THEN 'yandex.ru'
        WHEN 4 THEN 'example.com'
        ELSE 'test.com'
    END,

    CASE 
        WHEN random() < 0.3 THEN 'Разработчик ' || generate_random_string(15)
        WHEN random() < 0.6 THEN 'Аналитик данных с опытом в ' || generate_random_string(12)
        ELSE 'Специалист по ' || generate_random_string(20) || ' и ' || generate_random_string(15)
    END,
    
    CURRENT_TIMESTAMP - (random() * INTERVAL '1825 days')
FROM generate_series(1, 100000);

INSERT INTO users (full_name, email, bio, created_at) VALUES
('Иван Иванов', 'ivanov@example.com', 'Senior разработчик PostgreSQL и Python', '2024-01-15 10:30:00'),
('Мария Петрова', 'petrova@test.com', 'Data scientist с опытом машинного обучения', '2024-02-20 14:45:00'),
('Алексей Сидоров', 'sidorov@mail.ru', 'Full-stack разработчик JavaScript и React', '2024-03-10 09:15:00');

SELECT COUNT(*) as total_users FROM users;

SELECT * FROM users LIMIT 5;