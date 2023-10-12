CREATE TABLE [dbo].[QTYDefectCatalogueCategory] (
    [DefectCatalogueCategoryId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKDefectCategoryGroupId]            BIGINT         NULL,
    [IsDefault]                          BIT            CONSTRAINT [DF_DefectCatalogueCategory_IsDefault] DEFAULT ((0)) NOT NULL,
    [DefectCatalogueCategoryCode]        NVARCHAR (10)  NOT NULL,
    [DefectCatalogueCategoryName]        NVARCHAR (50)  NULL,
    [DefectCatalogueCategoryDescription] NVARCHAR (200) NULL,
    [EnumAssignmentType]                 SMALLINT       CONSTRAINT [DF_MVHDefectCatalogueCategory_EnumAssignmentType] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]                       DATETIME       CONSTRAINT [DF_QTYDefectCatalogueCategory_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]                   DATETIME       CONSTRAINT [DF_QTYDefectCatalogueCategory_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                       NVARCHAR (255) CONSTRAINT [DF_QTYDefectCatalogueCategory_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                     BIT            CONSTRAINT [DF_QTYDefectCatalogueCategory_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                          BIT            CONSTRAINT [DF_QTYDefectCatalogueCategory_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DefectCatalogueCategory] PRIMARY KEY CLUSTERED ([DefectCatalogueCategoryId] ASC),
    CONSTRAINT [FK_MVHDefectCatalogueCategory_MVHDefectCategoryGroups] FOREIGN KEY ([FKDefectCategoryGroupId]) REFERENCES [dbo].[QTYDefectCategoryGroups] ([DefectCategoryGroupId])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DefectCatalogueCategoryCode]
    ON [dbo].[QTYDefectCatalogueCategory]([DefectCatalogueCategoryCode] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QTYDefectCatalogueCategory_Audit] ON [dbo].[QTYDefectCatalogueCategory] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.DefectCatalogueCategoryId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.DefectCatalogueCategoryId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QTYDefectCatalogueCategory, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QTYDefectCatalogueCategory] SET AUDUpdatedBy = APP_NAME() WHERE DefectCatalogueCategoryId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QTYDefectCatalogueCategory] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE DefectCatalogueCategoryId = @RecordId
    END
END;