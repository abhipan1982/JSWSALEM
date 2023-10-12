CREATE   VIEW [hmi].[V_Measurements]
AS SELECT MV.MeasurementId, 
          MV.FKRawMaterialId AS RawMaterialId, 
          F.FeatureId, 
          F.FKUnitOfMeasureId AS UnitOfMeasureId, 
          A.AssetId AssetId, 
          F.FeatureCode, 
          F.FeatureName, 
          A.AssetName, 
          A2.AssetName AS ParentAssetName, 
          MV.IsValid, 
          MV.CreatedTs AS MeasurementTime, 
          MV.ValueMin AS MeasurementValueMin, 
          MV.ValueAvg AS MeasurementValueAvg, 
          MV.ValueMax AS MeasurementValueMax, 
          UOM.UnitSymbol
   FROM [dbo].[MVHMeasurements] MV
        INNER JOIN [dbo].[MVHFeatures] F ON MV.FKFeatureId = F.FeatureId
        INNER JOIN [smf].[UnitOfMeasure] UOM ON F.FKUnitOfMeasureId = UOM.UnitId
        INNER JOIN [dbo].[MVHAssets] A ON F.FKAssetId = A.AssetId
        LEFT JOIN [dbo].[MVHAssets] A2 ON A.FKParentAssetId = A2.AssetId;