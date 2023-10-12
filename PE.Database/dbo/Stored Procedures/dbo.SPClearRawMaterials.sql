CREATE PROCEDURE [dbo].[SPClearRawMaterials]
AS

/*
INSERT INTO smf.Parameters (Description,Name,ValueInt,EnumParameterValueType,ParameterGroupId) VALUES
('The number of months we keep Raw Matreials','MaterialDataKeptInMonths',60,1,5)
EXEC [dbo].[SPClearRawMaterials]
SELECT * FROM smf.Alarms ORDER BY 1 DESC
*/

    BEGIN
        DECLARE @LogValue NVARCHAR(300);
        DECLARE @MonthsToKeep INT;
        SET @MonthsToKeep =
        (
            SELECT ValueInt
            FROM [smf].[Parameters]
            WHERE [Name] = 'MaterialDataKeptInMonths'
        );
        IF @MonthsToKeep > 0
            BEGIN TRY
                -- Drop TemporaryTable
                IF OBJECT_ID('tempdb.dbo.#RawMaterials', 'U') IS NOT NULL
                    DROP TABLE #RawMaterials;
                -- Fill TemporaryTable
                SELECT RawMaterialId
                INTO #RawMaterials
                FROM TRKRawMaterials
                WHERE RawMaterialCreatedTs <= DATEADD(MONTH, -1 * @MonthsToKeep, GETDATE());
                -- Delete RawMaterials
                DELETE FROM TRKRawMaterials
                WHERE RawMaterialId IN
                (
                    SELECT RawMaterialId
                    FROM #RawMaterials
                );
                -- Log operation
                SET @LogValue = 'Found and deleted: ' + CAST(@@ROWCOUNT AS NVARCHAR(10)) + ' Raw Materials Older than ' + CAST(@MonthsToKeep AS VARCHAR) + ' month(s)';
                EXEC dbo.SPLogDB 
                     @LogValue;
            END TRY
            BEGIN CATCH
                EXEC dbo.SPLogDB 
                     @LogValue;
            END CATCH;
    END;