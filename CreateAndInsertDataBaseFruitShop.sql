Create database FruitShop;
use master;
---------------------------------------------------------------------------------
CREATE TABLE Customer(
 CustomerId Int NOT NULL IDENTITY(1,1),
 Dni Varchar(9) NOT NULL,
 Name Varchar(20) NOT NULL,
 LastName Varchar(100)
)

Alter table Customer ADD CONSTRAINT PK_Customer Primary KEY (CustomerId);
Insert into Customer(Dni,Name,LastName)Values('48070622R','Carlos','Jurado')
Insert into Customer(Dni,Name,LastName)Values('48233312S','Andres','Garcia')
Insert into Customer(Dni,Name,LastName)Values('13556633X','Sergio','Santos')
---------------------------------------------------------------------------------

CREATE TABLE ArticleType(
 ArticleTypeId Int NOT NULL Unique,
 Description varchar(20)
)

Alter table ArticleType ADD CONSTRAINT PK_ArticleType Primary KEY (ArticleTypeId);
Insert into ArticleType(ArticleTypeId,Description)Values(1,'Pear')
Insert into ArticleType(ArticleTypeId,Description)Values(2,'Apple')
Insert into ArticleType(ArticleTypeId,Description)Values(3,'Orange')

---------------------------------------------------------------------------------
CREATE TABLE Article(
 ArticleId Int NOT NULL IDENTITY(1,1),
 ArticleTypeId Int NOT NULL,
 UnitPrice decimal (5,2)
)

Alter table Article ADD CONSTRAINT PK_Article Primary KEY (ArticleId);
Insert into Article(ArticleTypeId,UnitPrice)Values(1,3.45)
Insert into Article(ArticleTypeId,UnitPrice)Values(2,1)
Insert into Article(ArticleTypeId,UnitPrice)Values(3,1.32)

Alter table Article ADD CONSTRAINT FK_Article_ArticleType Foreign KEY (ArticleTypeId)References ArticleType(ArticleTypeId);
---------------------------------------------------------------------------------

Create table Purchase(
PurchaseId int not null IDENTITY(1,1),
ArticleId int not null,
CustomerId int not null,
Quantity int,
TotalPrice decimal (7,2)
) 

Alter table Purchase ADD CONSTRAINT PK_Purchase Primary KEY (PurchaseId);
Insert into Purchase(ArticleId,CustomerId,Quantity,TotalPrice)Values(1,1,4,13.8)
Insert into Purchase(ArticleId,CustomerId,Quantity,TotalPrice)Values(2,1,10,10.0)
Insert into Purchase(ArticleId,CustomerId,Quantity,TotalPrice)Values(2,2,5,5.0)
Insert into Purchase(ArticleId,CustomerId,Quantity,TotalPrice)Values(3,3,2,6.9)
Insert into Purchase(ArticleId,CustomerId,Quantity,TotalPrice)Values(2,3,6,6.0)
Insert into Purchase(ArticleId,CustomerId,Quantity,TotalPrice)Values(1,3,6,20.7)

Alter table Purchase ADD CONSTRAINT FK_Purchase_Article Foreign KEY (ArticleId)References Article(ArticleId);
Alter table Purchase ADD CONSTRAINT FK_Purchase_Customer Foreign KEY (CustomerId)References Customer(CustomerId);

---------------------------------------------------------------------------------
select * from Article
select * from ArticleType
select * from Customer
select  * from Purchase
