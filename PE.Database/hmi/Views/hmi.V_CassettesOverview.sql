CREATE   VIEW [hmi].[V_CassettesOverview]
AS

/*
select * from [hmi].[V_CassettesOverview]
*/

WITH RollsetsInCassette
     AS (SELECT FKCassetteId, 
                dbo.FNGetRollSetGroovesSettings(RollSetHistoryId) AS RollSetGroovesSettings, 
                COUNT(FKRollSetId) AS RollsetsNumber, 
                MAX(FKRollSetId) AS RollSetId
         FROM dbo.RLSRollSetHistory
         WHERE(EnumRollSetHistoryStatus = 1)
         GROUP BY FKCassetteId, 
                  RollSetHistoryId)
     SELECT C.CassetteId, 
            C.CassetteName, 
            C.EnumCassetteStatus, 
            C.Arrangement, 
            C.NumberOfPositions, 
            CT.CassetteTypeId, 
            CT.CassetteTypeName, 
            CT.NumberOfRolls, 
            CT.IsInterCassette, 
            CT.EnumCassetteType, 
            RIC.RollsetsNumber, 
            RIC.RollSetId, 
            RIC.RollSetGroovesSettings, 
            S.StandName, 
            S.StandId, 
            S.StandNo
     FROM dbo.RLSCassettes AS C
          INNER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId
          LEFT OUTER JOIN RollsetsInCassette AS RIC ON C.CassetteId = RIC.FKCassetteId
          LEFT OUTER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId;