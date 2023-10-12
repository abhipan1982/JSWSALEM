
CREATE     VIEW [dw].[FactRatingCompensation] AS

/*
select * from dw.[FactRatingCompensation]
*/

SELECT ISNULL(CONVERT(NVARCHAR(50), DB_NAME()), 0) AS SourceName, 
       GETDATE() AS SourceTime, 
       ISNULL(CONVERT(BIT, 0), 0) AS FactRatingCompensationIsDeleted, 
       ISNULL(ROW_NUMBER() OVER(
       ORDER BY C.CompensationId), 0) AS FactRatingCompensationRow, 
       CAST(HASHBYTES('MD5', COALESCE(CAST(C.CompensationId AS NVARCHAR), ';') + COALESCE(CAST(C.FKRatingId AS NVARCHAR), ';')) AS VARBINARY(16)) AS FactRatingCompensationHash, 
       C.CompensationId AS FactRatingCompensationKey, 
       C.FKRatingId AS FactRatingKey, 
       RM.RawMaterialId AS DimMaterialKey, 
       C.CompensationName AS CompensationName, 
       CT.CompensationName AS CompensationTypeName, 
       C.CompensationAlternative AS CompensationAlternative, 
       C.CompensationInfo AS CompensationInfo, 
       C.CompensationDetail AS CompensationDetail, 
       C.IsChosen AS CompensationIsChosen, 
	   C.ChosenTs AS CompensationChosen,
(
    SELECT CA.CompensationAggregateId, 
           CA.FKAssetId
    FROM dbo.QECompensationAggregate AS CA
    WHERE CA.FKCompensationId = C.CompensationId FOR XML PATH('Aggregate'), ROOT('Root')
) AS CompensationAggregates
FROM dbo.QECompensation AS C
     INNER JOIN dbo.QECompensationType AS CT ON C.FKCompensationTypeId = CT.CompensationTypeId
     INNER JOIN dbo.QERating AS QER ON C.FKRatingId = QER.RatingId
     LEFT JOIN dbo.QEMappingValue AS QEMV
     INNER JOIN dbo.QERuleMappingValue AS QERMV ON QEMV.FKRuleMappingValueId = QERMV.RuleMappingValueId
     INNER JOIN dbo.TRKRawMaterials AS RM ON QERMV.FkRawMaterialId = RM.RawMaterialId ON QER.RatingId = QEMV.FKRatingId;