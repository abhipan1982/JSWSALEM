CREATE PROCEDURE [dbo].[SPCreateWorkOrderSIM]
AS

/*
	exec SPCreateWorkOrderSIM
	select * from prmmaterials
	TRUNCATE TABLE [xfr].[L3L2WorkOrderDefinition]
	select * from [xfr].[L3L2WorkOrderDefinition]
	TRUNCATE TABLE [xfrprj].[L3L2WorkOrderDefinitionEXT]
	select * from [xfrprj].[L3L2WorkOrderDefinitionEXT]
*/

     DECLARE @NumberOfBillets INT;
     DECLARE @BilletWeight FLOAT;
     DECLARE @BilletLength FLOAT;
     DECLARE @HeatName NVARCHAR(50);
     DECLARE @CustomerName NVARCHAR(50);
     DECLARE @MaterialCatalogueName NVARCHAR(50);
     DECLARE @InputThickness NVARCHAR(10);
     DECLARE @InputWidth NVARCHAR(10);
     DECLARE @InputShapeSymbol NVARCHAR(10);
     DECLARE @ProductCatalogueName NVARCHAR(50);
     DECLARE @OutputThickness NVARCHAR(10);
     DECLARE @OutputWidth NVARCHAR(10);
     DECLARE @OutputShapeSymbol NVARCHAR(10);
     DECLARE @SteelgradeCode NVARCHAR(50);
     DECLARE @DateNow NVARCHAR(14);
     DECLARE @DateDay NVARCHAR(8);
     DECLARE @OrderDeadline NVARCHAR(8);
    BEGIN
        -- Number Of Billets
        SELECT @NumberOfBillets = FLOOR(RAND() * (UpperValueInt - LowerValueInt + 1)) + LowerValueInt
        FROM [smf].[Limits]
        WHERE Name = 'SIM_WorkOrderNumMaterials';
        -- Billet Weight
        SELECT @BilletWeight = FLOOR(RAND() * (UpperValueFloat - LowerValueFloat) + LowerValueFloat)
        FROM [smf].[Limits]
        WHERE Name = 'BilletWeight';
        -- Billet Length
        SELECT @BilletLength = ROUND(RAND() * (UpperValueFloat - LowerValueFloat) + LowerValueFloat, 2)
        FROM [smf].[Limits]
        WHERE Name = 'BilletLength';
        -- Dates
        SELECT @DateNow = FORMAT(GETDATE(), 'yyyyMMddHHmmss'), 
               @DateDay = FORMAT(DATEADD(DAY, -2, GETDATE()), 'yyyyMMdd'), 
               @OrderDeadline = FORMAT(DATEADD(DAY, 30, GETDATE()), 'ddMMyyyy');
        -- Heat & Steelgrade
        SELECT TOP 1 @HeatName = HeatName, 
                     @SteelgradeCode = SteelgradeCode
        FROM PRMHeats H
             INNER JOIN PRMSteelgrades S ON H.FKSteelgradeId = S.SteelgradeId
        ORDER BY NEWID();
        -- Customer Name
        SELECT TOP 1 @CustomerName = CustomerName
        FROM PRMCustomers
        ORDER BY NEWID();
        -- Material Data
        SELECT TOP 1 @MaterialCatalogueName = MaterialCatalogueName, 
                     @InputThickness = ThicknessMin * 1000, 
                     @InputWidth = ISNULL(WidthMin, 1) * 1000, 
                     @InputShapeSymbol = S.ShapeCode
        FROM PRMMaterialCatalogue MC
             INNER JOIN PRMShapes S ON MC.FKShapeId = S.ShapeId
        ORDER BY NEWID();
        -- Product Data
        SELECT TOP 1 @ProductCatalogueName = ProductCatalogueName, 
                     @OutputThickness = Thickness * 1000, 
                     @OutputWidth = ISNULL(Width, 1) * 1000, 
                     @OutputShapeSymbol = S.ShapeCode
        FROM PRMProductCatalogue PC
             INNER JOIN PRMShapes S ON PC.FKShapeId = S.ShapeId
        -- WHERE ProductCatalogueId=14
        ORDER BY NEWID();
        -- Insert

        INSERT INTO [xfr].[L3L2WorkOrderDefinition]
        ([WorkOrderName], 
         [ExternalWorkOrderName], 
         [PreviousWorkOrderName], 
         [OrderDeadline], 
         [HeatName], 
         [NumberOfBillets], 
         [CustomerName], 
         [BundleWeightMin], 
         [BundleWeightMax], 
         [TargetWorkOrderWeight], 
         [TargetWorkOrderWeightMin], 
         [TargetWorkOrderWeightMax], 
         [MaterialCatalogueName], 
         [ProductCatalogueName], 
         [SteelgradeCode], 
         [InputThickness], 
         [InputWidth], 
         [InputShapeSymbol], 
         [BilletWeight], 
         [BilletLength], 
         [OutputThickness], 
         [OutputWidth], 
         [OutputShapeSymbol]
        )
        VALUES
        (CONCAT('WO', @DateNow), 
         CONCAT('EX_WO', @DateNow), 
         NULL, 
         @OrderDeadline, 
         @HeatName, 
         @NumberOfBillets, 
         @CustomerName, 
         NULL, 
         NULL, 
         @NumberOfBillets * @BilletWeight, 
         @NumberOfBillets * 0.9 * @BilletWeight, 
         @NumberOfBillets * 1.1 * @BilletWeight, 
         @MaterialCatalogueName, 
         @ProductCatalogueName, 
         @SteelgradeCode, 
         @InputThickness, 
         @InputWidth, 
         @InputShapeSymbol, 
         @BilletWeight, 
         @BilletLength, 
         @OutputThickness, 
         @OutputWidth, 
         @OutputShapeSymbol
        );

        --WAITFOR DELAY '00:00:05';
        SELECT *
        FROM [xfr].[L3L2WorkOrderDefinition]
        WHERE WorkOrderName = CONCAT('WO', @DateNow);
    END;