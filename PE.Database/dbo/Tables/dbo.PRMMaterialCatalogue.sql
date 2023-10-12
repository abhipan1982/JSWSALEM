CREATE TABLE [dbo].[PRMMaterialCatalogue] (
    [MaterialCatalogueId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKMaterialCatalogueTypeId]     BIGINT         NOT NULL,
    [FKShapeId]                     BIGINT         NOT NULL,
    [IsActive]                      BIT            CONSTRAINT [DF_RawMaterialCatalogue_Active] DEFAULT ((1)) NOT NULL,
    [IsDefault]                     BIT            CONSTRAINT [DF_RawMaterialCatalogue_IsDefault] DEFAULT ((0)) NOT NULL,
    [MaterialCatalogueName]         NVARCHAR (50)  NOT NULL,
    [MaterialCatalogueDescription]  NVARCHAR (200) NULL,
    [ExternalMaterialCatalogueName] NVARCHAR (50)  NULL,
    [LengthMin]                     FLOAT (53)     NULL,
    [LengthMax]                     FLOAT (53)     NULL,
    [ThicknessMin]                  FLOAT (53)     NOT NULL,
    [ThicknessMax]                  FLOAT (53)     NOT NULL,
    [WidthMin]                      FLOAT (53)     NULL,
    [WidthMax]                      FLOAT (53)     NULL,
    [WeightMin]                     FLOAT (53)     NULL,
    [WeightMax]                     FLOAT (53)     NULL,
    [AUDCreatedTs]                  DATETIME       CONSTRAINT [DF_PRMMaterialCatalogue_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]              DATETIME       CONSTRAINT [DF_PRMMaterialCatalogue_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                  NVARCHAR (255) CONSTRAINT [DF_PRMMaterialCatalogue_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                BIT            CONSTRAINT [DF_PRMMaterialCatalogue_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                     BIT            CONSTRAINT [DF_PRMMaterialCatalogue_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MaterialCatalogueId] PRIMARY KEY CLUSTERED ([MaterialCatalogueId] ASC),
    CONSTRAINT [FK_PRMMaterialCatalogue_PRMMaterialCatalogueTypes] FOREIGN KEY ([FKMaterialCatalogueTypeId]) REFERENCES [dbo].[PRMMaterialCatalogueTypes] ([MaterialCatalogueTypeId]),
    CONSTRAINT [FK_PRMMaterialCatalogue_PRMShapes] FOREIGN KEY ([FKShapeId]) REFERENCES [dbo].[PRMShapes] ([ShapeId]),
    CONSTRAINT [UQ_MaterialCatalogueName] UNIQUE NONCLUSTERED ([MaterialCatalogueName] ASC)
);
























GO





GO
CREATE NONCLUSTERED INDEX [NCI_ShapeId]
    ON [dbo].[PRMMaterialCatalogue]([FKShapeId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_MaterialCatalogueTypeId]
    ON [dbo].[PRMMaterialCatalogue]([FKMaterialCatalogueTypeId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMMaterialCatalogue_Audit] ON [dbo].[PRMMaterialCatalogue] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MaterialCatalogueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MaterialCatalogueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMMaterialCatalogue, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMMaterialCatalogue] SET AUDUpdatedBy = APP_NAME() WHERE MaterialCatalogueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMMaterialCatalogue] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MaterialCatalogueId = @RecordId
    END
END;