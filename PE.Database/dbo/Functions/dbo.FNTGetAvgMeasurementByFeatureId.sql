CREATE FUNCTION [dbo].[FNTGetAvgMeasurementByFeatureId]
(@RawMaterialId BIGINT, 
 @FeatureId     BIGINT
)
RETURNS TABLE
AS
     RETURN
     --DECLARE @RawMaterialId BIGINT = 82987
     --DECLARE @FeatureId       BIGINT = 304

     SELECT [ValueAvg] = AVG([ValueAvg]), 
            [ValueMin] = AVG([ValueMin]), 
            [ValueMax] = AVG([ValueMax]), 
            [CreatedTs] = MAX(CreatedTs)
     FROM dbo.MVHMeasurements AS MV
     WHERE FKRawMaterialId = @RawMaterialId
           AND FKFeatureId = @FeatureId
     GROUP BY FKRawMaterialId;