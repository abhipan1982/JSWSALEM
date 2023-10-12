



CREATE         VIEW [dw].[DimProductCatalogue] AS 
/*
	SELECT * FROM dw.DimProductCatalogue
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY PC.ProductCatalogueId), 0) AS DimProductCatalogueRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimProductCatalogueIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(PC.ProductCatalogueId AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.FKProductCatalogueTypeId AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.FKShapeId AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.ProductCatalogueName AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.ProductCatalogueDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(PCT.ProductCatalogueTypeCode AS NVARCHAR), ';') + 
			COALESCE(CAST(PCT.ProductCatalogueTypeName AS NVARCHAR), ';') + 
			COALESCE(CAST(S.ShapeCode AS NVARCHAR), ';') + 
			COALESCE(CAST(S.ShapeName AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.MaxOvality AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.StdProductivity AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.StdMetallicYield AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.[Length] AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.LengthMin AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.LengthMax AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.Thickness AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.ThicknessMin AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.ThicknessMax AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.Width AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.WidthMin AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.WidthMax AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.[Weight] AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.WeightMin AS NVARCHAR), ';') + 
			COALESCE(CAST(PC.WeightMax AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimProductCatalogueHash, 
          PC.ProductCatalogueId AS DimProductCatalogueKey, 
		  PC.FKProductCatalogueTypeId AS DimProductTypeKey,
		  PC.FKShapeId AS DimProductShapeKey,
          PC.ProductCatalogueName, 
          PC.ProductCatalogueDescription, 
		  PC.ExternalProductCatalogueName AS ProductCatalogueExternalName,
          PCT.ProductCatalogueTypeCode AS ProductTypeCode, 
          PCT.ProductCatalogueTypeName AS ProductTypeName, 
          S.ShapeCode AS ProductShapeCode, 
          S.ShapeName AS ProductShapeName, 
          PC.[Length] AS ProductCatalogueLength, 
          PC.LengthMin AS ProductCatalogueLengthMin, 
          PC.LengthMax AS ProductCatalogueLengthMax, 
          PC.Thickness AS ProductCatalogueThickness, 
          PC.ThicknessMin AS ProductCatalogueThicknessMin, 
          PC.ThicknessMax AS ProductCatalogueThicknessMax, 
          PC.Width AS ProductCatalogueWidth, 
          PC.WidthMin AS ProductCatalogueWidthMin, 
          PC.WidthMax AS ProductCatalogueWidthMax, 
          PC.[Weight] AS ProductCatalogueWeight, 
          PC.WeightMin AS ProductCatalogueWeightMin, 
          PC.WeightMax AS ProductCatalogueWeightMax, 
          PC.MaxOvality AS ProductCatalogueMaxOvality, 
          PC.StdProductivity AS ProductCatalogueStdProductivity, 
          PC.StdMetallicYield AS ProductCatalogueStdMetallicYield
   FROM dbo.PRMProductCatalogue AS PC
        INNER JOIN dbo.PRMProductCatalogueTypes AS PCT ON PC.FKProductCatalogueTypeId = PCT.ProductCatalogueTypeId
        INNER JOIN dbo.PRMShapes AS S ON PC.FKShapeId = S.ShapeId;