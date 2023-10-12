CREATE   FUNCTION dbo.FNTGetSamplesForFeatureName(@FeatureName   NVARCHAR(50), 
                                                         @RawMaterialId BIGINT) RETURNS TABLE

/*
select * from dbo.FNTGetSamplesForFeatureName('K_PR_REN.HMI_STAT.MSA',1060330)
*/

AS RETURN
   SELECT M.FKRawMaterialId AS RawMaterialId, 
          F.FeatureCode, 
          F.FeatureName, 
          S.SampleValue,
          CASE
              WHEN F.IsLengthRelated = 1
              THEN S.OffsetFromHead
              ELSE NULL
          END LengthOffsetFromHead,
          CASE
              WHEN F.IsLengthRelated = 0
              THEN DATEADD(MILLISECOND, 1000 * S.OffsetFromHead, M.FirstMeasurementTs)
              ELSE NULL
          END TimeOffsetFromHead
   FROM dbo.MVHSamples AS S
        INNER JOIN dbo.MVHMeasurements AS M ON S.FKMeasurementId = M.MeasurementId
        INNER JOIN dbo.MVHFeatures AS F ON M.FKFeatureId = F.FeatureId
   WHERE F.FeatureName = @FeatureName --'K_PR_REN.HMI_STAT.MSA'
         AND M.FKRawMaterialId = @RawMaterialId; --1060330;