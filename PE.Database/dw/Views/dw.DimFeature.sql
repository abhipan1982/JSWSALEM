




CREATE           VIEW [dw].[DimFeature] AS 
/*
	SELECT * FROM dw.DimFeature
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY F.FeatureId), 0) AS DimFeatureRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimFeatureIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(F.FeatureId AS NVARCHAR), ';') + 
			COALESCE(CAST(F.FKAssetId AS NVARCHAR), ';') + 
			COALESCE(CAST(F.FKUnitOfMeasureId AS NVARCHAR), ';') + 
			COALESCE(CAST(F.FKDataTypeId AS NVARCHAR), ';') + 
			COALESCE(CAST(F.FeatureCode AS NVARCHAR), ';') + 
			COALESCE(CAST(F.FeatureName AS NVARCHAR), ';') + 
			COALESCE(CAST(F.FeatureDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(F.IsMaterialRelated AS NVARCHAR), ';') + 
			COALESCE(CAST(F.IsLengthRelated AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimFeatureHash, 
          F.FeatureId AS DimFeatureKey, 
          F.FKAssetId AS DimAssetKey, 
          F.FKUnitOfMeasureId AS DimUnitKey, 
          F.FKDataTypeId AS DimDataTypeKey, 
          F.FeatureCode, 
          F.FeatureName, 
          F.FeatureDescription, 
          F.IsMaterialRelated AS FeatureIsMaterialRelated, 
          F.IsLengthRelated AS FeatureIsLengthRelated,
		  A.AssetName,
		  U.UnitSymbol AS UOMSymbol,
		  DT.DataType AS DataType
   FROM dbo.MVHFeatures AS F
   INNER JOIN dbo.MVHAssets AS A ON F.FKAssetId = A.AssetId
   INNER JOIN smf.UnitOfMeasure AS U ON F.FKUnitOfMeasureId = U.UnitId
   INNER JOIN dbo.DBDataTypes AS DT ON F.FKDataTypeId = DT.DataTypeId