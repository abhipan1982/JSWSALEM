
CREATE   VIEW [hmi].[V_QualityBundlesInspectionSearchGrid]
AS

/*
select * from [hmi].[V_QualityBundlesInspectionSearchGrid]
select * from hmi.V_Enums
*/

WITH Products
     AS (SELECT RawMaterialId, 
                SUM(ProductWeight) AS ProductsWeight,
				MIN(FKWorkOrderId) AS WorkOrderId
         FROM dbo.PRMProducts AS P
              INNER JOIN dbo.TRKRawMaterials RM ON P.ProductId = RM.FKProductId
         GROUP BY RawMaterialId),
	Defects
     AS (SELECT FKRawMaterialId, 
                COUNT(DefectId) AS DefectsNumber
         FROM QTYDefects
         GROUP BY FKRawMaterialId)
     SELECT RM.RawMaterialId, 
            RM.RawMaterialName, 
            RM.RawMaterialCreatedTs, 
            RM.RawMaterialStartTs, 
            RM.RawMaterialEndTs, 
            RM.RollingStartTs, 
            RM.RollingEndTs, 
            RM.ProductCreatedTs, 
            RM.IsVirtual AS RawMaterialIsVirtual, 
            RM.EnumRawMaterialStatus, 
            H.HeatName, 
            SG.SteelgradeCode, 
            SG.SteelgradeName, 
            WO.WorkOrderName, 
            WO.WorkOrderCreatedTs, 
            WO.WorkOrderStartTs, 
            WO.WorkOrderEndTs, 
            ISNULL(DefectsNumber, 0) AS DefectsNumber, 
            ISNULL(QI.EnumInspectionResult, 0) AS EnumInspectionResult
     FROM dbo.TRKRawMaterials AS RM
          LEFT JOIN dbo.QTYQualityInspections AS QI ON RM.RawMaterialId = QI.FKRawMaterialId
          LEFT JOIN Defects AS D ON RM.RawMaterialId = D.FKRawMaterialId
		  LEFT JOIN Products AS P ON RM.RawMaterialId = P.RawMaterialId
		  LEFT JOIN dbo.PRMWorkOrders AS WO ON P.WorkOrderId = WO.WorkOrderId
		  LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
		  LEFT JOIN dbo.PRMSteelgrades AS SG ON H.FKSteelgradeId = SG.SteelgradeId
     WHERE 1 = 1
           AND RM.EnumRawMaterialType = 2 --Bundle