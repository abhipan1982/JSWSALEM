CREATE PROCEDURE [dbo].[SPRawMaterialGenealogy] @RawMaterialId BIGINT
AS

/*
	EXEC SPRawMaterialGenealogy @RawMaterialId = 1088177;
	EXEC SPRawMaterialGenealogy @RawMaterialId = 1088181;
*/

    BEGIN
        DECLARE @ParentRawMaterialId BIGINT;
        DECLARE @Family TABLE
        (RawMaterialId       BIGINT, 
         ParentRawMaterialId BIGINT
        );

        --Check Parent
        SELECT @ParentRawMaterialId = ParentRawMaterialId
        FROM TRKRawMaterialRelations
        WHERE ChildRawMaterialId = @RawMaterialId;
        --Fill Temporary table
        IF @ParentRawMaterialId IS NULL
            BEGIN
                INSERT INTO @Family(RawMaterialId)
            VALUES(@RawMaterialId);
                INSERT INTO @Family
                       SELECT ChildRawMaterialId, 
                              ParentRawMaterialId
                       FROM TRKRawMaterialRelations
                       WHERE ParentRawMaterialId = @RawMaterialId;
            END;
            ELSE
            BEGIN
                INSERT INTO @Family(RawMaterialId)
            VALUES(@ParentRawMaterialId);
                INSERT INTO @Family
                       SELECT ChildRawMaterialId, 
                              ParentRawMaterialId
                       FROM TRKRawMaterialRelations
                       WHERE ParentRawMaterialId = @ParentRawMaterialId;
            END;
        -- Main Query
        -- SELECT * FROM @Family;

        SELECT RM.RawMaterialId, 
               F.ParentRawMaterialId AS ParentRawMaterialId, 
               RM.FKMaterialId AS MaterialId, 
               RM.FKProductId AS ProductId, 
               RM.RawMaterialName, 
               RM.EnumRawMaterialStatus, 
               RM.EnumTypeOfScrap, 
               RM.EnumRejectLocation, 
               RM.LastLength, 
               PCT.ProductCatalogueTypeId AS ProductCatalogueTypeId, 
               PCT.ProductCatalogueTypeCode, 
               RM.CuttingSeqNo, 
               RM.ChildsNo
        FROM TRKRawMaterials RM
             INNER JOIN @Family AS F ON RM.RawMaterialId = F.RawMaterialId
             LEFT JOIN PRMMaterials M
             INNER JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
             INNER JOIN PRMProductCatalogue PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
             INNER JOIN PRMProductCatalogueTypes PCT ON PC.FKProductCatalogueTypeId = PCT.ProductCatalogueTypeId ON RM.FKMaterialId = M.MaterialId
        --WHERE RM.EnumRawMaterialType = 0;
    END;