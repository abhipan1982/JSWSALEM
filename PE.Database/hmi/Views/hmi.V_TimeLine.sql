CREATE   VIEW [hmi].[V_TimeLine] AS 
SELECT WO.WorkOrderId, 
       WO.WorkOrderName, 
       Evt, 
       EvtName, 
       StartTime, 
       EndTime, 
       DATEDIFF(SECOND, StartTime, EndTime) Duration, 
       WorkOrderDuration, 
       DelayDuration, 
       WorkOrderLag, 
       RawMaterialId, 
       EvtGroup, 
       LastWeight, 
       SUM(DATEDIFF(SECOND, StartTime, EndTime)) OVER(PARTITION BY WO.WorkOrderId, 
                                                                   Evt) TotalEvtDuration, 
       SUM(DATEDIFF(SECOND, StartTime, EndTime)) OVER(PARTITION BY WO.WorkOrderId, 
                                                                   EvtGroup) TotalEvtGroupDuration, 
       ROW_NUMBER() OVER(
       ORDER BY StartTime ASC, 
                WO.WorkOrderStartTs ASC, 
                Evt DESC) AS OrderSeq
FROM
(
    SELECT 'WorkOrder' Evt, 
           WorkOrderStartTs StartTime, 
           WorkOrderEndTs EndTime, 
           WorkOrderId, 
           WorkOrderName EvtName, 
           NULL RawMaterialId, 
           NULL LastWeight,
           CASE
               WHEN IsTestOrder = 1
               THEN 'TestOrder'
               ELSE '0'
           END EvtGroup, 
           DATEDIFF(SECOND, WorkOrderStartTs, WorkOrderEndTs) WorkOrderDuration, 
           0 DelayDuration, 
           DATEDIFF(SECOND, LAG(WorkOrderEndTs) OVER(
           ORDER BY WorkOrderStartTs ASC), WorkOrderStartTs) WorkOrderLag
    FROM PRMWorkOrders
    --WHERE IsTestOrder=0 AND DATEDIFF(SECOND, WorkOrderStartTs, WorkOrderEndTs) != 0
    UNION ALL
    SELECT 'Material' Evt, 
           RawMaterialStartTs, 
           RawMaterialEndTs, 
           FKWorkOrderId, 
           RawMaterialName, 
           RawMaterialId, 
           LastWeight, 
           CAST(RM.EnumTypeOfScrap AS VARCHAR), 
           0, 
           0, 
           0
    FROM TRKRawMaterials RM
         INNER JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
         INNER JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId --AND WO.IsTestOrder = 0
    UNION ALL
    SELECT 'Event' Evt, 
           EventStartTs, 
           EventEndTs, 
           FKWorkOrderId, 
           DC.EventCatalogueName, 
           FKRawMaterialId, 
           NULL LastWeight, 
           DCG.EventCategoryGroupCode, 
           0, 
           DATEDIFF(SECOND, EventStartTs, EventEndTs) DelayDuration, 
           0
    FROM EVTEvents D
         INNER JOIN EVTEventCatalogue DC ON D.FKEventCatalogueId = DC.EventCatalogueId
         INNER JOIN EVTEventCatalogueCategory DCC ON DC.FKEventCatalogueCategoryId = DCC.EventCatalogueCategoryId
         INNER JOIN EVTEventCategoryGroups DCG ON DCC.FKEventCategoryGroupId = DCG.EventCategoryGroupId
) QRY
INNER JOIN PRMWorkOrders WO ON QRY.WorkOrderId = WO.WorkOrderId
WHERE 1 = 1
      --AND StartTime IS NOT NULL;
--AND StartTime >= '2021-02-21 21:00:00.000'
--AND EndTime <= '2021-02-26 21:00:00.000';