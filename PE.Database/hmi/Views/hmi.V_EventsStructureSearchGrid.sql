CREATE   VIEW [hmi].[V_EventsStructureSearchGrid]
AS

/*
select * from EVTEventTypes
select * from [hmi].[V_EventsStructureSearchGrid]
*/

SELECT ET.EventTypeId, 
       ET.EventTypeCode, 
       ET.EventTypeName, 
       ETP.EventTypeId AS ParentEventTypeId, 
       ETP.EventTypeCode AS ParentEventTypeCode, 
       ETP.EventTypeName AS ParentEventTypeName, 
       ECG.EventCategoryGroupId, 
       ECG.EventCategoryGroupCode, 
       ECG.EventCategoryGroupName, 
       ECC.EventCatalogueCategoryId, 
       ECC.EventCatalogueCategoryCode, 
       ECC.EventCatalogueCategoryName, 
       ECC.IsDefault AS IsDefaultCategory, 
       EC.EventCatalogueId, 
       EC.EventCatalogueCode, 
       EC.EventCatalogueName, 
       EC.IsActive AS IsActiveCatalogue, 
       EC.IsDefault AS IsDefaultCatalogue, 
       ECP.EventCatalogueId AS ParentEventCatalogueId, 
       ECP.EventCatalogueCode AS ParentEventCatalogueCode, 
       ECP.EventCatalogueName AS ParentEventCatalogueName
FROM dbo.EVTEventTypes AS ET
     LEFT JOIN dbo.EVTEventTypes AS ETP ON ET.FKParentEvenTypeId = ETP.EventTypeId
     LEFT JOIN dbo.EVTEventCatalogueCategory AS ECC ON ET.EventTypeId = ECC.FKEventTypeId
     LEFT JOIN dbo.EVTEventCategoryGroups AS ECG ON ECC.FKEventCategoryGroupId = ECG.EventCategoryGroupId
     LEFT JOIN dbo.EVTEventCatalogue AS EC ON ECC.EventCatalogueCategoryId = EC.FKEventCatalogueCategoryId
     LEFT JOIN dbo.EVTEventCatalogue AS ECP ON EC.FKParentEventCatalogueId = ECP.EventCatalogueId;