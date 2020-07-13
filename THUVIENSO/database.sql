﻿create database THUVIENSO1

create table account(
accountname varchar(50),
passwords varchar(max),
id int primary key,
levels int
)

create table customer(
id int primary key,
username nvarchar(200),
addres nvarchar (250),
phonenumber char (10),
sex varchar (3),
CONSTRAINT FK_ID  FOREIGN KEY(id) REFERENCES account (id) 
)

create table Monney(
id int primary key,
monney int,
CONSTRAINT FK_ID1  FOREIGN KEY(id) REFERENCES account (id)
)

create table booktopic(
id int primary key,
nametopic nvarchar(200)
)

create table book(
id int ,
booktitle nvarchar (200),
authorname nvarchar(100),
content nvarchar(max),
price int,
CONSTRAINT FK_ID2  FOREIGN KEY(id) REFERENCES booktopic (id)
)



