
CREATE     VIEW [dw].[FactRatingRootCause]
AS

/*
select * from dw.[FactRatingRootCause]
*/

SELECT ISNULL(CONVERT(NVARCHAR(50), DB_NAME()), 0) AS SourceName, 
       GETDATE() AS SourceTime, 
       ISNULL(CONVERT(BIT, 0), 0) AS FactRatingRootCauseIsDeleted, 
       ISNULL(ROW_NUMBER() OVER(
       ORDER BY RC.RootCauseId), 0) AS FactRatingRootCauseRow, 
       CAST(HASHBYTES('MD5', COALESCE(CAST(RC.RootCauseId AS NVARCHAR), ';') + COALESCE(CAST(RC.FKRatingId AS NVARCHAR), ';')) AS VARBINARY(16)) AS FactRatingRootCauseHash, 
       RC.RootCauseId AS FactRatingRootCauseKey, 
       RC.FKRatingId AS FactRatingKey, 
       RM.RawMaterialId AS DimMaterialKey, 
       RC.RootCauseName AS RootCauseName, 
       RC.RootCauseType AS RootCauseType, 
       RC.RootCausePriority AS RootCausePriority, 
       RC.RootCauseInfo AS RootCauseInfo, 
       RC.RootCauseCorrection AS RootCauseCorrection, 
       RC.RootCauseVerification AS RootCauseVerification, 
(
    SELECT RCA.RootCauseAggregateId, 
           RCA.FKAssetId
    FROM dbo.QERootCauseAggregate RCA
    WHERE RCA.FKRootCauseId = RC.RootCauseId FOR XML PATH('Aggregate'), ROOT('Root')
) AS RootCauseAggregates
FROM dbo.QERootCause RC
     INNER JOIN dbo.QERating AS QER ON RC.FKRatingId = QER.RatingId
     LEFT JOIN dbo.QEMappingValue AS QEMV
     INNER JOIN dbo.QERuleMappingValue AS QERMV ON QEMV.FKRuleMappingValueId = QERMV.RuleMappingValueId
     INNER JOIN dbo.TRKRawMaterials AS RM ON QERMV.FkRawMaterialId = RM.RawMaterialId ON QER.RatingId = QEMV.FKRatingId;