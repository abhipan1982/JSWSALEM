CREATE PROCEDURE [dbo].[SPWorkOrderSummary](@WorkOrderId BIGINT)
AS

/*
select * from PRMWorkOrders
select * from [hmi].[V_WorkOrderSummary]
exec SPWorkOrderSummary
*/

    BEGIN
        WITH Materials
             AS (SELECT FKWorkOrderId, 
                        SUM(MaterialWeight) AS MaterialsWeight, 
                        COUNT(MaterialId) AS MaterialsNumber
                 FROM dbo.PRMMaterials
                 WHERE FKWorkOrderId = @WorkOrderId
                 GROUP BY FKWorkOrderId),
             Products
             AS (SELECT FKWorkOrderId, 
                        SUM(ProductWeight) AS ProductsWeight, 
                        COUNT(ProductId) AS ProductsNumber
                 FROM dbo.PRMProducts
                 WHERE FKWorkOrderId = @WorkOrderId
                 GROUP BY FKWorkOrderId),
             RawMaterials
             AS (SELECT M.FKWorkOrderId, 
                        COUNT(RM.RawMaterialId) AS RawMaterialsNumber, 
                        SUM(RM.LastWeight) AS RawMaterialsWeight, 
                        SUM(CASE
                                WHEN RM.EnumRawMaterialStatus BETWEEN 30 AND 110
                                THEN 1
                                ELSE 0
                            END) AS MaterialsCharged, 
                        SUM(CASE
                                WHEN RM.EnumRawMaterialStatus BETWEEN 40 AND 110
                                THEN 1
                                ELSE 0
                            END) AS MaterialsDischarged, 
                        SUM(CASE
                                WHEN RM.EnumRawMaterialStatus BETWEEN 90 AND 110
                                THEN 1
                                ELSE 0
                            END) AS MaterialsRolled, 
                        -- SCRAP
                        SUM(CASE
                                WHEN RM.EnumRawMaterialStatus BETWEEN 120 AND 120
                                THEN 1
                                ELSE 0
                            END) AS MaterialsScrapped, 
                        SUM(CASE
                                WHEN RM.EnumTypeOfScrap = 2
                                THEN RM.LastWeight
                                ELSE ISNULL(RM.ScrapPercent * RM.LastWeight, 0)
                            END) MaterialsScrappedWeight, 
                        -- REJECT
                        SUM(CASE
                                WHEN RM.EnumRawMaterialStatus BETWEEN 130 AND 130
                                THEN 1
                                ELSE 0
                            END) AS MaterialsRejected, 
                        SUM(CASE
                                WHEN RM.EnumRawMaterialStatus BETWEEN 130 AND 130
                                THEN RM.LastWeight
                                ELSE 0
                            END) AS MaterialsRejectedWeight, 
                        SUM(CASE
                                WHEN EnumRejectLocation = 1
                                THEN 1
                                ELSE 0
                            END) AS MaterialsRejectedBeforeFurnace, 
                        SUM(CASE
                                WHEN EnumRejectLocation = 2
                                THEN 1
                                ELSE 0
                            END) AS MaterialsRejectedAfterFurnace, 
                        SUM(CASE
                                WHEN RM.EnumRejectLocation = 1
                                THEN RM.LastWeight
                                ELSE 0
                            END) AS MaterialsRejectedWeightBeforeFurnace, 
                        SUM(CASE
                                WHEN RM.EnumRejectLocation = 2
                                THEN RM.LastWeight
                                ELSE 0
                            END) AS MaterialsRejectedWeightAfterFurnace
                 FROM dbo.TRKRawMaterials AS RM
                      INNER JOIN dbo.PRMMaterials AS M ON M.MaterialId = RM.FKMaterialId
                 WHERE FKWorkOrderId = @WorkOrderId
                 GROUP BY M.FKWorkOrderId)

             -- MAIN QUERY
             SELECT WO.WorkOrderId, 
                    WO.WorkOrderName, 
                    WO.IsTestOrder, 
                    WO.IsBlocked, 
                    WO.IsSentToL3, 
                    WO.EnumWorkOrderStatus, 
                    WO.WorkOrderCreatedInL3Ts, 
                    WO.WorkOrderCreatedTs, 
                    WO.ToBeCompletedBeforeTs, 
                    WO.TargetOrderWeight, 
                    WO.WorkOrderStartTs, 
                    WO.WorkOrderEndTs, 
                    MC.MaterialCatalogueId, 
                    MC.MaterialCatalogueName, 
                    MS.ShapeName AS MaterialShapeName, 
                    PC.ProductCatalogueId, 
                    PC.ProductCatalogueName, 
                    PCT.ProductCatalogueTypeCode, 
                    PS.ShapeName AS ProductShapeName, 
                    PC.Thickness AS ProductThickness, 
                    PC.Width AS ProductWidth, 
                    S.SteelgradeId, 
                    S.SteelgradeCode, 
                    S.SteelgradeName, 
                    H.HeatId, 
                    H.HeatName, 
                    ISNULL(H.HeatWeight, 0) AS HeatWeight, 
                    SP.ScheduleId, 
                    SP.OrderSeq AS ScheduleOrderSeq, 
                    SP.PlannedDuration, 
                    SP.PlannedStartTime, 
                    SP.PlannedEndTime,
                    CASE
                        WHEN WO.EnumWorkOrderStatus = 4
                        THEN 1
                        ELSE 0
                    END AS IsProducedNow,
                    CASE
                        WHEN SP.ScheduleId IS NOT NULL
                        THEN 1
                        ELSE 0
                    END AS IsScheduled, 
                    ISNULL(WO.L3NumberOfBillets, 0) AS MaterialsPlanned, 
                    ISNULL(M.MaterialsNumber, 0) AS MaterialsNumber, 
                    ISNULL(M.MaterialsWeight, 0) AS MaterialsWeight, 
                    ISNULL(RM.RawMaterialsNumber, 0) AS RawMaterialsNumber, 
                    ISNULL(RM.RawMaterialsWeight, 0) AS RawMaterialsWeight, 
                    ISNULL(P.ProductsNumber, 0) AS ProductsNumber, 
                    ISNULL(P.ProductsWeight, 0) AS ProductsWeight, 
                    ISNULL(RM.MaterialsCharged, 0) AS MaterialsCharged, 
                    ISNULL(RM.MaterialsDischarged, 0) AS MaterialsDischarged, 
                    ISNULL(RM.MaterialsRolled, 0) AS MaterialsRolled, 
                    ISNULL(RM.MaterialsRejected, 0) AS MaterialsRejected, 
                    ISNULL(RM.MaterialsRejectedBeforeFurnace, 0) AS MaterialsRejectedBeforeFurnace, 
                    ISNULL(RM.MaterialsRejectedAfterFurnace, 0) AS MaterialsRejectedAfterFurnace, 
                    ISNULL(RM.MaterialsRejectedWeight, 0) AS MaterialsRejectedWeight, 
                    ISNULL(RM.MaterialsRejectedWeightBeforeFurnace, 0) AS MaterialsRejectedWeightBeforeFurnace, 
                    ISNULL(RM.MaterialsRejectedWeightAfterFurnace, 0) AS MaterialsRejectedWeightAfterFurnace, 
                    ISNULL(RM.MaterialsScrapped, 0) AS MaterialsScrapped, 
                    ISNULL(RM.MaterialsScrappedWeight, 0) AS MaterialsScrappedWeight, 
                    ISNULL(M.MaterialsWeight - RM.MaterialsRejectedWeight - RM.MaterialsScrappedWeight - P.ProductsWeight, 0) AS MaterialsLossWeight, 
                    ISNULL(WO.L3NumberOfBillets, 0) - ISNULL(RM.MaterialsCharged, 0) AS MaterialsPlannedVSCharged, 
                    ISNULL(P.ProductsWeight, 0) / WO.TargetOrderWeight AS WorkOrderCompletion,
                    CASE
                        WHEN M.MaterialsWeight > 0
                        THEN ISNULL(P.ProductsWeight, 0) / ISNULL(M.MaterialsWeight, 0)
                        ELSE 0
                    END AS MetallicYield
             FROM dbo.PRMWorkOrders AS WO
                  INNER JOIN dbo.PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
                  INNER JOIN dbo.PRMProductCatalogueTypes AS PCT ON PC.FKProductCatalogueTypeId = PCT.ProductCatalogueTypeId
                  INNER JOIN dbo.PRMMaterialCatalogue AS MC ON WO.FKMaterialCatalogueId = MC.MaterialCatalogueId
                  INNER JOIN dbo.PRMShapes AS MS ON MC.FKShapeId = MS.ShapeId
                  INNER JOIN dbo.PRMShapes AS PS ON PC.FKShapeId = PS.ShapeId
                  INNER JOIN dbo.PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
                  LEFT JOIN dbo.PPLSchedules SP ON WO.WorkOrderId = SP.FKWorkOrderId
                  LEFT JOIN dbo.PRMHeats AS H ON WO.FKHeatId = H.HeatId
                  LEFT JOIN Materials AS M ON WO.WorkOrderId = M.FKWorkOrderId
                  LEFT JOIN Products AS P ON WO.WorkOrderId = P.FKWorkOrderId
                  LEFT JOIN RawMaterials AS RM ON WO.WorkOrderId = RM.FKWorkOrderId
             WHERE WorkOrderId = @WorkOrderId;
    END;