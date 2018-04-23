IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'AddRow') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[CreateAddItemsTable]

CREATE PROCEDURE [dbo].[CreateAddItemsTable]

AS


BEGIN TRANSACTION

	IF OBJECT_ID('tempdb..#CreateAddItemsTable') IS NOT NULL
    DROP TABLE #CreateAddItemsTable

	CREATE TABLE #AddItems(
	name varchar(max),
	price decimal(10,2),
	quantity int,
	unit varchar(max),
	value decimal(10,2))

COMMIT TRANSACTION

IF @@ERROR <> 0
ROLLBACK TRANSACTION


