

CREATE       VIEW [hmi].[V_RollHistory] AS 
/*
select * from [hmi].[V_RollHistory]
*/
SELECT R.RollId, 
          R.RollName, 
          R.ActualDiameter, 
          R.InitialDiameter, 
          R.MinimumDiameter, 
          R.EnumRollStatus, 
          R.RollDescription, 
          R.Supplier, 
          R.ScrapTime, 
          R.EnumRollScrapReason, 
          RT.RollTypeName, 
          RT.DiameterMin, 
          RT.DiameterMax, 
          RS.RollSetId, 
          RS.RollSetName, 
          RS.EnumRollSetStatus, 
          RSH.RollSetHistoryId, 
          RSH.MountedTs, 
          RSH.DismountedTs, 
          RSH.MountedInMillTs, 
          RSH.DismountedFromMillTs, 
          RSH.EnumRollSetHistoryStatus, 
          RGH.CreatedTs AS GrooveCreatedTs, 
          RGH.ActivatedTs AS GrooveActivatedTs, 
          RGH.DeactivatedTs AS GrooveDeactivatedTs, 
          RGH.RollGrooveHistoryId, 
          RGH.FKGrooveTemplateId AS GrooveTemplateId, 
          RGH.EnumRollGrooveStatus, 
          RGH.GrooveNumber, 
          RGH.AccWeight, 
          RGH.AccBilletCnt, 
          RGH.AccWeightWithCoeff, 
          RGH.Remarks AS GrooveRemarks, 
          RGH.EnumGrooveCondition, 
          GT.GrooveTemplateName, 
          GT.EnumGrooveSetting, 
          GT.AccBilletCntLimit, 
          GT.AccWeightLimit, 
          'Upper' AS RollLocalization, 
          S.StandNo, 
          S.StandName
   FROM dbo.RLSRollSetHistory AS RSH
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
        INNER JOIN dbo.RLSRolls AS R
        INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId ON RS.FKUpperRollId = R.RollId
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                                       AND R.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
        LEFT JOIN RLSCassettes C
        INNER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId ON RSH.FKCassetteId = C.CassetteId
        LEFT JOIN RLSStands S ON S.StandId = C.FKStandId
   UNION
   SELECT R.RollId, 
          R.RollName, 
          R.ActualDiameter, 
          R.InitialDiameter, 
          R.MinimumDiameter, 
          R.EnumRollStatus, 
          R.RollDescription, 
          R.Supplier, 
          R.ScrapTime, 
          R.EnumRollScrapReason, 
          RT.RollTypeName, 
          RT.DiameterMin, 
          RT.DiameterMax, 
          RS.RollSetId, 
          RS.RollSetName, 
          RS.EnumRollSetStatus, 
          RSH.RollSetHistoryId, 
          RSH.MountedTs, 
          RSH.DismountedTs, 
          RSH.MountedInMillTs, 
          RSH.DismountedFromMillTs, 
          RSH.EnumRollSetHistoryStatus, 
          RGH.CreatedTs AS GrooveCreatedTs, 
          RGH.ActivatedTs AS GrooveActivatedTs, 
          RGH.DeactivatedTs AS GrooveDeactivatedTs, 
          RGH.RollGrooveHistoryId, 
          RGH.FKGrooveTemplateId AS GrooveTemplateId, 
          RGH.EnumRollGrooveStatus, 
          RGH.GrooveNumber, 
          RGH.AccWeight, 
          RGH.AccBilletCnt, 
          RGH.AccWeightWithCoeff, 
          RGH.Remarks AS GrooveRemarks, 
          RGH.EnumGrooveCondition, 
          GT.GrooveTemplateName, 
          GT.EnumGrooveSetting, 
          GT.AccBilletCntLimit, 
          GT.AccWeightLimit, 
          'Bottom' RollLocalization, 
          S.StandNo, 
          S.StandName
   FROM dbo.RLSRollSetHistory AS RSH
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
        INNER JOIN dbo.RLSRolls AS R
        INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId ON RS.FKBottomRollId = R.RollId
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                                       AND R.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
        LEFT JOIN RLSCassettes C
        INNER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId ON RSH.FKCassetteId = C.CassetteId
        LEFT JOIN RLSStands S ON S.StandId = C.FKStandId
   UNION
   SELECT R.RollId, 
          R.RollName, 
          R.ActualDiameter, 
          R.InitialDiameter, 
          R.MinimumDiameter, 
          R.EnumRollStatus, 
          R.RollDescription, 
          R.Supplier, 
          R.ScrapTime, 
          R.EnumRollScrapReason, 
          RT.RollTypeName, 
          RT.DiameterMin, 
          RT.DiameterMax, 
          RS.RollSetId, 
          RS.RollSetName, 
          RS.EnumRollSetStatus, 
          RSH.RollSetHistoryId, 
          RSH.MountedTs, 
          RSH.DismountedTs, 
          RSH.MountedInMillTs, 
          RSH.DismountedFromMillTs, 
          RSH.EnumRollSetHistoryStatus, 
          RGH.CreatedTs AS GrooveCreatedTs, 
          RGH.ActivatedTs AS GrooveActivatedTs, 
          RGH.DeactivatedTs AS GrooveDeactivatedTs, 
          RGH.RollGrooveHistoryId, 
          RGH.FKGrooveTemplateId AS GrooveTemplateId, 
          RGH.EnumRollGrooveStatus, 
          RGH.GrooveNumber, 
          RGH.AccWeight, 
          RGH.AccBilletCnt, 
          RGH.AccWeightWithCoeff, 
          RGH.Remarks AS GrooveRemarks, 
          RGH.EnumGrooveCondition, 
          GT.GrooveTemplateName, 
          GT.EnumGrooveSetting, 
          GT.AccBilletCntLimit, 
          GT.AccWeightLimit, 
          'Third' RollLocalization, 
          S.StandNo, 
          S.StandName
   FROM dbo.RLSRollSetHistory AS RSH
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
        INNER JOIN dbo.RLSRolls AS R
        INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId ON RS.FKThirdRollId = R.RollId
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                                       AND R.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
        LEFT JOIN RLSCassettes C
        INNER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId ON RSH.FKCassetteId = C.CassetteId
        LEFT JOIN RLSStands S ON S.StandId = C.FKStandId;