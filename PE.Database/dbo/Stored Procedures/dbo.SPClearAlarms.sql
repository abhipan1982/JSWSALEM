CREATE PROCEDURE [dbo].[SPClearAlarms]
AS

/*
INSERT INTO smf.Parameters (Description,Name,ValueInt,EnumParameterValueType,ParameterGroupId) VALUES
('The number of days we keep Alarms','AlarmKeptInDays',120,1,5)
EXEC [dbo].[SPClearAlarms]
SELECT * FROM smf.Alarms ORDER BY 1 DESC
*/

    BEGIN
        DECLARE @LogValue NVARCHAR(300);
        DECLARE @AlarmKeptInDays INT;
        SET @AlarmKeptInDays =
        (
            SELECT ValueInt
            FROM [smf].[Parameters]
            WHERE [Name] = 'AlarmKeptInDays'
        );
        IF @AlarmKeptInDays > 0
            BEGIN TRY
                -- Delete Alarms
                DELETE FROM [smf].[Alarms]
                WHERE AlarmDate <= DATEADD(DAY, -1 * @AlarmKeptInDays, GETDATE()); 
                -- Log operation
                SET @LogValue = CAST(ISNULL(@@ROWCOUNT, 0) AS NVARCHAR(10)) + ' Alarms older than ' + CAST(@AlarmKeptInDays AS VARCHAR) + ' days have been deleted. ';
                EXEC dbo.SPLogDB 
                     @LogValue;
            END TRY
            BEGIN CATCH
                EXEC dbo.SPLogDB 
                     @LogValue;
            END CATCH;
    END;