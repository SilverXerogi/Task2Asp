CREATE TABLE products (
	id uuid NOT NULL,
	category int4 NOT NULL,
	name varchar NOT NULL,
	description varchar NULL,
	price float8 NOT NULL,
	createddatetimeutc timestamp NOT NULL,
	modifieddatetimeutc timestamp NULL,
	isremoved bool NOT NULL,
	CONSTRAINT products_pk PRIMARY KEY (id)
);