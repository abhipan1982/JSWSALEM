CREATE   VIEW [hmi].[V_RollHistoryPerGroove]
AS

/*
select  * from  [hmi].[V_RollHistoryPerGroove]
*/

SELECT ROW_NUMBER() OVER(
       ORDER BY RSH.RollSetHistoryId DESC) AS OrderSeq, 
       R.RollId, 
       R.RollName, 
       RGH.RollGrooveHistoryId, 
       RGH.ActDiameter, 
       RGH.GrooveNumber, 
       RGH.AccWeight, 
       RGH.AccBilletCnt, 
       RGH.AccWeightWithCoeff, 
       RGH.EnumRollGrooveStatus, 
       RGH.EnumGrooveCondition, 
       RGH.Remarks AS GrooveRemarks, 
       RSH.FKRollSetId AS RollSetId, 
       RSH.RollSetHistoryId, 
       RSH.AccWeightLimit, 
       RSH.EnumRollSetHistoryStatus, 
       RSH.MountedTs AS RollSetMountedTs, 
       RSH.DismountedTs AS RollSetDismountedTs, 
       GT.GrooveTemplateName
FROM dbo.RLSRolls AS R
     INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON R.RollId = RGH.FKRollId
     INNER JOIN dbo.RLSRollSetHistory AS RSH ON RGH.FKRollSetHistoryId = RSH.RollSetHistoryId
     INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId;