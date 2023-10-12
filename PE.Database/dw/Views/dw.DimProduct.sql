CREATE   VIEW [dw].[DimProduct]
AS

/*
	SELECT * FROM dw.DimProduct
*/

WITH AssignedProducts
     AS (SELECT FKProductId AS ProductId, 
                COUNT(RawMaterialId) AS RawMaterialsAssigned
         FROM TRKRawMaterials
         WHERE FKProductId IS NOT NULL
         GROUP BY FKProductId)
     SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
            GETDATE() AS SourceTime, 
            ISNULL(ROW_NUMBER() OVER(
            ORDER BY P.ProductId), 0) AS DimProductRow, 
            ISNULL(CAST(0 AS BIT), 0) AS DimProductIsDeleted, 
            CAST(HASHBYTES('MD5', COALESCE(CAST(P.ProductId AS NVARCHAR), ';') + COALESCE(CAST(P.FKWorkOrderId AS NVARCHAR), ';') + COALESCE(CAST(P.ProductName AS NVARCHAR), ';') + COALESCE(CAST(P.IsAssigned AS NVARCHAR), ';') + +COALESCE(CAST(P.ProductWeight AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimProductHash, 
            P.ProductId AS DimProductKey, 
            P.FKWorkOrderId AS DimWorkOrderKey, 
            WO.FKSteelgradeId AS DimSteelgradeKey, 
            WO.FKHeatId AS DimHeatKey, 
            WO.FKProductCatalogueId AS DimProductCatalogueKey, 
            P.ProductName AS ProductName, 
            P.ProductWeight, 
            P.ProductCreatedTs AS ProductCreated, 
            P.IsAssigned AS ProductIsAssignedWithRawMaterial, 
            AP.RawMaterialsAssigned
     FROM PRMProducts AS P
          INNER JOIN PRMWorkOrders AS WO ON P.FKWorkOrderId = WO.WorkOrderId
          --INNER JOIN PRMProducts P2 ON P.ProductId = P2.ProductId -- fake join - to not create identity column (sic!)
          INNER JOIN AssignedProducts AS AP ON P.ProductId = AP.ProductId;