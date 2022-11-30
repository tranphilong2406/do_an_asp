create database db_bigexam

use db_bigexam

create table category(
	id int IDENTITY(1,1) primary key,
	name nvarchar(100) not null,
	slug nvarchar(100) not null,
	status int not null,
)

create table member(
	username varchar(100) primary key,
	password varchar(max) not null,
	role int not null,
	status int not null,
)

create table post(
	id int Identity(1,1) primary key,
	title nvarchar(300) not null,
	short_decription nvarchar(max) not null,
	full_content nvarchar(max) not null,
	img nvarchar(max),
	status int not null,
	cat_id int foreign key references category(id) not null,
	author varchar(100) foreign key references member(username) not null,
)
