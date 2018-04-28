IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DeleteItems') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteItems]

CREATE PROCEDURE [dbo].[DeleteItems]

	@name varchar(max)

AS

	BEGIN TRANSACTION

	DELETE FROM Inventory WHERE itemName = @name;
	
	COMMIT TRANSACTION

	IF @@ERROR <> 0 
	ROLLBACK TRANSACTION