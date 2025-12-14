SELECT 
    b.title AS название_книги,
    a.name AS автор,
    b.year_published AS год_публикации
FROM book b
INNER JOIN author a ON b.author_id = a.id;

SELECT 
    l.name AS название_библиотеки,
    b.title AS название_книги,
    lb.quantity AS количество
FROM library_book lb
INNER JOIN library l ON lb.library_id = l.id
INNER JOIN book b ON lb.book_id = b.id
WHERE lb.quantity > 0
ORDER BY l.name, b.title;

SELECT 
    b.title AS название_книги,
    l.name AS библиотека,
    lb.quantity AS количество
FROM book b
LEFT JOIN library_book lb ON b.id = lb.book_id
LEFT JOIN library l ON lb.library_id = l.id
ORDER BY b.title;

SELECT 
    a.name AS автор,
    COALESCE(SUM(lb.quantity), 0) AS общее_количество_экземпляров
FROM author a
LEFT JOIN book b ON a.id = b.author_id
LEFT JOIN library_book lb ON b.id = lb.book_id
GROUP BY a.id, a.name
ORDER BY общее_количество_экземпляров DESC;

SELECT 
    l.name AS название_библиотеки,
    b.title AS название_книги,
    lb.quantity AS количество
FROM library_book lb
RIGHT JOIN library l ON lb.library_id = l.id
LEFT JOIN book b ON lb.book_id = b.id
ORDER BY l.name, b.title;

SELECT 
    l.name AS название_библиотеки,
    COUNT(DISTINCT lb.book_id) AS количество_уникальных_книг
FROM library l
LEFT JOIN library_book lb ON l.id = lb.library_id
GROUP BY l.id, l.name
ORDER BY количество_уникальных_книг DESC;

SELECT 
    b.title AS название_книги,
    l.name AS название_библиотеки,
    COALESCE(lb.quantity, 0) AS количество
FROM book b
FULL JOIN library_book lb ON b.id = lb.book_id
FULL JOIN library l ON lb.library_id = l.id
WHERE b.title IS NOT NULL OR l.name IS NOT NULL
ORDER BY l.name, b.title;

SELECT 
    a.name AS автор,
    b.title AS название_книги,
    b.year_published AS год_публикации
FROM author a
FULL JOIN book b ON a.id = b.author_id
ORDER BY a.name, b.title;

SELECT 
    l.name AS библиотека,
    b.title AS книга,
    0 AS planned_copies
FROM library l
CROSS JOIN book b
LIMIT 50;

SELECT 
    l.name AS библиотека,
    b.year_published AS год_публикации
FROM library l
CROSS JOIN (
    SELECT DISTINCT year_published 
    FROM book 
    WHERE year_published IS NOT NULL
) b
ORDER BY l.name, b.year_published;

SELECT 
    l.name AS библиотека,
    book_info.название_книги,
    book_info.максимальное_количество
FROM library l
LEFT JOIN LATERAL (
    SELECT 
        b.title AS название_книги,
        lb.quantity AS максимальное_количество
    FROM library_book lb
    JOIN book b ON lb.book_id = b.id
    WHERE lb.library_id = l.id
    ORDER BY lb.quantity DESC
    LIMIT 1
) book_info ON TRUE
ORDER BY l.name;

SELECT 
    a.name AS автор,
    top_libs.библиотека,
    top_libs.общее_количество
FROM author a
LEFT JOIN LATERAL (
    SELECT 
        l.name AS библиотека,
        SUM(lb.quantity) AS общее_количество
    FROM library_book lb
    JOIN library l ON lb.library_id = l.id
    JOIN book b ON lb.book_id = b.id
    WHERE b.author_id = a.id
    GROUP BY l.id, l.name
    ORDER BY общее_количество DESC
    LIMIT 2
) top_libs ON TRUE
WHERE a.name IS NOT NULL
ORDER BY a.name, top_libs.общее_количество DESC;

SELECT 
    a1.name AS автор_1,
    a2.name AS автор_2,
    a1.birth_year AS год_рождения
FROM author a1
JOIN author a2 ON a1.birth_year = a2.birth_year 
    AND a1.id < a2.id
    AND a1.birth_year IS NOT NULL
ORDER BY a1.birth_year, a1.name;

SELECT 
    b1.title AS книга_1,
    a1.name AS автор_1,
    b2.title AS книга_2,
    a2.name AS автор_2,
    b1.year_published AS год_публикации
FROM book b1
JOIN book b2 ON b1.year_published = b2.year_published 
    AND b1.id < b2.id
    AND b1.year_published IS NOT NULL
LEFT JOIN author a1 ON b1.author_id = a1.id
LEFT JOIN author a2 ON b2.author_id = a2.id
WHERE (b1.author_id IS NULL OR b2.author_id IS NULL OR b1.author_id != b2.author_id)
ORDER BY b1.year_published, b1.title;