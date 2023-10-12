
CREATE     VIEW [hmi].[V_AreaRawMaterialMeasurements]
AS

/*
	select * from [hmi].[V_AreaRawMaterialMeasurements]
*/

SELECT A.AreaName, 
       A.AssetId, 
       A.OrderSeq, 
       A.AssetCode, 
       A.AssetName, 
       F.FeatureId, 
       F.FeatureCode, 
       F.FeatureName, 
       F.FKUnitOfMeasureId AS UnitOfMeasureId, 
       UOM.UnitSymbol, 
       UOMC.UnitCategoryId, 
       UOMC.CategoryName, 
       RM.RawMaterialId, 
       MV.MeasurementId, 
       MV.CreatedTs AS MeasurementCreatedTs, 
       MV.ValueAvg AS MeasurementValueAvg
FROM dbo.TRKRawMaterials RM
     RIGHT JOIN dbo.MVHFeatures F ON F.IsOnHMI = 1
     INNER JOIN hmi.V_Assets A ON F.FKAssetId = A.AssetId
     INNER JOIN smf.UnitOfMeasure UOM ON F.FKUnitOfMeasureId = UOM.UnitId
     INNER JOIN smf.UnitOfMeasureCategory UOMC ON UOM.UnitCategoryId = UOMC.UnitCategoryId
     LEFT JOIN dbo.MVHMeasurements MV ON MV.FKFeatureId = F.FeatureId
                                         AND RM.RawMaterialId = MV.FKRawMaterialId
WHERE RM.RawMaterialId IS NOT NULL;