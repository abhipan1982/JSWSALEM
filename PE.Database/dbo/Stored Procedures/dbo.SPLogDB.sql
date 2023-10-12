CREATE PROCEDURE [dbo].[SPLogDB] @LogValue NVARCHAR(300) = NULL, 
                                @LogDescr NVARCHAR(64)  = NULL
AS

/*
	exec [dbo].[SPLogDB] ''
	select * from smf.Alarms order by 1 desc
	SELECT * FROM [smf].[AlarmDefinitions] WHERE DefinitionCode = 'S003';
*/

    BEGIN
        DECLARE @AlarmDate DATETIME= GETDATE();
        DECLARE @ErrorMessage NVARCHAR(300);
        DECLARE @AlarmMessage NVARCHAR(300);
        DECLARE @AlarmOwner NVARCHAR(30)= 'DB';
        DECLARE @DefinitionId BIGINT;
        SET NOCOUNT ON;
        SET @ErrorMessage = ERROR_MESSAGE();
        SET @AlarmMessage = ISNULL(@ErrorMessage, ISNULL(@LogDescr, '') + @LogValue);
        SELECT @DefinitionId = AlarmDefinitionId
        FROM [smf].[AlarmDefinitions]
        WHERE DefinitionCode = 'T';
        INSERT INTO smf.Alarms
        (AlarmDate, 
         AlarmOwner, 
         FKAlarmDefinitionId, 
         DefaultMessage
        )
        VALUES
        (@AlarmDate, 
         @AlarmOwner, 
         @DefinitionId, 
         @AlarmMessage
        );
    END;