CREATE   VIEW [dw].[DimRawMaterial]
AS

/*
	SELECT * FROM dw.DimRawMaterial
*/

SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
       GETDATE() AS SourceTime, 
       ISNULL(ROW_NUMBER() OVER(
       ORDER BY WO.WorkOrderId, 
                M.SeqNo, 
                RM.CuttingSeqNo), 0) AS DimRawMaterialRow, 
       ISNULL(CAST(0 AS BIT), 0) AS DimRawMaterialIsDeleted, 
       CAST(HASHBYTES('MD5', COALESCE(CAST(RM.RawMaterialId AS NVARCHAR), ';') + COALESCE(CAST(M.MaterialId AS NVARCHAR), ';') + COALESCE(CAST(M.FKWorkOrderId AS NVARCHAR), ';') + COALESCE(CAST(RM.EnumRawMaterialStatus AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimRawMaterialHash, 
       RM.RawMaterialId AS DimRawMaterialKey, 
       M.MaterialId AS DimMaterialKey, 
       M.FKWorkOrderId AS DimWorkOrderKey, 
       WO.FKSteelgradeId AS DimSteelgradeKey, 
       M.FKHeatId AS DimHeatKey, 
       M.FKMaterialCatalogueId AS DimMaterialCatalogueKey, 
       RM.RawMaterialName, 
       M.MaterialName AS MaterialName, 
       M.SeqNo AS MaterialSeqNo, 
       RM.CuttingSeqNo AS RawMaterialCuttingSeqNo, 
       RM.LastWeight AS RawMaterialWeight, 
       RM.LastLength AS RawMaterialLength, 
       RM.RawMaterialCreatedTs AS RawMaterialCreated, 
       RM.RawMaterialStartTs AS RawMaterialProductionStart, 
       RM.RawMaterialEndTs AS RawMaterialProductionEnd, 
       RM.RollingStartTs AS RawMaterialRollingStart, 
       RM.RollingEndTs AS RawMaterialRollingEnd, 
       dbo.FNGetEnumKeyword('RawMaterialStatus', RM.EnumRawMaterialStatus) AS RawMaterialStatus
FROM TRKRawMaterials AS RM
     INNER JOIN PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId
     INNER JOIN PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId;