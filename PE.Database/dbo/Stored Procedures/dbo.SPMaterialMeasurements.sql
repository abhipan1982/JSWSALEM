CREATE PROCEDURE [dbo].[SPMaterialMeasurements](@MaterialId BIGINT)
AS

/*
	EXEC [SPMaterialMeasurements] 7224180
*/

     SELECT MV.MeasurementId, 
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
            F.IsMeasurementPoint, 
            F.EnumFeatureType, 
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
            CASE UOM.UnitSymbol
                WHEN 'Num'
                THEN ''
                ELSE UOM.UnitSymbol
            END AS UnitSymbol
     FROM MVHMeasurements MV
          INNER JOIN MVHFeatures F ON MV.FKFeatureId = F.FeatureId
          INNER JOIN smf.UnitOfMeasure UOM ON F.FKUnitOfMeasureId = UOM.UnitId
          INNER JOIN hmi.V_Assets A ON F.FKAssetId = A.AssetId
          INNER JOIN TRKRawMaterials RM ON MV.FKRawMaterialId = RM.RawMaterialId
          INNER JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
          INNER JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
     WHERE M.MaterialId = @MaterialId;