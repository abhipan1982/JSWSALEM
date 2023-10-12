
CREATE   VIEW [hmi].[V_Assets]
AS

/*
	select * from [hmi].[V_Assets]
	select * from hmi.V_Enums
*/

WITH TREE
     AS (SELECT [AssetId] = A1.AssetId, 
                [Level] = 1, 
                [Path] = CAST('Root' AS VARCHAR(100)), 
                [AreaId] = AssetId, 
                [AreaCode] = AssetCode, 
                [AreaName] = AssetName, 
                [AreaDescription] = AssetDescription, 
                [AreaTypeId] = FKAssetTypeId, 
                [ZoneCode] = AssetCode, 
                [ZoneName] = AssetName, 
                [ZoneDescription] = AssetDescription
         FROM dbo.MVHAssets AS A1
         WHERE A1.FKParentAssetId IS NULL
         UNION ALL
         SELECT [AssetId] = A2.AssetId, 
                [Level] = TREE.[Level] + 1, 
                [Path] = CAST(TREE.Path + '/' + RIGHT('0000000000' + CAST(ROW_NUMBER() OVER(
                                                      ORDER BY A2.OrderSeq) AS VARCHAR(10)), 10) AS VARCHAR(100)), 
                [AreaId] = CASE
                               WHEN IsArea = 0
                               THEN TREE.AreaId
                               ELSE A2.AssetId
                           END, 
                [AreaCode] = CASE
                                 WHEN IsArea = 0
                                 THEN TREE.AreaCode
                                 ELSE A2.AssetCode
                             END, 
                [AreaName] = CASE
                                 WHEN IsArea = 0
                                 THEN TREE.AreaName
                                 ELSE A2.AssetName
                             END, 
                [AreaDescription] = CASE
                                        WHEN IsArea = 0
                                        THEN TREE.AreaDescription
                                        ELSE A2.AssetDescription
                                    END, 
                [AreaTypeId] = CASE
                                   WHEN IsArea = 0
                                   THEN TREE.AreaTypeId
                                   ELSE A2.FKAssetTypeId
                               END, 
                [ZoneCode] = CASE
                                 WHEN IsZone = 0
                                 THEN TREE.ZoneCode
                                 ELSE A2.AssetCode
                             END, 
                [ZoneName] = CASE
                                 WHEN IsZone = 0
                                 THEN TREE.ZoneName
                                 ELSE A2.AssetName
                             END, 
                [ZoneDescription] = CASE
                                        WHEN IsZone = 0
                                        THEN TREE.ZoneDescription
                                        ELSE A2.AssetDescription
                                    END
         FROM dbo.MVHAssets AS A2
              INNER JOIN TREE ON TREE.AssetId = A2.FKParentAssetId),
     TagValidations
     AS (SELECT FKAssetId, 
                SUM(CASE
                        WHEN EnumTagValidationResult = 1
                        THEN 1
                        ELSE 0
                    END) AS ValidFeatures, 
                SUM(CASE
                        WHEN EnumFeatureProvider = 1
                        THEN 1
                        ELSE 0
                    END) AS L1Features, 
                COUNT(FeatureId) AS AllFeatures
         FROM MVHFeatures
		 WHERE IsActive = 1
         GROUP BY FKAssetId)
     SELECT ISNULL(ROW_NUMBER() OVER(
            ORDER BY TREE.[Path]), 0) AS OrderSeq, 
            A.AssetId, 
            A.OrderSeq AS AssetOrderSeq, 
            A.AssetCode, 
            A.AssetName, 
            A.AssetDescription, 
            AssetType.AssetTypeName AS AssetTypeName, 
            REPLICATE('    ', TREE.[Level] - 1) + A.AssetDescription AS Levels, 
            TREE.[AreaId], 
            TREE.[AreaCode], 
            TREE.[AreaName], 
            TREE.[AreaDescription], 
            TREE.[AreaTypeId], 
            AreaType.AssetTypeName AS AreaTypeName, 
            TREE.[ZoneCode], 
            TREE.[ZoneName], 
            TREE.[ZoneDescription], 
            A.IsActive, 
            A.IsArea, 
            A.IsZone, 
            A.IsPositionBased, 
            A.IsDelayCheckpoint, 
            A.IsTrackingPoint, 
            AEXT.IsQueue, 
            ISNULL(A.EnumTrackingAreaType, 0) AS EnumTrackingAreaType, 
            ISNULL(AssetType.EnumYardType, 0) AS EnumYardType, 
            ISNULL(AL.EnumFillPattern, 0) AS EnumFillPattern, 
            ISNULL(AL.EnumFillDirection, 0) AS EnumFillDirection, 
            dbo.FNGetEnumKeyword('TrackingAreaType', A.EnumTrackingAreaType) AS TrackingAreaType, 
            A.PositionsNumber, 
            A.VirtualPositionsNumber, 
            A.FKParentAssetId AS ParentAssetId, 
            A2.AssetName AS ParentAssetName, 
            TREE.[Path], 
            ISNULL(TV.ValidFeatures, 0) AS ValidFeatures, 
            ISNULL(TV.L1Features, 0) AS L1Features, 
            ISNULL(TV.AllFeatures, 0) AS AllFeatures,
            CAST(CASE
                WHEN ISNULL(TV.ValidFeatures,0) = ISNULL(TV.L1Features,0)
                THEN 1
                ELSE 0
            END AS BIT) AS FeaturesAreValid
     FROM TREE
          INNER JOIN dbo.MVHAssets AS A ON TREE.AssetId = A.AssetId
          LEFT JOIN prj.MVHAssetsEXT AS AEXT ON A.AssetId = AEXT.FKAssetId
          LEFT JOIN dbo.MVHAssetsLocation AS AL ON A.AssetId = AL.FKAssetId
          LEFT JOIN dbo.MVHAssets AS A2 ON A.FKParentAssetId = A2.AssetId
          LEFT JOIN dbo.MVHAssetTypes AS AssetType ON A.FKAssetTypeId = AssetType.AssetTypeId
          LEFT JOIN dbo.MVHAssetTypes AS AreaType ON TREE.AreaTypeId = AreaType.AssetTypeId
          LEFT JOIN TagValidations AS TV ON A.AssetId = TV.FKAssetId
     WHERE A.AssetCode > 0;