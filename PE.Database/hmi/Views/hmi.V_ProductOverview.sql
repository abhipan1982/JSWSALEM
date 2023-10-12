CREATE   VIEW [hmi].[V_ProductOverview]
AS

/*
select * from [hmi].[V_ProductOverview]
*/

WITH HeatChemicalAnalysis
     AS (SELECT FKHeatId, 
                COUNT(HeatChemAnalysisId) AS HeatChemicalAnalysisNumber, 
                MAX(HeatChemAnalysisId) AS HeatChemicalAnalysisId
         FROM PRMHeatChemicalAnalysis
         GROUP BY FKHeatId)
     SELECT P.ProductId, 
            P.IsDummy, 
            P.ProductCreatedTs, 
            P.ProductName, 
            P.IsAssigned, 
            P.ProductWeight, 
            WO.WorkOrderId, 
            WO.WorkOrderName, 
            PC.ProductCatalogueName, 
            PC.ExternalProductCatalogueName, 
            RM.RawMaterialName, 
            ISNULL(QI.EnumInspectionResult, 0) AS EnumInspectionResult,
            ISNULL(CAST(CASE
                     WHEN HeatChemicalAnalysisNumber > 0
                     THEN 1
                     ELSE 0
                 END AS BIT),0) AS HasHeatChemicalAnalysis
     FROM PRMProducts AS P
          LEFT JOIN PRMWorkOrders AS WO
          INNER JOIN PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
          LEFT JOIN HeatChemicalAnalysis AS HCA ON WO.FKHeatId = HCA.FKHeatId ON P.FKWorkOrderId = WO.WorkOrderId
          LEFT JOIN TRKRawMaterials AS RM ON P.ProductId = RM.FKProductId
          LEFT JOIN QTYQualityInspections AS QI ON RM.RawMaterialId = QI.FKRawMaterialId;