



CREATE         VIEW [dw].[DimMaterialCatalogue] AS 
/*
	SELECT * FROM dw.DimMaterialCatalogue
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY MC.MaterialCatalogueId), 0) AS DimMaterialCatalogueRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimMaterialCatalogueIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(MC.MaterialCatalogueId AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.FKMaterialCatalogueTypeId AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.FKShapeId AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.MaterialCatalogueName AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.MaterialCatalogueDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.ExternalMaterialCatalogueName AS NVARCHAR), ';') + 
			COALESCE(CAST(MCT.MaterialCatalogueTypeCode AS NVARCHAR), ';') + 
			COALESCE(CAST(MCT.MaterialCatalogueTypeName AS NVARCHAR), ';') + 
			COALESCE(CAST(S.ShapeCode AS NVARCHAR), ';') + 
			COALESCE(CAST(S.ShapeName AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.LengthMin AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.LengthMax AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.ThicknessMin AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.ThicknessMax AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.WidthMin AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.WidthMax AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.WeightMin AS NVARCHAR), ';') + 
			COALESCE(CAST(MC.WeightMax AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimMaterialCatalogueHash, 
          MC.MaterialCatalogueId AS DimMaterialCatalogueKey, 
		  MC.FKMaterialCatalogueTypeId AS DimMaterialTypeKey,
		  MC.FKShapeId AS DimMaterialShapeKey,
          MC.MaterialCatalogueName, 
          MC.MaterialCatalogueDescription, 
          MC.ExternalMaterialCatalogueName, 
          MCT.MaterialCatalogueTypeCode AS MaterialTypeCode, 
          MCT.MaterialCatalogueTypeName AS MaterialTypeName, 
          S.ShapeCode AS MaterialShapeCode, 
          S.ShapeName AS MaterialShapeName, 
          MC.LengthMin AS MaterialCatalogueLengthMin, 
          MC.LengthMax AS MaterialCatalogueLengthMax, 
          MC.ThicknessMin AS MaterialCatalogueThicknessMin, 
          MC.ThicknessMax AS MaterialCatalogueThicknessMax, 
          MC.WidthMin AS MaterialCatalogueWidthMin, 
          MC.WidthMax AS MaterialCatalogueWidthMax, 
          MC.WeightMin AS MaterialCatalogueWeightMin, 
          MC.WeightMax AS MaterialCatalogueWeightMax
   FROM dbo.PRMMaterialCatalogue AS MC
        INNER JOIN dbo.PRMMaterialCatalogueTypes AS MCT ON MC.FKMaterialCatalogueTypeId = MCT.MaterialCatalogueTypeId
        INNER JOIN dbo.PRMShapes AS S ON MC.FKShapeId = S.ShapeId;