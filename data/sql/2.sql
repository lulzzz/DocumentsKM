CREATE TABLE fragment_selection (
	id smallserial PRIMARY KEY,
	condition_num smallint NOT NULL,
	selection smallint NOT NULL,
	note varchar(50),
	exclude_point smallint NOT NULL,
	UNIQUE(condition_num, selection, exclude_point)
);