CREATE   VIEW [report].[V_Rolls]
AS WITH ACC
        AS (SELECT FKRollId, 
                   FKRollSetHistoryId, 
                   SUM(AccWeight) AS AccWeight, 
                   SUM(AccBilletCnt) AS AccBilletCnt, 
                   MIN(ActDiameter) AS ActDiameter, 
                   COUNT(FKRollId) OVER(PARTITION BY FKRollSetHistoryId) AS RollsInRollSet
            FROM RLSRollGroovesHistory
            GROUP BY FKRollId, 
                     FKRollSetHistoryId),
        RSH
        AS (SELECT RSH.RollSetHistoryId, 
                   RSH.FKRollsetId, 
                   RSH.MountedTs, 
                   RSH.DismountedTs, 
                   C.CassetteName, 
                   ISNULL(C.EnumCassetteStatus, 0) AS EnumCassetteStatus, 
                   S.StandName, 
                   S.StandNo, 
                   S.StandZoneName, 
                   S.EnumStandStatus
            FROM RLSRollSetHistory RSH
                 LEFT JOIN RLSCassettes C ON RSH.FKCassetteId = C.CassetteId
                 LEFT JOIN RLSStands S ON C.FKStandId = S.StandId
            WHERE RSH.EnumRollSetHistoryStatus = 1)
        SELECT RT.RollTypeName, 
               R.RollId, 
               R.RollName, 
               R.EnumRollStatus, 
               dbo.FNGetEnumKeyword('RollStatus', R.EnumRollStatus) AS RollStatus, 
               R.InitialDiameter, 
               R.ActualDiameter, 
               RS.RollSetName, 
               ISNULL(RS.EnumRollSetStatus, 0) AS EnumRollSetStatus, 
               dbo.FNGetEnumKeyword('RollSetStatus', RS.EnumRollSetStatus) AS RollSetStatus, 
               RS.CreatedTs AS RollSetCreatedTs, 
               RSH.MountedTs AS RollSetMountedTs, 
               RSH.DismountedTs AS RollSetDismountedTs, 
               RSH.CassetteName, 
               ISNULL(RSH.EnumCassetteStatus, 0) AS EnumCassetteStatus, 
               dbo.FNGetEnumKeyword('CassetteStatus', RSH.EnumCassetteStatus) AS CassetteStatus, 
               ISNULL(RSH.StandName, '') StandName, 
               RSH.StandNo, 
               RSH.StandZoneName, 
               ISNULL(RSH.EnumStandStatus, 0) AS EnumStandStatus, 
               dbo.FNGetEnumKeyword('StandStatus', RSH.EnumStandStatus) AS StandStatusName, 
               ACC.RollsInRollSet, 
               ACC.AccBilletCnt, 
               ACC.AccWeight
        FROM RLSRolls R
             INNER JOIN RLSRollTypes RT ON R.FKRollTypeId = RT.RollTypeId
             LEFT JOIN RLSRollSets RS ON R.RollId = FKUpperRollId
                                         OR R.RollId = FKBottomRollId
             LEFT JOIN RSH ON RS.RollSetId = RSH.FKRollSetId
             LEFT JOIN ACC ON R.RollId = ACC.FKRollId
                              AND RSH.RollSetHistoryId = ACC.FKRollSetHistoryId;

/*
SELECT *
FROM V_Enums;
SELECT *
FROM RLSRolls;
SELECT *
FROM RLSRollSets;
SELECT *
FROM RLSRollSetHistory;
SELECT *
FROM RLSCassettes;
SELECT *
FROM RLSStands;
SELECT *
FROM RLSRollGroovesHistory;
SELECT FORMAT(100,'X')
select * from evtshiftcalendar
*/