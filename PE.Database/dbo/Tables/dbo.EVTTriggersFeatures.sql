CREATE TABLE [dbo].[EVTTriggersFeatures] (
    [TriggersFeatureId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKTriggerId]       BIGINT         NOT NULL,
    [FKFeatureId]       BIGINT         NOT NULL,
    [PassNo]            SMALLINT       CONSTRAINT [DF_MVHTriggersFeatures_PassNo] DEFAULT ((1)) NOT NULL,
    [OrderSeq]          SMALLINT       CONSTRAINT [DF_MVHTriggersFeatures_OrderSeq] DEFAULT ((1)) NOT NULL,
    [Relations]         NVARCHAR (50)  NULL,
    [AUDCreatedTs]      DATETIME       CONSTRAINT [DF_EVTTriggersFeatures_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       CONSTRAINT [DF_EVTTriggersFeatures_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (255) CONSTRAINT [DF_EVTTriggersFeatures_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]    BIT            CONSTRAINT [DF_EVTTriggersFeatures_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_EVTTriggersFeatures_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHTriggersFeatures] PRIMARY KEY CLUSTERED ([TriggersFeatureId] ASC),
    CONSTRAINT [FK_MVHTriggersFeatures_MVHFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]),
    CONSTRAINT [FK_MVHTriggersFeatures_MVHTriggers] FOREIGN KEY ([FKTriggerId]) REFERENCES [dbo].[EVTTriggers] ([TriggerId])
);






GO
CREATE NONCLUSTERED INDEX [NCI_TriggerId]
    ON [dbo].[EVTTriggersFeatures]([FKTriggerId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_FeatureId]
    ON [dbo].[EVTTriggersFeatures]([FKFeatureId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTTriggersFeatures_Audit] ON [dbo].[EVTTriggersFeatures] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.TriggersFeatureId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.TriggersFeatureId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTTriggersFeatures, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTTriggersFeatures] SET AUDUpdatedBy = APP_NAME() WHERE TriggersFeatureId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTTriggersFeatures] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE TriggersFeatureId = @RecordId
    END
END;