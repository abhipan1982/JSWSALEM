
--select * from hmi.V_Assets
--select * from hmi.V_Features

CREATE   VIEW [hmi].[V_TrackingInstructions]
AS SELECT TOP 100 PERCENT F.FeatureName, 
                          AA.AssetName AS AreaName, 
                          PA.AssetName AS PointName, 
                          TI.SeqNo, 
                          TI.TrackingInstructionValue, 
                          dbo.FNGetEnumKeyword('TrackingInstructionType', TI.EnumTrackingInstructionType) AS InstructionType, 
                          dbo.FNGetEnumKeyword('FeatureType', F.EnumFeatureType) AS FeatureType, 
                          dbo.FNGetEnumKeyword('TrackingAreaType', AA.EnumTrackingAreaType) AS TrackingAreaType, 
                          PAT.AssetTypeName AS PointTypeName, 
                          F.FeatureCode, 
                          AA.AssetCode AS AreaCode, 
                          PA.AssetCode AS PointCode, 
                          EnumTrackingInstructionType, 
                          TrackingInstructionId
   FROM TRKTrackingInstructions TI
        INNER JOIN MVHFeatures F ON TI.FKFeatureId = F.FeatureId
        INNER JOIN MVHAssets AA ON TI.FKAreaAssetId = AA.AssetId
        LEFT JOIN MVHAssets PA ON TI.FKPointAssetId = PA.AssetId
        LEFT JOIN MVHAssetTypes PAT ON PA.FKAssetTypeId = PAT.AssetTypeId
   ORDER BY F.FeatureCode, 
            AA.OrderSeq, 
            PA.OrderSeq, 
            TI.SeqNo;