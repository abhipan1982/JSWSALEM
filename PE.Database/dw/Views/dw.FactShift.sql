CREATE   VIEW [dw].[FactShift]
AS

/*
	select * from hmi.V_Enums
	select * from PRMWorkOrders
	select * from EVTEvents
	select * from dw.FactEvent where EventIsDelay=1
	SELECT * FROM dw.FactShift
	SELECT * FROM dw.FactEvent
*/

WITH Delays
     AS (SELECT FKShiftCalendarId, 
                SUM(DATEDIFF_BIG(SECOND, E.EventStartTs, ISNULL(E.EventEndTs, SC.PlannedEndTime))) AS DelayDuration
         FROM dbo.EVTEvents AS E
              INNER JOIN dbo.EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
              INNER JOIN dbo.EVTShiftCalendar AS SC ON E.FKShiftCalendarId = SC.ShiftCalendarId
         WHERE ET.EventTypeId = 10
               OR ET.FKParentEvenTypeId = 10
         GROUP BY FKShiftCalendarId),
     EventsNumber
     AS (SELECT FKShiftCalendarId, 
                SUM(CASE
                        WHEN ET.EventTypeName = 'Charge'
                        THEN 1
                        ELSE 0
                    END) AS ChargeNumber, 
                SUM(CASE
                        WHEN ET.EventTypeName = 'Discharge'
                        THEN 1
                        ELSE 0
                    END) AS DischargeNumber, 
                SUM(CASE
                        WHEN ET.EventTypeName = 'Full Scrap'
                        THEN 1
                        ELSE 0
                    END) AS FullScrapNumber, 
                SUM(CASE
                        WHEN ET.EventTypeName = 'Partial Scrap'
                        THEN 1
                        ELSE 0
                    END) AS PartialScrapNumber, 
                SUM(CASE
                        WHEN ET.EventTypeName = 'Reject'
                        THEN 1
                        ELSE 0
                    END) AS RejectNumber, 
                SUM(CASE
                        WHEN ET.EventTypeName = 'Uncharge'
                        THEN 1
                        ELSE 0
                    END) AS UnchargeNumber, 
                SUM(CASE
                        WHEN ET.EventTypeName = 'Undischarge'
                        THEN 1
                        ELSE 0
                    END) AS UndischargeNumber, 
                SUM(CASE
                        WHEN ET.EventTypeName = 'Product Create'
                        THEN 1
                        ELSE 0
                    END) AS ProductCreateNumber
         FROM dbo.EVTEvents AS E
              INNER JOIN dbo.EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
         GROUP BY FKShiftCalendarId),
     MaterialsWeight
     AS (SELECT E.FKShiftCalendarId, 
                SUM(M.MaterialWeight) AS MaterialsWeight, 
                SUM(RM.LastWeight) AS RawMaterialsWeight, 
                SUM(M.MaterialWeight * RM.ScrapPercent) AS ScrappedMaterialsWeight, 
                SUM(M.MaterialWeight * CASE
                                           WHEN RM.EnumRejectLocation > 0
                                           THEN 1
                                           ELSE 0
                                       END) AS RejectedMaterialsWeight
         FROM dbo.EVTEvents AS E
              INNER JOIN dbo.EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
              INNER JOIN dbo.TRKRawMaterials AS RM ON E.FKRawMaterialId = RM.RawMaterialId
              INNER JOIN dbo.PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId
         WHERE ET.EventTypeName = 'Discharge'
         GROUP BY E.FKShiftCalendarId),
     ProductsWeight
     AS (SELECT E.FKShiftCalendarId, 
                SUM(P.ProductWeight) AS ProductsWeight
         FROM dbo.EVTEvents AS E
              INNER JOIN dbo.EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
              INNER JOIN dbo.TRKRawMaterials AS RM ON E.FKRawMaterialId = RM.RawMaterialId
              INNER JOIN dbo.PRMProducts AS P ON RM.FKProductId = P.ProductId
         WHERE ET.EventTypeName = 'Product Create'
         GROUP BY E.FKShiftCalendarId),
     WorkOrders
     AS (SELECT SC.ShiftCalendarId, 
                COUNT(WorkOrderId) AS WorkOrderNumber, 
                SUM(DATEDIFF(SECOND,
                             CASE
                                 WHEN WO.WorkOrderStartTs < SC.PlannedStartTime
                                 THEN SC.PlannedStartTime
                                 ELSE WO.WorkOrderStartTs
                             END,
                             CASE
                                 WHEN WO.WorkOrderEndTs > SC.PlannedEndTime
                                 THEN SC.PlannedEndTime
                                 ELSE WO.WorkOrderEndTs
                             END)) AS WorkOrderDuration
         FROM PRMWorkOrders WO
              INNER JOIN EVTShiftCalendar SC ON WO.WorkOrderEndTs BETWEEN SC.PlannedStartTime AND SC.PlannedEndTime
         GROUP BY SC.ShiftCalendarId)
     --Main Query
     SELECT ISNULL(CONVERT(NVARCHAR(50), DB_NAME()), 0) AS SourceName, 
            GETDATE() AS SourceTime, 
            ISNULL(CONVERT(BIT, 0), 0) AS FactShiftIsDeleted, 
            ISNULL(ROW_NUMBER() OVER(
            ORDER BY SC.ShiftCalendarId), 0) AS FactShiftRow, 
            CAST(HASHBYTES('MD5', COALESCE(CAST(SC.ShiftCalendarId AS NVARCHAR), ';')) AS VARBINARY(16)) AS FactShiftHash, 
            SC.ShiftCalendarId AS FactShiftKey, 
            ISNULL(DATEPART(YEAR, SC.PlannedStartTime), 0) AS DimYearKey, 
            ISNULL(CONVERT(INT, CONVERT(VARCHAR(8), SC.PlannedStartTime, 112)), 0) AS DimDateKey, 
            FORMAT(SC.PlannedStartTime, 'yyyy') AS DimYear, 
            FORMAT(SC.PlannedStartTime, 'yyyy-MM') AS DimMonth, 
            CONCAT(FORMAT(SC.PlannedStartTime, 'yyyy'), '-W', DATEPART(WEEK, SC.PlannedStartTime)) AS DimWeek, 
            FORMAT(SC.PlannedStartTime, 'yyyy-MM-dd') AS DimDate, 
            SD.ShiftCode AS DimShiftCode, 
            C.CrewName AS DimCrewName, 
            CONCAT(DOY.DateDay, ' ', SD.ShiftCode) AS ShiftDateWithCode, 
            SD.DefaultStartTime, 
            SD.DefaultEndTime, 
            SC.PlannedStartTime AS ShiftStartTime, 
            SC.PlannedEndTime AS ShiftEndTime, 
            DATEDIFF(SECOND, SC.PlannedStartTime, SC.PlannedEndTime) AS ShiftDuration, 
            ISNULL(D.DelayDuration, 0) AS DelayDuration, 
            ISNULL(EN.ChargeNumber, 0) AS ChargeNumber, 
            ISNULL(EN.DischargeNumber, 0) AS DischargeNumber, 
            ISNULL(EN.UnchargeNumber, 0) AS UnchargeNumber, 
            ISNULL(EN.UndischargeNumber, 0) AS UndischargeNumber, 
            ISNULL(EN.FullScrapNumber, 0) AS FullScrapNumber, 
            ISNULL(EN.PartialScrapNumber, 0) AS PartialScrapNumber, 
            ISNULL(EN.RejectNumber, 0) AS RejectNumber, 
            ISNULL(EN.ProductCreateNumber, 0) AS ProductCreateNumber, 
            ISNULL(WO.WorkOrderNumber, 0) AS WorkOrdersNumber, 
            ISNULL(WO.WorkOrderDuration, 0) AS WorkOrdersDuration, 
            ISNULL(MW.MaterialsWeight, 0) AS MaterialsWeight, 
            ISNULL(MW.ScrappedMaterialsWeight, 0) AS ScrappedMaterialsWeight, 
            ISNULL(MW.RejectedMaterialsWeight, 0) AS RejectedMaterialsWeight, 
            ISNULL(PW.ProductsWeight, 0) AS ProductsWeight
     FROM dbo.EVTShiftCalendar AS SC
          INNER JOIN dbo.EVTDaysOfYear AS DOY ON SC.FKDaysOfYearId = DOY.DaysOfYearId
          INNER JOIN dbo.EVTShiftDefinitions AS SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
          INNER JOIN dbo.EVTCrews AS C ON SC.FKCrewId = C.CrewId
          LEFT JOIN Delays AS D ON SC.ShiftCalendarId = D.FKShiftCalendarId
          LEFT JOIN EventsNumber AS EN ON SC.ShiftCalendarId = EN.FKShiftCalendarId
          LEFT JOIN MaterialsWeight AS MW ON SC.ShiftCalendarId = MW.FKShiftCalendarId
          LEFT JOIN ProductsWeight AS PW ON SC.ShiftCalendarId = PW.FKShiftCalendarId
          LEFT JOIN WorkOrders AS WO ON SC.ShiftCalendarId = WO.ShiftCalendarId;