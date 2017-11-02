/* DATABASE QUERY FOR PROJECT 'Pharmacy Management System using ASP.NET MVC' */


/*Table : User_Access*/

CREATE TABLE dbo.User_Access
(
Username VARCHAR(25) PRIMARY KEY,
Password VARCHAR(32) NOT NULL,
Usertype VARCHAR(10) NOT NULL
);


/*Table : Users Information*/

CREATE TABLE dbo.User_Information
(
ID INT IDENTITY(1,1) PRIMARY KEY,
Name VARCHAR(50) NOT NULL,
Email VARCHAR(50) NOT NULL,
Gender VARCHAR(10) NOT NULL,
Date_of_Birth DATE NOT NULL,
Age INT NOT NULL,
Address VARCHAR(100) NOT NULL,
Contact INT NOT NULL,
Blood_Group VARCHAR(15) NOT NULL,
Marital_Status VARCHAR(10) NOT NULL,
Join_Date DATE NOT NULL,
Salary INT NOT NULL,
Username VARCHAR(25) UNIQUE FOREIGN KEY REFERENCES dbo.User_Access(Username) NOT NULL,
);




/*Table : Manufacturer*/


CREATE TABLE dbo.Manufacturer
(
ID INT IDENTITY(1,1) PRIMARY KEY,
Manufacturer_Name VARCHAR(50) NOT NULL
);

/*Table : Drug Generic Name*/

CREATE TABLE dbo.Drug_Generic_name
(
ID INT IDENTITY(1,1) PRIMARY KEY,
Genric_Name VARCHAR(255) NOT NULL,
Description NVARCHAR(MAX) NOT NULL
);

/*Table : Medicine Information*/

CREATE TABLE dbo.Medicine_Information
(
ID INT IDENTITY(1,1) PRIMARY KEY,
Medicine_Name VARCHAR(50) NOT NULL,
Category VARCHAR(20) NOT NULL,
Generic_ID INT UNIQUE FOREIGN KEY REFERENCES dbo.Drug_Generic_name(ID) NOT NULL,
Manufacturer_ID INT FOREIGN KEY REFERENCES dbo.Manufacturer(ID) NOT NULL
);


/*Table : Supplier*/


CREATE TABLE dbo.Supplier
(
ID INT IDENTITY(1,1) PRIMARY KEY,
Supplier_Name VARCHAR(50) NOT NULL,
Contact VARCHAR(20) NOT NULL,
Email VARCHAR(50) NOT NULL
);

/*Table : Purchase*/

CREATE TABLE dbo.Purchase
(
ID INT IDENTITY(1,1),
Purchase_ID VARCHAR(50) PRIMARY KEY,
Amount INT NOT NULL,
Discount VARCHAR(10) NOT NULL,
Discount_Amount FLOAT NOT NULL,
Grand_Total FLOAT NOT NULL,
IsPaid VARCHAR(20) NOT NULL,
Entry_Date DATE NOT NULL,
Description VARCHAR(255) NOT NULL,
Supplier_ID INT FOREIGN KEY REFERENCES dbo.Supplier(ID) NOT NULL,
);

/*Table : Batch*/

CREATE TABLE dbo.Batch
(
ID INT IDENTITY(1,1),
Batch_ID INT PRIMARY KEY NOT NULL,
Quantity INT NOT NULL,
Cost_Price FLOAT NOT NULL,
Sell_Price FLOAT NOT NULL,
Production_Date DATE NOT NULL ,
Expire_Date DATE NOT NULL,
Purchase_ID VARCHAR(50) FOREIGN KEY REFERENCES dbo.Purchase(Purchase_ID) NOT NULL,
Medicine_ID INT FOREIGN KEY REFERENCES Medicine_Information(ID) NOT NULL
);


/*Table : Bill Information*/

CREATE TABLE dbo.Bill_Information
(
Invoice_No VARCHAR(50) PRIMARY KEY,
Total_Amount FLOAT NOT NULL,
Discount VARCHAR(10) NOT NULL,
Discount_Amount FLOAT NOT NULL,
Total_Payable FLOAT NOT NULL,
Paid FLOAT NOT NULL,
Returned FLOAT NOT NULL,
Date DATE NOT NULL,
);

/*Table : Sales*/

CREATE TABLE dbo.Sales
(
ID INT IDENTITY(1,1) PRIMARY KEY,
Quantity VARCHAR(50) NOT NULL,
Cost INT NOT NULL,
Amount VARCHAR(50) NOT NULL,
Medicine_ID INT UNIQUE FOREIGN KEY REFERENCES Medicine_Information(ID) NOT NULL,
Bill_Invoice INT FOREIGN KEY REFERENCES Bill_Information(Invoice_No) NOT NULL
);
