CREATE   VIEW [hmi].[V_ActualRollsOnStandsDiameter]
AS

/*

*/

SELECT ISNULL(ROW_NUMBER() OVER(
       ORDER BY S.Position), 0) AS OrderSeq, 
       S.StandName, 
       S.FKAssetId AS AssetId, 
       S.IsOnLine, 
       ISNULL(C.EnumCassetteStatus, 0) AS EnumCassetteStatus, 
       C.CassetteName, 
       ISNULL(RSH.EnumRollSetHistoryStatus, 0) AS EnumRollSetHistoryStatus, 
       ISNULL(RS.EnumRollSetStatus, 0) AS EnumRollSetStatus, 
       RS.RollSetName, 
       ISNULL(R.EnumRollStatus, 0) AS EnumRollStatus, 
       R.RollName, 
       R.ActualDiameter
FROM RLSStands AS S
     LEFT JOIN dbo.RLSCassettes AS C ON S.StandId = C.FKStandId
     LEFT JOIN dbo.RLSRollSetHistory AS RSH ON RSH.FKCassetteId = C.CassetteId
                                               AND RSH.EnumRollSetHistoryStatus = 1
     LEFT JOIN dbo.RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
     LEFT JOIN dbo.RLSRolls AS R ON RS.FKUpperRollId = R.RollId;