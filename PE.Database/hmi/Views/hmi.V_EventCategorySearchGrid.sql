CREATE   VIEW hmi.[V_EventCategorySearchGrid] AS 
/*
select * from hmi.EventCategorySearchGrid
*/
SELECT ECC.EventCatalogueCategoryId, 
          ECC.EventCatalogueCategoryCode, 
          ECC.EventCatalogueCategoryName, 
          ECC.IsDefault AS IsDefaultCategory, 
          ET.EventTypeId, 
          ET.EventTypeCode, 
          ET.EventTypeName, 
          ETP.EventTypeId AS ParentEventTypeId, 
          ETP.EventTypeCode AS ParentEventTypeCode, 
          ETP.EventTypeName AS ParentEventTypeName, 
          ECG.EventCategoryGroupId, 
          ECG.EventCategoryGroupCode, 
          ECG.EventCategoryGroupName
   FROM dbo.EVTEventCatalogueCategory ECC
        INNER JOIN dbo.EVTEventTypes AS ET ON ET.EventTypeId = ECC.FKEventTypeId
        LEFT JOIN dbo.EVTEventTypes AS ETP ON ET.FKParentEvenTypeId = ETP.EventTypeId
        LEFT JOIN dbo.EVTEventCategoryGroups AS ECG ON ECC.FKEventCategoryGroupId = ECG.EventCategoryGroupId;