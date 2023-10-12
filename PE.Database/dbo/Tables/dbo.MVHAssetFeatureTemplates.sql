CREATE TABLE [dbo].[MVHAssetFeatureTemplates] (
    [AssetFeatureTemplateId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKAssetTemplateId]      BIGINT         NOT NULL,
    [FKFeatureTemplateId]    BIGINT         NOT NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_MVHAssetFeatureTemplates_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_MVHAssetFeatureTemplates_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_MVHAssetFeatureTemplates_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_MVHAssetFeatureTemplates_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_MVHAssetFeatureTemplates_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHAssetFeatureTemplates] PRIMARY KEY CLUSTERED ([AssetFeatureTemplateId] ASC),
    CONSTRAINT [FK_MVHAssetFeatureTemplates_MVHAssetTemplates] FOREIGN KEY ([FKAssetTemplateId]) REFERENCES [dbo].[MVHAssetTemplates] ([AssetTemplateId]),
    CONSTRAINT [FK_MVHAssetFeatureTemplates_MVHFeatureTemplates] FOREIGN KEY ([FKFeatureTemplateId]) REFERENCES [dbo].[MVHFeatureTemplates] ([FeatureTemplateId])
);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHAssetFeatureTemplates_Audit] ON [dbo].[MVHAssetFeatureTemplates] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.AssetFeatureTemplateId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.AssetFeatureTemplateId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHAssetFeatureTemplates, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHAssetFeatureTemplates] SET AUDUpdatedBy = APP_NAME() WHERE AssetFeatureTemplateId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHAssetFeatureTemplates] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE AssetFeatureTemplateId = @RecordId
    END
END;