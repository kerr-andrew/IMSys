﻿IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetCategory') AND type IN (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCategory]

GO

CREATE PROCEDURE [dbo].[GetCategory]
	@id int,
	@OutputValue varchar(max) OUTPUT
	
AS
	SELECT @OutputValue = Name FROM Categories WHERE liId = @id;