CREATE TABLE student_groups (
	id uuid NOT NULL,
	name varchar NOT NULL,
	abbreviation varchar NOT NULL,
	startyear int4 NOT NULL,
	endyear int4 NOT NULL,
	studyformat int4 NOT NULL,
	createddatetimeutc timestamp NOT NULL,
	modifieddatetimeutc timestamp NULL,
	isremoved bool NOT NULL,
	CONSTRAINT student_groups_pk PRIMARY KEY (id)
);