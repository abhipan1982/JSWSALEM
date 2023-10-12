CREATE TABLE [dbo].[MVHAssetTemplates] (
    [AssetTemplateId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [AssetTemplateName]        NVARCHAR (50)  NOT NULL,
    [AssetTemplateDescription] NVARCHAR (100) NULL,
    [IsArea]                   BIT            CONSTRAINT [DF_MVHAssetTemplates_IsArea] DEFAULT ((0)) NOT NULL,
    [IsZone]                   BIT            CONSTRAINT [DF_MVHAssetTemplates_IsZone] DEFAULT ((0)) NOT NULL,
    [EnumTrackingAreaType]     SMALLINT       CONSTRAINT [DF_MVHAssetTemplates_EnumTrackingAreaType] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]             DATETIME       CONSTRAINT [DF_MVHAssetTemplates_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]         DATETIME       CONSTRAINT [DF_MVHAssetTemplates_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]             NVARCHAR (255) CONSTRAINT [DF_MVHAssetTemplates_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]           BIT            CONSTRAINT [DF_MVHAssetTemplates_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                BIT            CONSTRAINT [DF_MVHAssetTemplates_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHAssetTemplates] PRIMARY KEY CLUSTERED ([AssetTemplateId] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AssetTemplateName]
    ON [dbo].[MVHAssetTemplates]([AssetTemplateName] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHAssetTemplates_Audit] ON [dbo].[MVHAssetTemplates] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.AssetTemplateId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.AssetTemplateId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHAssetTemplates, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHAssetTemplates] SET AUDUpdatedBy = APP_NAME() WHERE AssetTemplateId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHAssetTemplates] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE AssetTemplateId = @RecordId
    END
END;