CREATE   VIEW [hmi].[V_RawMaterialLabels]
AS SELECT RM.RawMaterialId, 
          WO.WorkOrderId, 
          WO.WorkOrderName, 
          WO.EnumWorkOrderStatus, 
          S.SteelgradeName, 
          S.SteelgradeCode, 
          H.HeatName, 
          PC.ProductCatalogueName, 
          PC.Thickness, 
          M.MaterialName, 
          M.SeqNo AS MaterialSeqNo
   FROM TRKRawMaterials AS RM
        INNER JOIN PRMMaterials AS M
        INNER JOIN PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
        INNER JOIN PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
        INNER JOIN PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
        INNER JOIN PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
        INNER JOIN PRMHeats AS H ON M.FKHeatId = H.HeatId ON RM.FKMaterialId = M.MaterialId;