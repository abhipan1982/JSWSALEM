CREATE TABLE [dbo].[EVTTriggers] (
    [TriggerId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [TriggerCode]        NVARCHAR (10)  NOT NULL,
    [TriggerName]        NVARCHAR (50)  NOT NULL,
    [TriggerDescription] NVARCHAR (100) NULL,
    [IsActive]           BIT            CONSTRAINT [DF_MVHTriggers_IsActive] DEFAULT ((1)) NOT NULL,
    [EnumTriggerType]    SMALLINT       NOT NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_EVTTriggers_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_EVTTriggers_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_EVTTriggers_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_EVTTriggers_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_EVTTriggers_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHTriggers] PRIMARY KEY CLUSTERED ([TriggerId] ASC)
);






GO
CREATE NONCLUSTERED INDEX [NCI_TriggerType]
    ON [dbo].[EVTTriggers]([EnumTriggerType] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_TriggerCode]
    ON [dbo].[EVTTriggers]([TriggerCode] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTTriggers_Audit] ON [dbo].[EVTTriggers] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.TriggerId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.TriggerId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTTriggers, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTTriggers] SET AUDUpdatedBy = APP_NAME() WHERE TriggerId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTTriggers] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE TriggerId = @RecordId
    END
END;