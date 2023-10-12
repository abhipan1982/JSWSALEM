CREATE PROCEDURE [dbo].[SPExecSqlWithLog] @SQLStat  NVARCHAR(MAX), 
                                          @LogDescr NVARCHAR(64)  = NULL, 
                                          @LogParam NVARCHAR(40)  = NULL
AS
    BEGIN
        DECLARE @LogValue VARCHAR(64);
        DECLARE @ResultSet NVARCHAR(MAX);
        BEGIN TRY
            EXECUTE @ResultSet = sp_executesql 
                    @SQLStat;
            SET @LogValue = 'Proceed: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' record(s)';
            EXEC dbo.spLogDB 
                 @LogValue, 
                 @LogDescr;
        END TRY
        BEGIN CATCH
            EXEC dbo.spLogDB 
                 @LogValue, 
                 @LogDescr;
        END CATCH;
    END;