CREATE PROCEDURE [dbo].[SPGetMeasure4WOAndFeature] @WorkOrderId BIGINT, 
                                                  @FeatureId   BIGINT
AS
     SELECT FKWorkOrderId AS WorkOrderId, 
            AVG(MV.ValueAvg) AS ValueAvg, 
            MIN(MV.ValueMin) AS ValueMin, 
            MAX(MV.ValueMax) AS ValueMax
     FROM MVHMeasurements MV
          INNER JOIN TRKRawMaterials RM ON MV.FKRawMaterialId = RM.RawMaterialId
          INNER JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
          INNER JOIN MVHFeatures F ON MV.FKFeatureId = F.FeatureId
     WHERE F.FeatureCode = @FeatureId
           AND FKWorkOrderId = @WorkOrderId
     GROUP BY FKWorkOrderId;