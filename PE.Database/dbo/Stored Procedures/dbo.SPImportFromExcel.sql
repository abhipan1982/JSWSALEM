CREATE   PROCEDURE [dbo].[SPImportFromExcel]
   @SheetName varchar(20),
   @FilePath varchar(100),
   @HDR varchar(3),
   @TableName varchar(50)
AS
BEGIN
    
	DECLARE @SQL nvarchar(1000)
	SET @SQL = '
	IF OBJECT_ID('+@TableName+', ''U'') IS NOT NULL
                DROP TABLE '+@TableName

	--SET @SQL = 'TRUNCATE TABLE ' + @TABLENAME
	EXEC(@SQL)

    IF OBJECT_ID (@TableName,'U') IS NOT NULL
      SET @SQL = 'INSERT INTO ' + @TableName + ' SELECT * FROM OPENDATASOURCE'
    ELSE
      SET @SQL = 'SELECT * INTO ' + @TableName + ' FROM OPENDATASOURCE'
 
    SET @SQL = @SQL + '(''Microsoft.ACE.OLEDB.16.0'',''Data Source='
    SET @SQL = @SQL + @FilePath + ';Extended Properties=''''Excel 12.0;HDR='
    SET @SQL = @SQL + @HDR + ''''''')...['
    SET @SQL = @SQL + @SheetName + ']'
	print @sql
    --EXEC sp_executesql @SQL
END