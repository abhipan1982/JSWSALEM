CREATE   VIEW [hmi].[V_FeatureCustoms]
AS

/*
	used by Lite
*/

SELECT F.FeatureId, 
       F.FKUnitOfMeasureId AS UnitOfMeasureId, 
       F.FKExtUnitOfMeasureId AS ExtUnitOfMeasureId, 
       FC.FKLanguageId AS LanguageId, 
       FC.FKUnitOfMeasureId AS CustomUnitOfMeasureId, 
       FC.FKUnitOfMeasureFormatId AS CustomUnitOfMeasureFormatId, 
       F.FeatureCode, 
       UOM.UnitSymbol AS UnitSymbol, 
       UOME.UnitSymbol AS ExtUnitSymbol, 
       L.LanguageCode, 
       UOMC.UnitSymbol AS CustomUnitSymbol, 
       UOMCF.UnitFormat AS CustomUnitFormat
FROM dbo.MVHFeatures F
     INNER JOIN smf.UnitOfMeasure UOM ON F.FKUnitOfMeasureId = UOM.UnitId
     INNER JOIN smf.UnitOfMeasure UOME ON F.FKExtUnitOfMeasureId = UOME.UnitId
     INNER JOIN dbo.MVHFeatureCustoms FC ON F.FeatureId = FC.FKFeatureId
     INNER JOIN smf.Languages L ON FC.FKLanguageId = L.LanguageId
     INNER JOIN smf.UnitOfMeasure UOMC ON FC.FKUnitOfMeasureId = UOMC.UnitId
     INNER JOIN smf.UnitOfMeasureFormat UOMCF ON FC.FKUnitOfMeasureFormatId = UOMCF.UnitFormatId;