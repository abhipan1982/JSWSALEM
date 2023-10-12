
CREATE     VIEW [report].[V_Consumption]
AS WITH MVH
        AS (SELECT MV.FirstMeasurementTs, 
                   FORMAT(MV.FirstMeasurementTs, 'yyyy') AS DimYear, 
                   FORMAT(MV.FirstMeasurementTs, 'yyyy-MM') AS DimMonth, 
                   CONCAT(FORMAT(MV.FirstMeasurementTs, 'yyyy'), '-W', DATEPART(WEEK, MV.FirstMeasurementTs)) AS DimWeek, 
                   FORMAT(MV.FirstMeasurementTs, 'yyyy-MM-dd') AS DimDate,
                   CASE
                       WHEN F.FeatureCode = 11
                            AND LAG([ValueAvg], 1, 0) OVER(PARTITION BY MV.FkFeatureId
                                ORDER BY MV.FirstMeasurementTs) != 0
                       THEN [ValueAvg] - LAG([ValueAvg], 1, 0) OVER(PARTITION BY MV.FkFeatureId
                            ORDER BY MV.FirstMeasurementTs)
                       ELSE 0
                   END GAS,
                   CASE
                       WHEN F.FeatureCode = 12
                            AND LAG([ValueAvg], 1, 0) OVER(PARTITION BY MV.FkFeatureId
                                ORDER BY MV.FirstMeasurementTs) != 0
                       THEN [ValueAvg] - LAG([ValueAvg], 1, 0) OVER(PARTITION BY MV.FkFeatureId
                            ORDER BY MV.FirstMeasurementTs)
                       ELSE 0
                   END WATER,
                   CASE
                       WHEN F.FeatureCode = 13
                            AND LAG([ValueAvg], 1, 0) OVER(PARTITION BY MV.FkFeatureId
                                ORDER BY MV.FirstMeasurementTs) != 0
                       THEN [ValueAvg] - LAG([ValueAvg], 1, 0) OVER(PARTITION BY MV.FkFeatureId
                            ORDER BY MV.FirstMeasurementTs)
                       ELSE 0
                   END ELECTRICITY
            FROM [dbo].[MVHMeasurements] MV
                 INNER JOIN [dbo].[MVHFeatures] F ON MV.FKFeatureId = F.FeatureId
            WHERE MV.IsValid = 1
                  AND F.FeatureCode IN(11, 12, 13))
        SELECT DimYear, 
               DimMonth, 
               DimWeek, 
               DimDate, 
               SUM(MVH.GAS) AS GAS, 
               SUM(MVH.WATER) AS WATER, 
               SUM(MVH.ELECTRICITY) AS ELECTRICITY
        FROM MVH
        GROUP BY DimYear, 
                 DimMonth, 
                 DimWeek, 
                 DimDate;