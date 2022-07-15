USE master;

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'KebabDB')
BEGIN
  CREATE DATABASE KebabDB;
END;
GO

USE KebabDB;
GO

CREATE TABLE Categories (
	CategoryId INT NOT NULL IDENTITY(0,1) PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

CREATE TABLE Payments (
	PaymentId INT PRIMARY KEY IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL
);

CREATE TABLE Statuses (
	StatusId INT PRIMARY KEY IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL
);

CREATE TABLE OrderForm  (
	OrderFormId INT PRIMARY KEY IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL
);

CREATE TABLE Sauces  (
	StatusId INT PRIMARY KEY IDENTITY(0,1),
	Name VARCHAR(50) NOT NULL
);
INSERT INTO OrderForm (Name) VALUES ('TakeOut'), ('Local');
INSERT INTO Categories (Name) VALUES ('Kebabs'), ('Sides'), ('Salads'), ('Beverages');
INSERT INTO Statuses (Name) VALUES ('Added'), ('InProgress'), ('Completed');
INSERT INTO Payments (Name) VALUES ('Cash'), ('Card');

CREATE TABLE Menus 
(
	MenuId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	CreationDate DATETIME2 NOT NULL
);

CREATE NONCLUSTERED INDEX IDX_MENUS_CREATIONDATE   
    ON Menus (CreationDate);   

CREATE TABLE Items (
	ItemId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Name VARCHAR(50) NOT NULL,
	CategoryId INT NOT NULL,
	Description NVARCHAR NULL
)

CREATE TABLE MenuItems (
	MenuItemId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	MenuId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Menus(MenuId),
	ItemId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Items(ItemId),
	Price DECIMAL NOT NULL,
)

CREATE SEQUENCE DailyOrderNumber START WITH 1 INCREMENT BY 1 MAXVALUE 200 MINVALUE 1 CYCLE;

CREATE TABLE Orders (
	OrderId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	PaymentMethod INT NOT NULL FOREIGN KEY REFERENCES Payments(PaymentId),
	OrderForm INT NOT NULL FOREIGN KEY REFERENCES OrderForm(OrderFormId),
	StatusId INT NOT NULL FOREIGN KEY REFERENCES Statuses(StatusId),
	CreationDate DATETIME2 NOT NULL,
	Comment VARCHAR(1000) NULL,
	OrderNumber INT NOT NULL DEFAULT NEXT VALUE FOR DailyOrderNumber,

);
CREATE TABLE OrderItem (
	OrderItemId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	OrderId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Orders(OrderId),
	MenuItemId UNIQUEIDENTIFIER NOT NULL,
	Quantity INT NOT NULL
);
 
CREATE TABLE KebabSauceMap 
(
	OrderItemId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES OrderItem(OrderItemId),
	SauceId INT NOT NULL
);

Go
CREATE VIEW MenuView AS (
	SELECT 
	m.MenuId AS MenuId,
	mi.MenuItemId AS MenuItemId,
	i.ItemId AS ItemId,
	i.Name AS ItemName,
	m.CreationDate AS MenuCreationDate,
	i.Description AS ItemDescription,
	c.Name AS ItemCategory,
	mi.Price AS ItemPrice
	FROM Menus m 
	JOIN MenuItems mi ON m.MenuId = mi.MenuId
	JOIN Items i ON i.ItemId = mi.ItemId
	JOIN Categories c ON i.CategoryId = c.CategoryId
	);
GO

CREATE VIEW OrderView AS (
	SELECT 
		o.OrderId AS OrderId,
		o.CreationDate AS OrderCreationDate,
		i.Name AS Name,
		c.CategoryId,
		c.Name AS CategoryName,
		o.OrderNumber AS OrderNumber,
		oi.Quantity AS Quantity,
		s.Name AS StatusName,
		p.Name AS PaymentForm,
		orf.Name AS OrderForm
	FROM Orders o 
	JOIN OrderItem oi ON o.OrderId = oi.OrderId
	JOIN MenuItems mi ON oi.MenuItemId = mi.MenuItemId
	JOIN Items i ON i.ItemId = mi.ItemId
	JOIN Statuses s ON s.StatusId = o.StatusId
	JOIN Payments p ON p.PaymentId = o.PaymentMethod
	JOIN OrderForm orf ON orf.OrderFormId = o.OrderForm
	JOIN Categories c ON i.CategoryId = c.CategoryId
	)
GO

CREATE TABLE OrderArchive (
	OrderId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	PaymentMethod INT NOT NULL ,
	OrderForm INT NOT NULL ,
	StatusId INT NOT NULL ,
	CreationDate DATETIME2 NOT NULL,
	Comment VARCHAR(1000) NULL,
	OrderNumber INT NOT NULL,
	DateDeleted DATETIME2 NOT NULL

);
CREATE TABLE OrderItemArchive (
	OrderItemId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	OrderId UNIQUEIDENTIFIER NOT NULL ,
	MenuItemId UNIQUEIDENTIFIER NOT NULL,
	Quantity INT NOT NULL,
	DateDeleted DATETIME2 NOT NULL
);

GO

CREATE TRIGGER orderDelete
ON Orders
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[OrderArchive]
           ([OrderId]
           ,[PaymentMethod]
           ,[OrderForm]
           ,[StatusId]
           ,[CreationDate]
           ,[Comment]
           ,[OrderNumber]
           ,[DateDeleted])
	SELECT 
	   d.[OrderId]
      ,d.[PaymentMethod]
      ,d.[OrderForm]
      ,d.[StatusId]
      ,d.[CreationDate]
      ,d.[Comment]
      ,d.[OrderNumber],
	  SYSDATETIME()
  
    FROM
        deleted d
END

GO

CREATE TRIGGER orderItemsDelete
ON OrderItem
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO [dbo].[OrderItemArchive]
           ([OrderItemId]
           ,[OrderId]
           ,[MenuItemId]
           ,[Quantity]
           ,[DateDeleted])
SELECT d.[OrderItemId]
      ,d.[OrderId]
      ,d.[MenuItemId]
      ,d.[Quantity]
	  ,SYSDATETIME()
    FROM
        deleted d
END

GO


INSERT INTO Menus VALUES (NEWID(), SYSDATETIME());
INSERT INTO Items VALUES (NEWID(), 'Chicken Lawash', 0, '');
INSERT INTO Items VALUES (NEWID(), 'Chicken Bun', 0, '');
INSERT INTO Items VALUES (NEWID(), 'Sheep Lawash', 0, '');
INSERT INTO Items VALUES (NEWID(), 'Sheep Bun', 0, '');
INSERT INTO Items VALUES (NEWID(), 'Fries', 1, '');
INSERT INTO Items VALUES (NEWID(), 'Cola', 3, '');
INSERT INTO Items VALUES (NEWID(), 'Fanta', 3, '');
INSERT INTO Items VALUES (NEWID(), 'Sprite', 3, '');
INSERT INTO Items VALUES (NEWID(), 'Salad', 2, '');

INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Chicken Lawash'), 15.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Chicken Bun'), 15.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Sheep Lawash'), 15.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Sheep Bun'), 15.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Fries'), 10.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Cola'), 5.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Fanta'), 5.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Sprite'), 5.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Sprite'), 5.00);
INSERT INTO MenuItems VALUES (NEWID(), (SELECT TOP 1 MenuId FROM Menus), (SELECT TOP 1 ItemId FROM Items WHERE Name = 'Salad'), 12.00);

