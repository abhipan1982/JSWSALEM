CREATE   FUNCTION dbo.FNTGetAliasValue(@AliasName NVARCHAR(50), 
                                              @Param1    NVARCHAR(50) = 0, 
                                              @GetSample BIT          = 0) RETURNS @TableOfValues TABLE
(ResultValueText      NVARCHAR(255), 
 ResultValueBoolean   BIT, 
 ResultValueTimestamp DATETIME, 
 ResultValueNumber    FLOAT, 
 ResultValueSample    FLOAT, 
 LengthOffsetFromHead FLOAT, 
 TimeOffsetFromHead   DATETIME
)

/*
select * from dbo.FNTGetAliasValue('K_PR_REN.HMI_STAT.MSA',1060330,0)
*/

AS BEGIN
   IF @GetSample = 0
       BEGIN
           INSERT INTO @TableOfValues(ResultValueNumber)
                  SELECT ValueAvg
                  FROM dbo.MVHMeasurements AS M
                       INNER JOIN dbo.MVHFeatures AS F ON M.FKFeatureId = F.FeatureId
                  WHERE F.FeatureName = @AliasName --'K_PR_REN.HMI_STAT.MSA'
                        AND M.FKRawMaterialId = @Param1; --1060330
       END;
       ELSE
       IF @GetSample = 1
           BEGIN
               INSERT INTO @TableOfValues
               (ResultValueSample, 
                LengthOffsetFromHead, 
                TimeOffsetFromHead
               )
                      SELECT S.SampleValue,
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
                      WHERE F.FeatureName = @AliasName --'K_PR_REN.HMI_STAT.MSA'
                            AND M.FKRawMaterialId = @Param1; --1060330
           END;
                      RETURN;
               END;