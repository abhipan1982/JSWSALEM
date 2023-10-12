CREATE   VIEW [hmi].[V_WorkOrdersOnYards]
AS

/*
select * from [hmi].[V_WorkOrdersOnYards]
*/

WITH QRY_WorkOrders
     AS (SELECT WO.WorkOrderId, 
                WO.WorkOrderName, 
                WO.EnumWorkOrderStatus, 
                dbo.FNGetEnumKeyword('OrderStatus', WO.EnumWorkOrderStatus) AS WorkOrderStatus, 
                WO.ToBeCompletedBeforeTs, 
                CAST(CASE
                         WHEN ToBeCompletedBeforeTs < = GETDATE()
                         THEN 1
                         ELSE 0
                     END AS BIT) AS IsOverrun, 
                H.HeatId, 
                H.HeatName, 
                S.SteelgradeId, 
                S.SteelgradeName
         FROM dbo.PRMWorkOrders AS WO
              INNER JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
              INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId),
     QRY_Products
     AS (SELECT P.FKWorkOrderId, 
                SUM(P.ProductWeight) AS ProductsWeight, 
                COUNT(P.ProductId) AS ProductsNumber, 
                MAX(P.ProductCreatedTs) AS LastProductCreatedTs, 
                MAX(PS.StepCreatedTs) AS LastProductMovementTs
         FROM dbo.PRMProducts P
              INNER JOIN dbo.PRMProductSteps AS PS ON P.ProductId = PS.FKProductId
                                                      AND PS.StepNo = 0
         GROUP BY P.FKWorkOrderId),
     QRY_Materials
     AS (SELECT M.FKWorkOrderId, 
                SUM(M.MaterialWeight) AS MaterialsWeight, 
                COUNT(M.MaterialId) AS MaterialsNumber
         FROM dbo.PRMMaterials AS M
              INNER JOIN dbo.PRMMaterialSteps AS MS ON M.MaterialId = MS.FKMaterialId
                                                       AND MS.StepNo = 0
         GROUP BY M.FKWorkOrderId)
     SELECT WO.WorkOrderId, 
            WO.WorkOrderName, 
            WO.EnumWorkOrderStatus, 
            WO.WorkOrderStatus, 
            WO.ToBeCompletedBeforeTs, 
            WO.IsOverrun, 
            WO.HeatId, 
            WO.HeatName, 
            WO.SteelgradeId, 
            WO.SteelgradeName, 
            M.MaterialsNumber, 
            M.MaterialsWeight, 
            P.ProductsNumber, 
            P.ProductsWeight, 
            P.LastProductCreatedTs, 
            P.LastProductMovementTs
     FROM QRY_WorkOrders WO
          LEFT JOIN QRY_Materials M ON WO.WorkOrderId = M.FKWorkOrderId
          LEFT JOIN QRY_Products P ON WO.WorkOrderId = P.FKWorkOrderId
     WHERE ProductsNumber IS NOT NULL
           OR MaterialsNumber IS NOT NULL;