CREATE   PROCEDURE dbo.SPRawMaterialsInFurnace @RawMaterialId_list TRK_RM_ID_LIST READONLY
AS
     SELECT RMF.OrderSeq, 
            RM.RawMaterialId, 
            RM.RawMaterialName, 
            RMF.Temperature, 
            WO.WorkOrderId, 
            WO.WorkOrderName, 
            H.HeatName, 
            S.SteelgradeCode
     FROM dbo.TRKRawMaterialsInFurnace RMF
          INNER JOIN dbo.TRKRawMaterials AS RM ON RMF.FKRawMaterialId = RM.RawMaterialId
          LEFT OUTER JOIN dbo.PRMMaterials AS M
          INNER JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
          INNER JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId ON RM.FKMaterialId = M.MaterialId
     WHERE RawMaterialId IN
     (
         SELECT RawMaterialId
         FROM @RawMaterialId_list
     );