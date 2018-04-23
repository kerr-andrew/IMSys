IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetItems') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetItems]

CREATE PROCEDURE [dbo].[GetItems]

	@id int

AS

	SELECT itemName, Price, Quantity, Unit, Value FROM Inventory
	WHERE liId = @id;

RETURN SELECT COUNT(*) FROM Inventory;