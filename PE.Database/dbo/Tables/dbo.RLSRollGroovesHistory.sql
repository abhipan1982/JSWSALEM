CREATE TABLE [dbo].[RLSRollGroovesHistory] (
    [RollGrooveHistoryId]  BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKRollId]             BIGINT         NOT NULL,
    [FKGrooveTemplateId]   BIGINT         NOT NULL,
    [FKRollSetHistoryId]   BIGINT         NULL,
    [GrooveNumber]         SMALLINT       CONSTRAINT [DF_RollGrooves_GrooveNumber] DEFAULT ((0)) NOT NULL,
    [EnumRollGrooveStatus] SMALLINT       CONSTRAINT [DF_RLSRollGroovesHistory_Status] DEFAULT ((0)) NOT NULL,
    [EnumGrooveCondition]  SMALLINT       CONSTRAINT [DF_RLSRollGroovesHistory_EnumGrooveCondition] DEFAULT ((1)) NOT NULL,
    [CreatedTs]            DATETIME       NULL,
    [ActivatedTs]          DATETIME       NULL,
    [DeactivatedTs]        DATETIME       NULL,
    [AccWeight]            FLOAT (53)     CONSTRAINT [DF_RollGroovesHistory_AccWeight] DEFAULT ((0)) NOT NULL,
    [AccWeightWithCoeff]   FLOAT (53)     NULL,
    [AccBilletCnt]         BIGINT         CONSTRAINT [DF_RollGroovesHistory_AccBilletCnt] DEFAULT ((0)) NOT NULL,
    [ActDiameter]          FLOAT (53)     NULL,
    [Remarks]              NVARCHAR (255) NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_RLSRollGroovesHistory_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_RLSRollGroovesHistory_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_RLSRollGroovesHistory_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_RLSRollGroovesHistory_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_RLSRollGroovesHistory_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RollGroovesHistory] PRIMARY KEY CLUSTERED ([RollGrooveHistoryId] ASC),
    CONSTRAINT [FK_RollGrooves_GrooveTemplates] FOREIGN KEY ([FKGrooveTemplateId]) REFERENCES [dbo].[RLSGrooveTemplates] ([GrooveTemplateId]),
    CONSTRAINT [FK_RollGroovesHistory_Rolls] FOREIGN KEY ([FKRollId]) REFERENCES [dbo].[RLSRolls] ([RollId]),
    CONSTRAINT [FK_RollGroovesHistory_RollSetHistory] FOREIGN KEY ([FKRollSetHistoryId]) REFERENCES [dbo].[RLSRollSetHistory] ([RollSetHistoryId]) ON DELETE CASCADE
);












GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_RLSRollGroovesHistory_Audit] ON [dbo].[RLSRollGroovesHistory] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RollGrooveHistoryId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RollGrooveHistoryId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: RLSRollGroovesHistory, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[RLSRollGroovesHistory] SET AUDUpdatedBy = APP_NAME() WHERE RollGrooveHistoryId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[RLSRollGroovesHistory] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RollGrooveHistoryId = @RecordId
    END
END;