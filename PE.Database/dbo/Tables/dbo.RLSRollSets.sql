CREATE TABLE [dbo].[RLSRollSets] (
    [RollSetId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKUpperRollId]      BIGINT         NULL,
    [FKBottomRollId]     BIGINT         NULL,
    [FKThirdRollId]      BIGINT         NULL,
    [EnumRollSetStatus]  SMALLINT       NOT NULL,
    [RollSetType]        SMALLINT       NOT NULL,
    [CreatedTs]          DATETIME       NOT NULL,
    [RollSetName]        NVARCHAR (50)  NOT NULL,
    [RollSetDescription] NVARCHAR (100) NULL,
    [IsThirdRoll]        BIT            CONSTRAINT [DF_RLSRollSets_IsThirdRoll] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_RLSRollSets_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_RLSRollSets_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_RLSRollSets_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_RLSRollSets_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_RLSRollSets_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RollSets] PRIMARY KEY CLUSTERED ([RollSetId] ASC),
    CONSTRAINT [FK_RollSets_RollsBottom] FOREIGN KEY ([FKBottomRollId]) REFERENCES [dbo].[RLSRolls] ([RollId]),
    CONSTRAINT [FK_RollSets_RollsThird] FOREIGN KEY ([FKThirdRollId]) REFERENCES [dbo].[RLSRolls] ([RollId]),
    CONSTRAINT [FK_RollSets_RollsUpper] FOREIGN KEY ([FKUpperRollId]) REFERENCES [dbo].[RLSRolls] ([RollId])
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_RLSRollSets_Audit] ON [dbo].[RLSRollSets] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RollSetId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RollSetId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: RLSRollSets, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[RLSRollSets] SET AUDUpdatedBy = APP_NAME() WHERE RollSetId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[RLSRollSets] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RollSetId = @RecordId
    END
END;