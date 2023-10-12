CREATE   VIEW [hmi].[V_PassChangeDataActual]
AS

/*
select * from [hmi].[V_PassChangeDataActual]
*/

SELECT RSH.RollSetHistoryId, 
       RSH.MountedTs, 
       RSH.EnumRollSetHistoryStatus, 
       RSH.PositionInCassette, 
       RS.RollSetId, 
       RS.RollSetName, 
       RS.RollSetType, 
       C.CassetteId, 
       C.CassetteName, 
       C.Arrangement, 
       S.StandId, 
       S.StandNo, 
       S.StandName, 
       S.Position, 
       S.EnumStandStatus, 
       S.FKAssetId AS AssetId, 
       R.ActualDiameter, 
       RT.RollTypeName, 
       RGH.AccWeight, 
       RGH.AccBilletCnt, 
       RGH.AccWeightWithCoeff, 
       RGH.GrooveNumber, 
       RGH.EnumRollGrooveStatus, 
       GT.AccBilletCntLimit, 
       GT.AccWeightLimit, 
       GT.GrooveTemplateId, 
       GT.GrooveTemplateName, 
       GT.EnumGrooveSetting,
       CASE
           WHEN ISNULL(GT.AccBilletCntLimit, 0) = 0
           THEN CAST(0 AS FLOAT)
           ELSE CAST(ISNULL(RGH.AccBilletCnt, 0) AS FLOAT) / CAST(GT.AccBilletCntLimit AS FLOAT)
       END AS AccBilletCntRatio,
       CASE
           WHEN ISNULL(GT.AccWeightLimit, 0) = 0
           THEN CAST(0 AS FLOAT)
           ELSE ISNULL(RGH.AccWeight, 0) / GT.AccWeightLimit
       END AS AccWeightRatio,
       CASE
           WHEN ISNULL(GT.AccWeightLimit, 0) = 0
           THEN CAST(0 AS FLOAT)
           ELSE ISNULL(RGH.AccWeightWithCoeff, 0) / GT.AccWeightLimit
       END AS AccWeightCoeffRatio
FROM dbo.RLSRollSetHistory AS RSH
     INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
     INNER JOIN dbo.RLSCassettes AS C ON RSH.FKCassetteId = C.CassetteId
     INNER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId
     INNER JOIN dbo.RLSRolls AS R ON RS.FKUpperRollId = R.RollId
     INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId
     INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                                    AND R.RollId = RGH.FKRollId
     INNER JOIN dbo.RLSGrooveTemplates GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
WHERE(RSH.EnumRollSetHistoryStatus = 1);