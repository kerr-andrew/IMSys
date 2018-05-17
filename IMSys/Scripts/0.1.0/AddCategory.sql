IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AddCategory') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddCategory]

GO

CREATE PROCEDURE [dbo].[AddCategory]
	@name VARCHAR(max)
AS
	BEGIN TRANSACTION
	
		INSERT INTO Categories VALUES (@name)

	COMMIT TRANSACTION

	IF @@ERROR <> 0
	ROLLBACK TRANSACTION

