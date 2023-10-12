CREATE   VIEW [hmi].[V_RollSetOverview] AS

/*

*/

WITH RSHMax
     AS (SELECT FKRollSetId, 
                MAX(RollSetHistoryId) AS MaxRSHId, 
                CAST(1 AS BIT) AS IsLastOne
         FROM dbo.RLSRollSetHistory
         GROUP BY FKRollSetId)
     SELECT RSH.RollSetHistoryId, 
            RSH.EnumRollSetHistoryStatus, 
            RSH.MountedTs, 
            RSH.DismountedTs, 
            RSH.PositionInCassette, 
            RS.RollSetId, 
            RS.RollSetName, 
            RS.RollSetDescription, 
            RS.EnumRollSetStatus, 
            RS.RollSetType, 
            RS.IsThirdRoll, 
            C.CassetteId, 
            C.CassetteName, 
            ISNULL(C.EnumCassetteStatus, 0) AS EnumCassetteStatus, 
            CT.CassetteTypeId, 
            CT.CassetteTypeName, 
            S.StandId, 
            S.StandNo, 
            S.StandName, 
            S.IsOnLine, 
            S.Arrangement, 
            ISNULL(S.EnumStandStatus, 0) AS EnumStandStatus, 
            RollsBottom.RollId AS BottomRollId, 
            RollsBottom.RollName AS BottomRollName, 
            RollsBottom.ActualDiameter AS BottomActualDiameter, 
            RollTypesBottom.RollTypeId AS BottomRollTypeId, 
            RollTypesBottom.RollTypeName AS BottomRollTypeName, 
            RollsUpper.RollId AS UpperRollId, 
            RollsUpper.RollName AS UpperRollName, 
            RollsUpper.ActualDiameter AS UpperActualDiameter, 
            RollTypesUpper.RollTypeId AS UpperRollTypeId, 
            RollTypesUpper.RollTypeName AS UpperRollTypeName, 
            RollsThird.RollId AS ThirdRollId, 
            RollsThird.RollName AS ThirdRollName, 
            RollsThird.ActualDiameter AS ThirdActualDiameter, 
            RollTypesThird.RollTypeId AS ThirdRollTypeId, 
            RollTypesThird.RollTypeName AS ThirdRollTypeName, 
            GAGI.GrooveNumber, 
            GAGI.GrooveTemplateName, 
            ISNULL(GAGI.EnumGrooveSetting, 0) AS EnumGrooveSetting, 
            dbo.FNGetRollSetGroovesSettings(RollSetHistoryId) AS RollSetGroovesSettings, 
            ISNULL(RSHMax.IsLastOne, 0) AS IsLastOne
     FROM RLSRollSetHistory AS RSH
          INNER JOIN RLSRollSets AS RS ON RSH.FKRollSetId = RS.RollSetId
          LEFT JOIN RSHMax ON(RSH.RollSetHistoryId = RSHMax.MaxRSHId
                              AND RS.RollSetId = RSHMax.FKRollSetId)
          LEFT JOIN RLSCassettes AS C
          INNER JOIN dbo.RLSCassetteTypes AS CT ON C.FKCassetteTypeId = CT.CassetteTypeId ON RSH.FKCassetteId = C.CassetteId
          LEFT JOIN RLSStands AS S ON S.StandId = C.FKStandId
          LEFT JOIN RLSRolls AS RollsBottom
          INNER JOIN RLSRollTypes AS RollTypesBottom ON RollsBottom.FKRollTypeId = RollTypesBottom.RollTypeId ON RS.FKBottomRollId = RollsBottom.RollId
          LEFT JOIN RLSRolls AS RollsUpper
          INNER JOIN RLSRollTypes AS RollTypesUpper ON RollsUpper.FKRollTypeId = RollTypesUpper.RollTypeId ON RS.FKUpperRollId = RollsUpper.RollId
          LEFT JOIN RLSRolls AS RollsThird
          INNER JOIN RLSRollTypes AS RollTypesThird ON RollsThird.FKRollTypeId = RollTypesThird.RollTypeId ON RS.FKThirdRollId = RollsThird.RollId
          OUTER APPLY dbo.FNTGetActualGrooveInformations(RollSetHistoryId) AS GAGI;