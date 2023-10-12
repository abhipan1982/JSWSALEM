CREATE   FUNCTION [dbo].[FNGetKPIValueWO](@WorkOrderId BIGINT, 
                                                 @KPICode     NVARCHAR(10)) RETURNS FLOAT AS

/*
select * from PRFKPIValues where fkworkorderid=176774
select * from PRFKPIDefinitions
SELECT [dbo].[FNGetKPIValueWO] (176774,'WOY')
*/

BEGIN
DECLARE @KPIValue FLOAT;
WITH LastKPITime
     AS (SELECT FKKPIDefinitionId, 
                FKWorkOrderId, 
                MAX(KPITime) AS LastKPITime
         FROM PRFKPIValues AS KV
              INNER JOIN PRFKPIDefinitions AS KD ON KV.FKKPIDefinitionId = KD.KPIDefinitionId
         WHERE KD.KPICode = @KPICode
               AND KV.FKWorkOrderId = @WorkOrderId
         GROUP BY FKKPIDefinitionId, 
                  FKWorkOrderId)
     SELECT @KPIValue = KPIValue
     FROM PRFKPIValues AS KV
          INNER JOIN LastKPITime AS LKT ON KV.FKKPIDefinitionId = LKT.FKKPIDefinitionId
                                           AND KV.FKWorkOrderId = LKT.FKWorkOrderId
                                           AND KV.KPITime = LKT.LastKPITime;
     IF @KPIValue IS NULL
         SET @KPIValue = 0;
     RETURN @KPIValue;
     END;