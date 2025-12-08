CREATE TABLE employees (
    id SERIAL PRIMARY KEY,
    full_name TEXT NOT NULL,
    department TEXT NOT NULL,
    position TEXT NOT NULL,
    salary NUMERIC(10, 2) NOT NULL CHECK(salary >= 30000 AND salary <= 150000),
    hire_date DATE NOT NULL CHECK(hire_date >= '2000-01-01' AND hire_date <= CURRENT_DATE)
);

-- Наполнение таблицы test data
INSERT INTO employees (full_name, department, position, salary, hire_date) VALUES
('Иван Иванов', 'Sales', 'Senior Sales Manager', 60000, '2020-05-15'),
('Анна Смирнова', 'Marketing', 'Marketing Specialist', 55000, '2019-03-10'),
('Михаил Петров', 'Support', 'Customer Support Trainee', 40000, '2014-06-20'),
('Елена Кузнецова', 'HR', 'Human Resources Assistant', 45000, '2017-08-25'),
('Сергей Васильев', 'Engineering', 'Software Engineer Intern', 35000, '2021-02-01'),
('Ольга Новикова', 'Sales', 'Junior Sales Representative', 40000, '2022-11-30'),
('Дмитрий Федоров', 'Marketing', 'Senior Marketing Analyst', 65000, '2020-07-15'),
('Марина Алексеева', 'Support', 'Technical Support Specialist', 50000, '2016-01-01'),
('Александр Сергеев', 'HR', 'Recruitment Officer', 55000, '2018-09-10'),
('Светлана Андреева', 'Engineering', 'Data Scientist', 70000, '2021-03-15'),
('Алексей Дмитриев', 'Sales', 'Senior Account Executive', 75000, '2022-01-01');

SELECT *
-- SELECT-запрос
FROM employees
WHERE LOWER(department) LIKE '%sales%'
AND POSITION IN ('Senior Sales Manager', 'Senior Account Executive', 'Business Development Manager')
AND hire_date BETWEEN '2018-01-01' AND '2022-12-31'
ORDER BY full_name;

-- UPDATE-запрос
UPDATE employees
SET salary = salary * 1.1
WHERE department = 'Marketing'
AND salary BETWEEN 50000 AND 70000
AND POSITION NOT ILIKE '%intern%';

-- DELETE-запрос
DELETE FROM employees
WHERE department = 'Support'
AND hire_date < '2015-01-01'
AND POSITION ILIKE '%trainee%';