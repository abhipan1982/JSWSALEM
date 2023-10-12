CREATE   VIEW [hmi].[V_CassettesInStands]
AS

/*
select * from [hmi].[V_CassettesInStands]
*/

SELECT ROW_NUMBER() OVER(
       ORDER BY S.StandNo) AS OrderSeq, 
       S.StandId, 
       S.StandNo, 
       ISNULL(S.EnumStandStatus, 0) AS EnumStandStatus, 
       S.StandZoneName, 
       S.IsOnLine, 
       S.Position, 
       C.CassetteId, 
       ISNULL(C.EnumCassetteStatus, 0) AS EnumCassetteStatus, 
       C.CassetteName, 
       C.Arrangement, 
       CT.CassetteTypeName, 
       ISNULL(CT.EnumCassetteType, 0) AS EnumCassetteType, 
       CT.CassetteTypeId, 
       RSH.RollSetHistoryId, 
       RSH.MountedTs, 
       RSH.DismountedTs, 
       RSH.MountedInMillTs, 
       RSH.DismountedFromMillTs, 
       ISNULL(RSH.EnumRollSetHistoryStatus, 0) AS EnumRollSetHistoryStatus
FROM dbo.RLSCassettes AS C
     INNER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId
     INNER JOIN dbo.RLSRollSetHistory AS RSH ON C.CassetteId = RSH.FKCassetteId
     RIGHT OUTER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId;