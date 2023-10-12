
CREATE   VIEW [hmi].[V_RawMaterialLocations]
AS
     SELECT RML.OrderSeq, 
            RML.IsVirtual, 
            A.AssetId, 
            A.AssetCode, 
            A.AssetName, 
            A.AssetDescription, 
            A.IsArea, 
            RM.RawMaterialId, 
            RM.RawMaterialName, 
            RM.LastWeight, 
            RM.LastLength, 
            M.MaterialId, 
            M.MaterialName, 
            WO.WorkOrderId, 
            WO.WorkOrderName, 
            H.HeatId, 
            H.HeatName, 
            S.SteelgradeId, 
            S.SteelgradeCode, 
            S.SteelgradeName
     FROM dbo.TRKRawMaterialLocations AS RML
          INNER JOIN dbo.MVHAssets AS A ON RML.FKAssetId = A.AssetId
          LEFT OUTER JOIN dbo.TRKRawMaterials AS RM  ON RML.FKRawMaterialId = RM.RawMaterialId
          LEFT OUTER JOIN dbo.PRMMaterials AS M
          INNER JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
          INNER JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId ON RM.FKMaterialId = M.MaterialId;