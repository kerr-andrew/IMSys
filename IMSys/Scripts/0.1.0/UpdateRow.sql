﻿IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UpdateRow') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].UpdateRow

GO

CREATE PROCEDURE [dbo].[UpdateRow]
	
	
	@id int,
	@name varchar(max),
	@price decimal(10,2),
	@quantity int,
	@unit varchar(max),
	@categoryId int

	AS
	
	BEGIN TRANSACTION

	UPDATE Inventory 
	SET itemName = @name, Price = @price, Quantity = @quantity, Unit = @unit, CategoryId = @categoryId
	WHERE liId = @id;
	
	COMMIT TRANSACTION

	IF @@ERROR <> 0 
	ROLLBACK TRANSACTION