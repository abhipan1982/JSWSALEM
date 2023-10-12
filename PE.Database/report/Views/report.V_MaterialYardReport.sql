CREATE   VIEW [report].[V_MaterialYardReport]
AS SELECT ROW_NUMBER() OVER(PARTITION BY A.AssetCode
          ORDER BY MS.GroupNo DESC, 
                   MS.PositionX DESC, 
                   MS.PositionY DESC) AS OrderSeq, 
          CAST(CASE
                   WHEN RANK() OVER(PARTITION BY AssetId
                        ORDER BY PositionY DESC, 
                                 PositionX DESC) = 1
                   THEN 1
                   ELSE 0
               END AS BIT) AS IsFirstInQueue, 
          M.MaterialId, 
          M.MaterialName, 
          M.MaterialWeight, 
          A.AreaName, 
          A.AreaDescription, 
          A.AssetName, 
          A.AssetDescription, 
          MS.PositionX, 
          MS.PositionY, 
          H.HeatId, 
          H.HeatName, 
          H.HeatWeight, 
          GS.ShiftKey AS LastMovementShiftKey, 
          MS.StepCreatedTs AS LastMovementTs, 
          MC.MaterialCatalogueId, 
          MC.MaterialCatalogueName, 
          S.SteelgradeId, 
          S.SteelgradeCode, 
          S.SteelgradeName, 
          WO.WorkOrderId, 
          WO.WorkOrderName
   FROM PRMMaterials AS M
        INNER JOIN PRMMaterialSteps AS MS ON M.MaterialId = MS.FKMaterialId
                                             AND MS.StepNo = 0
        INNER JOIN hmi.V_Assets AS A ON MS.FKAssetId = A.AssetId
        --AND ZoneName = 'STR'
        INNER JOIN PRMHeats AS H ON M.FKHeatId = H.HeatId
        CROSS APPLY dbo.FNTGetShiftId(MS.StepCreatedTs) AS GS
        LEFT JOIN PRMMaterialCatalogue AS MC ON M.FKMaterialCatalogueId = MC.MaterialCatalogueId
        LEFT JOIN PRMSteelgrades AS S ON H.FKSteelgradeId = S.SteelgradeId
        LEFT JOIN PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId;

--SELECT * FROM V_Assets
--select * from V_Enums
--SELECT * FROM PRMMaterialSteps WHERE StepNo=0