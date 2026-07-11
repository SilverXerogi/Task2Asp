ALTER TABLE student_groups DROP COLUMN startyear;
ALTER TABLE student_groups DROP COLUMN endyear;

ALTER TABLE student_groups ADD COLUMN startdatetime DATE;
ALTER TABLE student_groups ADD COLUMN enddatetime DATE;