CREATE PROCEDURE [dbo].[SPExecSqlWithLog] @SQLStat    NVARCHAR(MAX) = NULL, 
                                         @LogMessage NVARCHAR(MAX) = NULL
AS
/*
exec SPExecSqlWithLog 'select count(1) from dw.FactRawMaterial'
select * from dbo.DBLogs
*/
    BEGIN
        DECLARE @LogValue VARCHAR(255);
        DECLARE @ResultSet NVARCHAR(MAX);
        BEGIN TRY
            EXECUTE @ResultSet = sp_executesql 
                    @SQLStat;
            SET @LogValue = 'Proceed: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' record(s)';
            EXEC dbo.SPLogDB 
                 @LogValue = @LogValue, 
                 @LogMessage = @LogMessage;
        END TRY
        BEGIN CATCH
            EXEC dbo.SPLogDB 
                 @LogValue = @LogValue, 
                 @LogMessage = @LogMessage;
        END CATCH;
    END;
	