CREATE   VIEW [dw].[DimAsset]
AS

/*
	SELECT * FROM dw.DimAsset;
*/

SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
       GETDATE() AS SourceTime, 
       ISNULL(ROW_NUMBER() OVER(
       ORDER BY A.AssetId), 0) AS DimAssetRow, 
       ISNULL(CAST(0 AS BIT), 0) AS DimAssetIsDeleted, 
       CAST(HASHBYTES('MD5', COALESCE(CAST(A.AssetId AS NVARCHAR), ';') + COALESCE(CAST(A.FKParentAssetId AS NVARCHAR), ';') + COALESCE(CAST(A.AssetCode AS NVARCHAR), ';') + COALESCE(CAST(A.AssetName AS NVARCHAR), ';') + COALESCE(CAST(A.AssetDescription AS NVARCHAR), ';') + COALESCE(CAST(A.IsDelayCheckpoint AS NVARCHAR), ';') + COALESCE(CAST(A.IsArea AS NVARCHAR), ';') + COALESCE(CAST(A.IsZone AS NVARCHAR), ';') + COALESCE(CAST(A.IsReversible AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimAssetHash, 
       A.AssetId AS DimAssetKey, 
       A.FKParentAssetId AS DimAssetKeyParent, 
       A.AssetCode, 
       A.AssetName, 
       A.AssetDescription, 
       A.OrderSeq AS AssetOrderSeq, 
       A.IsDelayCheckpoint AS AssetIsDelayCheckpoint, 
       A.IsArea AS AssetIsArea, 
       A.IsZone AS AssetIsZone, 
       A.IsReversible AS AssetIsReversible, 
       AP.AssetCode AS AssetCodeParent, 
       AP.AssetName AS AssetNameParent, 
       AP.AssetDescription AS AssetDescriptionParent
FROM dbo.MVHAssets A
     LEFT JOIN dbo.MVHAssets AP ON A.FKParentAssetId = AP.AssetId;