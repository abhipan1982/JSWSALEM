CREATE   VIEW [dw].[FactWorkOrder]
AS

/*
	All work orders
	select * from prmworkorders
	select * from hmi.V_Enums
	SELECT * FROM dw.FactWorkOrder
	select * from PRFKPIDefinitions
*/

WITH Materials
     AS (SELECT FKWorkOrderId AS WorkOrderId, 
                COUNT(MaterialId) AS MaterialNumber, 
                SUM(MaterialWeight) AS MaterialWeight
         FROM dbo.PRMMaterials AS M
         GROUP BY FKWorkOrderId),
     Products
     AS (SELECT FKWorkOrderId AS WorkOrderId, 
                COUNT(ProductId) AS ProductNumber, 
                SUM(ProductWeight) AS ProductWeight, 
                SUM(CASE
                        WHEN QI.EnumInspectionResult = 2
                        THEN P.ProductWeight
                        ELSE 0
                    END) AS ProductWeightBad
         FROM dbo.PRMProducts AS P
              LEFT JOIN dbo.QTYQualityInspections AS QI ON P.ProductId = QI.FKProductId
         GROUP BY FKWorkOrderId),
     RawMaterials
     AS (SELECT M.FKWorkOrderId AS WorkOrderId, 
                COUNT(RawMaterialId) AS RawMaterialNumber, 
                SUM(LastWeight) AS RawMaterialWeight, 
                MIN(FurnaceExitTemperature) AS MinFurnaceExitTemperature, 
                MAX(FurnaceExitTemperature) AS MaxFurnaceExitTemperature, 
                AVG(FurnaceExitTemperature) AS AvgFurnaceExitTemperature, 
                0.1 AS MinSpeed, 
                0.1 AS MaxSpeed, 
                0.1 AS AvgSpeed, 
                0.1 AS MinRollGap, 
                0.1 AS MaxRollGap, 
                0.1 AS AvgRollGap, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 11
                        THEN RM.LastWeight
                        ELSE 0
                    END) AS RejectedWeight, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 11
                        THEN 1
                        ELSE 0
                    END) AS RejectedNumber, 
                SUM(CASE
                        WHEN RM.EnumTypeOfScrap = 2 --Full Scrap
                        THEN RM.LastWeight
                        WHEN RM.EnumTypeOfScrap = 1 --Partial Scrap
                        THEN RM.LastWeight * RM.ScrapPercent
                        ELSE 0
                    END) AS ScrappedWeight, 
                SUM(CASE
                        WHEN RM.EnumTypeOfScrap > 0
                        THEN 1
                        ELSE 0
                    END) AS ScrappedNumber
         FROM dbo.TRKRawMaterials AS RM
              INNER JOIN dbo.PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId
              INNER JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
         WHERE RM.CuttingSeqNo = 0
         GROUP BY M.FKWorkOrderId),
     Delays
     AS (SELECT FKWorkOrderId, 
                SUM(DATEDIFF_BIG(SECOND, E.EventStartTs, ISNULL(E.EventEndTs, GETDATE()))) AS DelayDuration
         FROM dbo.EVTEvents AS E
              INNER JOIN dbo.EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
         WHERE ET.EventTypeId = 10
               OR ET.FKParentEvenTypeId = 10
         GROUP BY FKWorkOrderId)
     --Main Query
     SELECT ISNULL(CONVERT(NVARCHAR(50), DB_NAME()), 0) AS SourceName, 
            GETDATE() AS SourceTime, 
            ISNULL(CONVERT(BIT, 0), 0) AS FactWorkOrderIsDeleted, 
            ISNULL(ROW_NUMBER() OVER(
            ORDER BY WO.WorkOrderId), 0) AS FactWorkOrderRow, 
            CAST(HASHBYTES('MD5', COALESCE(CAST(WO.WorkOrderId AS NVARCHAR), ';') + COALESCE(CAST(WO.FKSteelgradeId AS NVARCHAR), ';') + COALESCE(CAST(WO.EnumWorkOrderStatus AS NVARCHAR), ';') + COALESCE(CAST(WO.WorkOrderCreatedTs AS NVARCHAR), ';') + COALESCE(CAST(M.MaterialWeight AS NVARCHAR), ';') + COALESCE(CAST(P.ProductWeight AS NVARCHAR), ';')) AS VARBINARY(16)) AS FactWorkOrderHash, 
            WO.WorkOrderId AS FactWorkOrderKey, 
            WO.FKParentWorkOrderId AS FactWorkOrderKeyParent, 
            ISNULL(DATEPART(YEAR, WO.WorkOrderCreatedTs), 0) AS DimYearKey, 
            ISNULL(CONVERT(INT, CONVERT(VARCHAR(8), WO.WorkOrderCreatedTs, 112)), 0) AS DimDateKey, 
            FORMAT(WO.WorkOrderCreatedTs, 'yyyy') AS DimYear, 
            FORMAT(WO.WorkOrderCreatedTs, 'yyyy-MM') AS DimMonth, 
            CONCAT(FORMAT(WO.WorkOrderCreatedTs, 'yyyy'), '-W', DATEPART(WEEK, WO.WorkOrderCreatedTs)) AS DimWeek, 
            FORMAT(WO.WorkOrderCreatedTs, 'yyyy-MM-dd') AS DimDate, 
            GS.ShiftCode AS DimShiftCode, 
            GS.ShiftKey AS DimShiftKey, 
            GS.CrewName AS DimCrewName, 
            WO.FKMaterialCatalogueId AS DimMaterialCatalogueKey, 
            MC.MaterialCatalogueName AS DimMaterialCatalogueName, 
            WO.FKProductCatalogueId AS DimProductCatalogueKey, 
            PC.ProductCatalogueName AS DimProductCatalogueName, 
            CAST(ROUND(PC.Thickness * 1000, 2) AS VARCHAR) AS DimProductThickness, 
            WO.FKSteelgradeId AS DimSteelgradeKey, 
            S.SteelgradeCode AS DimSteelgradeCode, 
            S.SteelgradeName AS DimSteelgradeName, 
            S.FKSteelGroupId AS DimSteelGroupKey, 
            S.FKScrapGroupId AS DimScrapGroupKey, 
            WO.FKHeatId AS DimHeatKey, 
            H.HeatName AS DimHeatName, 
            WO.FKCustomerId AS DimCustomerKey, 
            WO.EnumWorkOrderStatus AS DimWorkOrderStatusKey, 
            WO.WorkOrderName, 
            WO.WorkOrderCreatedTs AS WorkOrderCreated, 
            WO.WorkOrderCreatedInL3Ts AS WorkOrderCreatedInL3, 
            WO.ToBeCompletedBeforeTs AS WorkOrderDueDate, 
            WO.WorkOrderStartTs AS WorkOrderStart, 
            WO.WorkOrderEndTs AS WorkOrderEnd, 
            WO.ExternalWorkOrderName AS WorkOrderExternalName, 
            ISNULL(DATEDIFF_BIG(SECOND, WO.WorkOrderStartTs, ISNULL(WO.WorkOrderEndTs, GETDATE())), 0) AS WorkOrderDuration, 
            CONCAT(CONVERT(NVARCHAR(10), ISNULL(FLOOR(CAST(ISNULL(WO.WorkOrderEndTs, GETDATE()) - WO.WorkOrderStartTs AS FLOAT)), 0)), CONVERT(NVARCHAR(10),
                                                                                                                                                          CASE
                                                                                                                                                              WHEN FLOOR(CAST(ISNULL(WO.WorkOrderEndTs, GETDATE()) - WO.WorkOrderStartTs AS FLOAT)) = 1
                                                                                                                                                              THEN ' day '
                                                                                                                                                              ELSE ' days '
                                                                                                                                                          END), CONVERT(NVARCHAR(10), ISNULL(WO.WorkOrderEndTs, GETDATE()) - WO.WorkOrderStartTs, 8)) AS WorkOrderDurationFT, 
            WO.IsTestOrder AS WorkOrderIsTest, 
            WO.TargetOrderWeight AS WorkOrderTargetWeight, 
            WO.TargetOrderWeightMin AS WorkOrderTargetMinWeight, 
            WO.TargetOrderWeightMax AS WorkOrderTargetMaxWeight, 
            M.MaterialNumber AS WorkOrderMaterialNumber, 
            M.MaterialWeight AS WorkOrderMaterialWeight, 
            ISNULL(P.ProductNumber, 0) AS WorkOrderProductNumber, 
            ISNULL(P.ProductWeight, 0) AS WorkOrderProductWeight, 
            ISNULL(P.ProductWeightBad, 0) AS WorkOrderProductWeightBad, 
            ISNULL(RM.RawMaterialNumber, 0) AS WorkOrderRawMaterialNumber, 
            ISNULL(RM.RawMaterialWeight, 0) AS WorkOrderRawMaterialWeight, 
            ISNULL(RM.RejectedWeight, 0) AS WorkOrderRejectedWeight, 
            ISNULL(RM.RejectedNumber, 0) AS WorkOrderRejectedNumber, 
            ISNULL(RM.ScrappedWeight, 0) AS WorkOrderScrappedWeight, 
            ISNULL(RM.ScrappedNumber, 0) AS WorkOrderScrappedNumber, 
            RM.MinFurnaceExitTemperature, 
            RM.MaxFurnaceExitTemperature, 
            RM.AvgFurnaceExitTemperature, 
            RM.MinSpeed, 
            RM.MaxSpeed, 
            RM.AvgSpeed, 
            RM.MinRollGap, 
            RM.MaxRollGap, 
            RM.AvgRollGap, 
            ISNULL(D.DelayDuration, 0) AS WorkOrderDelayDuration, 
            ISNULL(DATEDIFF_BIG(SECOND, WO.WorkOrderStartTs, ISNULL(WO.WorkOrderEndTs, GETDATE())), 0) - ISNULL(D.DelayDuration, 0) AS WorkOrderRollingDuration, 
            dbo.FNGetKPIValue(WO.WorkOrderId, 'MY') AS WorkOrderMetallicYield, 
            dbo.FNGetKPIValue(WO.WorkOrderId, 'QY') AS WorkOrderQualityYield, 
            dbo.FNGetKPIValue(WO.WorkOrderId, 'PPV') AS WorkOrderProductionPlanVariance, 
            dbo.FNGetKPIValue(WO.WorkOrderId, 'TTP') AS WorkOrderTestTimePercentageOfTotal, 
            dbo.FNGetEnumKeyword('WorkOrderStatus', WO.EnumWorkOrderStatus) AS WorkOrderStatus
     FROM dbo.PRMWorkOrders AS WO
          INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
          INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
          INNER JOIN Materials AS M ON WO.WorkOrderId = M.WorkOrderId
          LEFT JOIN Products AS P ON WO.WorkOrderId = P.WorkOrderId
          LEFT JOIN RawMaterials AS RM ON WO.WorkOrderId = RM.WorkOrderId
          LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
          LEFT JOIN Delays AS D ON WO.WorkOrderId = D.FKWorkOrderId
          OUTER APPLY dbo.FNTGetShiftId(WO.WorkOrderStartTs) AS GS;