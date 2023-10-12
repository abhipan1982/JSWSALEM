CREATE   VIEW [hmi].[V_RawMaterialOverview]
AS

/*
select * from [hmi].[V_RawMaterialOverview]
select count(1) from TRKRawMaterials
*/

WITH Products
     AS (SELECT RawMaterialId, 
                SUM(ProductWeight) AS ProductsWeight
         FROM dbo.PRMProducts AS P
              INNER JOIN dbo.TRKRawMaterials RM ON P.ProductId = RM.FKProductId
         GROUP BY RawMaterialId),
     Defects
     AS (SELECT FKRawMaterialId, 
                COUNT(DefectId) AS DefectsNumber
         FROM QTYDefects AS D
         GROUP BY FKRawMaterialId),
     Relations
     AS (SELECT ChildRawMaterialId, 
                MAX(ParentRawMaterialId) ParentRawMaterialId
         FROM TRKRawMaterialRelations
         GROUP BY ChildRawMaterialId),
		 Layers
     AS (SELECT ChildLayerRawMaterialId, 
                MAX(ParentLayerRawMaterialId) ParentLayerRawMaterialId
         FROM TRKLayerRawMaterialRelations
         GROUP BY ChildLayerRawMaterialId)
     SELECT ISNULL(ROW_NUMBER() OVER(
            ORDER BY RM.RawMaterialId), 0) AS OrderSeq, 
            RM.RawMaterialId, 
            ISNULL(M.MaterialName, CONCAT(RM.RawMaterialName, ' *')) AS DisplayedMaterialName, 
            RM.RawMaterialName, 
            RM.RawMaterialCreatedTs, 
            RM.IsDummy AS RawMaterialIsDummy, 
            RM.IsVirtual AS RawMaterialIsVirtual, 
            RM.CuttingSeqNo AS RawMaterialCutNo, 
            RM.ChildsNo AS RawMaterialChildNo, 
            RM.FKMaterialId AS MaterialId, 
            RM.FKProductId AS ProductId,
            CASE
                WHEN RM.EnumRawMaterialType = 1
                THEN 1
                ELSE 0
            END IsLayer, 
            RML.ParentLayerRawMaterialId AS ParentLayerId, 
            RM.SlittingFactor, 
            RM.ScrapPercent, 
            RM.ScrapRemarks, 
            RM.EnumRawMaterialType, 
            RM.EnumTypeOfScrap, 
            RM.EnumRejectLocation, 
            RM.EnumRawMaterialStatus, 
            RM.OutputPieces, 
            RM.WeighingStationWeight, 
            RM.LastWeight AS RawMaterialLastWeight, 
            RM.LastLength AS RawMaterialLastLength, 
            ISNULL(CAST(CASE
                            WHEN RM.EnumTypeOfScrap > 0
                            THEN 1
                            ELSE 0
                        END AS BIT), 0) AS IsScrap, 
            ISNULL(CAST(CASE
                            WHEN RM.FKMaterialId IS NOT NULL
                            THEN 1
                            ELSE 0
                        END AS BIT), 0) AS IsMaterialAssigned, 
            ISNULL(CAST(CASE
                            WHEN RM.FKProductId IS NOT NULL
                            THEN 1
                            ELSE 0
                        END AS BIT), 0) AS IsProductAssigned, 
            RM1.RawMaterialName AS ParentRawMaterialName, 
            RM1.CuttingSeqNo AS ParentRawMaterialCutNo, 
            RM1.ChildsNo AS ParentRawMaterialChildNo, 
            RMS0.IsAssetExit, 
            A.AssetId, 
            A.AssetName, 
            A2.AssetName AS ParentAssetName, 
            M.MaterialName, 
            M.SeqNo AS MaterialSeqNo, 
            M.MaterialWeight, 
            WO.WorkOrderId, 
            WO.WorkOrderName, 
            MC.MaterialCatalogueName, 
            PC.ProductCatalogueName, 
            S.SteelgradeCode, 
            S.SteelgradeName, 
            H.HeatId, 
            H.HeatName, 
            SG.SteelGroupCode, 
            SG.SteelGroupName, 
            CONCAT(CONVERT(VARCHAR(10), SC.PlannedStartTime, 120), ' ', ShiftCode) AS ShiftKey, 
            SD.ShiftCode AS ShiftCode, 
            C.CrewName, 
            P.ProductsWeight, 
            ISNULL(D.DefectsNumber, 0) AS DefectsNumber, 
            QI.DiameterMin, 
            QI.DiameterMax, 
            QI.VisualInspection, 
            ISNULL(QI.EnumCrashTest, 0) AS EnumCrashTest, 
            ISNULL(QI.EnumInspectionResult, 0) AS EnumInspectionResult, 
            CAST(CASE
                     WHEN ISNULL(D.DefectsNumber, 0) > 0
                     THEN 1
                     ELSE 0
                 END AS BIT) AS HasDefects, 
            dbo.FNGetTimeOfEvent(RM.RawMaterialId, 301) AS ChargeTime, 
            dbo.FNGetTimeOfEvent(RM.RawMaterialId, 303) AS DischargeTime, 
            dbo.FNGetEnumKeyword('InspectionResult', QI.EnumInspectionResult) AS InspectionResult, 
            dbo.FNGetEnumKeyword('RawMaterialStatus', RM.EnumRawMaterialStatus) AS RawMaterialStatus
     FROM dbo.TRKRawMaterials AS RM
          INNER JOIN dbo.TRKRawMaterialsSteps AS RMS0 ON RM.RawMaterialId = RMS0.FKRawMaterialId
                                                         AND RMS0.ProcessingStepNo = 0
          
          LEFT JOIN dbo.MVHAssets AS A ON RMS0.FKAssetId = A.AssetId
          LEFT JOIN dbo.MVHAssets AS A2 ON A.FKParentAssetId = A2.AssetId
          LEFT JOIN dbo.PRMMaterials AS M
          INNER JOIN dbo.PRMWorkOrders AS WO ON M.FKWorkOrderId = WO.WorkOrderId
          INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
          INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
          INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
          LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
          LEFT JOIN dbo.PRMSteelGroups AS SG ON S.FKSteelGroupId = SG.SteelGroupId ON RM.FKMaterialId = M.MaterialId
          INNER JOIN dbo.EVTShiftCalendar AS SC
          INNER JOIN dbo.EVTShiftDefinitions AS SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
          INNER JOIN dbo.EVTCrews AS C ON SC.FKCrewId = C.CrewId ON RM.FKShiftCalendarId = SC.ShiftCalendarId
          LEFT JOIN Products AS P ON RM.RawMaterialId = P.RawMaterialId
          LEFT JOIN Defects AS D ON RM.RawMaterialId = D.FKRawMaterialId
		  LEFT JOIN Layers AS RML ON RM.RawMaterialId = RML.ChildLayerRawMaterialId
		  LEFT JOIN Relations AS RMR ON RM.RawMaterialId = RMR.ChildRawMaterialId
		  LEFT OUTER JOIN dbo.TRKRawMaterials AS RM1 ON RMR.ParentRawMaterialId = RM1.RawMaterialId
          LEFT JOIN dbo.QTYQualityInspections AS QI ON RM.RawMaterialId = QI.FKRawMaterialId;