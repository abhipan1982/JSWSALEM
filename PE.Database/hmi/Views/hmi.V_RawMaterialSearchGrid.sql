CREATE   VIEW [hmi].[V_RawMaterialSearchGrid]
AS

/*
select * from TRKRawMaterials
select * from [hmi].[V_RawMaterialSearchGrid]
*/

WITH Relations
     AS (SELECT ChildRawMaterialId, 
                MAX(ParentRawMaterialId) AS ParentRawMaterialId
         FROM TRKRawMaterialRelations
         GROUP BY ChildRawMaterialId)
     SELECT RM.RawMaterialId, 
            RM.RawMaterialName, 
            RM.RawMaterialCreatedTs, 
            RM.RawMaterialStartTs, 
            RM.RawMaterialEndTs, 
            RM.RollingStartTs, 
            RM.RollingEndTs, 
            RM.ProductCreatedTs, 
            RM.EnumRawMaterialStatus, 
            ISNULL(RMR.ParentRawMaterialId, RM.RawMaterialId) AS RootRawMaterialId, 
            M.MaterialId, 
            M.MaterialName, 
            M.MaterialCreatedTs, 
            M.MaterialStartTs, 
            M.MaterialEndTs, 
            ISNULL(M.IsAssigned, 0) AS MaterialIsAssigned, 
            COALESCE(M.MaterialName, CONCAT(RM.RawMaterialName, ' *')) AS DisplayedMaterialName
     FROM dbo.TRKRawMaterials AS RM
          LEFT JOIN Relations AS RMR ON RM.RawMaterialId = RMR.ChildRawMaterialId
          LEFT JOIN dbo.PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId
     WHERE RM.EnumRawMaterialType = 0;