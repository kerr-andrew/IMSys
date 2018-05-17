IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'RenameCategory') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[RenameCategory]

GO

CREATE PROCEDURE [dbo].[RenameCategory]
	@oldCategory VARCHAR(max),
	@newCategory VARCHAR(max)
AS
	BEGIN TRANSACTION

	UPDATE Categories
	SET Name = @newCategory
	WHERE Name = @oldCategory;

	COMMIT TRANSACTION

	IF @@ERROR <> 0 
	ROLLBACK TRANSACTION
