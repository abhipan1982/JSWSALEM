CREATE   VIEW [hmi].[V_MeasurementSearchGrid]
AS

/*
select * from [hmi].[V_MeasurementSearchGrid] 
*/

SELECT DISTINCT 
       RM.RawMaterialId, 
       RM.RawMaterialName, 
       RM.RawMaterialCreatedTs, 
       RM.RawMaterialStartTs, 
       RM.RawMaterialEndTs, 
       RM.RollingStartTs, 
       RM.RollingEndTs, 
       RM.ProductCreatedTs, 
       RM.EnumRawMaterialStatus, 
       RM.EnumLayerStatus, 
       ISNULL(CAST(CASE
                       WHEN RM.EnumRawMaterialType = 1
                       THEN 1
                       ELSE 0
                   END AS BIT), 0) AS RawMaterialIsLayer, 
       ISNULL(CAST(CASE
                       WHEN RMRP.ParentRawMaterialId IS NOT NULL
                       THEN 1
                       ELSE 0
                   END AS BIT), 0) IsParent, 
       ISNULL(CAST(CASE
                       WHEN RMRC.ParentRawMaterialId IS NOT NULL
                       THEN 1
                       ELSE 0
                   END AS BIT), 0) IsChild, 
       RMRC.ParentRawMaterialId AS ParentRawMaterialId, 
       ISNULL(RMRC.ParentRawMaterialId, RM.RawMaterialId) AS RootRawMaterialId, 
       ISNULL(LRMR.ParentLayerRawMaterialId, RM.RawMaterialId) AS RootLayerId, 
       M.MaterialId, 
       M.MaterialName, 
       ISNULL(M.MaterialName, CONCAT(RM.RawMaterialName, ' *')) AS DisplayedMaterialName
FROM dbo.TRKRawMaterials AS RM
     INNER JOIN dbo.MVHMeasurements AS MV ON RM.RawMaterialId = MV.FKRawMaterialId
     LEFT JOIN dbo.TRKRawMaterialRelations AS RMRP ON RM.RawMaterialId = RMRP.ParentRawMaterialId
     LEFT JOIN dbo.TRKRawMaterialRelations AS RMRC ON RM.RawMaterialId = RMRC.ChildRawMaterialId
     LEFT JOIN dbo.TRKLayerRawMaterialRelations AS LRMR ON RM.RawMaterialId = LRMR.ChildLayerRawMaterialId
     LEFT JOIN dbo.PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId;