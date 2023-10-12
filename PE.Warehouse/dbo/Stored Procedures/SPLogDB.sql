CREATE PROCEDURE [dbo].[SPLogDB] @LogType    NCHAR(10)     = NULL, 
                                @LogSource  NVARCHAR(50)  = NULL, 
                                @LogValue   NVARCHAR(255) = NULL, 
                                @LogMessage NVARCHAR(MAX) = NULL
AS
    BEGIN
        DECLARE @ErrorMessage NVARCHAR(MAX);
        SET NOCOUNT ON;
        SET @ErrorMessage = ERROR_MESSAGE();
        INSERT INTO dbo.DBLogs
        (LogType, 
         LogSource, 
         LogValue, 
         LogMessage, 
         ErrorMessage
        )
        VALUES
        (@LogType, 
         @LogSource, 
         @LogValue, 
         @LogMessage, 
         @ErrorMessage
        );
    END;