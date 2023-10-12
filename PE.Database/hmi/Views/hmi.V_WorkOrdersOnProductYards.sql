
CREATE     VIEW [hmi].[V_WorkOrdersOnProductYards]
AS

/*
select * from [hmi].[V_WorkOrdersOnProductYards]
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
       H.HeatId, 
       H.HeatName, 
       H.HeatWeight, 
       S.SteelgradeId, 
       S.SteelgradeCode, 
       S.SteelgradeName, 
       CAST(CASE
                WHEN WO.ToBeCompletedBeforeTs < = GETDATE()
                THEN 1
                ELSE 0
            END AS BIT) AS IsOverrun, 
       SUM(ProductWeight) AS WeightOnArea, 
       COUNT(ProductId) AS ProductsOnArea, 
       MAX(ProductCreatedTs) AS LastProductCreatedTs, 
       MAX(StepCreatedTs) AS LastProductMovementTs
FROM dbo.PRMProducts AS P
     INNER JOIN dbo.PRMProductSteps AS PS ON P.ProductId = PS.FKProductId
                                             AND PS.StepNo = 0
     INNER JOIN hmi.V_Assets AS A ON PS.FKAssetId = A.AssetId
     INNER JOIN dbo.PRMWorkOrders AS WO ON P.FKWorkOrderId = WO.WorkOrderId
                                           AND WO.EnumWorkOrderStatus != 7
     LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
     LEFT JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
GROUP BY A.AreaId, 
       A.AreaName, 
       A.AreaDescription, 
       WO.WorkOrderId, 
       WO.WorkOrderName, 
       WO.ToBeCompletedBeforeTs, 
       WO.EnumWorkOrderStatus, 
       H.HeatId, 
       H.HeatName, 
       H.HeatWeight, 
       S.SteelgradeId, 
       S.SteelgradeCode, 
       S.SteelgradeName