CREATE TABLE students (
	id uuid NOT NULL,
    fullname varchar NOT NULL,
    gender int4 NOT NULL,
    age int4 NOT NULL,
    averagegrade numeric(3,2) NOT NULL,
    specialmarks text NULL,
    studentgroupid uuid NOT NULL,
    createddatetimeutc timestamp NOT NULL,
    modifieddatetimeutc timestamp NULL,
    isremoved bool NOT NULL,
    CONSTRAINT students_pk PRIMARY KEY (id),
    CONSTRAINT students_student_groups_fk FOREIGN KEY (studentgroupid) REFERENCES student_groups(id)
);