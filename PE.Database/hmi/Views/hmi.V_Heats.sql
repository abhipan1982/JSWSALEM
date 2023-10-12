CREATE   VIEW [hmi].[V_Heats]
AS SELECT H.HeatId, 
          H.HeatName, 
          H.FKHeatSupplierId AS HeatSupplierId, 
          H.HeatCreatedTs, 
          H.HeatWeight, 
          H.IsDummy, 
          S.SteelgradeCode, 
          S.SteelgradeName, 
          S.Density, 
          HS.HeatSupplierName, 
          MC.MaterialCatalogueName, 
          COUNT(MaterialId) AS MaterialsNumber
   FROM dbo.PRMHeats AS H
        LEFT OUTER JOIN dbo.PRMSteelgrades AS S ON H.FKSteelgradeId = S.SteelgradeId
        LEFT OUTER JOIN dbo.PRMHeatSuppliers AS HS ON H.FKHeatSupplierId = HS.HeatSupplierId
        LEFT OUTER JOIN dbo.PRMMaterials AS M
        INNER JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
        INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
        INNER JOIN dbo.PRMShapes AS SH ON MC.FKShapeId = SH.ShapeId ON H.HeatId = M.FKHeatId
   GROUP BY H.HeatId, 
            H.HeatName, 
            H.FKHeatSupplierId, 
            H.HeatCreatedTs, 
            H.HeatWeight, 
            H.IsDummy, 
            S.SteelgradeCode, 
            S.SteelgradeName, 
            S.Density, 
            HS.HeatSupplierName, 
            MC.MaterialCatalogueName;