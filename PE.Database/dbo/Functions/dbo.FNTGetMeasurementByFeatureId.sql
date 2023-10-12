CREATE FUNCTION [dbo].[FNTGetMeasurementByFeatureId](@RawMaterialId BIGINT, 
                                                    @FeatureId     BIGINT)
RETURNS TABLE
AS
     RETURN
     --DECLARE @RawMaterialId BIGINT = 5214
     --DECLARE @FeatureId       BIGINT = 5200154

     SELECT [ValueAvg] = AVG([ValueAvg]), 
            [ValueMin] = MIN([ValueMin]), 
            [ValueMax] = MAX([ValueMax]), 
            [CreatedTs] = MAX(CreatedTs)
     FROM dbo.MVHMeasurements AS MV
     WHERE FKRawMaterialId = @RawMaterialId
           AND FKFeatureId = @FeatureId
     GROUP BY FKRawMaterialId;