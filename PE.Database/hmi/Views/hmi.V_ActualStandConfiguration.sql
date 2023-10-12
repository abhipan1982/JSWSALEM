
CREATE     VIEW [hmi].[V_ActualStandConfiguration]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY S.StandNo), 0) AS OrderSeq, 
          S.StandNo, 
          S.StandName, 
          S.EnumStandStatus, 
          S.StandId, 
          S.NumberOfRolls, 
          S.StandZoneName, 
          S.IsOnLine, 
          S.IsCalibrated, 
          S.Position, 
          C.CassetteName, 
          C.NumberOfPositions, 
          C.Arrangement, 
          C.FKCassetteTypeId AS CassetteTypeId, 
          C.CassetteId, 
          CO.RollsetsNumber, 
          CO.RollSetId, 
          CO.RollSetGroovesSettings
   FROM dbo.RLSCassettes AS C
        LEFT OUTER JOIN hmi.V_CassettesOverview AS CO ON C.CassetteId = CO.CassetteId
        RIGHT OUTER JOIN dbo.RLSStands AS S ON C.FKStandId = S.StandId;