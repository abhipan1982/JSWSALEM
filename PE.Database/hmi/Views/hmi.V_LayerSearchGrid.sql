CREATE   VIEW [hmi].[V_LayerSearchGrid]
AS

/* 
select * from [hmi].[V_LayerSearchGrid]
*/

SELECT RM.RawMaterialId, 
       RM.RawMaterialName, 
       RM.EnumLayerStatus, 
       RM.RawMaterialCreatedTs, 
       RM.RawMaterialStartTs, 
       RM.RawMaterialEndTs, 
       RM.RollingStartTs, 
       RM.RollingEndTs, 
       RM.ProductCreatedTs
FROM dbo.TRKRawMaterials AS RM
WHERE RM.EnumRawMaterialType = 1;