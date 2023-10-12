CREATE   VIEW [hmi].[V_MaterialLocations]
AS

/*
	select * from hmi.V_MaterialLocations
*/

SELECT RM.RawMaterialId, 
       M.MaterialId, 
       S.SteelgradeId, 
       H.HeatId, 
       WO.WorkOrderId, 
       A.AssetId, 
       A.AssetCode, 
       A.AreaCode, 
       RML.PositionSeq, 
       RML.OrderSeq, 
       RM.RawMaterialName, 
       M.MaterialName, 
       WO.WorkOrderName, 
       H.HeatName, 
       S.SteelgradeCode, 
       S.SteelgradeName
FROM [dbo].[TRKRawMaterialLocations] RML
     INNER JOIN [dbo].[TRKRawMaterials] RM ON RML.FKRawMaterialId = RM.RawMaterialId
     INNER JOIN [dbo].[PRMMaterials] M ON RM.FKMaterialId = M.MaterialId
     INNER JOIN [dbo].[PRMWorkOrders] WO ON M.FKWorkOrderId = WO.WorkOrderId
     INNER JOIN [dbo].[PRMHeats] H ON WO.FKHeatId = H.HeatId
     INNER JOIN [dbo].[PRMSteelgrades] S ON WO.FKSteelgradeId = S.SteelgradeId
     INNER JOIN [hmi].[V_Assets] A ON RML.FKAssetId = A.AssetId;