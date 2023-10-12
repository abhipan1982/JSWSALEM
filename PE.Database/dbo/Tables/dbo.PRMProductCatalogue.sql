CREATE TABLE [dbo].[PRMProductCatalogue] (
    [ProductCatalogueId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKProductCatalogueTypeId]     BIGINT         NOT NULL,
    [FKShapeId]                    BIGINT         NOT NULL,
    [IsActive]                     BIT            CONSTRAINT [DF_ProductCatalogue_Active] DEFAULT ((1)) NOT NULL,
    [IsDefault]                    BIT            CONSTRAINT [DF_ProductCatalogue_IsDefault] DEFAULT ((0)) NOT NULL,
    [ProductCatalogueName]         NVARCHAR (50)  NOT NULL,
    [ProductCatalogueDescription]  NVARCHAR (200) NULL,
    [ExternalProductCatalogueName] NVARCHAR (50)  NULL,
    [Length]                       FLOAT (53)     NULL,
    [LengthMin]                    FLOAT (53)     NULL,
    [LengthMax]                    FLOAT (53)     NULL,
    [Thickness]                    FLOAT (53)     NOT NULL,
    [ThicknessMin]                 FLOAT (53)     NOT NULL,
    [ThicknessMax]                 FLOAT (53)     NOT NULL,
    [Width]                        FLOAT (53)     NULL,
    [WidthMin]                     FLOAT (53)     NULL,
    [WidthMax]                     FLOAT (53)     NULL,
    [Weight]                       FLOAT (53)     NULL,
    [WeightMin]                    FLOAT (53)     NULL,
    [WeightMax]                    FLOAT (53)     NULL,
    [MaxOvality]                   FLOAT (53)     NULL,
    [StdProductivity]              FLOAT (53)     CONSTRAINT [DF_PRMProductCatalogue_StdProductivity] DEFAULT ((1)) NOT NULL,
    [StdMetallicYield]             FLOAT (53)     CONSTRAINT [DF_PRMProductCatalogue_StdMetallicYield] DEFAULT ((1)) NOT NULL,
    [AUDCreatedTs]                 DATETIME       CONSTRAINT [DF_PRMProductCatalogue_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]             DATETIME       CONSTRAINT [DF_PRMProductCatalogue_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                 NVARCHAR (255) CONSTRAINT [DF_PRMProductCatalogue_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]               BIT            CONSTRAINT [DF_PRMProductCatalogue_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                    BIT            CONSTRAINT [DF_PRMProductCatalogue_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProductCatalogueId] PRIMARY KEY CLUSTERED ([ProductCatalogueId] ASC),
    CONSTRAINT [FK_PRMProductCatalogue_PRMShapes] FOREIGN KEY ([FKShapeId]) REFERENCES [dbo].[PRMShapes] ([ShapeId]),
    CONSTRAINT [FK_ProductCatalogue_ProductTypeId] FOREIGN KEY ([FKProductCatalogueTypeId]) REFERENCES [dbo].[PRMProductCatalogueTypes] ([ProductCatalogueTypeId])
);
























GO





GO
CREATE NONCLUSTERED INDEX [NCI_ShapeId]
    ON [dbo].[PRMProductCatalogue]([FKShapeId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ProductCatalogueTypeId]
    ON [dbo].[PRMProductCatalogue]([FKProductCatalogueTypeId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductCatalogueName]
    ON [dbo].[PRMProductCatalogue]([ProductCatalogueName] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMProductCatalogue_Audit] ON [dbo].[PRMProductCatalogue] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ProductCatalogueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ProductCatalogueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMProductCatalogue, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMProductCatalogue] SET AUDUpdatedBy = APP_NAME() WHERE ProductCatalogueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMProductCatalogue] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ProductCatalogueId = @RecordId
    END
END;