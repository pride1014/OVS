Use Master
Go

If Exists (Select * from sys.databases where name = 'OVS')
ALTER DATABASE OVS
SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE OVS
Go
Create Database OVS
Go


Use OVS 
go

Create table User_Access_Permission /****** Access_Permission******/
(User_Access_Permission_ID int primary key identity(1,1) not null,
User_Role_Name varchar(50) not null,
User_Role_Description varchar(100)not null
)

 INSERT [User_Access_Permission] ( User_Role_Name ,User_Role_Description) VALUES ( 'Customer', 'Allow customers to browse and buy products')
 INSERT [User_Access_Permission] ( User_Role_Name ,User_Role_Description) VALUES ( 'Employee', 'Allow employees to navigate through the system')
 INSERT [User_Access_Permission] ( User_Role_Name ,User_Role_Description) VALUES ( 'Manager', 'Allow managers to navigate through the system')

Create table [User]
([User_ID] int primary key identity(1,1),
[User_Password] varchar(12)not null,
[User_Name] varchar(20) not null,
 User_Access_Permission_ID int foreign key references User_Access_Permission (User_Access_Permission_ID))
 Alter table [User]
alter column  User_Password varchar (256) not null


 INSERT [User] ( [User_Password] ,[User_Name],[User_Access_Permission_ID]) VALUES ( 'ManagerOVS10', 'ManagerOVS',3)


create table Employee_Type(
Employee_Type_ID int identity(1,1) primary key not null,
Employee_Type_Description varchar(100) not null 
)

CREATE TABLE Employee (
Employee_ID int identity(1,1) primary key not null,
Employee_Name varchar(30) not null,
Employee_Surname varchar(30) not null,
Employee_Phone_Number varchar(13) not null,
Employee_Email_Address varchar(50) null,
Employee_Type_ID int foreign key references Employee_Type(Employee_Type_ID),
User_ID int foreign key references [User](User_ID),
)

create table Shift_Type(
Shift_Type_ID int identity(1,1) primary key not null,
Shift_Type_Description varchar(100) not null 
)

CREATE TABLE [Shift] (
Shift_ID int identity(1,1) primary key not null,
Shift_Type_ID int foreign key references Shift_Type(Shift_Type_ID)
)

create table [Date](
Date_ID int primary key not null,
Date_Description date not null 
)

create table Time_Slot(
Time_Slot_ID int identity(1,1) primary key not null,
Starting_time time not null, 
Ending_time time not null, 
)

CREATE TABLE Branch (
Branch_ID int identity(1,1) primary key not null,
Branch_Name varchar(50) not null,
Branch_Location_Storage_Capacity int not null,
Branch_Address varchar(50) not null, 
)

create table Product_Category(
Product_Category_ID int identity(1,1) primary key not null,
Product_Category_Name varchar(1024) not null 
) 

create table Product_Type(
Product_Type_ID int identity(1,1) primary key not null,
Product_Type_Name varchar(100) not null,
Product_Category_ID int foreign key references Product_Category(Product_Category_ID)
)

Create table Product(
Product_ID int identity(1,1) primary key not null,
Product_Name varchar (100) not null, 
Product_Description varchar(100) not null,
Product_Image varbinary(Max) not null,
Quantity_on_hand int null, 
Product_Type_ID int foreign key references Product_Type(Product_Type_ID)
)

create table Size (
Size_ID int identity(1,1) primary key not null,
Size_Description varchar (20) not null
)



create table Price (
Price_ID int identity(1,1) primary key not null,
Price_Amount float (10) not null,
Price_Date date not null,
Product_Size_ID int foreign key references Product_Size(Product_Size_ID)

)

create table Special (
Special_ID int identity(1,1) primary key not null,
[Start_Date] date not null,
[End_Date] date not null,
)

Create table Product_Size (
Product_Size_ID int identity(1,1) primary key not null,
Price_ID int foreign key references Price(Price_ID),
Product_ID int foreign key references Product(Product_ID),

)

Create table Product_Special (
Product_Special_ID int identity(1,1) primary key not null,
Product_Size_ID int foreign key references Product_Size(Product_Size_ID),
Special_ID int foreign key references Special(Special_ID),
Price_Amount float (10) not null
)

create table Colour (
Colour_ID int identity(1,1) primary key not null,
Colour_Description varchar (50) not null,
Product_Size_ID int foreign key references Product_Size(Product_Size_ID),
)


Create table Date_Time_Slot (
Date_Time_Slot_ID int identity(1,1) primary key not null,
Shift_ID int foreign key references Shift(Shift_ID),
Time_Slot_ID int foreign key references Time_Slot(Time_Slot_ID),
Date_ID int foreign key references [Date] (Date_ID)
)

