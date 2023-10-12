CREATE   VIEW [report].[V_MaterialYardIOReport]
AS WITH CTE_MM
        AS (SELECT FkMaterialId, 
                   StepNo, 
                   StepCreatedTs AS MaterialMovementTs, 
                   FkAssetId, 
                   LAG(FKAssetId) OVER(PARTITION BY FkMaterialId
                   ORDER BY StepNo) AS PrevAssetId, 
                   LEAD(FKAssetId) OVER(PARTITION BY FkMaterialId
                   ORDER BY StepNo) AS NextAssetId
            FROM PRMMaterialSteps
            WHERE StepNo > 0)
        SELECT CAST(CASE
                        WHEN CTE_MM.NextAssetId = 1100
                        THEN 1
                        ELSE 0
                    END AS BIT) AS IsOutgoing, 
               S.SteelgradeId, 
               S.SteelgradeCode, 
               S.SteelgradeName, 
               H.HeatId, 
               H.HeatName, 
               H.HeatWeight, 
               M.MaterialId, 
               M.MaterialName, 
               M.MaterialWeight, 
               MC.MaterialCatalogueId, 
               CTE_MM.MaterialMovementTs, 
               CAST(CTE_MM.MaterialMovementTs AS DATE) AS MaterialMovementDate, 
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
               NA.AssetDescription AS NextAssetDescription, 
               WO.WorkOrderId, 
               WO.WorkOrderName
        FROM PRMMaterials AS M
             INNER JOIN PRMHeats AS H ON M.FKHeatId = H.HeatId
             INNER JOIN CTE_MM ON M.MaterialId = CTE_MM.FKMaterialId
                                  AND (CTE_MM.NextAssetId = 1100 --Outgoings (to Charging Grid)
                                       OR CTE_MM.PrevAssetId = 3017) --Incomings (from Reception)
             --AND CTE_MM.PrevAssetId = 3017 --Incomings (from Reception)
             INNER JOIN hmi.V_Assets AS A ON CTE_MM.FKAssetId = A.AssetId
             LEFT JOIN PRMMaterialCatalogue AS MC ON M.FKMaterialCatalogueId = MC.MaterialCatalogueId
             CROSS APPLY dbo.FNTGetShiftId(CTE_MM.MaterialMovementTs) AS GS
             LEFT JOIN hmi.V_Assets AS PA ON CTE_MM.PrevAssetId = PA.AssetId
             LEFT JOIN hmi.V_Assets AS NA ON CTE_MM.NextAssetId = NA.AssetId
             LEFT JOIN PRMSteelgrades AS S ON H.FKSteelgradeId = S.SteelgradeId
             LEFT JOIN PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId;