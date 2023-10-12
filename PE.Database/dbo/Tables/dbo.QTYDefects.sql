CREATE TABLE [dbo].[QTYDefects] (
    [DefectId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKDefectCatalogueId] BIGINT         NOT NULL,
    [FKRawMaterialId]     BIGINT         NULL,
    [FKProductId]         BIGINT         NULL,
    [FKAssetId]           BIGINT         NULL,
    [DefectName]          NVARCHAR (50)  NULL,
    [DefectDescription]   NVARCHAR (200) NULL,
    [DefectPosition]      SMALLINT       NULL,
    [DefectFrequency]     SMALLINT       NULL,
    [DefectScale]         SMALLINT       NULL,
    [DefectCreatedTs]     DATETIME       CONSTRAINT [DF_QTYDefects_DefectCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_QTYDefects_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_QTYDefects_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_QTYDefects_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_QTYDefects_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_QTYDefects_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Defects] PRIMARY KEY CLUSTERED ([DefectId] ASC),
    CONSTRAINT [FK_Defects_DefectCatalogue] FOREIGN KEY ([FKDefectCatalogueId]) REFERENCES [dbo].[QTYDefectCatalogue] ([DefectCatalogueId]),
    CONSTRAINT [FK_Defects_MVHRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MVHDefects_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_MVHDefects_PRMProducts] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId]) ON DELETE SET NULL
);






GO
CREATE NONCLUSTERED INDEX [NCI_RawMaterialId]
    ON [dbo].[QTYDefects]([FKRawMaterialId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DefectCatalogueId]
    ON [dbo].[QTYDefects]([FKDefectCatalogueId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QTYDefects_Audit] ON [dbo].[QTYDefects] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.DefectId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.DefectId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QTYDefects, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QTYDefects] SET AUDUpdatedBy = APP_NAME() WHERE DefectId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QTYDefects] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE DefectId = @RecordId
    END
END;