
CREATE     VIEW [hmi].[V_RawMaterialInFurnace]
AS

/*
select * from TRKRawMaterialsInFurnace
select * from [hmi].[V_RawMaterialInFurnace]
select * from hmi.V_Assets
*/

SELECT ROW_NUMBER() OVER(
       ORDER BY RMF.OrderSeq) AS OrderSeq, 
       RMF.RawMaterialInFurnaceId, 
       RMF.ChargingTs, 
       RMF.Temperature, 
       RMF.TimeInFurnace, 
       RM.RawMaterialId, 
       RM.RawMaterialName, 
       ISNULL(RM.EnumRawMaterialStatus, 0) AS EnumRawMaterialStatus, 
       M.MaterialId, 
       M.MaterialName, 
       MC.MaterialCatalogueName, 
       WO.WorkOrderId, 
       WO.WorkOrderName, 
       H.HeatId, 
       H.HeatName, 
       S.SteelgradeId, 
       S.SteelgradeCode, 
       S.SteelgradeName, 
       PC.Thickness, 
       RM.LastWeight, 
       RM.LastLength, 
       RML.RawMaterialLocationId
FROM dbo.TRKRawMaterialsInFurnace AS RMF
     INNER JOIN
(
    SELECT OrderSeq, 
           MAX(ChargingTs) AS ChargingTs
    FROM dbo.TRKRawMaterialsInFurnace
    GROUP BY OrderSeq
) RMFMax ON(RMF.OrderSeq = RMFMax.OrderSeq
            AND RMF.ChargingTs = RMFMax.ChargingTs)
     LEFT JOIN dbo.TRKRawMaterials AS RM ON RMF.FKRawMaterialId = RM.RawMaterialId
     LEFT JOIN dbo.PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId
     LEFT JOIN dbo.PRMMaterialCatalogue AS MC ON M.FKMaterialCatalogueId = MC.MaterialCatalogueId
     LEFT JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
     LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
     LEFT JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
     LEFT JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
     LEFT JOIN dbo.TRKRawMaterialLocations AS RML ON(RMF.FKRawMaterialId = RML.FKRawMaterialId
                                                     AND RMF.OrderSeq = RML.OrderSeq
                                                     AND RML.IsVirtual = 0
                                                     AND AssetCode = 3200000); --AssetCode of Furnace