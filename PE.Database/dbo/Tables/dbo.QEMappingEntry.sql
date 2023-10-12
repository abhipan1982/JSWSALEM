CREATE TABLE [dbo].[QEMappingEntry] (
    [MappingEntryId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [SignalIdentifier]     NVARCHAR (255) NOT NULL,
    [RulesIdentifier]      NVARCHAR (400) NOT NULL,
    [RulesIdentifierPart1] NVARCHAR (50)  NULL,
    [RulesIdentifierPart2] NVARCHAR (50)  NULL,
    [RulesIdentifierPart3] NVARCHAR (50)  NULL,
    [EnumQEDirection]      SMALLINT       NOT NULL,
    [EnumQEParamType]      SMALLINT       NOT NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_QEMappingEntry_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_QEMappingEntry_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_QEMappingEntry_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_QEMappingEntry_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_QEMappingEntry_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QEMappingEntry] PRIMARY KEY CLUSTERED ([MappingEntryId] ASC)
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QEMappingEntry_Audit] ON [dbo].[QEMappingEntry] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MappingEntryId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MappingEntryId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QEMappingEntry, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QEMappingEntry] SET AUDUpdatedBy = APP_NAME() WHERE MappingEntryId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QEMappingEntry] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MappingEntryId = @RecordId
    END
END;