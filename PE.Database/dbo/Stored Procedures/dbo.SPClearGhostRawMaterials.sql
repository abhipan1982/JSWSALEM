CREATE PROCEDURE [dbo].[SPClearGhostRawMaterials]
AS

/*
INSERT INTO smf.Parameters (Description,Name,ValueInt,EnumParameterValueType,ParameterGroupId) VALUES
('The number of months we keep Ghost Raw Matreials','GhostRMKeptInMonths',1,1,5)
EXEC [dbo].[SPClearGhostRawMaterials]
SELECT * FROM smf.Alarms ORDER BY 1 DESC
*/

    BEGIN
        DECLARE @LogValue NVARCHAR(300);
        DECLARE @GhostRMKeptInMonths INT;
        SET @GhostRMKeptInMonths =
        (
            SELECT ValueInt
            FROM [smf].[Parameters]
            WHERE [Name] = 'GhostRMKeptInMonths'
        );
        IF @GhostRMKeptInMonths > 0
            BEGIN TRY
                -- Drop TemporaryTable
                IF OBJECT_ID('tempdb.dbo.#GRawMaterials', 'U') IS NOT NULL
                    DROP TABLE #GRawMaterials;
                -- Fill TemporaryTable
                SELECT RawMaterialId
                INTO #GRawMaterials
                FROM TRKRawMaterials RM
                WHERE FKMaterialId IS NULL
                      AND RM.RawMaterialCreatedTs <= DATEADD(MONTH, -@GhostRMKeptInMonths, GETDATE());
                -- Delete RawMaterials
                DELETE FROM TRKRawMaterials
                WHERE RawMaterialId IN
                (
                    SELECT RawMaterialId
                    FROM #GRawMaterials
                );
                -- Log operation
                SET @LogValue = 'Found and deleted: ' + CAST(ISNULL(@@ROWCOUNT, 0) AS NVARCHAR(10)) + ' Ghost Raw Materials older than ' + CAST(ISNULL(@GhostRMKeptInMonths, -999) AS VARCHAR) + ' month(s)';
                EXEC dbo.SPLogDB 
                     @LogValue;
            END TRY
            BEGIN CATCH
                EXEC dbo.SPLogDB 
                     @LogValue;
            END CATCH;
    END;