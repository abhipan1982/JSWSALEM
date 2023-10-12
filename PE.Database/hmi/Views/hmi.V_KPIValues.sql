CREATE   VIEW [hmi].[V_KPIValues]
AS

/*
select * from [hmi].[V_KPIValues]
*/

SELECT KV.KPIValueId, 
       KV.KPITime, 
       KD.KPIDefinitionId, 
       KV.KPIValue, 
       KV.FKWorkOrderId AS WorkOrderId, 
       KD.KPICode, 
       KD.KPIName, 
       KD.MinValue, 
       KD.AlarmTo, 
       KD.WarningTo, 
       KD.MaxValue, 
       KD.FKUnitId AS UnitId, 
       KD.EnumGaugeDirection, 
       UOM.UnitSymbol
FROM PRFKPIValues AS KV
     INNER JOIN PRFKPIDefinitions AS KD ON KV.FKKPIDefinitionId = KD.KPIDefinitionId
     INNER JOIN smf.UnitOfMeasure AS UOM ON KD.FKUnitId = UOM.UnitId;