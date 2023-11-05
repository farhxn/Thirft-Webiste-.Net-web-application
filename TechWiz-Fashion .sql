create database fashion
use fashion

create table products(
P_ID int primary key identity(1,1),
P_Name nVarchar(max),
P_Discription nvarchar(max),
P_Discount int,
P_Price int,
P_Stock int,
P_Pic varchar(max),
P_Type varchar(max)
)

create table users(
U_id int primary key identity(1,1),
U_Name nvarchar(max),
U_Email nvarchar(max),
U_Contact nvarchar(max),
U_Password varchar(200),
U_Address nvarchar(max),
U_Pic varchar(max)
)

create table admins(
A_id int primary key identity(1,1),
A_Name nvarchar(max),
A_Email nvarchar(max),
A_Password varchar(100),
A_Pic nvarchar(max)
)


create table Billing(
 B_id int primary key identity(1,1),
 B_Name varchar(max),
 B_Address varchar(max),
 B_Total int,
 B_email varchar(max),
 B_city varchar(max),
 B_Postal int,
B_User_ID int
 )

 drop table Billing
create table Collections(
C_ID int primary key identity(1,1),
C_Name varchar(max),
C_Pic varchar(max)
)

create table blogs(
B_ID int primary key identity(1,1),
B_User varchar(max),
B_Title varchar(Max),
B_Blog varchar(max),

B_Img varchar(max)) 

create table cart(
C_ID int primary key identity(1,1),
Pro_ID varchar(max),
Pro_Name varchar(max),
Pro_qty varchar(max)
)

CREATE TABLE Review(

R_id int primary key identity(1,1),
R_Name Varchar(max),
R_Title varchar (max),
R_discriptiopn varchar(max),
R_ProID int 
) 



insert into blogs values ('M.Farhan','Its all about how you wear','On sait depuis longtemps que travailler avec du texte lisible et contenant du sens est source de distractions, et empêche de se concentrer sur la mise en page elle-même. Lavantage du Lorem Ipsum sur un texte générique comme Du texte. Du texte. Du texte. est quil possède une distribution de lettres plus ou moins normale, et en tout cas comparable avec celle du français standard. De nombreuses suites logicielles de......','post-img1.jpg')
drop table 
update  blogs set B_Img = 'blog-post-1.jpg' where B_ID = 2
delete from products where P_ID = 1
insert into Collections values ('Jewellery','collection-page8.jpg')
insert into admins(A_Name, A_Email, A_Password, A_Pic) values('lynx', 'admin@lynx.com', 'admin', 'admin.jpg');
insert into products values('Hiking Brown Shoes', 'Strong & Durable', 1500, 7500, 750,  'home7-product2.jpeg','Shoes');
insert into users(U_Contact, U_Name, U_Email, U_Password, U_Address, U_Pic) values('+9212312312', 'user', 'user@lynx.com', 'user' ,'Fake Street USA, Kansas', 'user.jpg')
update products  set P_Pic = 'home7-product2.jpeg' where P_ID = 12



select * from products
select * from users
select * from products
select * from admins
select * from Collections
select * from Review
select * from billing