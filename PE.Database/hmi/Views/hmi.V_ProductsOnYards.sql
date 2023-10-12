

CREATE     VIEW [hmi].[V_ProductsOnYards]
AS
     SELECT ROW_NUMBER() OVER(
            ORDER BY A.AssetOrderSeq) OrderSeq, 
            CAST(CASE
                     WHEN RANK() OVER(PARTITION BY AssetId
                          ORDER BY PositionY DESC, 
                                   PositionX DESC) = 1
                     THEN 1
                     ELSE 0
                 END AS BIT) IsFirstInQueue, 
            ProductId, 
            ProductName, 
            P.[ProductWeight], 
            P.IsAssigned, 
            H.HeatId, 
            H.HeatName, 
            H.HeatWeight, 
            SteelgradeId, 
            SteelgradeCode, 
            SteelgradeName, 
            WorkOrderId, 
            WorkOrderName, 
            AssetOrderSeq, 
            AssetId, 
            AssetName, 
            AreaId, 
            AreaName, 
            AreaDescription, 
            PositionX, 
            PositionY
     FROM PRMProducts P
          INNER JOIN PRMProductSteps PS ON P.ProductId = PS.FKProductId
                                           AND PS.StepNo = 0
          INNER JOIN V_Assets A ON PS.FKAssetId = A.AssetId
          LEFT JOIN PRMWorkOrders WO ON P.FKWorkOrderId = WO.WorkOrderId
          LEFT JOIN PRMHeats H ON WO.FKHeatId = H.HeatId
          LEFT JOIN PRMSteelgrades S ON WO.FKSteelgradeId = S.SteelgradeId;