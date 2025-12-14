SELECT id, source_file, line_no, raw_line
FROM hr_import_lines
WHERE raw_line ~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

SELECT id, source_file, line_no, raw_line
FROM hr_import_lines
WHERE raw_line !~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

SELECT id, source_file, line_no, raw_line
FROM hr_import_lines
WHERE source_file = 'import_log.txt' AND raw_line ~* 'error';

SELECT id, source_file, line_no, raw_line
FROM hr_import_lines
WHERE source_file = 'import_log.txt' AND raw_line !~* 'error';

SELECT 
    id,
    source_file,
    line_no,
    (regexp_match(raw_line, '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}'))[1] AS email
FROM hr_import_lines
WHERE raw_line ~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

SELECT 
    id,
    source_file,
    line_no,
    unnest(regexp_matches(raw_line, '[A-Za-z0-9]{2,}-[A-Za-z0-9]+(-[A-Za-z0-9]+)?', 'g')) AS asset_code
FROM hr_import_lines
WHERE raw_line ~ '[A-Za-z0-9]{2,}-[A-Za-z0-9]+(-[A-Za-z0-9]+)?';

SELECT 
    id,
    source_file,
    line_no,
    raw_line,
    regexp_replace(raw_line, '[^\d]', '', 'g') AS normalized_phone
FROM hr_import_lines
WHERE raw_line ~ '[+\d\s()-]{7,}';

SELECT 
    id,
    source_file,
    line_no,
    raw_line,
    regexp_split_to_array(
        regexp_replace(
            regexp_replace(raw_line, 'tags:\s*', '', 'i'),
            '\s*,\s*',
            ','
        ),
        ','
    ) AS tags_array
FROM hr_import_lines
WHERE raw_line ~* 'tags:';

SELECT 
    id,
    source_file,
    line_no,
    unnest(regexp_split_to_array(
        regexp_replace(raw_line, '"[^"]+"', 
            regexp_replace(regexp_replace(matched[1], ',', '||COMMA||', 'g'), '"', '', 'g'), 
            'g'
        ),
        ','
    )) AS csv_field
FROM (
    SELECT 
        id,
        source_file,
        line_no,
        raw_line,
        regexp_matches(raw_line, '"([^"]+)"', 'g') AS matched
    FROM hr_import_lines
    WHERE source_file = 'payroll_dirty.csv'
) sub;

SELECT 
    id,
    source_file,
    line_no,
    regexp_replace(raw_line, 'error', 'ERROR', 'gi') AS replaced_log
FROM hr_import_lines
WHERE source_file = 'import_log.txt';