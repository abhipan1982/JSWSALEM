CREATE PROCEDURE [dbo].[SPClearTestWorkOrderDetails]
AS

/*
INSERT INTO smf.Parameters (Description,Name,ValueInt,EnumParameterValueType,ParameterGroupId) VALUES
('The number of months we keep details of Test WorkOrders','TestWOKeptInMonths',2,1,5)
EXEC [dbo].[SPClearTestWorkOrderDetails]
SELECT * FROM smf.Alarms ORDER BY 1 DESC
*/

    BEGIN
        DECLARE @LogValue NVARCHAR(300);
        DECLARE @TestWOKeptInMoths INT;
        SET @TestWOKeptInMoths =
        (
            SELECT ValueInt
            FROM [smf].[Parameters]
            WHERE [Name] = 'TestWOKeptInMonths'
        );
        IF @TestWOKeptInMoths > 0
            BEGIN TRY
                -- Drop TemporaryTable
                IF OBJECT_ID('tempdb.dbo.#RawMaterials', 'U') IS NOT NULL
                    DROP TABLE #RawMaterials;
                -- Fill TemporaryTable
                SELECT WorkOrderId, 
                       RawMaterialId
                INTO #RawMaterials
                FROM PRMWorkOrders WO
                     INNER JOIN PRMMaterials M ON WO.WorkOrderId = M.FKWorkOrderId
                     INNER JOIN TRKRawMaterials RM ON M.MaterialId = RM.FKMaterialId
                WHERE IsTestOrder = 1
                      AND WO.WorkOrderCreatedTs <= DATEADD(MONTH, -@TestWOKeptInMoths, GETDATE());
                -- Delete RawMaterials
                DELETE FROM TRKRawMaterials
                WHERE RawMaterialId IN
                (
                    SELECT RawMaterialId
                    FROM #RawMaterials
                );
                -- Log operation
                SET @LogValue = 'Found and deleted: ' + CAST(ISNULL(@@ROWCOUNT, 0) AS NVARCHAR(10)) + ' Raw Materials from Test Work Orders older than ' + CAST(@TestWOKeptInMoths AS VARCHAR) + ' month(s)';
                EXEC dbo.SPLogDB 
                     @LogValue;
            END TRY
            BEGIN CATCH
                EXEC dbo.SPLogDB 
                     @LogValue;
            END CATCH;
    END;