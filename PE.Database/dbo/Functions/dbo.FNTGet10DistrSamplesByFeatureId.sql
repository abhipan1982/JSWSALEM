CREATE FUNCTION [dbo].[FNTGet10DistrSamplesByFeatureId]
(@RawMaterialId BIGINT, 
 @FeatureId     BIGINT
)
RETURNS TABLE
AS
     RETURN
     --DECLARE @RawMaterialId BIGINT = 82944;
     --DECLARE @FeatureId       BIGINT = 197;

     WITH CTE
          AS (SELECT NTILE(10) OVER(
                     ORDER BY MS.SampleId) Ntiles, 
                     MS.SampleValue
              FROM MVHSamples MS
                   INNER JOIN MVHMeasurements MV ON MS.FKMeasurementId = MV.MeasurementId
              WHERE FKRawMaterialId = @RawMaterialId
                    AND FKFeatureId = @FeatureId)
          SELECT Ntiles, 
                 AVG(SampleValue) AvgValue, 
                 MIN(SampleValue) MinValue, 
                 MAX(SampleValue) MaxValue
          FROM CTE
          GROUP BY Ntiles;