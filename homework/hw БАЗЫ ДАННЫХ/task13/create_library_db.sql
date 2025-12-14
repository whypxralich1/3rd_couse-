DROP TABLE IF EXISTS issuance CASCADE;
DROP TABLE IF EXISTS library_book CASCADE;
DROP TABLE IF EXISTS reader_library CASCADE;
DROP TABLE IF EXISTS book CASCADE;
DROP TABLE IF EXISTS author CASCADE;
DROP TABLE IF EXISTS library CASCADE;
DROP TABLE IF EXISTS reader CASCADE;

CREATE TABLE author (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    birth_year INT
);

CREATE TABLE book (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    author_id INT REFERENCES author(id) ON DELETE SET NULL,
    year_published INT,
    isbn VARCHAR(40) UNIQUE,
    annotation TEXT
);

CREATE TABLE library (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    year_founded INT
);

CREATE TABLE reader (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE reader_library (
    library_id INT NOT NULL REFERENCES library(id) ON DELETE CASCADE,
    reader_id INT NOT NULL REFERENCES reader(id) ON DELETE CASCADE,
    card_number TEXT NOT NULL,
    membership_started DATE DEFAULT CURRENT_DATE,
    membership_ends DATE,
    PRIMARY KEY (library_id, reader_id),
    CONSTRAINT uniq_card_per_library UNIQUE (library_id, card_number)
);

CREATE TABLE library_book (
    library_id INT NOT NULL REFERENCES library(id) ON DELETE CASCADE,
    book_id INT NOT NULL REFERENCES book(id) ON DELETE CASCADE,
    quantity INT NOT NULL CHECK (quantity >= 0),
    shelf TEXT,
    PRIMARY KEY (library_id, book_id)
);

CREATE TABLE issuance (
    id SERIAL PRIMARY KEY,
    library_id INT NOT NULL REFERENCES library(id) ON DELETE CASCADE,
    card_number TEXT NOT NULL,
    book_id INT NOT NULL REFERENCES book(id) ON DELETE RESTRICT,
    term_days INT NOT NULL CHECK (term_days > 0),
    issued_at DATE NOT NULL DEFAULT CURRENT_DATE,
    returned_at DATE,
    CONSTRAINT fk_reader_card FOREIGN KEY (library_id, card_number)
        REFERENCES reader_library (library_id, card_number)
        ON DELETE RESTRICT
);

CREATE INDEX idx_issuance_card ON issuance(library_id, card_number);
CREATE INDEX idx_issuance_book ON issuance(book_id);

INSERT INTO author (id, name, birth_year) VALUES
(1, 'Фёдор Достоевский', 1821),
(2, 'Лев Толстой', 1828),
(3, 'Маргарет Этвуд', 1939),
(4, 'Джордж Оруэлл', 1903),
(5, 'Харуки Мураками', 1949),
(6, 'Неизвестный автор', NULL),
(7, 'Джон Смит', 1975),
(8, 'Эмили Браун', 1982),
(9, 'Александр Куприн', 1870),
(10, 'Анна Каренина-автор (демо)', 1877),
(11, 'Доп. автор 1', 1960),
(12, 'Доп. автор 2', 1970),
(13, 'Доп. автор 3', 1985);

SELECT setval(pg_get_serial_sequence('author','id'), (SELECT COALESCE(MAX(id),0) FROM author));

INSERT INTO book (id, title, author_id, year_published, isbn, annotation) VALUES
(1, 'Преступление и наказание', 1, 1866, '978-5-17-118366-1', 'Роман о моральных дилеммах.'),
(2, 'Война и мир', 2, 1869, '978-5-17-084875-6', 'Эпопея о России эпохи Наполеоновских войн.'),
(3, 'Рассказ служанки', 3, 1985, '978-0-241-12239-1', 'Антиутопия.'),
(4, '1984', 4, 1949, '978-0-452-28423-4', 'Антиутопия про тоталитаризм.'),
(5, 'Норвежский лес', 5, 1987, '978-0-670-80752-0', 'Роман о взрослении.'),
(6, 'Идиот', 1, 1869, '978-5-17-089565-5', 'О человеке с необычной добротой.'),
(7, 'Анна Каренина', 2, 1877, '978-5-699-98973-4', 'Трагедия любви и общества.'),
(8, 'Хор любимых женщин', 5, 2013, '978-0-316-25280-9', 'Сборник рассказов.'),
(9, 'Загадочная книга', 6, 2000, NULL, 'Книга с неясной авторской информацией.'),
(10, 'Путешествие в горы', 11, 2010, '111-1111111111', 'Приключенческая книга.'),
(11, 'Тайные сны', 12, 2018, '222-2222222222', 'Романтическая проза.'),
(12, 'Книга без автора', NULL, 2020, '333-3333333333', 'Преднамеренно без автора.'),
(13, 'Редкая рукопись', NULL, NULL, NULL, 'Редкая рукопись.'),
(14, 'Дополнительная книга A', 7, 2005, '444-4444444444', 'Доп. для демо.'),
(15, 'Дополнительная книга B', 8, 2012, '555-5555555555', 'Доп. для демо.');

SELECT setval(pg_get_serial_sequence('book','id'), (SELECT COALESCE(MAX(id),0) FROM book));

INSERT INTO library (id, name, year_founded) VALUES
(1, 'Центральная городская библиотека', 1952),
(2, 'Библиотека на Тихой улице', 1998),
(3, 'Библиотека на холме', 2005),
(4, 'Старая районная библиотека', NULL),
(5, 'Маленький читалый клуб', 2023);

SELECT setval(pg_get_serial_sequence('library','id'), (SELECT COALESCE(MAX(id),0) FROM library));

INSERT INTO reader (id, name) VALUES
(1, 'Алексей Иванов'),
(2, 'Мария Смирнова'),
(3, 'Ольга Петрова'),
(4, 'Игорь Кузнецов'),
(5, 'Читатель без библиотеки'),
(6, 'Ветеран чтения'),
(7, 'Постоянный посетитель'),
(8, 'Гость');

SELECT setval(pg_get_serial_sequence('reader','id'), (SELECT COALESCE(MAX(id),0) FROM reader));

INSERT INTO reader_library (library_id, reader_id, card_number, membership_started) VALUES
(1, 1, 'C-0001', '2020-02-10'),
(1, 2, 'C-0002', '2021-06-15'),
(2, 3, 'T-1001', '2022-01-05'),
(2, 4, 'T-1002', '2023-09-20'),
(2, 1, 'T-2001', '2024-03-01'),
(1, 5, 'C-0100', '2024-05-10'),
(3, 6, 'H-2001', '2023-01-01'),
(4, 7, 'O-3001', '2024-12-01'),
(5, 4, 'M-5001', '2025-02-20');

INSERT INTO library_book (library_id, book_id, quantity, shelf) VALUES
(1, 1, 3, 'A-1'),
(1, 2, 1, 'A-2'),
(1, 4, 2, 'B-1'),
(1, 6, 1, 'A-3'),
(1, 5, 1, 'B-2'),
(2, 3, 4, 'C-1'),
(2, 5, 2, 'C-2'),
(2, 7, 1, 'C-3'),
(2, 8, 2, 'C-4'),
(3, 9, 2, 'D-1'),
(3, 10, 1, 'D-2'),
(3, 15, 1, 'D-3'),
(4, 11, 3, 'E-1'),
(5, 12, 1, 'F-1'),
(5, 13, 2, 'F-2'),
(5, 14, 1, 'F-3');

INSERT INTO issuance (library_id, card_number, book_id, term_days, issued_at) VALUES

(1, 'C-0001', 1, 21, '2025-11-01'),
(1, 'C-0002', 4, 14, '2025-10-28'),
(1, 'C-0001', 6, 30, '2025-09-15'),
(2, 'T-1001', 3, 14, '2025-11-10'),
(2, 'T-1002', 5, 21, '2025-11-05'),
(2, 'T-2001', 8, 14, '2025-10-20'),
(3, 'H-2001', 9, 14, '2025-11-01'),
(3, 'H-2001', 10, 30, '2025-11-05'),
(4, 'O-3001', 11, 21, '2025-11-10'),
(5, 'M-5001', 12, 7, '2025-11-05'),
(5, 'M-5001', 13, 14, '2025-11-12');