CREATE TABLE [xfr].[L3L2WorkOrderDefinition] (
    [CounterId]                BIGINT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUDCreatedTs]             DATETIME        CONSTRAINT [DF_L3L2WorkOrderDefinition_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [CreatedTs]                DATETIME        CONSTRAINT [DF_L3L2WorkOrderDefinition_CreatedTs_1] DEFAULT (getdate()) NOT NULL,
    [UpdatedTs]                DATETIME        CONSTRAINT [DF_L3L2WorkOrderDefinition_UpdatedTs] DEFAULT (getdate()) NOT NULL,
    [IsUpdated]                BIT             CONSTRAINT [DF_L3L2WorkOrderDefinition_IsUpdated] DEFAULT ((0)) NOT NULL,
    [CommStatus]               SMALLINT        CONSTRAINT [DF_L3L2WorkOrderDefinition_CommStatus] DEFAULT ((0)) NOT NULL,
    [CommMessage]              NVARCHAR (400)  NULL,
    [ValidationCheck]          NVARCHAR (4000) NULL,
    [WorkOrderName]            NVARCHAR (50)   NULL,
    [ExternalWorkOrderName]    NVARCHAR (50)   NULL,
    [PreviousWorkOrderName]    NVARCHAR (50)   NULL,
    [OrderDeadline]            NVARCHAR (8)    NULL,
    [HeatName]                 NVARCHAR (50)   NULL,
    [NumberOfBillets]          NVARCHAR (3)    NULL,
    [CustomerName]             NVARCHAR (50)   NULL,
    [BundleWeightMin]          NVARCHAR (10)   NULL,
    [BundleWeightMax]          NVARCHAR (10)   NULL,
    [TargetWorkOrderWeight]    NVARCHAR (10)   NULL,
    [TargetWorkOrderWeightMin] NVARCHAR (10)   NULL,
    [TargetWorkOrderWeightMax] NVARCHAR (10)   NULL,
    [MaterialCatalogueName]    NVARCHAR (50)   NULL,
    [ProductCatalogueName]     NVARCHAR (50)   NULL,
    [SteelgradeCode]           NVARCHAR (50)   NULL,
    [InputThickness]           NVARCHAR (10)   NULL,
    [InputWidth]               NVARCHAR (10)   NULL,
    [InputShapeSymbol]         NVARCHAR (10)   NULL,
    [BilletWeight]             NVARCHAR (10)   NULL,
    [BilletLength]             NVARCHAR (10)   NULL,
    [OutputThickness]          NVARCHAR (10)   NULL,
    [OutputWidth]              NVARCHAR (10)   NULL,
    [OutputShapeSymbol]        NVARCHAR (10)   NULL,
    CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED ([CounterId] ASC)
);


















GO

GO
CREATE NONCLUSTERED INDEX [NCI_CommStatus_UpdatedTs]
    ON [xfr].[L3L2WorkOrderDefinition]([CommStatus] ASC, [UpdatedTs] ASC)
    INCLUDE([AUDCreatedTs], [CommMessage], [WorkOrderName], [CustomerName], [ValidationCheck], [SteelgradeCode]);




GO
CREATE NONCLUSTERED INDEX [NCI_WorkOrderName_CounterId]
    ON [xfr].[L3L2WorkOrderDefinition]([WorkOrderName] ASC, [CounterId] ASC);


GO
CREATE   TRIGGER [xfr].[L3L2WorkOrderDefinitionValidate] 
   ON  [xfr].[L3L2WorkOrderDefinition]
   AFTER INSERT, UPDATE
   --remember that it works if only one row per time is inserted
