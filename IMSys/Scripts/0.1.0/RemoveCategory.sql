IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'RemoveCategory') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[RemoveCategory]

GO


CREATE PROCEDURE [dbo].[RemoveCategory]
	@removed int,
	@newid int
AS

	BEGIN TRANSACTION

	UPDATE Inventory 
	SET CategoryId = @newid
	WHERE CategoryId = @removed

	DELETE FROM Categories WHERE
	liId = @removed

	COMMIT TRANSACTION

	IF @@ERROR <> 0
	ROLLBACK TRANSACTION