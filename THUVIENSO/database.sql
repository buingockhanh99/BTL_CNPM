create database THUVIENSO

create table Account(
accountname varchar(max),
passwords varchar(max),
id int primary key,
levels int
)

create table Information(
id int primary key,
username nvarchar(200),
CONSTRAINT FK_ID  FOREIGN KEY(id) REFERENCES account (id) 
)

create table Monney(
id int primary key,
monney int,
CONSTRAINT FK_ID1  FOREIGN KEY(id) REFERENCES account (id)
)

create table themebook(
id int primary key,
nametheme nvarchar(200)
)

create table book(
id int,
namebook nvarchar (200),
content nvarchar(max),
price int,
CONSTRAINT FK_ID2  FOREIGN KEY(id) REFERENCES themebook (id)
)

