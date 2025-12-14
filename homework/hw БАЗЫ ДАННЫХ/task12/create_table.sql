DROP TABLE IF EXISTS hr_import_lines;

CREATE TABLE hr_import_lines (
  id serial PRIMARY KEY,
  source_file text NOT NULL,
  line_no int NOT NULL,
  raw_line text NOT NULL,
  imported_at timestamp with time zone DEFAULT now(),
  note text
);

INSERT INTO hr_import_lines (source_file, line_no, raw_line, note) VALUES
('candidates_2025_10.csv', 1, 'Ivan Ivanov ivan.ivanov@example.com, +7 (912) 345-67-89, "python,sql"', 'contact record'),
('candidates_2025_10.csv', 2, 'Мария Смирнова, maria.smirnova@company.co, 8-912-3456789, "python,django"', 'contact record'),
('candidates_2025_10.csv', 3, 'Petrov <not-an-email@@example..com>, 09123456789, "rust, systems"', 'broken email'),

('assets_import.csv', 10, 'ASSET: AB-123-XY; location: HQ; qty: 2', 'asset record'),
('assets_import.csv', 11, 'asset: zx9999; note: legacy-id', 'asset record'),

('skills_teams.csv', 1, 'tags: sql, postgres, regex,  performance', 'tags field'),
('skills_teams.csv', 2, 'tags: fastapi,python', 'tags field'),
('skills_teams.csv', 3, 'tags: sql,, ,postgres', 'possible empty tags'),

('payroll_dirty.csv', 5, '"Ivanov, Ivan", Москва, "30,000"', 'csv row with commas'),
('payroll_dirty.csv', 6, '"Sidorova, Anna", "St.Petersburg, Nevsky", "1,200,000"', 'csv row with many commas'),

('import_log.txt', 201, 'INFO: Started import of candidates_2025_10.csv', 'log'),
('import_log.txt', 202, 'Warning: missing email for line 3', 'log'),
('import_log.txt', 203, 'error: failed to parse csv_row at line 6', 'log'),
('import_log.txt', 204, 'Error: phone number invalid', 'log'),

('candidates_2025_10.csv', 20, 'Contact: bad@-domain.com, +7 912 ABC-67-89, "node, js"', 'trap-invalid-email-and-phone'),
('assets_import.csv', 12, 'SKU: 12-AB-!!; qty: one', 'trap-bad-sku');