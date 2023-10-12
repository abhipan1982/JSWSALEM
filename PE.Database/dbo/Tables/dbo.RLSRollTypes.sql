CREATE TABLE [dbo].[RLSRollTypes] (
    [RollTypeId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RollTypeName]        NVARCHAR (50)  NOT NULL,
    [RollTypeDescription] NVARCHAR (100) NULL,
    [DiameterMin]         FLOAT (53)     NULL,
    [DiameterMax]         FLOAT (53)     NULL,
    [RoughnessMin]        FLOAT (53)     NULL,
    [RoughnessMax]        FLOAT (53)     NULL,
    [YieldStrengthRef]    FLOAT (53)     NULL,
    [RollLength]          FLOAT (53)     NULL,
    [AccWeightLimit]      FLOAT (53)     NULL,
    [AccBilletCntLimit]   BIGINT         NULL,
    [MatchingRollsetType] SMALLINT       NULL,
    [RollSteelgrade]      NVARCHAR (30)  NULL,
    [DrawingName]         NVARCHAR (50)  NULL,
    [ChokeType]           NVARCHAR (20)  NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_RLSRollTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_RLSRollTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_RLSRollTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_RLSRollTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_RLSRollTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RollTypes] PRIMARY KEY CLUSTERED ([RollTypeId] ASC),
    CONSTRAINT [UQ_RollType_Name] UNIQUE NONCLUSTERED ([RollTypeName] ASC)
);








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'refers to PE.Core.Constants.RollSetType', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSRollTypes', @level2type = N'COLUMN', @level2name = N'MatchingRollsetType';


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_RLSRollTypes_Audit] ON [dbo].[RLSRollTypes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RollTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RollTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: RLSRollTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[RLSRollTypes] SET AUDUpdatedBy = APP_NAME() WHERE RollTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[RLSRollTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RollTypeId = @RecordId
    END
END;