
CREATE     VIEW [hmi].[V_AS_Defects]
AS SELECT D.DefectId AS DimDefectId, 
          FORMAT(D.DefectCreatedTs, 'yyyy') AS DimYear, 
          FORMAT(D.DefectCreatedTs, 'yyyy-MM') AS DimMonth, 
          CONCAT(FORMAT(D.DefectCreatedTs, 'yyyy'), '-W', DATEPART(WEEK, D.DefectCreatedTs)) AS DimWeek, 
          FORMAT(D.DefectCreatedTs, 'yyyy-MM-dd') AS DimDate, 
          GS.ShiftCode AS DimShiftCode, 
          GS.ShiftKey AS DimShiftKey, 
          GS.CrewName AS DimCrewName, 
          DC.DefectCatalogueCode AS DimDefectCatalogueCode, 
          DC.DefectCatalogueName AS DimDefectCatalogueName, 
          DCC.DefectCatalogueCategoryCode AS DimDefectCatalogueCategoryCode, 
          DCC.DefectCatalogueCategoryName AS DimDefectCatalogueCategoryName, 
          RM.RawMaterialName AS DimRawMaterialName, 
          MC.MaterialCatalogueName AS DimMaterialCatalogueName, 
          PC.ProductCatalogueName AS DimProductCatalogueName, 
          CAST(ROUND(PC.Thickness * 1000, 2) AS VARCHAR) AS DimProductThickness, 
          WO.WorkOrderName AS DimWorkOrderName, 
          S.SteelgradeName AS DimSteelgradeName, 
          H.HeatName AS DimHeatName, 
          A.AssetName AS DimAssetName, 
          1 AS DefectsNumber
   FROM QTYDefects AS D
        INNER JOIN QTYDefectCatalogue AS DC ON D.FKDefectCatalogueId = DC.DefectCatalogueId
        INNER JOIN QTYDefectCatalogueCategory AS DCC ON DC.FKDefectCatalogueCategoryId = DCC.DefectCatalogueCategoryId
        LEFT JOIN TRKRawMaterials AS RM
        INNER JOIN PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId
        INNER JOIN PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
        INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
        INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
        INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
        INNER JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId ON D.FKRawMaterialId = RM.RawMaterialId
        LEFT JOIN MVHAssets A ON D.FKAssetId = A.AssetId
        CROSS APPLY dbo.FNTGetShiftId(D.DefectCreatedTs) AS GS;