CREATE   VIEW [hmi].[V_ProductSearchGrid]
AS

/*
select * from [hmi].[V_ProductSearchGrid]
select count(1) from PRMProducts
*/

WITH Defects
     AS (SELECT FKRawMaterialId, 
                COUNT(DefectId) AS DefectsNumber
         FROM QTYDefects
         GROUP BY FKRawMaterialId)
     SELECT P.ProductName, 
            P.ProductId, 
            P.ProductCreatedTs, 
            CAST(P.ProductCreatedTs AS DATE) AS ProductRollingDate, 
            P.IsAssigned, 
            P.ProductWeight, 
            WO.WorkOrderId, 
            WO.WorkOrderName, 
            PC.ProductCatalogueName, 
            S.SteelgradeCode, 
            S.SteelgradeName, 
            H.HeatName, 
            RM.RawMaterialId, 
            RM.RawMaterialName, 
            RM.EnumRawMaterialType, 
            ISNULL(QI.EnumInspectionResult, 0) AS EnumInspectionResult, 
            ISNULL(D.DefectsNumber, 0) AS DefectsNumber
     FROM dbo.PRMProducts AS P
          LEFT JOIN dbo.PRMWorkOrders AS WO
          INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
          LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId ON P.FKWorkOrderId = WO.WorkOrderId
          LEFT JOIN dbo.TRKRawMaterials AS RM ON P.ProductId = RM.FKProductId
          LEFT JOIN dbo.QTYQualityInspections AS QI ON RM.RawMaterialId = QI.FKRawMaterialId
          LEFT JOIN Defects AS D ON RM.RawMaterialId = D.FKRawMaterialId;