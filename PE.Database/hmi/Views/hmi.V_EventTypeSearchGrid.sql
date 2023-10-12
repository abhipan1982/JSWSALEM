CREATE   VIEW hmi.[V_EventTypeSearchGrid]
AS

/*
select * from hmi.EventTypeSearchGrid
*/

SELECT ET.EventTypeId, 
       ET.EventTypeCode, 
       ET.EventTypeName, 
       ETP.EventTypeId AS ParentEventTypeId, 
       ETP.EventTypeCode AS ParentEventTypeCode, 
       ETP.EventTypeName AS ParentEventTypeName
FROM dbo.EVTEventTypes AS ET
     LEFT JOIN dbo.EVTEventTypes AS ETP ON ET.FKParentEvenTypeId = ETP.EventTypeId;