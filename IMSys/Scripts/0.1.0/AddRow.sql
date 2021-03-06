﻿IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AddRow') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].AddRow

GO

CREATE PROCEDURE [dbo].[AddRow]
	
	@name varchar(max),
	@price decimal(10,2),
	@quantity int,
	@unit varchar(max),
	@categoryid int = 1

	AS
	
	BEGIN TRANSACTION

	INSERT INTO Inventory 
	VALUES (@name, @price, @quantity, @unit, @categoryid)
	

	COMMIT TRANSACTION

	IF @@ERROR <> 0 
	ROLLBACK TRANSACTION