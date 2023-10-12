CREATE   PROCEDURE [dbo].[SPRawMaterialsInArea] @RawMaterialId_list TRK_RM_ID_LIST READONLY
AS
     SELECT RML.OrderSeq, 
            A.AreaCode, 
            RM.RawMaterialId, 
            RM.RawMaterialName, 
            RM.LastWeight, 
            RM.LastLength, 
			WO.WorkOrderId,
            WO.WorkOrderName, 
			H.HeatId,
            H.HeatName, 
			S.SteelgradeId,
            S.SteelgradeCode
     FROM dbo.TRKRawMaterialLocations AS RML
          INNER JOIN dbo.TRKRawMaterials AS RM ON RML.FKRawMaterialId = RM.RawMaterialId
          INNER JOIN hmi.V_Assets AS A ON RML.FKAssetId = A.AssetId
          LEFT OUTER JOIN dbo.PRMMaterials AS M
          INNER JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
          INNER JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId ON RM.FKMaterialId = M.MaterialId
     WHERE RawMaterialId IN
     (
         SELECT RawMaterialId
         FROM @RawMaterialId_list
     );