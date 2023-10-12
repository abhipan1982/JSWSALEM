CREATE FUNCTION [dbo].[FNGetKPIValue](@WorkOrderId BIGINT, 
                                     @KPICode     NVARCHAR(10))
RETURNS FLOAT
AS

/*
SELECT [dbo].[FNGetKPIValue] (176894,'WOY')
*/

     BEGIN
         DECLARE @KPIValue FLOAT;
         SELECT @KPIValue = KPIValue
         FROM PRFKPIValues KV
              INNER JOIN PRFKPIDefinitions KD ON KV.FKKPIDefinitionId = KD.KPIDefinitionId
                                                 AND KPICode = @KPICode
                                                 AND FKWorkOrderId = @WorkOrderId;
         IF @KPIValue IS NULL
             SET @KPIValue = 0;
         RETURN @KPIValue;
     END;