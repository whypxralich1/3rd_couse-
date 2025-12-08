DROP SCHEMA IF EXISTS fitness_center CASCADE;

CREATE SCHEMA IF NOT EXISTS fitness_center;

CREATE TABLE IF NOT EXISTS fitness_center.members (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    registration_date DATE DEFAULT CURRENT_DATE
);

CREATE TABLE IF NOT EXISTS fitness_center.trainers (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    specialization TEXT NOT NULL,
    experience INTEGER NOT NULL
);