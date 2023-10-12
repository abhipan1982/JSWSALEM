CREATE PROCEDURE [dbo].[SPGenerateMergeSQL] @TableName VARCHAR(50)
AS

/*
	EXEC dbo.SPGenerateMergeSQL DimProduct
*/

    BEGIN
        SET NOCOUNT ON;
        DECLARE @SQLStat VARCHAR(MAX), @SourceInsertColumns VARCHAR(MAX), @DestInsertColumns VARCHAR(MAX), @UpdateClause VARCHAR(MAX);
        DECLARE @LogMessage NVARCHAR(4000);
        DECLARE @LinkedServer VARCHAR(50);
        DECLARE @SourceDB VARCHAR(50), @DestinationDB VARCHAR(50);
        DECLARE @SourceSchema VARCHAR(50), @DestinationSchema VARCHAR(50);
        DECLARE @ColumnName VARCHAR(50), @KeyColName VARCHAR(50), @HashColName VARCHAR(50);
        DECLARE @IsKey BIT, @IsRow BIT, @IsHash BIT;
        DECLARE @NewLine CHAR= CHAR(10);
        SELECT @LinkedServer = ServerName, 
               @SourceDB = DatabaseName, 
               @SourceSchema = DatabaseSchema
        FROM dbo.DBDatabases
        WHERE IsWarehouse = 0;
        SELECT @DestinationDB = DatabaseName, 
               @DestinationSchema = DatabaseSchema
        FROM dbo.DBDatabases
        WHERE IsWarehouse = 1;
        SET @SQLStat = '';
        SET @SourceInsertColumns = '';
        SET @DestInsertColumns = '';
        SET @UpdateClause = '';
        SET @ColumnName = '';
        SET @IsKey = 0;
        SET @IsRow = 0;
        SET @IsHash = 0;
        SET @KeyColName = '';
        SET @HashColName = '';
        DECLARE @ColNames CURSOR;
        SET @ColNames = CURSOR
        FOR SELECT COLUMN_NAME, 
                   CAST(CASE
                            WHEN CAST(COLUMN_NAME AS VARCHAR) = CONCAT(CAST(TABLE_NAME AS VARCHAR), CAST('Key' AS VARCHAR))
                            THEN 1
                            ELSE 0
                        END AS INT) AS IsKey, 
                   CAST(CASE
                            WHEN CAST(COLUMN_NAME AS VARCHAR) = CONCAT(CAST(TABLE_NAME AS VARCHAR), CAST('Row' AS VARCHAR))
                            THEN 1
                            ELSE 0
                        END AS INT) AS IsRow, 
                   CAST(CASE
                            WHEN CAST(COLUMN_NAME AS VARCHAR) = CONCAT(CAST(TABLE_NAME AS VARCHAR), CAST('Hash' AS VARCHAR))
                            THEN 1
                            ELSE 0
                        END AS INT) AS IsHash
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @TableName
            ORDER BY ORDINAL_POSITION;
        OPEN @ColNames;
        FETCH NEXT FROM @ColNames INTO @ColumnName, @IsKey, @IsRow, @IsHash;
        WHILE @@FETCH_STATUS = 0
            BEGIN
                IF @IsRow = 0
                    BEGIN
                        SET @SourceInsertColumns = @SourceInsertColumns + CASE
                                                                              WHEN @SourceInsertColumns = ''
                                                                              THEN ''
                                                                              ELSE ','
                                                                          END + 'Src.' + @ColumnName;
                        SET @DestInsertColumns = @DestInsertColumns + CASE
                                                                          WHEN @DestInsertColumns = ''
                                                                          THEN ''
                                                                          ELSE ','
                                                                      END + @ColumnName;
                    END;
                IF(@IsKey = 0
                   AND @IsRow = 0)
                    BEGIN
                        SET @UpdateClause = @UpdateClause + CASE
                                                                WHEN @UpdateClause = ''
                                                                THEN ''
                                                                ELSE ','
                                                            END + @ColumnName + ' = ' + 'Src.' + @ColumnName + CHAR(10);
                    END;
                IF @IsKey = 1
                    BEGIN
                        SET @KeyColName = @ColumnName;
                    END;
                IF @IsHash = 1
                    BEGIN
                        SET @HashColName = @ColumnName;
                    END;
                FETCH NEXT FROM @ColNames INTO @ColumnName, @IsKey, @IsRow, @IsHash;
            END;
        CLOSE @ColNames;
        DEALLOCATE @ColNames;
        SET @SQLStat = 'MERGE ' + @DestinationSchema + '.' + @TableName + ' AS Dst
                USING [' + @LinkedServer + '].' + @SourceDB + '.' + @SourceSchema + '.' + @TableName + ' AS Src
                ON (Dst.' + @KeyColName + ' = Src.' + @KeyColName + ')
            WHEN NOT MATCHED BY TARGET THEN 
				INSERT(' + @DestInsertColumns + ') 
                VALUES(' + @SourceInsertColumns + ')
            WHEN MATCHED AND Dst.' + @HashColName + ' ! = Src.' + @HashColName + ' THEN 
				UPDATE SET 
                    ' + @UpdateClause + '
            WHEN NOT MATCHED BY SOURCE THEN 
				UPDATE SET ' + @TableName + 'IsDeleted = 1 ' + '
            OUTPUT $action, Inserted.*, Deleted.*;' + @NewLine;
        SET @LogMessage = 'Merging with ' + @TableName;
        --PRINT @SQLStat;
        EXEC dbo.SPExecSqlWithLog 
             @SQLStat, 
             @LogMessage;
    END;