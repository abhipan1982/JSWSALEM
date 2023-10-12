CREATE   VIEW hmi.[V_GroovesView4Accumulation]
AS

/*
*/

SELECT RSH.FKRollSetId, 
       RSH.RollSetHistoryId, 
       RSH.MountedTs, 
	   RSH.DismountedTs,
       RSH.EnumRollSetHistoryStatus, 
       RSH.PositionInCassette, 
       RGH.RollGrooveHistoryId, 
       RGH.GrooveNumber, 
       RGH.FKGrooveTemplateId, 
       RGH.EnumRollGrooveStatus, 
       RGH.AccBilletCnt, 
       RGH.AccWeight, 
       RGH.ActDiameter, 
       C.FKCassetteTypeId, 
       C.CassetteName, 
       C.Arrangement, 
       S.StandNo, 
       S.EnumStandStatus, 
       S.StandId, 
       S.NumberOfRolls, 
       RS.EnumRollSetStatus, 
       RS.IsThirdRoll
FROM dbo.RLSRollSetHistory RSH
     INNER JOIN dbo.RLSRollGroovesHistory RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
     INNER JOIN dbo.RLSCassettes C ON RSH.FKCassetteId = C.CassetteId
     INNER JOIN dbo.RLSStands S ON C.FKStandId = S.StandId
     INNER JOIN dbo.RLSRollSets RS ON RSH.FKRollSetId = RS.RollSetId;