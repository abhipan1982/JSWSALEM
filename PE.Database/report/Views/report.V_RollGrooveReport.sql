CREATE   VIEW [report].[V_RollGrooveReport] AS 
/*
select * from [report].[V_RollGrooveReport]
*/
SELECT S.StandId, 
          S.StandNo, 
          S.StandName, 
          S.Position, 
          S.EnumStandStatus AS StandStatus, 
          S.FKAssetId AS AssetId, 
          RS.RollSetId, 
          RS.RollSetType, 
          RS.RollSetName, 
          RS.EnumRollSetStatus AS RollSetStatus, 
          RSH.RollSetHistoryId, 
          RSH.CreatedTs AS RollSetCreated, 
          RSH.MountedTs AS RollSetMounted, 
          C.CassetteId, 
          C.CassetteName, 
          C.Arrangement, 
          RU.RollId AS UpperRollId, 
          RU.RollName AS UpperRollName, 
          RU.ActualDiameter AS UpperRollDiameter, 
          RTU.RollTypeName AS UpperRollType, 
          RB.RollId AS BottomRollId, 
          RB.RollName AS BottomRollName, 
          RB.ActualDiameter AS BottomRollDiameter, 
          RTB.RollTypeName AS BottomRollType, 
          RGH.GrooveNumber, 
          RGH.AccWeight, 
          RGH.AccWeightWithCoeff, 
          RGH.AccBilletCnt, 
          RGH.EnumRollGrooveStatus AS GrooveStatus, 
          RGH.CreatedTs AS GrooveCreated, 
          RGH.Remarks, 
          GT.GrooveTemplateId, 
          GT.GrooveTemplateName, 
          GT.EnumGrooveSetting
   FROM dbo.RLSRollSetHistory AS RSH
        INNER JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
        INNER JOIN dbo.RLSCassettes AS C ON RSH.FKCassetteId = C.CassetteId
        INNER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId
        INNER JOIN dbo.RLSRolls AS RU ON RS.FKUpperRollId = RU.RollId
        INNER JOIN dbo.RLSRollTypes AS RTU ON RU.FKRollTypeId = RTU.RollTypeId
        INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
                                                       AND RU.RollId = RGH.FKRollId
        INNER JOIN dbo.RLSGrooveTemplates GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
        LEFT JOIN dbo.RLSRolls AS RB
        INNER JOIN dbo.RLSRollTypes AS RTB ON RB.FKRollTypeId = RTB.RollTypeId ON RS.FKBottomRollId = RB.RollId
   WHERE(RSH.EnumRollSetHistoryStatus = 1);