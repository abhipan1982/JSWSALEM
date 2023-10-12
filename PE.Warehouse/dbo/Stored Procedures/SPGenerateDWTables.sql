CREATE PROCEDURE [dbo].[SPGenerateDWTables]
AS

/*
	EXEC dbo.SPGenerateDWTables

	THIS PROCEDURE DROPS AND CREATES ALL EXISTING DIM AND FACT TABLES !!!
	
	SELECT * FROM DBLogs ORDER BY 1 DESC;

	TRUNCATE TABLE DBLogs

	SELECT sys.tables.name TableName, 
		   sys.columns.name ColumnName, 
		   is_identity IsIdentity
	FROM sys.columns
		 INNER JOIN sys.tables ON sys.tables.object_id = sys.columns.object_id
								  AND sys.columns.is_identity = 1;
*/

    BEGIN
        SET ANSI_NULLS ON;
        SET ANSI_WARNINGS ON;
        DECLARE @SQLStat NVARCHAR(4000);
        DECLARE @SQLCur NVARCHAR(4000);
        DECLARE @LogMessage NVARCHAR(4000);
        DECLARE @LinkedServer VARCHAR(50);
        DECLARE @SourceDB VARCHAR(50), @DestinationDB VARCHAR(50);
        DECLARE @SourceSchema VARCHAR(50), @DestinationSchema VARCHAR(50);
        DECLARE @ObjectName NVARCHAR(100);
        DECLARE @IdentityType VARCHAR(10);
        DECLARE @NewLine CHAR= CHAR(10);
		--select * FROM dbo.DBDatabases
        SELECT @LinkedServer = ServerName, 
               @SourceDB = DatabaseName, 
               @SourceSchema = DatabaseSchema
        FROM dbo.DBDatabases
        WHERE IsWarehouse = 0;
        SELECT @DestinationDB = DatabaseName, 
               @DestinationSchema = DatabaseSchema
        FROM dbo.DBDatabases
        WHERE IsWarehouse = 1;
        SET @SQLCur = 'DECLARE Objects CURSOR FOR SELECT TABLE_NAME AS ObjectName
			FROM [' + @LinkedServer + '].[' + @SourceDB + '].' + '[INFORMATION_SCHEMA].[VIEWS]
			WHERE TABLE_SCHEMA = ''' + @SourceSchema + ''' ORDER BY TABLE_NAME;';
        --PRINT @SQLCur;
        EXEC sp_executesql 
             @SQLCur;
        OPEN Objects;
        FETCH NEXT FROM Objects INTO @ObjectName;
        WHILE(@@FETCH_STATUS = 0)
            BEGIN
                SET @IdentityType = CASE
                                        WHEN UPPER(LEFT(@ObjectName, 4)) = 'FACT'
                                        THEN 'BIGINT'
                                        ELSE 'INT'
                                    END;
                SET @SQLStat = 'IF OBJECT_ID(''' + @DestinationDB + '.' + @DestinationSchema + '.' + @ObjectName + ''') IS NOT NULL' + @NewLine;
                SET @SQLStat = @SQLStat + 'DROP TABLE ' + @DestinationDB + '.' + @DestinationSchema + '.' + @ObjectName + ';' + @NewLine;
                SET @SQLStat = @SQLStat + 'SELECT * INTO ' + @DestinationDB + '.' + @DestinationSchema + '.' + @ObjectName + @NewLine;
                SET @SQLStat = @SQLStat + 'FROM [' + @LinkedServer + '].' + @SourceDB + '.' + @SourceSchema + '.' + @ObjectName + @NewLine;
                SET @SQLStat = @SQLStat + 'WHERE 1 = 0;' + @NewLine;
                SET @SQLStat = @SQLStat + 'ALTER TABLE ' + @DestinationDB + '.' + @DestinationSchema + '.' + @ObjectName + ' DROP COLUMN ' + @ObjectName + 'Row;' + @NewLine;
                SET @SQLStat = @SQLStat + 'ALTER TABLE ' + @DestinationDB + '.' + @DestinationSchema + '.' + @ObjectName + ' ADD ' + @ObjectName + 'Row ' + @IdentityType + ' IDENTITY(1, 1);' + @NewLine;
                SET @SQLStat = @SQLStat + 'SELECT 1 FROM ' + @DestinationDB + '.' + @DestinationSchema + '.' + @ObjectName + ';' + @NewLine;
                SET @LogMessage = 'Creating table: ' + @DestinationSchema + '.' + @ObjectName;
                --PRINT @SQLStat;
                EXEC dbo.SPExecSqlWithLog 
                     @SQLStat, 
                     @LogMessage;
                FETCH NEXT FROM Objects INTO @ObjectName;
            END;
        CLOSE Objects;
        DEALLOCATE Objects;
    END;