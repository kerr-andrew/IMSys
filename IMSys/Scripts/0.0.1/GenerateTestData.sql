IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GenerateTestData') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].GenerateTestData

GO

CREATE PROCEDURE [dbo].GenerateTestData

AS

BEGIN TRANSACTION 

	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_NAME = N'Inventory')
	BEGIN 
		DROP TABLE Inventory
	END

	CREATE TABLE Inventory(
	liId int NOT NULL IDENTITY(1,1),
	itemName VARCHAR(MAX),
	Price Decimal(10,2),
	Quantity int,
	Unit VARCHAR(255),
	Value Decimal(10,2));

	INSERT INTO Inventory VALUES('Copper Wire','0.09','20','ft','1.80'),
								('Plug','2.99','39','unit','116.61'),
								('Outlet','3.99','500','outlets','300.00'),
								('maretts','0.01','2000','maretts','20.00'),
								('screws','50.00','20000','screws','2000000.00');

COMMIT TRANSACTION

	IF @@ERROR <> 0 
	ROLLBACK TRANSACTION

	