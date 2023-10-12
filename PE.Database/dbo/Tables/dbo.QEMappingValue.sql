CREATE TABLE [dbo].[QEMappingValue] (
    [MappingValueId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKRuleMappingValueId] BIGINT         NOT NULL,
    [FKMappingEntryId]     BIGINT         NOT NULL,
    [FKRatingId]           BIGINT         NULL,
    [NumValue]             FLOAT (53)     NULL,
    [TextValue]            NVARCHAR (400) NULL,
    [BooleanValue]         BIT            NULL,
    [TimeStampValue]       BIGINT         NULL,
    [RulesObjectValue]     NVARCHAR (MAX) NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_QEMappingValue_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_QEMappingValue_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_QEMappingValue_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_QEMappingValue_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_QEMappingValue_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QEMappingValue] PRIMARY KEY CLUSTERED ([MappingValueId] ASC),
    CONSTRAINT [FK_QEMappingValue_QEMappingEntry] FOREIGN KEY ([FKMappingEntryId]) REFERENCES [dbo].[QEMappingEntry] ([MappingEntryId]),
    CONSTRAINT [FK_QEMappingValue_QERating] FOREIGN KEY ([FKRatingId]) REFERENCES [dbo].[QERating] ([RatingId]),
    CONSTRAINT [FK_QEMappingValue_QERuleMappingValue] FOREIGN KEY ([FKRuleMappingValueId]) REFERENCES [dbo].[QERuleMappingValue] ([RuleMappingValueId]) ON DELETE CASCADE
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QEMappingValue_Audit] ON [dbo].[QEMappingValue] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MappingValueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MappingValueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QEMappingValue, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QEMappingValue] SET AUDUpdatedBy = APP_NAME() WHERE MappingValueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QEMappingValue] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MappingValueId = @RecordId
    END
END;