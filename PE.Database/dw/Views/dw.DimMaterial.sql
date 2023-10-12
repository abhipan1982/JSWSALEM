CREATE   VIEW [dw].[DimMaterial]
AS

/*
	SELECT * FROM dw.DimMaterial where MaterialName = 'TestOrder20221123102233_1'
*/

WITH AssignedMaterials
     AS (SELECT FKMaterialId AS MaterialId, 
                COUNT(RawMaterialId) AS RawMaterialsAssigned
         FROM TRKRawMaterials
         WHERE FKMaterialId IS NOT NULL
         GROUP BY FKMaterialId)
     SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
            GETDATE() AS SourceTime, 
            ISNULL(ROW_NUMBER() OVER(
            ORDER BY M.MaterialId), 0) AS DimMaterialRow, 
            ISNULL(CAST(0 AS BIT), 0) AS DimMaterialIsDeleted, 
            CAST(HASHBYTES('MD5', COALESCE(CAST(M.MaterialId AS NVARCHAR), ';') + COALESCE(CAST(M.IsAssigned AS NVARCHAR), ';') + COALESCE(CAST(M.FKHeatId AS NVARCHAR), ';') + COALESCE(CAST(M.FKWorkOrderId AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimMaterialHash, 
            M.MaterialId AS DimMaterialKey, 
            M.FKWorkOrderId AS DimWorkOrderKey, 
            WO.FKSteelgradeId AS DimSteelgradeKey, 
            M.FKHeatId AS DimHeatKey, 
            M.FKMaterialCatalogueId AS DimMaterialCatalogueKey, 
            M.MaterialName AS MaterialName, 
            M.SeqNo AS MaterialSeqNo, 
            M.MaterialWeight, 
            M.MaterialLength, 
            M.MaterialThickness, 
            M.MaterialWidth, 
            M.MaterialCreatedTs AS MaterialCreated, 
            M.MaterialStartTs AS MaterialProductionStart, 
            M.MaterialEndTs AS MaterialProductionEnd, 
            M.IsAssigned AS MaterialIsAssignedWithRawMaterial, 
            ISNULL(AM.RawMaterialsAssigned, 0) AS RawMaterialsAssigned
     FROM PRMMaterials AS M
          LEFT JOIN PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
          LEFT JOIN AssignedMaterials AS AM ON M.MaterialId = AM.MaterialId;