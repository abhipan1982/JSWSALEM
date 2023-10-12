CREATE TABLE [dbo].[QTYDefectCatalogue] (
    [DefectCatalogueId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKDefectCatalogueCategoryId] BIGINT         NOT NULL,
    [FKParentDefectCatalogueId]   BIGINT         NULL,
    [IsActive]                    BIT            CONSTRAINT [DF_DefectCatalogue_IsActive] DEFAULT ((1)) NOT NULL,
    [IsDefault]                   BIT            CONSTRAINT [DF_DefectCatalogue_IsDefault] DEFAULT ((0)) NOT NULL,
    [DefectCatalogueCode]         NVARCHAR (10)  NOT NULL,
    [DefectCatalogueName]         NVARCHAR (50)  NOT NULL,
    [DefectCatalogueDescription]  NVARCHAR (200) NULL,
    [AUDCreatedTs]                DATETIME       CONSTRAINT [DF_QTYDefectCatalogue_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]            DATETIME       CONSTRAINT [DF_QTYDefectCatalogue_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                NVARCHAR (255) CONSTRAINT [DF_QTYDefectCatalogue_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]              BIT            CONSTRAINT [DF_QTYDefectCatalogue_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                   BIT            CONSTRAINT [DF_QTYDefectCatalogue_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DefectCatalogue] PRIMARY KEY CLUSTERED ([DefectCatalogueId] ASC),
    CONSTRAINT [FK_DefectCatalogue_DefectCatalogue] FOREIGN KEY ([FKParentDefectCatalogueId]) REFERENCES [dbo].[QTYDefectCatalogue] ([DefectCatalogueId]),
    CONSTRAINT [FK_DefectCatalogue_DefectCatalogueCategory] FOREIGN KEY ([FKDefectCatalogueCategoryId]) REFERENCES [dbo].[QTYDefectCatalogueCategory] ([DefectCatalogueCategoryId])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DefectCatalogueCode]
    ON [dbo].[QTYDefectCatalogue]([DefectCatalogueCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParentDefectCatalogueId]
    ON [dbo].[QTYDefectCatalogue]([FKParentDefectCatalogueId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DefectCatalogueCategoryId]
    ON [dbo].[QTYDefectCatalogue]([FKDefectCatalogueCategoryId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QTYDefectCatalogue_Audit] ON [dbo].[QTYDefectCatalogue] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.DefectCatalogueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.DefectCatalogueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QTYDefectCatalogue, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QTYDefectCatalogue] SET AUDUpdatedBy = APP_NAME() WHERE DefectCatalogueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QTYDefectCatalogue] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE DefectCatalogueId = @RecordId
    END
END;