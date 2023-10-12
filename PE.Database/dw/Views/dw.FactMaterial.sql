CREATE   VIEW [dw].[FactMaterial]
AS

/*
	SELECT * FROM dw.FactMaterial
	select * from prmMaterials
	SELECT * FROM hmi.V_Enums
*/

WITH Defects
     AS (SELECT FKRawMaterialId AS RawMaterialId, 
                MAX(FKDefectCatalogueId) AS LastDefectCatalogueId, 
                COUNT(DefectId) AS DefectsNumber
         FROM dbo.QTYDefects AS D
         GROUP BY FKRawMaterialId),
     Delays
     AS (SELECT FKRawMaterialId AS RawMaterialId, 
                SUM(DATEDIFF_BIG(SECOND, E.EventStartTs, ISNULL(E.EventEndTs, GETDATE()))) AS DelayDuration
         FROM dbo.EVTEvents AS E
              INNER JOIN dbo.EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
         WHERE ET.EventTypeId = 10
               OR ET.FKParentEvenTypeId = 10
         GROUP BY FKRawMaterialId)
     SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
            GETDATE() AS SourceTime, 
            ISNULL(CAST(0 AS BIT), 0) AS FactMaterialIsDeleted, 
            ISNULL(ROW_NUMBER() OVER(
            ORDER BY WO.WorkOrderId, 
                     M.SeqNo, 
                     RM.CuttingSeqNo), 0) AS FactMaterialRow, 
            CAST(HASHBYTES('MD5', COALESCE(CAST(RM.RawMaterialId AS NVARCHAR), ';') + COALESCE(CAST(M.MaterialId AS NVARCHAR), ';') + COALESCE(CAST(M.FKWorkOrderId AS NVARCHAR), ';') + COALESCE(CAST(RM.EnumRawMaterialStatus AS NVARCHAR), ';')) AS VARBINARY(16)) AS FactMaterialHash, 
            M.MaterialId AS FactMaterialKey, 
            RM.RawMaterialId AS FactRawMaterialKey, 
            ISNULL(DATEPART(YEAR, M.MaterialCreatedTs), 0) AS DimYearKey, 
            ISNULL(CONVERT(INT, CONVERT(VARCHAR(8), M.MaterialCreatedTs, 112)), 0) AS DimDateKey, 
            RM.FKShiftCalendarId AS DimShiftKey, 
            SC.FKShiftDefinitionId AS DimShiftDefinitionKey, 
            SC.FKCrewId AS DimCrewKey, 
            M.MaterialId AS DimMaterialKey, 
            RM.RawMaterialId AS DimRawMaterialKey, 
            M.FKWorkOrderId AS DimWorkOrderKey, 
            M.FKHeatId AS DimHeatKey, 
            S.SteelgradeId AS DimSteelgradeKey, 
            S.FKSteelGroupId AS DimSteelGroupKey, 
            RM.EnumRawMaterialStatus AS DimMaterialStatusKey, 
            WO.FKCustomerId AS DimCustomerKey, 
            WO.FKMaterialCatalogueId AS DimMaterialCatalogueKey, 
            WO.FKProductCatalogueId AS DimProductCatalogueKey, 
            RM.FKLastAssetId AS DimAssetKeyLastOn, 
            S.FKScrapGroupId AS DimScrapGroupKey, 
            RM.EnumTypeOfScrap AS DimTypeOfScrapKey, 
            QI.EnumInspectionResult AS DimInspectionResultKey, 
            RM.FKScrapAssetId AS DimAssetKeyMaterialScrap, 
            D.LastDefectCatalogueId AS DimDefectCatalogueKeyLastOne, 
            P.ProductId AS DimProductKey, 
            M.MaterialName, 
            RM.RawMaterialName, 
            M.SeqNo AS MaterialSeqNo, 
            RM.CuttingSeqNo AS MaterialCuttingSeq, 
            RM.ChildsNo AS MaterialChildSeq, 
            M.MaterialCreatedTs AS MaterialCreated, 
            M.MaterialWeight, 
            M.MaterialThickness, 
            M.MaterialWidth, 
            M.MaterialLength, 
            MC.MaterialCatalogueName, 
            PC.ProductCatalogueName, 
            M.IsDummy AS MaterialIsTest, 
            RM.IsVirtual AS MaterialIsVirtual, 
            ISNULL(CAST(CASE
                            WHEN RM.EnumRejectLocation > 0
                            THEN 1
                            ELSE 0
                        END AS BIT), 0) AS MaterialIsReject, 
            ISNULL(CAST(CASE
                            WHEN RM.EnumTypeOfScrap > 0
                            THEN 1
                            ELSE 0
                        END AS BIT), 0) AS MaterialIsScrap, 
            H.HeatName AS MaterialHeatName, 
            S.SteelgradeCode, 
            S.SteelgradeName, 
            SG.SteelGroupCode, 
            SG.SteelGroupName, 
            WO.WorkOrderName, 
            RM.LastWeight AS MaterialLastWeight, 
            RM.LastLength AS MaterialLastLength, 
            RM.LastTemperature AS MaterialLastTemperature, 
            RM.LastGrading AS MaterialLastGrading, 
            RM.SlittingFactor AS MaterialSlittingFactor, 
            ISNULL(RM.ScrapPercent, 0) AS MaterialScrapPercent, 
            ISNULL(RM.ScrapPercent * M.MaterialWeight, 0) AS MaterialScrapWeight, 
            ISNULL(CASE
                       WHEN RM.EnumRejectLocation > 0
                       THEN 1
                       ELSE 0
                   END * M.MaterialWeight, 0) AS MaterialRejectWeight, 
            RM.ScrapRemarks AS MaterialScrapRemarks, 
            RM.FurnaceExitTemperature, 
            RM.FurnaceHeatingDuration, 
            ISNULL(D.DefectsNumber, 0) AS MaterialDefectsNumber, 
            dbo.FNGetEnumKeyword('InspectionResult', QI.EnumInspectionResult) AS MaterialInspectionResult, 
            dbo.FNGetEnumKeyword('RawMaterialStatus', RM.EnumRawMaterialStatus) AS MaterialStatus, 
            dbo.FNGetEnumKeyword('ChargeType', RM.EnumChargeType) AS MaterialChargeType, 
            dbo.FNGetEnumKeyword('WorkOrderStatus', WO.EnumWorkOrderStatus) AS WorkOrderStatus, 
            P.ProductCreatedTs AS ProductCreated, 
            P.ProductName AS ProductName, 
            ISNULL(P.ProductWeight, 0) AS ProductWeight, 
            ISNULL(CASE
                       WHEN QI.EnumInspectionResult = 2
                       THEN P.ProductWeight
                       ELSE 0
                   END, 0) AS ProductWeightBad, 
            ISNULL(DL.DelayDuration, 0) AS MaterialDelayDuration, 
            M.MaterialStartTs AS MaterialProductionStart, 
            M.MaterialEndTs AS MaterialProductionEnd, 
            ISNULL(DATEDIFF_BIG(SECOND, M.MaterialStartTs, ISNULL(M.MaterialEndTs, GETDATE())), 0) - ISNULL(DL.DelayDuration, 0) AS MaterialProductionDuration, 
            RM.RawMaterialStartTs AS RawMaterialProductionStart, 
            RM.RawMaterialEndTs AS RawMaterialProductionEnd, 
            ISNULL(DATEDIFF_BIG(SECOND, RM.RawMaterialStartTs, ISNULL(RM.RawMaterialEndTs, GETDATE())), 0) - ISNULL(DL.DelayDuration, 0) AS RawMaterialProductionDuration, 
            RM.RollingStartTs AS MaterialRollingStart, 
            RM.RollingEndTs AS MaterialRollingEnd, 
            ISNULL(DATEDIFF_BIG(SECOND, RM.RollingStartTs, ISNULL(RM.RollingEndTs, GETDATE())), 0) - ISNULL(DL.DelayDuration, 0) AS MaterialRollingDuration, 
            dbo.FNGetTimeOfEvent(RM.RawMaterialId, 301) AS MaterialCharged, 
            dbo.FNGetTimeOfEvent(RM.RawMaterialId, 303) AS MaterialDischarged, 
            CONCAT(CONVERT(VARCHAR(10), SC.PlannedStartTime, 120), ' ', SD.ShiftCode) AS ShiftDateWithCode, 
            C.CrewName
     FROM dbo.PRMMaterials AS M
          INNER JOIN dbo.PRMHeats AS H ON M.FKHeatId = H.HeatId
          LEFT JOIN dbo.PRMMaterialCatalogue AS MC ON M.FKMaterialCatalogueId = MC.MaterialCatalogueId
          LEFT JOIN dbo.PRMWorkOrders AS WO
          INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
          INNER JOIN dbo.PRMSteelGroups AS SG ON S.FKSteelGroupId = SG.SteelGroupId ON M.FKWorkOrderId = WO.WorkOrderId
          LEFT JOIN dbo.TRKRawMaterials AS RM
          INNER JOIN dbo.EVTShiftCalendar AS SC ON RM.FKShiftCalendarId = SC.ShiftCalendarId
          INNER JOIN dbo.EVTShiftDefinitions AS SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
          INNER JOIN dbo.EVTCrews AS C ON SC.FKCrewId = C.CrewId ON M.MaterialId = RM.FKMaterialId
                                                                    AND RM.EnumRawMaterialType = 0
          LEFT JOIN dbo.PRMProducts AS P ON RM.FKProductId = P.ProductId
          LEFT JOIN Defects AS D ON RM.RawMaterialId = D.RawMaterialId
          LEFT JOIN Delays AS DL ON RM.RawMaterialId = DL.RawMaterialId
          LEFT JOIN dbo.QTYQualityInspections AS QI ON P.ProductId = QI.FKProductId;