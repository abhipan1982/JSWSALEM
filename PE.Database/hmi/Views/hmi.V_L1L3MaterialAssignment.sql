
CREATE     VIEW [hmi].[V_L1L3MaterialAssignment]
AS

/*
select count(RawMaterialId) from TRKRawMaterials
select count(MaterialId) from PRMMaterials

select * from [hmi].[V_L1L3MaterialAssignment]
*/

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
            ORDER BY M.MaterialId DESC), 0) AS OrderSeq, 
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
            ISNULL(RM.EnumRawMaterialStatus, 0) AS EnumRawMaterialStatus, 
            RM.LastWeight, 
            RMR.ParentRawMaterialId AS ParentRawMaterialId, 
            ISNULL(RM.EnumRejectLocation, 0) AS EnumRejectLocation, 
            ISNULL(RM.EnumTypeOfScrap, 0) AS EnumTypeOfScrap, 
            RM.OutputPieces, 
            RM.ScrapPercent, 
            RM.FKProductId AS ProductId, 
            dbo.FNGetEnumKeyword('RawMaterialStatus', RM.EnumRawMaterialStatus) AS RawMaterialStatus, 
            ISNULL(D.DefectsNumber, 0) AS DefectsNumber
     FROM PRMMaterials AS M
          LEFT JOIN TRKRawMaterials AS RM ON M.MaterialId = RM.FKMaterialId
          LEFT JOIN Relations AS RMR ON RM.RawMaterialId = RMR.ChildRawMaterialId
          LEFT JOIN Defects AS D ON RM.RawMaterialId = D.FKRawMaterialId;