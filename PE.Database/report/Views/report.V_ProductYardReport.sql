CREATE   VIEW [report].[V_ProductYardReport]
AS SELECT ROW_NUMBER() OVER(PARTITION BY AssetId
          ORDER BY PS.GroupNo DESC, 
                   PS.PositionX DESC, 
                   PS.PositionY DESC) AS OrderSeq, 
          CAST(CASE
                   WHEN RANK() OVER(PARTITION BY AssetId
                        ORDER BY PositionY DESC, 
                                 PositionX DESC) = 1
                   THEN 1
                   ELSE 0
               END AS BIT) AS IsFirstInQueue, 
          P.ProductId, 
          P.ProductName, 
          P.ProductWeight, 
          A.AreaName, 
          A.AreaDescription, 
          A.AssetId, 
          A.AssetName, 
          A.AssetDescription, 
          PS.PositionX, 
          PS.PositionY, 
          H.HeatId, 
          H.HeatName, 
          H.HeatWeight, 
          PS.StepCreatedTs AS LastMovementTs, 
          PC.ProductCatalogueId, 
          PC.ProductCatalogueName, 
          S.SteelgradeId, 
          S.SteelgradeCode, 
          S.SteelgradeName, 
          WO.WorkOrderId, 
          WO.WorkOrderName, 
          WO.TargetOrderWeight
   FROM PRMProducts P
        INNER JOIN PRMProductSteps PS ON P.ProductId = PS.FKProductId
                                         AND PS.StepNo = 0
        INNER JOIN hmi.V_Assets A ON PS.FKAssetId = A.AssetId
        --AND ZoneName = 'STR'
        INNER JOIN PRMWorkOrders WO
        INNER JOIN PRMProductCatalogue PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
        LEFT JOIN PRMHeats H ON WO.FKHeatId = H.HeatId
        LEFT JOIN PRMSteelgrades S ON WO.FKSteelgradeId = S.SteelgradeId ON P.FKWorkOrderId = WO.WorkOrderId;