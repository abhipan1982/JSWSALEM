CREATE   PROCEDURE [dbo].[SPProfileReport] @WorkOrderId BIGINT
AS

/*
	DECLARE @WorkOrderId BIGINT= 177082;
	exec SPProfileReport  @WorkOrderId = 177082
	select * from hmi.V_Features order by featureCode
*/

    BEGIN
        WITH Features
             AS (SELECT TOP 100 PERCENT FeatureId, 
                                        FeatureCode, 
                                        FeatureName, 
                                        FeatureDescription, 
                                        AssetCode, 
                                        AssetName, 
                                        AssetDescription
                 FROM MVHFeatures F
                      INNER JOIN MVHAssets A ON F.FKAssetId = A.AssetId
                 WHERE FeatureCode IN(5400658, 5500308)
                 ORDER BY FeatureCode),
             Materials
             AS (SELECT SeqNo AS MaterialSeqNo, 
                        MaterialId, 
                        MaterialName, 
                        WorkOrderName
                 FROM PRMMaterials M
                      INNER JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
                 WHERE FKWorkOrderId = @WorkOrderId),
             Measurements
             AS (SELECT MaterialId, 
                        FeatureId, 
                        ValueAvg, 
                        ValueMin, 
                        ValueMax
                 FROM MVHMeasurements MV
                      INNER JOIN TRKRawMaterials RM ON MV.FKRawMaterialId = RM.RawMaterialId
                      INNER JOIN Materials M ON RM.FKMaterialId = M.MaterialId
                      INNER JOIN Features F ON MV.FKFeatureId = F.FeatureId)
             SELECT M.MaterialSeqNo, 
                    M.MaterialId, 
                    M.MaterialName, 
                    M.WorkOrderName, 
                    F.AssetCode, 
                    F.AssetName, 
                    F.AssetDescription, 
                    MV.ValueAvg, 
                    MV.ValueMin, 
                    MV.ValueMax
             FROM Materials M
                  CROSS JOIN Features F
                  LEFT JOIN Measurements MV ON M.MaterialId = MV.MaterialId
                                               AND F.FeatureId = MV.FeatureId;
    END;