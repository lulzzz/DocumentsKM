CREATE TABLE general_data_sections (
	id smallserial PRIMARY KEY,
	title varchar(255) NOT NULL UNIQUE,
	short_title varchar(50) NOT NULL UNIQUE,
	print boolean NOT NULL,
	order_num smallint NOT NULL,
	multiple_points boolean NOT NULL
);