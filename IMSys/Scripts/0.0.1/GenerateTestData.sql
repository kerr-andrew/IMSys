IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GenerateTestData') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].GenerateTestData

GO


CREATE PROCEDURE [dbo].GenerateTestData

AS

BEGIN TRANSACTION 

	IF (OBJECT_ID('FK_Inv_Cat', 'F') IS NOT NULL)
	BEGIN
		ALTER TABLE Inventory DROP CONSTRAINT FK_Inv_Cat
	END
	IF (OBJECT_ID('PK_InventoryID', 'PK') IS NOT NULL)
	BEGIN
		ALTER TABLE Inventory DROP CONSTRAINT PK_InventoryID
	END
		IF (OBJECT_ID('PK_CategoriesID', 'PK') IS NOT NULL)
	BEGIN
		ALTER TABLE Categories DROP CONSTRAINT PK_CategoriesID
	END
	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_NAME = N'Inventory')
	BEGIN 
		DROP TABLE Inventory
	END

	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_NAME = N'Categories')
	BEGIN
		DROP TABLE Categories
	END

	CREATE TABLE Categories(
		liId int not null identity(1, 1),
		Name varchar(max) not null,
		constraint PK_CategoriesID primary key (liId)
	);

	insert into Categories values ('none'), ('dn'), ('deez nuts'), ('deez nutz'), ('bdc');

	CREATE TABLE Inventory(
	liId int NOT NULL IDENTITY(1,1),	
	itemName VARCHAR(MAX),
	Price Decimal(10,2),
	Quantity int,
	Unit VARCHAR(255),
	CategoryId int NOT NULL DEFAULT 1,
	CONSTRAINT PK_InventoryID PRIMARY KEY (liId),
	CONSTRAINT FK_Inv_Cat FOREIGN KEY (CategoryId) REFERENCES Categories(liId)
	);

	INSERT INTO Inventory (itemName, Price, Quantity, Unit, CategoryId)
						VALUES	('Copper Wire','0.09','20','ft',1),
								('Plug','2.99','39','unit',1),
								('Outlet','3.99','500','outlets',1),
								('maretts','0.01','2000','maretts',1),
								('screws','50.00','20000','screws',1);

COMMIT TRANSACTION

	IF @@ERROR <> 0 
	ROLLBACK TRANSACTION