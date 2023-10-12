CREATE   VIEW [hmi].[V_WorkOrdersOnYardLocations]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY AreaName, 
                   AssetName), 0) AS OrderSeq, 
          AreaId, 
          AreaName, 
          AreaDescription, 
          AssetId, 
          AssetName, 
          AssetDescription, 
          AssetTypeName, 
          WorkOrderId, 
          WorkOrderName, 
          HeatId, 
          HeatName, 
          SteelgradeId, 
          SteelgradeName, 
          HeatWeight, 
          SUM([ProductWeight]) AS WeightOnAsset, 
          COUNT(ProductId) AS ProductsOnAsset
   FROM
   (
       SELECT P.ProductId, 
              P.ProductName, 
              P.ProductWeight, 
              H.HeatId, 
              H.HeatName, 
              H.HeatWeight, 
              S.SteelgradeId, 
              S.SteelgradeCode, 
              S.SteelgradeName, 
              WO.WorkOrderId, 
              WO.WorkOrderName, 
              A.AssetOrderSeq, 
              A.AssetId, 
              A.AssetName, 
              A.AssetDescription, 
              A.AssetTypeName, 
              A.AreaId, 
              A.AreaName, 
              A.AreaDescription, 
              PS.PositionX, 
              PS.PositionY
       FROM dbo.PRMProducts AS P
            INNER JOIN dbo.PRMProductSteps AS PS ON P.ProductId = PS.FKProductId
                                                    AND PS.StepNo = 0
            INNER JOIN hmi.V_Assets AS A ON PS.FKAssetId = A.AssetId
            INNER JOIN dbo.PRMWorkOrders AS WO ON P.FKWorkOrderId = WO.WorkOrderId
            LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
            LEFT JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
   ) QRY
   GROUP BY AreaId, 
            AreaName, 
            AreaDescription, 
            AssetId, 
            AssetName, 
            AssetDescription, 
            AssetTypeName, 
            WorkOrderId, 
            WorkOrderName, 
            HeatId, 
            HeatName, 
            SteelgradeId, 
            SteelgradeName, 
            HeatWeight;