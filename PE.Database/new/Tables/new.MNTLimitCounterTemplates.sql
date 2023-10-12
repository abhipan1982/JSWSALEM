CREATE TABLE [new].[MNTLimitCounterTemplates] (
    [LimitCounterTemplateId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKQuantityTypeId]       BIGINT         NOT NULL,
    [FKEquipmentTypeId]      BIGINT         NULL,
    [ValueWarning]           FLOAT (53)     NULL,
    [ValueAlarm]             FLOAT (53)     NULL,
    [ValueMax]               FLOAT (53)     NULL,
    [TimeWarning]            INT            NULL,
    [TimeAlarm]              INT            NULL,
    [TimeMax]                INT            NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_MNTLimitCounterTemplates_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_MNTLimitCounterTemplates_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_MNTLimitCounterTemplates_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_MNTLimitCounterTemplates_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_MNTLimitCounterTemplates_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTLimitCounterTemplates] PRIMARY KEY CLUSTERED ([LimitCounterTemplateId] ASC),
    CONSTRAINT [FK_MNTLimitCounterTemplates_MNTEquipmentTypes] FOREIGN KEY ([FKEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId]),
    CONSTRAINT [FK_MNTLimitCounterTemplates_MNTQuantityTypes] FOREIGN KEY ([FKQuantityTypeId]) REFERENCES [new].[MNTQuantityTypes] ([QuantityTypeId])
);




GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTLimitCounterTemplates_Audit] ON new.MNTLimitCounterTemplates AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.LimitCounterTemplateId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.LimitCounterTemplateId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTLimitCounterTemplates, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTLimitCounterTemplates] SET AUDUpdatedBy = APP_NAME() WHERE LimitCounterTemplateId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTLimitCounterTemplates] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE LimitCounterTemplateId = @RecordId
    END
END;