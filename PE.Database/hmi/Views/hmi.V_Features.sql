
CREATE     VIEW [hmi].[V_Features]
AS

/*
	select * from [hmi].[V_Features]
*/

SELECT ISNULL(ROW_NUMBER() OVER(
       ORDER BY A.AssetOrderSeq, 
                F.FeatureCode), 0) AS OrderSeq, 
       DENSE_RANK() OVER(
       ORDER BY A.AssetOrderSeq) AS AssetSeq, 
       A.AssetId, 
       A.AssetCode, 
       A.AssetName, 
       A.IsDelayCheckpoint, 
       F.FeatureId, 
       F.FeatureCode, 
       F.FeatureName, 
       F.FeatureDescription, 
       F.IsMaterialRelated, 
       F.IsLengthRelated, 
       F.IsActive, 
       F.IsSampledFeature, 
       F.IsDigital, 
       F.IsOnHMI, 
       F.IsQETrigger, 
       F.IsTrackingPoint, 
       F.IsMeasurementPoint, 
       F.IsConsumptionPoint, 
       F.EnumCommChannelType, 
       F.EnumFeatureType, 
       F.EnumFeatureProvider, 
       F.EnumAggregationStrategy, 
       F.EnumTagValidationResult, 
       dbo.FNGetEnumKeyword('CommChannelType', F.EnumCommChannelType) AS CommChannelType, 
       dbo.FNGetEnumKeyword('FeatureType', F.EnumFeatureType) AS FeatureType, 
       dbo.FNGetEnumKeyword('FeatureProvider', F.EnumFeatureProvider) AS FeatureProvider, 
       dbo.FNGetEnumKeyword('AggregationStrategy', F.EnumAggregationStrategy) AS AggregationStrategy, 
       dbo.FNGetEnumKeyword('TagValidationResult', F.EnumTagValidationResult) AS TagValidationResult, 
       F.RetentionFactor, 
       F.MinValue, 
       F.MaxValue, 
       F.SampleOffsetTime, 
       F.ConsumptionAggregationTime, 
       FP.FeatureCode AS ParentFeatureCode, 
       FP.FeatureName AS ParentFeatureName, 
       F.CommAttr1 AS CommAddress, 
       F.CommAttr2 AS CommTagNameSpace, 
       F.CommAttr3, 
       F.FKUnitOfMeasureId AS UnitId, 
       UOM.UnitSymbol, 
       UOMC.CategoryName AS UnitCategoryName, 
       DT.DataTypeId, 
       DT.DataTypeName, 
       DT.[MaxLength], 
       A.AreaCode, 
       A.AreaName
FROM dbo.MVHFeatures AS F
     INNER JOIN hmi.V_Assets AS A ON F.FKAssetId = A.AssetId
     INNER JOIN smf.UnitOfMeasure AS UOM ON F.FKUnitOfMeasureId = UOM.UnitId
     INNER JOIN smf.UnitOfMeasureCategory AS UOMC ON UOM.UnitCategoryId = UOMC.UnitCategoryId
     INNER JOIN dbo.DBDataTypes AS DT ON F.FKDataTypeId = DT.DataTypeId
     LEFT JOIN dbo.MVHFeatures AS FP ON F.FKParentFeatureId = FP.FeatureId
WHERE F.FeatureCode > 0;