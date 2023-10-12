CREATE PROCEDURE [dbo].[SPLoadingDW]
AS

/*
	EXEC dbo.SPLoadingDW
	SELECT * FROM DBLogs ORDER BY 1 DESC;
	TRUNCATE TABLE DBLogs;
*/

    BEGIN
        DECLARE @SQLStat NVARCHAR(MAX);
        DECLARE @SQLCur NVARCHAR(4000);
        DECLARE @LogMessage NVARCHAR(4000);
        DECLARE @DestinationSchema VARCHAR(50);
        DECLARE @ObjectName NVARCHAR(100);
        SET @DestinationSchema = 'dw';
        SET @SQLCur = 'DECLARE Objects CURSOR FOR 
			SELECT TABLE_NAME AS ObjectName
			FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_SCHEMA = ''' + @DestinationSchema + ''' ORDER BY TABLE_NAME;';
        EXEC sp_executesql 
             @SQLCur;
        OPEN Objects;
        FETCH NEXT FROM Objects INTO @ObjectName;
        WHILE(@@FETCH_STATUS = 0)
            BEGIN
                SET @SQLStat = 'EXEC dbo.SPGenerateMergeSQL ' + @ObjectName;
                SET @LogMessage = 'Generating Merge Script for ' + @ObjectName;
                --PRINT @SQLStat;
                EXEC dbo.SPExecSqlWithLog 
                     @SQLStat, 
                     @LogMessage;
                FETCH NEXT FROM Objects INTO @ObjectName;
            END;
        CLOSE Objects;
        DEALLOCATE Objects;
    END;