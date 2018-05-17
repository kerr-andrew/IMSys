IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'RemoveCategory') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[RemoveCategory]

GO


CREATE PROCEDURE [dbo].[RemoveCategory]
	@categoryChange int,
	@categoryDelete varchar(max)

AS

	BEGIN TRANSACTION

	UPDATE Inventory 
	SET CategoryId = @categoryChange
	WHERE CategoryId = (Select liId FROM Categories WHERE Name = @categoryDelete);

	DELETE FROM Categories WHERE
	Name = @categoryDelete;

	COMMIT TRANSACTION

	IF @@ERROR <> 0
	ROLLBACK TRANSACTION