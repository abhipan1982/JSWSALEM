CREATE   VIEW [hmi].[V_MaterialSearchGrid]
AS

/*
select * from [hmi].[V_MaterialSearchGrid]
*/

SELECT M.MaterialId, 
       M.MaterialName, 
       M.SeqNo AS MaterialSeqNo, 
       M.IsAssigned AS MaterialIsAssigned, 
       M.MaterialWeight, 
       M.MaterialWidth, 
       M.MaterialThickness, 
       M.MaterialLength, 
       M.MaterialCreatedTs, 
       M.MaterialStartTs, 
       M.MaterialEndTs, 
       H.HeatId, 
       H.HeatName, 
       WO.WorkOrderId, 
       WO.WorkOrderName, 
       MC.MaterialCatalogueId, 
       MC.MaterialCatalogueName
FROM dbo.PRMMaterials AS M
     INNER JOIN dbo.PRMHeats AS H ON M.FKHeatId = H.HeatId
     LEFT JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
     LEFT JOIN dbo.PRMMaterialCatalogue AS MC ON M.FKMaterialCatalogueId = MC.MaterialCatalogueId;