CREATE TABLE Cash_Register  (
Register_ID int identity(1,1) primary key not null,
Cash_Register_Name varchar(250) not null,
Branch_ID int foreign key references Branch(Branch_ID)
)

CREATE TABLE Shift_Branch ( 
Shift_Branch_ID int identity(1,1) primary key not null,
Shift_ID int foreign key references Shift(Shift_ID),
Branch_ID int foreign key references Branch(Branch_ID)
)

CREATE TABLE Shift_Branch_Employee ( 
Shift_Branch_Employee_ID int identity(1,1) primary key not null,
Shift_Branch_ID int foreign key references Shift_Branch(Shift_Branch_ID),

Employee_ID int foreign key references Employee(Employee_ID)
)


Create table Discount 
(Discount_ID int primary key identity(1,1) NOT NULL,
Discount_Description varchar(100)not null,
Discount_Name varchar (100) not null,
Discount_Percentage decimal not null
)

Create table Customer_Type
(
Customer_Type_ID int primary key identity(1,1),
Customer_Type_Description varchar(50),
 Discount_ID int foreign key references Discount(Discount_ID),
)

CREATE TABLE [dbo].[Customer](
	[Customer_ID] int primary key identity(1,1) NOT NULL,
	[Customer_Name] varchar(50) NOT NULL,
	[Customer_Surname] varchar(50) not NULL,
	[Customer_Cellphone_Number] varchar(15) not NULL,
	[Customer_Date_Of_Birth] Date not NULL,
	[Customer_Email_Address] varchar(100) NULL,
	[Customer_Physical_Address] varchar(500) not NULL,
	 Customer_Type_ID int references Customer_Type(Customer_Type_ID),
	 User_ID int references [User](User_ID),
	  )

 Create table Cart 
 (
 Cart_ID int primary key identity(1,1),
 User_ID int foreign key references [User](User_ID),
 Customer_ID int foreign key references Customer(Customer_ID)
 
 )

  Create table Cart_Line 
 (
 Cart_Line_ID int identity(1,1) primary key not null,
 Cart_ID int foreign key references Cart (Cart_ID),
 Product_ID int foreign key references Product (Product_ID),
 Quantity int NULL
 )

 Create table Wishlist (
 Wishlist_ID int identity(1,1) primary key not null,
 Customer_ID int foreign key references Customer(Customer_ID)
 )

 Create table Wishlist_Product (
 Wishlist_Product_ID int identity(1,1) primary key not null,
 Product_ID int foreign key references Product(Product_ID),
 Wishlist_ID int foreign key references Wishlist(Wishlist_ID),
 Quantity int not null
 )

  Create Table Order_Status
 (
 Order_Status_ID int primary key identity(1,1),
Order_Status_Description varchar(50) 
 )


INSERT [Order_Status] ( Order_Status_Description) VALUES ( 'Placed')
INSERT [Order_Status] ( Order_Status_Description) VALUES ( 'Packed')
INSERT [Order_Status] ( Order_Status_Description) VALUES ( 'Dispatched')
INSERT [Order_Status] ( Order_Status_Description) VALUES ( 'Delivered')
INSERT [Order_Status] ( Order_Status_Description) VALUES ( 'Returned')
INSERT [Order_Status] ( Order_Status_Description) VALUES ( 'Cancelled')

 Create table [Order]
 (
 Order_ID int primary key identity(1,1),
 Order_Date Datetime not null,
 Order_Finalizastion_Date Datetime not null,
 Delivery bit not null DEFAULT (0),
 Customer_ID int foreign key references Customer(Customer_ID),
 Order_Status_ID int foreign key references Order_Status(Order_Status_ID),
 Employee_ID int foreign key references Employee(Employee_ID)
 ) 

 Create table Order_Line 
 (
 Order_Line_ID int primary key identity(1,1),
 Order_ID int ,
 Product_ID int ,
  Quantity int null,

  CONSTRAINT FK_Order_ID
	FOREIGN KEY (Order_ID)
	REFERENCES [Order](Order_ID),
	
	CONSTRAINT FK_Product_ID
	FOREIGN KEY (Product_ID)
	REFERENCES Product(Product_ID)
)
 
 Create table Return_Order_Request
 (
 Return_Order_Request_ID int primary key identity(1,1),
 Customer_ID int foreign key references Customer(Customer_ID),
 Order_ID int foreign key references [Order](Order_ID),
 Request_Order_Date Datetime not null
 )

 Create table Return_Order_Request_Order_Line
 (
 Return_Order_Request_Order_Line_ID int primary key identity(1,1),
 
 Return_Order_Request_ID int,
 Order_Line_ID int ,
 Return_Reason varchar(100) not null ,
 Quantity int not null,

	CONSTRAINT FK_Order_Line_ID
	FOREIGN KEY (Order_Line_ID)
	REFERENCES Order_Line(Order_Line_ID),

	CONSTRAINT FK_Return_Order_Request_ID
	FOREIGN KEY (Return_Order_Request_ID)
	REFERENCES Return_Order_Request(Return_Order_Request_ID)

 )

 Create Table Credit_Note
 (
  Credit_Note_ID int primary key identity(1,1) not null,
  Customer_ID int foreign key references Customer(Customer_ID),
  Return_Order_Request_ID int foreign key references Return_Order_Request(Return_Order_Request_ID),

 )