AS 
BEGIN
	SET NOCOUNT ON;
	DECLARE @MaterialCatalogueIsUsed BIT;
	DECLARE @ProductCatalogueIsUsed BIT;

	DECLARE @CounterId BIGINT;
	DECLARE @CommStatus SMALLINT;
	DECLARE @ValidationCheck NVARCHAR(4000);
	DECLARE @NewLine NVARCHAR(2)=CHAR(13) + CHAR(10);

	DECLARE @WorkOrderName nvarchar(50);
	DECLARE @PreviousWorkOrderName nvarchar(50);

	DECLARE @SteelgradeCode nvarchar(10);
	
	DECLARE @MaterialCatalogueName nvarchar(50);
	DECLARE @ProductCatalogueName nvarchar(50);

	DECLARE @CHK_Check BIT=0;
	DECLARE @CHK_CommStatus BIT=0;

	DECLARE @CHK_WorkOrderName BIT=0;
	DECLARE @CHK_PreviousWorkOrderName BIT=0;
	DECLARE @CHK_OrderDeadline BIT=0;
	DECLARE @CHK_HeatName BIT=0;
	DECLARE @CHK_BilletWeight BIT=0;
	DECLARE @CHK_NumberOfBillets BIT=0;
	DECLARE @CHK_BundleWeightMin BIT=0;
	DECLARE @CHK_BundleWeightMax BIT=0;
	DECLARE @CHK_TargetWorkOrderWeight BIT=0;
	DECLARE @CHK_MaterialCatalogueName BIT=0;
	DECLARE @CHK_ProductCatalogueName BIT=0;
	DECLARE @CHK_SteelgradeCode BIT=0;
	
	DECLARE @CHK_InputThickness BIT=0;
	DECLARE @CHK_InputWidth BIT=0;
	DECLARE @CHK_InputShapeSymbol BIT=0;

	DECLARE @CHK_OutputThickness BIT=0;
	DECLARE @CHK_OutputWidth BIT=0;
	DECLARE @CHK_OutputShapeSymbol BIT=0;

	SET @MaterialCatalogueIsUsed =
        (
            SELECT CAST(ValueInt AS BIT)
            FROM [smf].[Parameters]
            WHERE [Name] = 'MaterialCatalogueIsUsed'
        );
	SET @ProductCatalogueIsUsed =
        (
            SELECT CAST(ValueInt AS BIT)
            FROM [smf].[Parameters]
            WHERE [Name] = 'ProductCatalogueIsUsed'
        );

