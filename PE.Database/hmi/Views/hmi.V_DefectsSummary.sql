CREATE   VIEW [hmi].[V_DefectsSummary]
AS SELECT DF.DefectId, 
          DF.FKDefectCatalogueId AS DefectCatalogueId, 
          DF.FKRawMaterialId AS RawMaterialId, 
          DF.FKProductId AS ProductId, 
          DF.DefectName, 
          DF.DefectPosition, 
          DF.DefectFrequency, 
          DF.DefectScale, 
          DF.DefectDescription, 
          DFC.DefectCatalogueCode, 
          DFC.DefectCatalogueName, 
          DFCC.DefectCatalogueCategoryCode, 
          DFCC.DefectCatalogueCategoryName, 
          CAST(CASE
                   WHEN DF.FKRawMaterialId IS NOT NULL
                   THEN 1
                   ELSE 0
               END AS BIT) AS IsRawMaterialRelated, 
          CAST(CASE
                   WHEN DF.FKProductId IS NOT NULL
                   THEN 1
                   ELSE 0
               END AS BIT) AS IsProductRelated, 
          RM.RawMaterialName, 
          M.MaterialName, 
          P.ProductName, 
          ISNULL(M.FKWorkOrderId, P.FKWorkOrderId) AS WorkOrderId, 
          ISNULL(WOM.WorkOrderName, WOP.WorkOrderName) AS WorkOrderName, 
          A.AssetId, 
          A.AssetName
   FROM dbo.QTYDefects AS DF
        INNER JOIN dbo.QTYDefectCatalogue AS DFC ON DF.FKDefectCatalogueId = DFC.DefectCatalogueId
        INNER JOIN dbo.QTYDefectCatalogueCategory AS DFCC ON DFC.FKDefectCatalogueCategoryId = DFCC.DefectCatalogueCategoryId
        LEFT OUTER JOIN dbo.TRKRawMaterials AS RM
        LEFT JOIN PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId
        LEFT JOIN PRMWorkOrders AS WOM ON M.FKWorkOrderId = WOM.WorkOrderId ON DF.FKRawMaterialId = RM.RawMaterialId
        LEFT OUTER JOIN dbo.PRMProducts AS P
        LEFT JOIN PRMWorkOrders AS WOP ON P.FKWorkOrderId = WOP.WorkOrderId ON DF.FKProductId = P.ProductId
        LEFT OUTER JOIN dbo.MVHAssets A ON DF.FKAssetId = A.AssetId;