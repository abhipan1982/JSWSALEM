
CREATE     VIEW [dw].[FactRating]
AS

/*
select * from QERating
select * from dw.[FactRating]
*/

SELECT ISNULL(CONVERT(NVARCHAR(50), DB_NAME()), 0) AS SourceName, 
       GETDATE() AS SourceTime, 
       ISNULL(CONVERT(BIT, 0), 0) AS FactRatingIsDeleted, 
       ISNULL(ROW_NUMBER() OVER(
       ORDER BY QER.RatingId), 0) AS FactRatingRow, 
       CAST(HASHBYTES('MD5', COALESCE(CAST(QER.RatingId AS NVARCHAR), ';') + COALESCE(CAST(QER.RatingId AS NVARCHAR), ';')) AS VARBINARY(16)) AS FactRatingHash, 
       QER.RatingId AS FactRatingKey, 
       A.AssetId AS DimAssetKey, 
       RM.FKMaterialId AS DimMaterialKey, 
	   RM.RawMaterialId AS DimRawMaterialKey,
       CONVERT(INT, CONVERT(VARCHAR(4), QER.RatingCreated, 112)) AS DimYearKey, 
       ISNULL(CONVERT(INT, CONVERT(VARCHAR(8), QER.RatingCreated, 112)), 0) AS DimDateKey, 
	   
       DENSE_RANK() OVER(PARTITION BY A.AssetId, 
                                      RM.RawMaterialId
       ORDER BY COALESCE(QER.RatingValueForced, QEC.CompensationAlternative, QER.RatingValue) DESC) AS RatingRanking, 
       COALESCE(QER.RatingValueForced, QEC.CompensationAlternative, QER.RatingValue) AS RatingValueCurrent, 
       QER.RatingValueForced, 
       QEC.CompensationAlternative AS RatingValueAlternative, 
       QER.RatingValue AS RatingValueOriginal, 
       RM.RawMaterialName AS MaterialName, 
       A.AssetName, 
	   QEME.SignalIdentifier AS RuleSignal,
	   QEME.RulesIdentifierPart2 AS RuleName,
       QER.RatingType, 
       QER.RatingGroup, 
       QER.RatingCreated, 
       QER.RatingModified, 
       QER.RatingAlarm, 
       QER.RatingCode
FROM dbo.QERating AS QER
     LEFT JOIN dbo.QEMappingValue AS QEMV
     INNER JOIN dbo.QERuleMappingValue AS QERMV ON QEMV.FKRuleMappingValueId = QERMV.RuleMappingValueId
	 INNER JOIN dbo.QEMappingEntry AS QEME ON QEMV.FKMappingEntryId = QEME.MappingEntryId
     INNER JOIN dbo.TRKRawMaterials AS RM ON QERMV.FKRawMaterialId = RM.RawMaterialId
     INNER JOIN dbo.QETrigger AS T ON QERMV.FKTriggerId = T.TriggerId
     INNER JOIN dbo.MVHAssets AS A ON T.FKAssetId = A.AssetId ON QER.RatingId = QEMV.FKRatingId
     LEFT JOIN dbo.QECompensation AS QEC ON QER.RatingId = QEC.FKRatingId
                                            AND QEC.IsChosen = 1
WHERE 1 = 1;