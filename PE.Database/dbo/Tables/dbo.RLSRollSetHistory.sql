CREATE TABLE [dbo].[RLSRollSetHistory] (
    [RollSetHistoryId]         BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKRollSetId]              BIGINT         NOT NULL,
    [FKCassetteId]             BIGINT         NULL,
    [CreatedTs]                DATETIME       NULL,
    [MountedTs]                DATETIME       NULL,
    [DismountedTs]             DATETIME       NULL,
    [MountedInMillTs]          DATETIME       NULL,
    [DismountedFromMillTs]     DATETIME       NULL,
    [EnumRollSetHistoryStatus] SMALLINT       CONSTRAINT [DF_RLSRollSetHistory_Status] DEFAULT ((0)) NOT NULL,
    [PositionInCassette]       SMALLINT       NULL,
    [AccWeightLimit]           FLOAT (53)     NULL,
    [AUDCreatedTs]             DATETIME       CONSTRAINT [DF_RLSRollSetHistory_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]         DATETIME       CONSTRAINT [DF_RLSRollSetHistory_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]             NVARCHAR (255) CONSTRAINT [DF_RLSRollSetHistory_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]           BIT            CONSTRAINT [DF_RLSRollSetHistory_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                BIT            CONSTRAINT [DF_RLSRollSetHistory_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RollSetHistory] PRIMARY KEY CLUSTERED ([RollSetHistoryId] ASC),
    CONSTRAINT [FK_RollSetHistory_Cassettes] FOREIGN KEY ([FKCassetteId]) REFERENCES [dbo].[RLSCassettes] ([CassetteId]),
    CONSTRAINT [FK_RollSetHistory_RollSets] FOREIGN KEY ([FKRollSetId]) REFERENCES [dbo].[RLSRollSets] ([RollSetId])
);










GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_RLSRollSetHistory_Audit] ON [dbo].[RLSRollSetHistory] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RollSetHistoryId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RollSetHistoryId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: RLSRollSetHistory, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[RLSRollSetHistory] SET AUDUpdatedBy = APP_NAME() WHERE RollSetHistoryId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[RLSRollSetHistory] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RollSetHistoryId = @RecordId
    END
END;