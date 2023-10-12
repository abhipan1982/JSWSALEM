CREATE TABLE [dbo].[QERuleMappingValue] (
    [RuleMappingValueId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKTriggerId]               BIGINT         NULL,
    [FKRawMaterialId]           BIGINT         NULL,
    [RuleMappingValueInfo]      NVARCHAR (400) NULL,
    [EnumQEEvalExecutionStatus] SMALLINT       CONSTRAINT [DF_QERuleMappingValue_EnumQEEvalExecutionStatus] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]              DATETIME       CONSTRAINT [DF_QERuleMappingValue_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]          DATETIME       CONSTRAINT [DF_QERuleMappingValue_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]              NVARCHAR (255) CONSTRAINT [DF_QERuleMappingValue_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]            BIT            CONSTRAINT [DF_QERuleMappingValue_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                 BIT            CONSTRAINT [DF_QERuleMappingValue_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QERuleMappingValue] PRIMARY KEY CLUSTERED ([RuleMappingValueId] ASC),
    CONSTRAINT [FK_QERuleMappingValue_QETrigger] FOREIGN KEY ([FKTriggerId]) REFERENCES [dbo].[QETrigger] ([TriggerId]),
    CONSTRAINT [FK_QERuleMappingValue_TRKRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QERuleMappingValue_Audit] ON [dbo].[QERuleMappingValue] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RuleMappingValueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RuleMappingValueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QERuleMappingValue, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QERuleMappingValue] SET AUDUpdatedBy = APP_NAME() WHERE RuleMappingValueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QERuleMappingValue] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RuleMappingValueId = @RecordId
    END
END;