CREATE PROCEDURE [dbo].[CreateTable]
	@tblname varchar,
	@sys as sys
AS
	SET NOCOUNT ON;

	DECLARE @sql NVARCHAR(MAX), @cols NVARCHAR(MAX) = N'';
	SELECT @cols += N',' + name + ' ' + system_type_name
		
		FROM sys.dm_exec_describe_first_result_set(N'SELECT * FROM test', NULL, 1);

	SET @cols = STUFF(@cols, 1, 1, N'');

	SET @sql = N'CREATE TABLE tmp(' + @cols + ');'

	DECLARE @dbs TABLE(db SYSNAME);

	INSERT @dbs VALUES(N'test');
	  -- SELECT whatever FROM dbo.databases

	INSERT INTO @
	SELECT @sql += N'
	  INSERT tmp SELECT ' + @cols + ' FROM ' + QUOTENAME(db) + ';'
	  FROM @dbs;

	SET @sql += N'
	  SELECT ' + @cols + ' FROM tmp;';

	PRINT @sql;
RETURN 0
