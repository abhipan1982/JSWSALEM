CREATE PROCEDURE [dbo].[SPL3L1MaterialAssignment] @WorkOrderId BIGINT
AS

/*
EXECUTE dbo.SPL3L1MaterialAssignment 177048;
exec sp_describe_first_result_set N'EXECUTE dbo.SPL3L1MaterialAssignment 177048',null,1;
select count(RawMaterialId) from TRKRawMaterials
select count(MaterialId) from PRMMaterials
*/

    BEGIN TRY
        WITH Defects
             AS (SELECT FKRawMaterialId, 
                        COUNT(DefectId) AS DefectsNumber
                 FROM QTYDefects AS D
                 GROUP BY FKRawMaterialId),
             Relations
             AS (SELECT ChildRawMaterialId, 
                        MAX(ParentRawMaterialId) ParentRawMaterialId
                 FROM TRKRawMaterialRelations
                 GROUP BY ChildRawMaterialId)
             SELECT ISNULL(ROW_NUMBER() OVER(
                    ORDER BY M.MaterialId, 
                             RM.RawMaterialId DESC), 0) AS OrderSeq, 
                    M.MaterialId, 
                    M.MaterialName, 
                    M.SeqNo AS MaterialSeqNo, 
                    M.MaterialCreatedTs, 
                    M.FKHeatId AS HeatId, 
                    M.FKWorkOrderId AS WorkOrderId, 
                    M.MaterialWeight, 
                    M.IsAssigned, 
                    M.IsDummy, 
                    ISNULL(RM.RawMaterialName, CONCAT(M.MaterialName, ' *')) AS DisplayedMaterialName, 
                    RM.RawMaterialId, 
                    RM.RawMaterialName, 
                    RM.RawMaterialCreatedTs, 
                    RM.EnumRawMaterialStatus, 
                    RM.LastWeight, 
                    RMR.ParentRawMaterialId AS ParentRawMaterialId, 
                    RM.ChildsNo, 
                    RM.CuttingSeqNo, 
                    RM.EnumRejectLocation, 
                    RM.EnumTypeOfScrap, 
                    RM.OutputPieces, 
                    RM.ScrapPercent, 
                    RM.FKProductId AS ProductId, 
                    PCT.ProductCatalogueTypeCode, 
                    dbo.FNGetEnumKeyword('RawMaterialStatus', RM.EnumRawMaterialStatus) AS RawMaterialStatus, 
                    ISNULL(D.DefectsNumber, 0) AS DefectsNumber
             FROM PRMMaterials AS M
                  INNER JOIN PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
                  INNER JOIN PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
                  INNER JOIN PRMProductCatalogueTypes AS PCT ON PC.FKProductCatalogueTypeId = PCT.ProductCatalogueTypeId
                  LEFT JOIN TRKRawMaterials AS RM ON M.MaterialId = RM.FKMaterialId
                  LEFT JOIN Defects AS D ON RM.RawMaterialId = D.FKRawMaterialId
                  LEFT JOIN Relations AS RMR ON RM.RawMaterialId = RMR.ChildRawMaterialId
             WHERE M.FKWorkOrderId = @WorkOrderId;
    END TRY
    BEGIN CATCH
        EXEC dbo.SPGetErrorInfo;
    END CATCH;
        RETURN;