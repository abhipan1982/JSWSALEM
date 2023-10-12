
CREATE     VIEW [hmi].[V_BundleSearchGrid]
AS

/*
	select * from TRKRawMaterials
	select * from [hmi].[V_BundleSearchGrid]
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
            ISNULL(RMR.ParentRawMaterialId, RM.RawMaterialId) AS RootRawMaterialId
     FROM dbo.TRKRawMaterials AS RM
          LEFT JOIN Relations AS RMR ON RM.RawMaterialId = RMR.ChildRawMaterialId
     WHERE RM.EnumRawMaterialType = 2;