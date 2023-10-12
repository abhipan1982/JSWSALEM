CREATE   VIEW [hmi].[V_QualityInspectionSearchGrid]
AS

/*
select * from [hmi].[V_QualityInspectionSearchGrid]
select * from hmi.V_Enums
*/

WITH Defects
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
            M.MaterialId, 
            M.MaterialName, 
            M.MaterialCreatedTs, 
            M.MaterialStartTs, 
            M.MaterialEndTs, 
            H.HeatName, 
            S.SteelgradeCode, 
            S.SteelgradeName, 
            WO.WorkOrderName, 
            WO.WorkOrderCreatedTs, 
            WO.WorkOrderStartTs, 
            WO.WorkOrderEndTs, 
            ISNULL(DefectsNumber, 0) AS DefectsNumber, 
            ISNULL(QI.EnumInspectionResult, 0) AS EnumInspectionResult
     FROM dbo.TRKRawMaterials AS RM
          INNER JOIN dbo.PRMMaterials AS M
          INNER JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
          INNER JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId ON RM.FKMaterialId = M.MaterialId
          LEFT JOIN dbo.QTYQualityInspections AS QI ON RM.RawMaterialId = QI.FKRawMaterialId
          LEFT JOIN Defects AS D ON RM.RawMaterialId = D.FKRawMaterialId
     WHERE 1 = 1
           AND RM.EnumRawMaterialType = 0 --Material
           AND RM.EnumCuttingTip IN(0, 1) --None or Parent
          AND RM.EnumRawMaterialStatus >= 40; --Discharged