-- Checking every single rules
SELECT 
	 @CounterId = [CounterId], @CommStatus =[CommStatus]
	,@WorkorderName = [WorkOrderName], @PreviousWorkOrderName = [PreviousWorkOrderName]
	,@MaterialCatalogueName = [MaterialCatalogueName], @ProductCatalogueName = [ProductCatalogueName], @SteelgradeCode = [SteelgradeCode]
	,@CHK_CommStatus = CASE WHEN ([CommStatus] >= (-2) AND [CommStatus] <= (2)) THEN 1 ELSE 0 END
	,@CHK_WorkOrderName = CASE WHEN ([WorkOrderName] IS NOT NULL) THEN 1 ELSE 0 END
	,@CHK_OrderDeadline = CASE WHEN ISNUMERIC([OrderDeadline]) = 1 THEN 1 ELSE 0 END
	,@CHK_HeatName = CASE WHEN ([HeatName] IS NOT NULL) THEN 1 ELSE 0 END
	,@CHK_BilletWeight = CASE WHEN ISNUMERIC([BilletWeight]) = 1 AND [BilletWeight]>0 THEN 1 ELSE 0 END
	,@CHK_NumberOfBillets = CASE WHEN ISNUMERIC([NumberOfBillets]) = 1 AND [NumberOfBillets] > 0 THEN 1 ELSE 0 END
	,@CHK_BundleWeightMin = CASE WHEN [BundleWeightMin] IS NULL THEN 1 
		WHEN ISNUMERIC([BundleWeightMin])=1 AND [BundleWeightMin] > 0 THEN 1 ELSE 0 END
	,@CHK_BundleWeightMax = CASE WHEN [BundleWeightMax] IS NULL THEN 1 
		WHEN ISNUMERIC([BundleWeightMax])=1 AND [BundleWeightMax] > 0 THEN 1 ELSE 0 END
		
	,@CHK_TargetWorkOrderWeight = CASE WHEN ISNUMERIC([TargetWorkOrderWeight]) = 1 AND [TargetWorkOrderWeight]>0 THEN 
		CASE WHEN CAST([TargetWorkOrderWeight] AS FLOAT) BETWEEN CAST([BilletWeight] AS FLOAT) * 1.0 * [NumberOfBillets] * 0.9 AND CAST([BilletWeight] AS FLOAT) * 1.0 * [NumberOfBillets] * 1.1 THEN 1 ELSE 0 END ELSE 0 END
		
	,@CHK_InputThickness = CASE WHEN @MaterialCatalogueIsUsed = 1 THEN 1
		WHEN @MaterialCatalogueIsUsed = 0 AND ISNUMERIC([InputThickness]) = 1 AND CAST(REPLACE([InputThickness], ',', '.') AS FLOAT) > 0 THEN 1 ELSE 0 END
	,@CHK_InputWidth = CASE WHEN @MaterialCatalogueIsUsed = 1 THEN 1 
		WHEN @MaterialCatalogueIsUsed = 0 AND ISNUMERIC([InputWidth]) = 1 AND CAST(REPLACE([InputWidth], ',', '.') AS FLOAT) > 0 THEN 1 ELSE 0 END
	,@CHK_InputShapeSymbol = CASE WHEN @MaterialCatalogueIsUsed = 1 THEN 1 WHEN @MaterialCatalogueIsUsed = 0 AND [InputShapeSymbol] IS NOT NULL THEN 1 ELSE 0 END
	,@CHK_OutputThickness = CASE WHEN @ProductCatalogueIsUsed = 1 THEN 1
		WHEN @ProductCatalogueIsUsed = 0 AND ISNUMERIC([OutputThickness])=1 AND CAST(REPLACE([OutputThickness], ',', '.') AS FLOAT) > 0 THEN 1 ELSE 0 END
	,@CHK_OutputWidth = CASE WHEN @ProductCatalogueIsUsed = 1 THEN 1 
		WHEN @ProductCatalogueIsUsed = 0 AND ISNUMERIC([OutputWidth]) = 1 AND CAST(REPLACE([OutputWidth], ',', '.') AS FLOAT) > 0 THEN 1 ELSE 0 END
	,@CHK_OutputShapeSymbol = CASE WHEN @ProductCatalogueIsUsed = 1 THEN 1 WHEN @ProductCatalogueIsUsed = 0 AND [OutputShapeSymbol] IS NOT NULL THEN 1 ELSE 0 END
FROM 
	INSERTED

--If Material Catalogue Name exists in Table
IF @MaterialCatalogueIsUsed = 0 SET @CHK_MaterialCatalogueName=1
ELSE IF EXISTS (SELECT 1 FROM PRMMaterialCatalogue WHERE MaterialCatalogueName=@MaterialCatalogueName) 
	SET @CHK_MaterialCatalogueName=1 
ELSE SET @CHK_MaterialCatalogueName=0

--If Product Catalogue Name exists in Table
IF @ProductCatalogueIsUsed = 0 SET @CHK_ProductCatalogueName=1
ELSE IF EXISTS (SELECT 1 FROM PRMProductCatalogue WHERE ProductCatalogueName=@ProductCatalogueName) 
	SET @CHK_ProductCatalogueName=1 
ELSE SET @CHK_ProductCatalogueName=0

--If Steelgrade Code exists in Table
IF EXISTS (SELECT 1 FROM PRMSteelgrades WHERE SteelgradeCode=@SteelgradeCode) SET @CHK_SteelgradeCode=1 
ELSE SET @CHK_SteelgradeCode=0

IF @PreviousWorkOrderName IS NULL SET @CHK_PreviousWorkOrderName=1
ELSE IF EXISTS  (SELECT 1 FROM PRMWorkOrders WHERE WorkOrderName=@PreviousWorkOrderName) SET @CHK_PreviousWorkOrderName=1 
ELSE SET @CHK_PreviousWorkOrderName=0

