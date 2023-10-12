CREATE TABLE [dbo].[MNTMaintenanceActions] (
    [MaintenanceActionId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKMaintenanceScheduleId] BIGINT         NOT NULL,
    [FKMemberId]              BIGINT         NOT NULL,
    [ActionStatus]            SMALLINT       CONSTRAINT [DF_MNTMaintenanceActions_ActionStatus] DEFAULT ((0)) NOT NULL,
    [ActionName]              NVARCHAR (50)  NOT NULL,
    [ActionDescription]       NVARCHAR (100) NULL,
    [AUDCreatedTs]            DATETIME       CONSTRAINT [DF_MNTMaintenanceActions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]        DATETIME       CONSTRAINT [DF_MNTMaintenanceActions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]            NVARCHAR (255) CONSTRAINT [DF_MNTMaintenanceActions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]          BIT            CONSTRAINT [DF_MNTMaintenanceActions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_MNTMaintenanceActions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [FK_MNTMaintenanceActions_MNTMaintenanceSchedules] FOREIGN KEY ([FKMaintenanceScheduleId]) REFERENCES [dbo].[MNTMaintenanceSchedules] ([MaintenanceScheduleId]),
    CONSTRAINT [FK_MNTMaintenanceActions_MNTMembers] FOREIGN KEY ([FKMemberId]) REFERENCES [dbo].[MNTMembers] ([MemberId])
);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MNTMaintenanceActions_Audit] ON [dbo].[MNTMaintenanceActions] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MaintenanceActionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MaintenanceActionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTMaintenanceActions, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTMaintenanceActions] SET AUDUpdatedBy = APP_NAME() WHERE MaintenanceActionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTMaintenanceActions] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MaintenanceActionId = @RecordId
    END
END;