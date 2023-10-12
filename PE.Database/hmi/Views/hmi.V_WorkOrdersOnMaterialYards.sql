

CREATE       VIEW [hmi].[V_WorkOrdersOnMaterialYards]
AS

/*
	select * from [hmi].[V_WorkOrdersOnMaterialYards]
*/

SELECT ISNULL(ROW_NUMBER() OVER(
       ORDER BY A.AreaName), 0) AS OrderSeq, 
       A.AreaId, 
       A.AreaName, 
       A.AreaDescription, 
       WO.WorkOrderId, 
       WO.WorkOrderName, 
       WO.ToBeCompletedBeforeTs, 
       WO.EnumWorkOrderStatus, 
       WO.HeatId, 
       WO.HeatName, 
       WO.HeatWeight, 
       WO.SteelgradeId, 
       WO.SteelgradeCode, 
       WO.SteelgradeName, 
       WO.MaterialsPlanned, 
       WO.MaterialsNumber, 
       WO.MaterialsCharged, 
       WO.IsScheduled, 
       WO.ScheduleId, 
       WO.ScheduleOrderSeq, 
       WO.MaterialCatalogueId, 
       WO.MaterialCatalogueName, 
       WO.MaterialsPlannedVSCharged, 
       SUM(M.MaterialWeight) AS WeightOnArea, 
       COUNT(M.MaterialId) AS MaterialsOnArea, 
       MAX(M.MaterialCreatedTs) AS LastMaterialCreatedTs, 
       MAX(MS.StepCreatedTs) AS LastMaterialMovementTs,
	   A.EnumYardType
FROM PRMMaterials M
     INNER JOIN PRMMaterialSteps MS ON M.MaterialId = MS.FKMaterialId
                                       AND MS.StepNo = 0
     INNER JOIN hmi.V_Assets A ON MS.FKAssetId = A.AssetId
     INNER JOIN hmi.V_WorkOrderSummary AS WO ON M.FKWorkOrderId = WO.WorkOrderId
GROUP BY A.AreaId, 
         A.AreaName, 
         A.AreaDescription, 
         WO.WorkOrderId, 
         WO.WorkOrderName, 
         WO.ToBeCompletedBeforeTs, 
         WO.EnumWorkOrderStatus, 
         WO.HeatId, 
         WO.HeatName, 
         WO.HeatWeight, 
         WO.SteelgradeId, 
         WO.SteelgradeCode, 
         WO.SteelgradeName, 
         WO.MaterialsPlanned, 
         WO.MaterialsNumber, 
         WO.MaterialsCharged, 
         WO.IsScheduled, 
         WO.ScheduleId, 
         WO.ScheduleOrderSeq, 
         WO.MaterialCatalogueId, 
         WO.MaterialCatalogueName, 
         WO.MaterialsPlannedVSCharged,
		 A.EnumYardType