--If any has 0 then main Check = 0
SET @CHK_Check = 
	CAST(@CHK_CommStatus AS INT) * 
	CAST(@CHK_WorkOrderName AS INT) * 
	CAST(@CHK_PreviousWorkOrderName AS INT) * 
	CAST(@CHK_OrderDeadline AS INT) * 
	CAST(@CHK_HeatName AS INT) * 
	CAST(@CHK_BilletWeight AS INT) * 
	CAST(@CHK_NumberOfBillets AS INT) * 
	CAST(@CHK_BundleWeightMin AS INT) * 
	CAST(@CHK_BundleWeightMax AS INT) * 
	CAST(@CHK_TargetWorkOrderWeight AS INT) * 
	CAST(@CHK_MaterialCatalogueName AS INT) * 
	CAST(@CHK_ProductCatalogueName AS INT) * 
	CAST(@CHK_SteelgradeCode AS INT) * 
	CAST(@CHK_InputThickness AS INT) * 
	CAST(@CHK_InputWidth AS INT) * 
	CAST(@CHK_InputShapeSymbol AS INT) *
	CAST(@CHK_OutputThickness AS INT) * 
	CAST(@CHK_OutputWidth AS INT) * 
	CAST(@CHK_OutputShapeSymbol AS INT)

-- Preparing JSON to HMI 1 = OK, 0 = fault
SET @ValidationCheck = CONCAT('{ ', '"CommStatus": ',@CHK_CommStatus,
							', "WorkOrderName": ',@CHK_WorkOrderName,
							', "PreviousWorkOrderName": ',@CHK_PreviousWorkOrderName,
							', "OrderDeadline": ',@CHK_OrderDeadline,
							', "HeatName": ',@CHK_HeatName,
							', "BilletWeight": ',@CHK_BilletWeight,
							', "NumberOfBillets": ',@CHK_NumberOfBillets,
							', "BundleWeightMin": ',@CHK_BundleWeightMin,
							', "BundleWeightMax": ',@CHK_BundleWeightMax,
							', "TargetWorkOrderWeight": ',@CHK_TargetWorkOrderWeight,
							', "MaterialCatalogueName": ',@CHK_MaterialCatalogueName,
							', "ProductCatalogueName": ',@CHK_ProductCatalogueName,
							', "SteelgradeCode": ',@CHK_SteelgradeCode,
							', "InputThickness": ',@CHK_InputThickness,
							', "InputWidth": ',@CHK_InputWidth,
							', "InputShapeSymbol": ', @CHK_InputShapeSymbol,
							', "OutputThickness": ',@CHK_OutputThickness,
							', "OutputWidth": ',@CHK_OutputWidth,
							', "OutputShapeSymbol": ', @CHK_OutputShapeSymbol,
							'}');
--If any check has 0 then CommStatus = -1
IF(@CommStatus = 0)
    BEGIN
        IF(@CHK_Check = 0)
            BEGIN
                SET @CommStatus = -1;
        END;
            ELSE
            BEGIN
                SET @CommStatus = 0;
        END;
		-- fill validation check and comm status if new row
        UPDATE [xfr].[L3L2WorkOrderDefinition]
          SET 
              ValidationCheck = @ValidationCheck, 
              CommStatus = @CommStatus, 
			  IsUpdated = 0,
			  UpdatedTs = GETDATE()
        WHERE CounterId = @CounterId;
		-- update only validation check if WO exists before
        UPDATE [xfr].[L3L2WorkOrderDefinition]
          SET 
			  ValidationCheck = @ValidationCheck, 
			  IsUpdated = 1,
              UpdatedTs = GETDATE()
        WHERE WorkOrderName = @WorkOrderName
              AND CounterId != @CounterId;
END;
    ELSE
    BEGIN
        SET @CommStatus = @CommStatus;
END;

END;