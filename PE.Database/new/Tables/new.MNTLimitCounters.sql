CREATE TABLE [new].[MNTLimitCounters] (
    [LimitCounterId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKQuantityTypeId] BIGINT         NOT NULL,
    [FKEquipmentSetId] BIGINT         NOT NULL,
    [IsActive]         BIT            CONSTRAINT [DF_MNTLimitCounters_IsActive] DEFAULT ((1)) NOT NULL,
    [Value]            FLOAT (53)     CONSTRAINT [DF_MNTLimitCounters_Value] DEFAULT ((0)) NULL,
    [ValueWarning]     FLOAT (53)     CONSTRAINT [DF_MNTLimits_ValueWarning] DEFAULT ((0)) NULL,
    [ValueAlarm]       FLOAT (53)     NULL,
    [ValueMax]         FLOAT (53)     NULL,
    [DateWarningTs]    DATETIME       NULL,
    [DateAlarmTs]      DATETIME       NULL,
    [DateMaxTs]        DATETIME       NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MNTLimitCounters_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MNTLimitCounters_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MNTLimitCounters_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MNTLimitCounters_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MNTLimitCounters_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTLimits] PRIMARY KEY CLUSTERED ([LimitCounterId] ASC),
    CONSTRAINT [FK_MNTLimits_MNTDeviceComponents] FOREIGN KEY ([FKEquipmentSetId]) REFERENCES [new].[MNTEquipmentSets] ([EquipmentSetId]),
    CONSTRAINT [FK_MNTLimits_MNTQuantityTypes] FOREIGN KEY ([FKQuantityTypeId]) REFERENCES [new].[MNTQuantityTypes] ([QuantityTypeId])
);




GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTLimitCounters_Audit] ON new.MNTLimitCounters AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.LimitCounterId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.LimitCounterId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTLimitCounters, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTLimitCounters] SET AUDUpdatedBy = APP_NAME() WHERE LimitCounterId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTLimitCounters] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE LimitCounterId = @RecordId
    END
END;