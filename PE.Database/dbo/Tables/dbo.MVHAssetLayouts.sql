CREATE TABLE [dbo].[MVHAssetLayouts] (
    [AssetLayoutId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKLayoutId]       BIGINT         NOT NULL,
    [FKPrevAssetId]    BIGINT         NOT NULL,
    [FKNextAssetId]    BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MVHAssetLayouts_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MVHAssetLayouts_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MVHAssetLayouts_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MVHAssetLayouts_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MVHAssetLayouts_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHAssetLayouts] PRIMARY KEY CLUSTERED ([AssetLayoutId] ASC),
    CONSTRAINT [FK_MVHAssetLayouts_MVHAssets] FOREIGN KEY ([FKPrevAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_MVHAssetLayouts_MVHAssets1] FOREIGN KEY ([FKNextAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_MVHAssetLayouts_MVHLayouts] FOREIGN KEY ([FKLayoutId]) REFERENCES [dbo].[MVHLayouts] ([LayoutId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHAssetLayouts_Audit] ON [dbo].[MVHAssetLayouts] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.AssetLayoutId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.AssetLayoutId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHAssetLayouts, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHAssetLayouts] SET AUDUpdatedBy = APP_NAME() WHERE AssetLayoutId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHAssetLayouts] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE AssetLayoutId = @RecordId
    END
END;