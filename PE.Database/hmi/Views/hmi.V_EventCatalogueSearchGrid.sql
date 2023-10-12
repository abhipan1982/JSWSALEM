CREATE   VIEW hmi.V_EventCatalogueSearchGrid
AS

/*
select * from hmi.V_EventCatalogueSearchGrid
*/

SELECT EC.EventCatalogueId, 
       EC.EventCatalogueCode, 
       EC.EventCatalogueName, 
       EC.IsActive AS IsActiveCatalogue, 
       EC.IsDefault AS IsDefaultCatalogue, 
       ECC.EventCatalogueCategoryId, 
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
       ECG.EventCategoryGroupName, 
       ECP.EventCatalogueId AS ParentEventCatalogueId, 
       ECP.EventCatalogueCode AS ParentEventCatalogueCode, 
       ECP.EventCatalogueName AS ParentEventCatalogueName
FROM dbo.EVTEventCatalogue AS EC
     INNER JOIN dbo.EVTEventCatalogueCategory AS ECC ON EC.FKEventCatalogueCategoryId = ECC.EventCatalogueCategoryId
     INNER JOIN dbo.EVTEventTypes AS ET ON ECC.FKEventTypeId = ET.EventTypeId
     LEFT JOIN dbo.EVTEventTypes AS ETP ON ET.FKParentEvenTypeId = ETP.EventTypeId
     LEFT JOIN dbo.EVTEventCategoryGroups AS ECG ON ECC.FKEventCategoryGroupId = ECG.EventCategoryGroupId
     LEFT JOIN dbo.EVTEventCatalogue AS ECP ON EC.FKParentEventCatalogueId = ECP.EventCatalogueId;