Create table Quote_Status
(
Quote_Status_ID int primary key identity(1,1) not null,
Quote_Status_Description varchar(50)
)

INSERT [Quote_Status] ( Quote_Status_Description) VALUES ( 'Rejected')
INSERT [Quote_Status] ( Quote_Status_Description) VALUES ( 'Accepted')

Create table Request_Quote
(
Request_Quote_ID int primary key identity(1,1) not null,
[Date] Datetime not null,
Quote_Status_ID int foreign key references Quote_Status(Quote_Status_ID)
)

Create table Quote_Line
(
Quote_Line_ID int primary key identity(1,1) not null,
Quantity int not null,
Product_ID int foreign key references Product(Product_ID),
Product_Size_ID int foreign key references Product_Size(Product_Size_ID),
Request_Quote_ID int foreign key references Request_Quote(Request_Quote_ID)
)


Create table Sale
(
Sale_ID int primary key identity(1,1) not null,
Sale_Date Datetime not null,
Request_Quote_ID int foreign key references Request_Quote(Request_Quote_ID)
)

Create table Sale_Line
(
Sale_Line_ID int primary key identity(1,1) not null,
Sale_ID int ,
Product_ID int foreign key references Product(Product_ID),
Quantity int not null,

	CONSTRAINT FK_Sale_ID
	FOREIGN KEY (Sale_ID)
	REFERENCES Sale(Sale_ID),
	
	--CONSTRAINT FK_Product_ID
	--FOREIGN KEY (Product_ID)
	--REFERENCES Product(Product_ID)
)

Create table Return_Sale_Request
(
Return_Sale_Request_ID int primary key identity(1,1) not null,
Return_Request_Date Datetime not null,
Customer_ID int foreign key references Customer(Customer_ID)
)

Create table Return_Sale_Request_Sale_Line
(
Return_Sale_Request_Sale_Line_ID int primary key identity(1,1) not null,
Return_Sale_Reason varchar(100),
Return_Sale_Request_ID int,
Sale_Line_ID int ,

Quantity int null,

	CONSTRAINT FK_Sale_Line_ID
	FOREIGN KEY (Sale_Line_ID)
	REFERENCES Sale_Line(Sale_Line_ID),

	CONSTRAINT FK_Return_Sale_Request_ID
	FOREIGN KEY (Return_Sale_Request_ID)
	REFERENCES Return_Sale_Request(Return_Sale_Request_ID)
)

Create table Sale_Payment_Status
(
Sale_Payment_Status_ID int primary key identity(1,1) not null,
Sale_Payment_Status_Desc varchar(50)
)

INSERT [Sale_Payment_Status] ( Sale_Payment_Status_Desc) VALUES ( 'Processing')
INSERT [Sale_Payment_Status] ( Sale_Payment_Status_Desc) VALUES ( 'Awaiting Payment')
INSERT [Sale_Payment_Status] ( Sale_Payment_Status_Desc) VALUES ( 'Paid')

Create table Payment_Type
(
Payment_Type_ID int primary key identity(1,1) not null,
Payment_Type_Description varchar(50),
Payment_Type_Name varchar(20)
)

Create table Sale_Payment
(
Sale_Payment_ID int primary key identity(1,1) not null,
Sale_Payment_Amount float not null,
Sale_Payment_Date Datetime not null,
Sale_Payment_Status_ID int foreign key references Sale_Payment_Status(Sale_Payment_Status_ID),
Sale_ID int foreign key references Sale(Sale_ID),
Payment_Type_ID int foreign key references Payment_Type(Payment_Type_ID),
Register_ID int foreign key references Cash_Register(Register_ID)
)

Create table Order_Payment_Status
(
Order_Payment_Status_ID int primary key identity(1,1) not null,
Order_Payment_Status_Description varchar(50)
)




