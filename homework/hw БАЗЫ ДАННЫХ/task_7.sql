CREATE TABLE flights (
    id SERIAL PRIMARY KEY,
    flight_code VARCHAR(10) NOT NULL UNIQUE,
    duration INTEGER NOT NULL CHECK(duration BETWEEN 30 AND 600),
    ticket_price NUMERIC(10, 2) NOT NULL CHECK(ticket_price > 100)
);

ALTER TABLE flights ADD CONSTRAINT check_flight_code_prefix CHECK(flight_code LIKE 'FL_______');

INSERT INTO flights (flight_code, duration, ticket_price) VALUES
('FL123ABC', 120, 250.00),
('FL456DEF', 180, 300.00),
('FL789GHI', 240, 350.00);