CREATE   VIEW [hmi].[V_WorkOrderSearchGrid]
AS

/*
select * from [hmi].[V_WorkOrderSearchGrid]
*/

SELECT WO.WorkOrderId, 
       WO.WorkOrderName, 
       WO.IsTestOrder, 
       WO.IsBlocked, 
       WO.EnumWorkOrderStatus, 
       WO.TargetOrderWeight, 
       WO.L3NumberOfBillets, 
       WO.WorkOrderCreatedTs, 
       WO.WorkOrderStartTs, 
       WO.WorkOrderEndTs, 
       WO.WorkOrderCreatedInL3Ts, 
       WO.ToBeCompletedBeforeTs, 
       PC.ProductCatalogueName, 
       MC.MaterialCatalogueName, 
       S.SteelgradeId, 
       S.SteelgradeCode, 
       S.SteelgradeName, 
       H.HeatId, 
       H.HeatName
FROM dbo.PRMWorkOrders AS WO
     INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
     INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
     LEFT JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
     LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId;