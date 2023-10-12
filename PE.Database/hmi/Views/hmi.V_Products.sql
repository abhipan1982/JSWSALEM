
CREATE     VIEW hmi.V_Products
AS WITH Defects
        AS (SELECT FKRawMaterialId AS RawMaterialId, 
                   COUNT(DefectId) AS DefectsNumber
            FROM QTYDefects AS D
            GROUP BY FKRawMaterialId)
        SELECT P.ProductId, 
               P.ProductCreatedTs, 
               P.IsDummy, 
               P.ProductName, 
                
               P.IsAssigned, 
               P.ProductWeight, 
               RM.RawMaterialId, 
			   WO.WorkOrderId,
               WO.WorkOrderName, 
               PC.ProductCatalogueName, 
               PC.Thickness AS ProductThickness, 
               S.SteelgradeCode, 
               S.SteelgradeName, 
               H.HeatName, 
               CAST(P.ProductCreatedTs AS DATE) AS ProductRollingDate, 
               ISNULL(CAST(CASE
                               WHEN D.DefectsNumber > 0
                               THEN 1
                               ELSE 0
                           END AS BIT), 0) AS HasDefect, 
               ISNULL(D.DefectsNumber, 0) AS DefectsNumber, 
               ISNULL(QI.EnumInspectionResult,0) AS EnumInspectionResult
        FROM dbo.PRMProducts P
             LEFT JOIN TRKRawMaterials RM ON P.ProductId = RM.FKProductId
             LEFT JOIN QTYQualityInspections QI ON RM.RawMaterialId = QI.FKRawMaterialId
             LEFT JOIN PRMWorkOrders WO
             INNER JOIN PRMHeats H ON WO.FKHeatId = H.HeatId
             INNER JOIN PRMSteelgrades S ON WO.FKSteelgradeId = S.SteelgradeId
             INNER JOIN PRMProductCatalogue PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId ON P.FKWorkOrderId = WO.WorkOrderId
             LEFT JOIN Defects AS D ON RM.RawMaterialId = D.RawMaterialId;