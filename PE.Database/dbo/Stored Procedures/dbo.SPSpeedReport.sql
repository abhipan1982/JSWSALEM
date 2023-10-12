
CREATE   PROCEDURE dbo.SPSpeedReport @WorkOrderId BIGINT
AS

/*
	DECLARE @WorkOrderId BIGINT= 177082;
	exec SPSpeedReport  @WorkOrderId = 177082
	select * from hmi.V_Features
	select * from trkrawmaterials where fkmaterialid=7224818
*/

    BEGIN
        WITH Features1
             AS (SELECT TOP 100 PERCENT FeatureId, 
                                        FeatureCode, 
                                        FeatureName, 
                                        AssetCode, 
                                        AssetName, 
                                        'Speed' AS FeatureLabel, 
                                        UOM.UnitSymbol
                 FROM MVHFeatures F
                      INNER JOIN MVHAssets A ON F.FKAssetId = A.AssetId
                      INNER JOIN smf.UnitOfMeasure UOM ON F.FKUnitOfMeasureId = UOM.UnitId
                 WHERE FeatureCode IN(5200312, 5200362, 5200412, 5200462, 5200512, 5200562, 5300162, 5300212, 5300262, 5300312, 5300362, 5300412, 5400112, 5400162, 5400212, 5400262)
                 ORDER BY FeatureCode),
             Features2
             AS (SELECT TOP 100 PERCENT FeatureId, 
                                        FeatureCode, 
                                        FeatureName, 
                                        AssetCode, 
                                        AssetName, 
                                        'Torque' AS FeatureLabel, 
                                        UOM.UnitSymbol
                 FROM MVHFeatures F
                      INNER JOIN MVHAssets A ON F.FKAssetId = A.AssetId
                      INNER JOIN smf.UnitOfMeasure UOM ON F.FKUnitOfMeasureId = UOM.UnitId
                 WHERE FeatureCode IN(5200310, 5200360, 5200410, 5200460, 5200510, 5200560, 5300160, 5300210, 5300260, 5300310, 5300360, 5300410, 5400110, 5400160, 5400210, 5400260)
                 ORDER BY FeatureCode),
             Features
             AS (SELECT FeatureId, 
                        FeatureCode, 
                        FeatureName, 
                        AssetCode, 
                        AssetName, 
                        FeatureLabel, 
                        UnitSymbol
                 FROM Features1
                 UNION ALL
                 SELECT FeatureId, 
                        FeatureCode, 
                        FeatureName, 
                        AssetCode, 
                        AssetName, 
                        FeatureLabel, 
                        UnitSymbol
                 FROM Features2),
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
             -- Main Query
             SELECT M.MaterialSeqNo, 
                    M.MaterialId, 
                    M.MaterialName, 
                    M.WorkOrderName, 
                    F.AssetCode, 
                    F.AssetName, 
                    F.FeatureLabel, 
                    F.UnitSymbol, 
                    AVG(MV.ValueAvg) AS ValueAvg
             FROM Materials M
                  CROSS JOIN Features F
                  LEFT JOIN Measurements MV ON M.MaterialId = MV.MaterialId
                                               AND F.FeatureId = MV.FeatureId
             GROUP BY M.MaterialSeqNo, 
                      M.MaterialId, 
                      M.MaterialName, 
                      M.WorkOrderName, 
                      F.AssetCode, 
                      F.AssetName, 
                      F.FeatureLabel, 
                      F.UnitSymbol;
    END;