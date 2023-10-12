
CREATE     VIEW [hmi].[V_QERating]
AS

/*
select * from [hmi].[V_QERating]
*/

SELECT QER.RatingId, 
       A.AssetId, 
       A.AssetName, 
       RM.RawMaterialId, 
       RM.RawMaterialName, 
       DENSE_RANK() OVER(PARTITION BY A.AssetId, 
                                      RM.RawMaterialId
       ORDER BY COALESCE(QER.RatingValueForced, QEC.CompensationAlternative, QER.RatingValue) DESC) AS RatingRanking, 
	   QEME.SignalIdentifier,
	   QEME.RulesIdentifier,
	   QEME.RulesIdentifierPart1,
	   QEME.RulesIdentifierPart2,
	   QEME.RulesIdentifierPart3,
       QER.RatingValueForced, 
       QEC.CompensationAlternative AS RatingAlternative, 
       QER.RatingValue, 
       COALESCE(QER.RatingValueForced, QEC.CompensationAlternative, QER.RatingValue) AS RatingCurrentValue, 
	   QEC.ChosenTs,
       QER.RatingType, 
       QER.RatingCreated, 
       QER.RatingModified
FROM dbo.QERating AS QER
     INNER JOIN dbo.QEMappingValue AS QEMV ON QER.RatingId = QEMV.FKRatingId
     INNER JOIN dbo.QERuleMappingValue AS QERMV ON QEMV.FKRuleMappingValueId = QERMV.RuleMappingValueId
	 Inner join dbo.QEMappingEntry AS QEME ON QEMV.FKMappingEntryId = QEME.MappingEntryId
     INNER JOIN dbo.TRKRawMaterials AS RM ON QERMV.FKRawMaterialId = RM.RawMaterialId
     INNER JOIN dbo.QETrigger AS T ON QERMV.FKTriggerId = T.TriggerId
     INNER JOIN dbo.MVHAssets AS A ON T.FKAssetId = A.AssetId
     LEFT JOIN dbo.QECompensation AS QEC ON QER.RatingId = QEC.FKRatingId
                                            AND QEC.IsChosen = 1;