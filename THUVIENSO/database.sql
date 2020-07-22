create database THUVIENSO
go
use THUVIENSO
go

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
id int IDENTITY (1,1)  primary key,
nametopic nvarchar(200)
)

create table book(
id int not null ,
idbook int IDENTITY (1,1) primary key,
booktitle nvarchar (200),
authorname nvarchar(100),
img varchar(max),
DataContent varchar(max),
price int,
CONSTRAINT FK_ID2  FOREIGN KEY(id) REFERENCES booktopic (id)
)




insert into account values ('admin','E10ADC3949BA59ABBE56E057F20F883E',1,'1')
insert into account values ('khachhang','E10ADC3949BA59ABBE56E057F20F883E',123456,'2')

insert into customer values (123456,N'Bùi Ngọc Khánh',N'Vân Hội - Trấn Yên - Yên Bái','0368699895',N'Nam')

insert into Monney values (123456,0)

insert into booktopic values (N'Khoa Học')
insert into booktopic values (N'Văn Học')
insert into booktopic values (N'Lịch Sử')
insert into booktopic values (N'Doanh Nhân')
insert into booktopic values (N'Trinh Thám')
insert into booktopic values (N'Truyện Ngắn')
insert into booktopic values (N'Tiểu Thuyết')
insert into booktopic values (N'Được Yêu Thích')

insert into book(id,booktitle,authorname,img,DataContent,price) values (1,N'LIFE 3.0',N'Bùi Ngọc Khánh','~/Image/820323613.jpg','~/PDF/book20323615.pdf',100000)
insert into book(id,booktitle,authorname,img,DataContent,price) values (1,N'Chạy Đua Với Robot',N'Bùi Ngọc Khánh','~/Image/920331775.jpg','~/PDF/book20331776.pdf',100000)
insert into book(id,booktitle,authorname,img,DataContent,price) values (1,N'Cuộc Sống Cách Mạng',N'Bùi Ngọc Khánh','~/Image/1020335498.jpg','~/PDF/book20335499.pdf',100000)

insert into book(id,booktitle,authorname,img,DataContent,price) values (2,N'Bích luyện thành thần',N'Lê Hoàng','~/Image/120421328.jpg','~/PDF/book20421329.pdf',100000)
insert into book(id,booktitle,authorname,img,DataContent,price) values (2,N'Hồ Sơ Bí Ẩn',N'Ma Mị','~/Image/220495969.jpg','~/PDF/book20495970.pdf',100000)
insert into book(id,booktitle,authorname,img,DataContent,price) values (2,N'Thư Viện Thiên Đạo',N'Hoành Tảo','~/Image/720522108.jpg','~/PDF/book20522109.pdf',100000)

insert into book(id,booktitle,authorname,img,DataContent,price) values (6,N'Việt Ma Tân Lục',N'A Tũn','~/Image/320544833.jpg','~/PDF/book20544835.pdf',100000)
insert into book(id,booktitle,authorname,img,DataContent,price) values (6,N'Quái Vật Xứ Khác',N'A Tũn','~/Image/420564320.jpg','~/PDF/book20564321.pdf',100000)
insert into book(id,booktitle,authorname,img,DataContent,price) values (6,N'Hồ Sơ Bí Ẩn 2',N'Ma Mị','~/Image/520320306.jpg','~/PDF/book20320336.pdf',100000)