Create table Order_Payment
(
Order_Payment_ID int primary key identity(1,1) not null,
Order_Payment_Amount int not null,
Order_Payment_Date Datetime not null,
Payment_Type_ID int foreign key references Payment_Type(Payment_Type_ID),
Order_Payment_Status_ID int foreign key references Order_Payment_Status(Order_Payment_Status_ID),
Order_ID int foreign key references [Order](Order_ID)
)

 create table Supplier (
 Supplier_ID int identity(1,1) primary key not null,
 Supplier_Name varchar (30) not null,
 Supplier_Phone_Number varchar (13) not null,
 Supplier_Address varchar (100) not null
 )

  create table Supplier_Order (
 Supplier_Order_ID int identity(1,1) primary key not null,
 Supplier_Order_Description varchar (100) not null,
 Supplier_ID int foreign key references Supplier(Supplier_ID)
 )

 create table Unit (
 Unit_ID int identity(1,1) primary key not null,
 Unit_Measurement varchar(50) not null,
 )

 Create table Raw_Material (
 Raw_Material_ID int identity(1,1) primary key not null,
 Raw_Material_Name varchar(250) not null,
 Quantity_on_hand int not null,
 Raw_material_description varchar (250) not null,
 Unit_ID int foreign key references Unit(Unit_ID),
 )
 
   create table Supplier_Order_Line (
   Supplier_Order_Line_ID int identity(1,1) primary key not null,
 Supplier_Order_ID int references Supplier_Order(Supplier_Order_ID),
 Quantity int not null,
 Product_ID int  references Product(Product_ID),
 Raw_Material_ID int foreign key references Raw_Material(Raw_Material_ID),
 )
 
 Create table Recipe_Line_Unit(
 Recipe_Line_unit_ID int Identity(1,1) primary key not null,
 Unit int not null, 
 )

 Create table Recipe (
 Recipe_ID int Identity(1,1) primary key not null,
 Recipe_Description varchar(50) not null,
 Quantity_produced int not null,
 Recipe_Name varchar (100) not null,
 )

 create table Reciple_Line(
 Reciple_Line_ID int Identity(1,1) primary key not null,
 Quantity int not null,
 Recipe_ID int references Recipe(Recipe_ID),
 Raw_Material_ID int foreign key references Raw_Material(Raw_Material_ID),
 Recipe_Line_unit_ID int foreign key references Recipe_Line_Unit(Recipe_Line_unit_ID),
 ) 

 Create table Job_Status(
 Job_Status_ID int identity(1,1) primary key not null,
 Job_Status_Description varchar (100) not null
 )
INSERT [Job_Status] ( [Job_Status_Description]) VALUES ( 'Not Initiated')
INSERT [Job_Status] ( [Job_Status_Description]) VALUES ( 'Incomplete')
INSERT [Job_Status] ( [Job_Status_Description]) VALUES ( 'Completed')


  Create table Job_Task_Status(
 Job_Task_Status_ID int identity(1,1) primary key not null,
 Job_Task_Description_Status varchar (100) not null
 )

 INSERT [Job_Task_Status] ( [Job_Task_Description_Status]) VALUES ( 'Not Initiated')
 INSERT [Job_Task_Status] ( [Job_Task_Description_Status]) VALUES ( 'Completed')


  Create table Task(
 Task_ID int identity(1,1) primary key not null,
 Task_Description varchar (100) not null
 )

  Create table Job_Task_Type(
  Job_Task_Type_ID int identity(1,1) primary key not null,
 Job_Task_Type_Description varchar (50) not null
 )

  INSERT Job_Task_Type ( Job_Task_Type_Description) VALUES ( 'Production')


 create Table Job(
  Job_ID int identity(1,1) primary key not null,
 Job_Description varchar (100) not null,
 Job_Status_ID int foreign key references Job_Status(Job_Status_ID),
 Product_ID int foreign key references Product(Product_ID),
 )

 Create table Job_task(
 Job_task_ID int identity(1,1) primary key not null,
 Job_ID int foreign key references Job(Job_ID),
 Task_ID int foreign key references Task(Task_ID),
 Job_Task_Status_ID int foreign key references Job_Task_Status(Job_Task_Status_ID),
 Job_Task_Type_ID int foreign key references Job_Task_Type(Job_Task_Type_ID),
 )

 Create Table Job_Instance(
 Job_Instance_ID int identity(1,1) primary key not null,
Job_task_ID int foreign key references Job_task(Job_task_ID),
 Shift_Branch_Employee_ID int foreign key references Shift_Branch_Employee(Shift_Branch_Employee_ID),

 [Start_Date] date not null,
 [End_Date] date not null,
 )

 Create Table Job_Instance_Task(
 Job_Instance_Task_ID int identity(1,1) primary key not null,
 Job_task_ID int foreign key references Job_task(Job_task_ID),
 Job_Instance_ID int foreign key references Job_Instance(Job_Instance_ID),
 [Start_Date] date not null,
 [End_Date] date not null,
 )

 Create Table VAT (
 VAT_ID int identity(1,1) primary key not null,
 VAT_Percentage int not null,
 VAT_Date date not null
 )