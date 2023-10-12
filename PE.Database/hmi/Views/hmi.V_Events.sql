

CREATE       VIEW [hmi].[V_Events] AS 
/*
select * from hmi.V_Events
*/

SELECT ISNULL(ROW_NUMBER() OVER(
       ORDER BY E.EventStartTs DESC), 0) AS OrderSeq, 
       DOY.DateDay, 
       DOY.Year, 
       DOY.Quarter, 
       DOY.Month, 
       DOY.DayNumber, 
       DOY.WeekDayNumber, 
       DOY.WeekNumber, 
       E.EventId, 
	   ET.EventTypeCode, 
       ET.EventTypeName, 
	   E.EventStartTs, 
       E.EventEndTs, 
       E.FKParentEventId AS ParentEventId, 
       E.FKShiftCalendarId AS ShiftCalendarId, 
       E.FKWorkOrderId AS WorkOrderId, 
       E.FKRawMaterialId AS RawMaterialId, 
       E.FKAssetId AS AssetId, 
       E.FKUserId AS UserId, 
       E.UserComment, 
       
       ISNULL(CONVERT(BIT,
                      CASE
                          WHEN ET.EventTypeId = 10
                               OR ET.FKParentEvenTypeId = 10
                          THEN 1
                          ELSE 0
                      END), 0) AS EventIsDelay, 
       ISNULL(E.IsPlanned, 0) AS EventIsPlanned, 
       ISNULL(CONVERT(BIT,
                      CASE
                          WHEN E.EventEndTs IS NULL
                          THEN 1
                          ELSE 0
                      END), 0) AS EventIsOpen, 
       ET.EventTypeId, 
       
       ET.EventTypeDescription, 
       ET.FKParentEvenTypeId AS ParentEventTypeId, 
       ET.HMILink, 
       ET.HMIColor, 
       EC.EventCatalogueCode, 
       EC.EventCatalogueName, 
       EC.EventCatalogueDescription, 
	   RM.RawMaterialName,
       A.AssetName
FROM EVTEvents AS E
     INNER JOIN EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
     INNER JOIN EVTDaysOfYear AS DOY ON CAST(E.EventStartTs AS DATE) = DOY.DateDay
     LEFT JOIN EVTEventCatalogue AS EC ON E.FKEventCatalogueId = EC.EventCatalogueId
	 LEFT JOIN TRKRawMaterials AS RM ON E.FKRawMaterialId = RM.RawMaterialId
     LEFT JOIN MVHAssets AS A ON E.FKAssetId = A.AssetId;