

CREATE     VIEW [dw].[DimEventCatalogue] AS 
/*
	SELECT * FROM dw.DimEventCatalogue
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY DC.EventCatalogueId), 0) AS DimEventCatalogueRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimEventCatalogueIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(DC.EventCatalogueId AS NVARCHAR), ';') + 
			COALESCE(CAST(DC.FKParentEventCatalogueId AS NVARCHAR), ';') + 
			COALESCE(CAST(ET.EventTypeCode AS NVARCHAR), ';') + 
			COALESCE(CAST(ET.EventTypeName AS NVARCHAR), ';') + 
			COALESCE(CAST(ET.EventTypeDescription AS NVARCHAR), ';') +
			COALESCE(CAST(DC.EventCatalogueCode AS NVARCHAR), ';') + 
			COALESCE(CAST(DC.EventCatalogueName AS NVARCHAR), ';') + 
			COALESCE(CAST(DC.EventCatalogueDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(DC.StdEventTime AS NVARCHAR), ';') + 
			COALESCE(CAST(DCC.EventCatalogueCategoryCode AS NVARCHAR), ';') + 
			COALESCE(CAST(DCC.EventCatalogueCategoryName AS NVARCHAR), ';') + 
			COALESCE(CAST(DCC.EventCatalogueCategoryDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(DCG.EventCategoryGroupCode AS NVARCHAR), ';') + 
			COALESCE(CAST(DCG.EventCategoryGroupName AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimEventCatalogueHash, 
          DC.EventCatalogueId AS DimEventCatalogueKey, 
          ET.EventTypeId AS DimEventTypeKey, 
          DC.FKEventCatalogueCategoryId AS DimEventCategoryKey, 
          DCC.FKEventCategoryGroupId AS DimEventGroupKey, 
          DC.FKParentEventCatalogueId AS DimEventCatalogueKeyParent,
          CASE
              WHEN ET.EventTypeId = 10
                   OR ET.FKParentEvenTypeId = 10
              THEN 1
              ELSE 0
          END AS EventIsDelay, 
          ET.EventTypeCode, 
          ET.EventTypeName, 
          ET.EventTypeDescription, 
          DC.EventCatalogueCode AS EventCatalogueCode, 
          DC.EventCatalogueName AS EventCatalogueName, 
          DC.EventCatalogueDescription AS EventCatalogueDescription, 
          DC.StdEventTime AS EventStdTime, 
		  DC.IsPlanned AS EventIsPlanned,
          DCC.EventCatalogueCategoryCode AS EventCategoryCode, 
          DCC.EventCatalogueCategoryName AS EventCategoryName, 
          DCC.EventCatalogueCategoryDescription AS EventCategoryDescription, 
		  dbo.FNGetEnumKeyword('AssignmentType',DCC.EnumAssignmentType) AS EventCategoryAssignmentType,
          DCG.EventCategoryGroupCode AS EventGroupCode, 
          DCG.EventCategoryGroupName AS EventGroupName, 
          DCP.EventCatalogueCode AS EventCatalogueCodeParent, 
          DCP.EventCatalogueName AS EventCatalogueNameParent, 
          DCP.EventCatalogueDescription AS EventCatalogueDescriptionParent
   FROM dbo.EVTEventCatalogue DC
        INNER JOIN dbo.EVTEventCatalogueCategory DCC ON DCC.EventCatalogueCategoryId = DC.FKEventCatalogueCategoryId
        INNER JOIN dbo.EVTEventTypes ET ON DCC.FKEventTypeId = ET.EventTypeId
        LEFT JOIN dbo.EVTEventCategoryGroups DCG ON DCG.EventCategoryGroupId = DCC.FKEventCategoryGroupId
        LEFT JOIN dbo.EVTEventCatalogue DCP ON DC.FKParentEventCatalogueId = DCP.EventCatalogueId;