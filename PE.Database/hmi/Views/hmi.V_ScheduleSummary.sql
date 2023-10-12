CREATE   VIEW [hmi].[V_ScheduleSummary]
AS

/*
select * from TRKRawMaterials
select * from PRMWorkOrders
select * from hmi.V_ScheduleSummary
select * from hmi.V_Enums where EnumName='RawMaterialStatus'
*/

WITH Materials
     AS (SELECT FKWorkOrderId, 
                SUM(MaterialWeight) AS MaterialsWeight, 
                COUNT(MaterialId) AS MaterialsNumber
         FROM dbo.PRMMaterials
         GROUP BY FKWorkOrderId),
     RawMaterials
     AS (SELECT M.FKWorkOrderId, 
                COUNT(RM.RawMaterialId) AS RawMaterialsNumber, 
                SUM(CASE
                        WHEN EnumCuttingTip IN(0, 1) --Parent
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsParents, 
                SUM(CASE
                        WHEN EnumCuttingTip IN(2, 3) --Child or CuttedChild
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsKids, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 0
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsInvalid, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 10
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsUnassigned, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 20
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsAssigned, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 30
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsCharged, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 40
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsDischarged, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 50
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsInStorage, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 60
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsInMill, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 70
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsInFinalProduction, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 80
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsOnCoolingBed, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 90
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsRolled, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 100
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsInTransport, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 110
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsFinished, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 120
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsScrapped, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 130
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsRejected, 
                SUM(CASE
                        WHEN RM.EnumRawMaterialStatus = 140
                        THEN 1
                        ELSE 0
                    END) AS RawMaterialsDivided, 
                MIN(RM.EnumRawMaterialStatus) AS LowestRawMaterialStatus, 
                MAX(RM.EnumRawMaterialStatus) AS HighestRawMaterialStatus
         FROM dbo.TRKRawMaterials AS RM
              INNER JOIN dbo.PRMMaterials AS M ON M.MaterialId = RM.FKMaterialId
         GROUP BY M.FKWorkOrderId)

     -- MAIN QUERY
     SELECT WO.WorkOrderId, 
            WO.WorkOrderName, 
            WO.EnumWorkOrderStatus, 
            WO.IsTestOrder, 
            WO.WorkOrderStartTs, 
            WO.WorkOrderEndTs, 
            MC.MaterialCatalogueId, 
            MC.MaterialCatalogueName, 
            PC.ProductCatalogueId, 
            PC.ProductCatalogueName, 
            PCT.ProductCatalogueTypeCode, 
            S.SteelgradeId, 
            S.SteelgradeCode, 
            S.SteelgradeName, 
            H.HeatId, 
            H.HeatName, 
            SP.ScheduleId, 
            SP.OrderSeq AS ScheduleOrderSeq, 
            SP.PlannedDuration, 
            ISNULL(WO.L3NumberOfBillets, 0) AS MaterialsPlanned, 
            ISNULL(M.MaterialsNumber, 0) AS MaterialsNumber, 
            ISNULL(RM.RawMaterialsNumber, 0) AS RawMaterialsNumber, 
            ISNULL(RM.RawMaterialsParents, 0) AS RawMaterialsParents, 
            ISNULL(RM.RawMaterialsKids, 0) AS RawMaterialsKids, 
            ISNULL(RM.RawMaterialsInvalid, 0) AS RawMaterialsInvalid, 
            ISNULL(RM.RawMaterialsUnassigned, 0) AS RawMaterialsUnassigned, 
            ISNULL(RM.RawMaterialsAssigned, 0) AS RawMaterialsAssigned, 
            ISNULL(RM.RawMaterialsCharged, 0) AS RawMaterialsCharged, 
            ISNULL(RM.RawMaterialsDischarged, 0) AS RawMaterialsDischarged, 
            ISNULL(RM.RawMaterialsInStorage, 0) AS RawMaterialsInStorage, 
            ISNULL(RM.RawMaterialsInMill, 0) AS RawMaterialsInMill, 
            ISNULL(RM.RawMaterialsInFinalProduction, 0) AS RawMaterialsInFinalProduction, 
            ISNULL(RM.RawMaterialsOnCoolingBed, 0) AS RawMaterialsOnCoolingBed, 
            ISNULL(RM.RawMaterialsRolled, 0) AS RawMaterialsRolled, 
            ISNULL(RM.RawMaterialsInTransport, 0) AS RawMaterialsInTransport, 
            ISNULL(RM.RawMaterialsFinished, 0) AS RawMaterialsFinished, 
            ISNULL(RM.RawMaterialsScrapped, 0) AS RawMaterialsScrapped, 
            ISNULL(RM.RawMaterialsRejected, 0) AS RawMaterialsRejected, 
            ISNULL(RM.RawMaterialsDivided, 0) AS RawMaterialsDivided, 
            RM.LowestRawMaterialStatus, 
            RM.HighestRawMaterialStatus
     FROM dbo.PRMWorkOrders AS WO
          INNER JOIN dbo.PPLSchedules SP ON WO.WorkOrderId = SP.FKWorkOrderId
          INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
          INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
          INNER JOIN dbo.PRMProductCatalogueTypes AS PCT ON PC.FKProductCatalogueTypeId = PCT.ProductCatalogueTypeId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
          LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
          LEFT JOIN Materials AS M ON WO.WorkOrderId = M.FKWorkOrderId
          LEFT JOIN RawMaterials AS RM ON WO.WorkOrderId = RM.FKWorkOrderId;