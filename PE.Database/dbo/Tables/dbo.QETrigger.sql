CREATE TABLE [dbo].[QETrigger] (
    [TriggerId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [TriggerName]        NVARCHAR (50)  NOT NULL,
    [TriggerDescription] NVARCHAR (50)  NULL,
    [FKAssetId]          BIGINT         NULL,
    [IsActive]           BIT            CONSTRAINT [DF_QETrigger_IsActive] DEFAULT ((1)) NOT NULL,
    [LastTriggerVersion] NVARCHAR (255) NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_QETrigger_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_QETrigger_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_QETrigger_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_QETrigger_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_QETrigger_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TPQCTrigger] PRIMARY KEY CLUSTERED ([TriggerId] ASC),
    CONSTRAINT [FK_QETrigger_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QETrigger_Audit] ON [dbo].[QETrigger] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.TriggerId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.TriggerId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QETrigger, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QETrigger] SET AUDUpdatedBy = APP_NAME() WHERE TriggerId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QETrigger] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE TriggerId = @RecordId
    END
END;