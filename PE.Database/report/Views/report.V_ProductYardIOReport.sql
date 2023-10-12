CREATE   VIEW [report].[V_ProductYardIOReport]
AS WITH CTE_PM
        AS (SELECT FKProductId, 
                   StepNo, 
                   StepCreatedTs AS ProductMovementTs, 
                   FKAssetId, 
                   LAG(FKAssetId) OVER(PARTITION BY FkProductId
                   ORDER BY StepNo) AS PrevAssetId, 
                   LEAD(FKAssetId) OVER(PARTITION BY FkProductId
                   ORDER BY StepNo) AS NextAssetId
            FROM PRMProductSteps
            WHERE StepNo > 0)
        SELECT CAST(CASE
                        WHEN CTE_PM.FKAssetId = 900
                        THEN 1
                        ELSE 0
                    END AS BIT) AS IsOutgoing, 
               WO.WorkOrderId, 
               WO.WorkOrderName, 
               WO.ToBeCompletedBeforeTs, 
               WO.TargetOrderWeight, 
               CAST(CASE
                        WHEN ToBeCompletedBeforeTs <  = GETDATE()
                        THEN 1
                        ELSE 0
                    END AS BIT) AS IsOverrun, 
               S.SteelgradeId, 
               S.SteelgradeCode, 
               S.SteelgradeName, 
               H.HeatId, 
               H.HeatName, 
               H.HeatWeight, 
               P.ProductId, 
               P.ProductName, 
               P.ProductWeight, 
               PC.ProductCatalogueId, 
               CTE_PM.ProductMovementTs, 
               CAST(CTE_PM.ProductMovementTs AS DATE) AS ProductMovementDate, 
               GS.ShiftCode, 
               PA.AreaName AS PrevAreaName, 
               PA.AreaDescription AS PrevAreaDescription, 
               PA.AssetCode AS PrevAssetCode, 
               PA.AssetName AS PrevAssetName, 
               PA.AssetDescription AS PrevAssetDescription, 
               A.AreaName, 
               A.AreaDescription, 
               A.AssetCode, 
               A.AssetName, 
               A.AssetDescription, 
               NA.AreaName AS NextAreaName, 
               NA.AreaDescription AS NextAreaDescription, 
               NA.AssetCode AS NextAssetCode, 
               NA.AssetName AS NextAssetName, 
               NA.AssetDescription AS NextAssetDescription
        FROM PRMProducts AS P
             INNER JOIN PRMWorkOrders AS WO ON P.FKWorkOrderId = WO.WorkOrderId
             INNER JOIN PRMHeats AS H ON WO.FKHeatId = H.HeatId
             INNER JOIN CTE_PM ON P.ProductId = CTE_PM.FKProductId
                                  AND (CTE_PM.PrevAssetId IS NULL --Incomings (from Production)
                                       OR CTE_PM.FKAssetId = 900) --Outgoings (to Dispatched Products)
             INNER JOIN hmi.V_Assets AS A ON CTE_PM.FKAssetId = A.AssetId
             INNER JOIN PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
             CROSS APPLY dbo.FNTGetShiftId(CTE_PM.ProductMovementTs) AS GS
             LEFT JOIN hmi.V_Assets AS PA ON CTE_PM.PrevAssetId = PA.AssetId
             LEFT JOIN hmi.V_Assets AS NA ON CTE_PM.NextAssetId = NA.AssetId
             LEFT JOIN PRMSteelgrades AS S ON H.FKSteelgradeId = S.SteelgradeId;