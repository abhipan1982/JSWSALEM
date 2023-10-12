CREATE TABLE [dbo].[PRFKPIValues] (
    [KPIValueId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [KPITime]           DATETIME       CONSTRAINT [DF_PRFKPIValues_KPITime] DEFAULT (getdate()) NOT NULL,
    [KPIValue]          FLOAT (53)     NOT NULL,
    [FKKPIDefinitionId] BIGINT         NOT NULL,
    [FKWorkOrderId]     BIGINT         NULL,
    [FKShiftCalendarId] BIGINT         NULL,
    [AUDCreatedTs]      DATETIME       CONSTRAINT [DF_PRFKPIValues_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       CONSTRAINT [DF_PRFKPIValues_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (255) CONSTRAINT [DF_PRFKPIValues_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]    BIT            CONSTRAINT [DF_PRFKPIValues_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_PRFKPIValues_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_KPIValues] PRIMARY KEY CLUSTERED ([KPIValueId] ASC),
    CONSTRAINT [FK_PRFKPIValues_EVTShiftCalendar] FOREIGN KEY ([FKShiftCalendarId]) REFERENCES [dbo].[EVTShiftCalendar] ([ShiftCalendarId]),
    CONSTRAINT [FKKPIDefinitionId] FOREIGN KEY ([FKKPIDefinitionId]) REFERENCES [dbo].[PRFKPIDefinitions] ([KPIDefinitionId]),
    CONSTRAINT [FKWorkOrderId] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId]) ON DELETE CASCADE
);












GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRFKPIValues_Audit] ON [dbo].[PRFKPIValues] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.KPIValueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.KPIValueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRFKPIValues, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRFKPIValues] SET AUDUpdatedBy = APP_NAME() WHERE KPIValueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRFKPIValues] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE KPIValueId = @RecordId
    END
END;