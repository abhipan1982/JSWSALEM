



CREATE         VIEW [hmi].[V_RawMaterialMeasurements]
/*
SELECT * FROM hmi.V_RawMaterialMeasurements where rawmaterialname='SIM20200907114419310'
*/
AS SELECT MV.MeasurementId, 
          RM.RawMaterialId, 
          RM.RawMaterialName, 
          M.MaterialId, 
          M.MaterialName, 
          M.FKWorkOrderId AS WorkOrderId, 
          WO.WorkOrderName, 
          WO.FKHeatId AS HeatId, 
          WO.FKSteelgradeId AS SteelgradeId, 
          F.FeatureId, 
          F.FKUnitOfMeasureId AS UnitOfMeasureId, 
          F.FeatureCode, 
          F.FeatureName, 
          F.FeatureDescription, 
          F.IsSampledFeature, 
          A.AssetId, 
          A.AssetCode, 
          A.AssetName, 
          A.AreaCode, 
          A.AreaName, 
          A.ZoneCode, 
          A.ZoneName, 
          A.ParentAssetName, 
          MV.IsValid, 
          MV.CreatedTs AS MeasurementTime, 
          MV.ValueMin AS MeasurementValueMin, 
          MV.ValueAvg AS MeasurementValueAvg, 
          MV.ValueMax AS MeasurementValueMax, 
          UOM.UnitSymbol
   FROM MVHMeasurements MV
        INNER JOIN MVHFeatures F ON MV.FKFeatureId = F.FeatureId
        INNER JOIN smf.UnitOfMeasure UOM ON F.FKUnitOfMeasureId = UOM.UnitId
        INNER JOIN hmi.V_Assets A ON F.FKAssetId = A.AssetId
        INNER JOIN TRKRawMaterials RM ON MV.FKRawMaterialId = RM.RawMaterialId
        LEFT JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
        LEFT JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId;