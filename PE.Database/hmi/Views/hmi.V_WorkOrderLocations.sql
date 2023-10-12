CREATE   VIEW [hmi].[V_WorkOrderLocations]
AS WITH CTE
        AS (SELECT RML.RawMaterialLocationId, 
                   ROW_NUMBER() OVER(PARTITION BY A.AreaCode
                   ORDER BY A.AssetOrderSeq ASC, 
                            RML.OrderSeq ASC) SeqNewest, 
                   ROW_NUMBER() OVER(PARTITION BY A.AreaCode
                   ORDER BY A.AssetOrderSeq DESC, 
                            RML.OrderSeq DESC) SeqOldest, 
                   RML.OrderSeq, 
                   A.AreaCode, 
                   A.AreaName, 
                   A.AssetOrderSeq, 
                   WO.WorkOrderId, 
                   WO.WorkOrderName, 
                   H.HeatId, 
                   H.HeatName, 
                   S.SteelgradeId, 
                   S.SteelgradeName, 
                   RM.RawMaterialId
            FROM TRKRawMaterialLocations RML
                 INNER JOIN hmi.V_Assets A ON RML.FKAssetId = A.AssetId
                 INNER JOIN TRKRawMaterials RM
                 LEFT JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
                 LEFT JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
                 LEFT JOIN PRMHeats H ON WO.FKHeatId = H.HeatId
                 LEFT JOIN PRMSteelgrades S ON WO.FKSteelgradeId = S.SteelgradeId ON RML.FKRawMaterialId = RM.RawMaterialId)
        SELECT ISNULL(ROW_NUMBER() OVER(
               ORDER BY AssetOrderSeq), 0) AS OrderSeq, 
               AreaCode, 
               AreaName, 
               WorkOrderId, 
               WorkOrderName, 
               HeatId, 
               HeatName, 
               SteelgradeId, 
               SteelgradeName, 
               COUNT(RawMaterialId) AS RawMaterialNumber
        FROM CTE
        GROUP BY AreaCode, 
                 AreaName, 
                 WorkOrderId, 
                 WorkOrderName, 
                 HeatId, 
                 HeatName, 
                 SteelgradeId, 
                 SteelgradeName, 
                 AssetOrderSeq;