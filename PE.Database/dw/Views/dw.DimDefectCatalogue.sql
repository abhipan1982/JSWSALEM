


CREATE       VIEW [dw].[DimDefectCatalogue] AS 
/*
	SELECT * FROM dw.DimDefectCatalogue
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY DC.DefectCatalogueId), 0) AS DimDefectCatalogueRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimDefectCatalogueIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(DC.DefectCatalogueId AS NVARCHAR), ';') + 
			COALESCE(CAST(DC.FKParentDefectCatalogueId AS NVARCHAR), ';') + 
			COALESCE(CAST(DC.DefectCatalogueCode AS NVARCHAR), ';') + 
			COALESCE(CAST(DC.DefectCatalogueName AS NVARCHAR), ';') + 
			COALESCE(CAST(DC.DefectCatalogueDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(DCC.DefectCatalogueCategoryCode AS NVARCHAR), ';') + 
			COALESCE(CAST(DCC.DefectCatalogueCategoryName AS NVARCHAR), ';') + 
			COALESCE(CAST(DCC.DefectCatalogueCategoryDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(DCG.DefectCategoryGroupCode AS NVARCHAR), ';') + 
			COALESCE(CAST(DCG.DefectCategoryGroupName AS NVARCHAR), ';') + 
			COALESCE(CAST(DCG.DefectCategoryGroupDescription AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimDefectCatalogueHash, 
          DC.DefectCatalogueId AS DimDefectCatalogueKey, 
          DC.FKParentDefectCatalogueId AS DimDefectCatalogueKeyParent, 
          DC.DefectCatalogueCode, 
          DC.DefectCatalogueName, 
          DC.DefectCatalogueDescription, 
          DCC.DefectCatalogueCategoryCode AS DefectCategoryCode, 
          DCC.DefectCatalogueCategoryName AS DefectCategoryName, 
          DCC.DefectCatalogueCategoryDescription AS DefectCategoryDescription, 
		  dbo.FNGetEnumKeyword('AssignmentType',DCC.EnumAssignmentType) AS DefectCategoryAssignmentType,
          DCG.DefectCategoryGroupCode AS DefectGroupCode, 
          DCG.DefectCategoryGroupName AS DefectGroupName, 
          DCG.DefectCategoryGroupDescription AS DefectGroupDescription, 
          DCP.DefectCatalogueCode AS DefectCatalogueCodeParent, 
          DCP.DefectCatalogueName AS DefectCatalogueNameParent, 
          DCP.DefectCatalogueDescription AS DefectCatalogueDescriptionParent
   FROM dbo.QTYDefectCatalogue AS DC
        INNER JOIN dbo.QTYDefectCatalogueCategory AS DCC ON DC.FKDefectCatalogueCategoryId = DCC.DefectCatalogueCategoryId
        LEFT JOIN dbo.QTYDefectCategoryGroups AS DCG ON DCC.FKDefectCategoryGroupId = DCG.DefectCategoryGroupId
        LEFT JOIN dbo.QTYDefectCatalogue AS DCP ON DC.FKParentDefectCatalogueId = DCP.DefectCatalogueId;