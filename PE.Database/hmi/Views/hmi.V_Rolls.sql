

CREATE       VIEW [hmi].[V_Rolls] AS
/*
*/

SELECT R.RollId, 
       R.RollName, 
       R.RollDescription, 
       R.EnumRollStatus, 
       R.InitialDiameter, 
       R.ActualDiameter, 
       R.MinimumDiameter, 
       R.GroovesNumber, 
       R.Supplier, 
       R.EnumRollScrapReason, 
       R.ScrapTime, 
       RT.RollTypeId, 
       RT.RollTypeName, 
       RT.RollTypeDescription, 
       RT.MatchingRollsetType, 
       RT.RollLength, 
       RT.DiameterMin, 
       RT.DiameterMax, 
       RT.RoughnessMin, 
       RT.RoughnessMax, 
       RT.YieldStrengthRef, 
       RT.RollSteelgrade, 
       COALESCE(RollSetUpper.RollSetId, RollSetBottom.RollSetId, RollSetThird.RollSetId) AS RollSetId, 
       COALESCE(RollSetUpper.RollSetName, RollSetBottom.RollSetName, RollSetThird.RollSetName) AS RollSetName,
       CASE
           WHEN RollSetUpper.RollSetId IS NOT NULL
           THEN 1
           ELSE 0
       END AS IsUpperRoll,
       CASE
           WHEN RollSetBottom.RollSetId IS NOT NULL
           THEN 1
           ELSE 0
       END AS IsBottomRoll,
       CASE
           WHEN RollSetThird.RollSetId IS NOT NULL
           THEN 1
           ELSE 0
       END AS IsThirdRoll
FROM dbo.RLSRolls AS R
     INNER JOIN dbo.RLSRollTypes AS RT ON R.FKRollTypeId = RT.RollTypeId
     LEFT OUTER JOIN dbo.RLSRollSets AS RollSetThird ON R.RollId = RollSetThird.FKThirdRollId
     LEFT OUTER JOIN dbo.RLSRollSets AS RollSetBottom ON R.RollId = RollSetBottom.FKBottomRollId
     LEFT OUTER JOIN dbo.RLSRollSets AS RollSetUpper ON R.RollId = RollSetUpper.FKUpperRollId;