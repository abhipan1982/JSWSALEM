CREATE TABLE [dbo].[MNTMaintenanceSchedules] (
    [MaintenanceScheduleId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [PlannedStartTime]               DATETIME       NOT NULL,
    [PlannedEndTime]                 DATETIME       NOT NULL,
    [StartTime]                      DATETIME       NULL,
    [EndTime]                        DATETIME       NULL,
    [MaintenenaceScheduleStatus]     SMALLINT       NOT NULL,
    [MaintenanceScheduleName]        NVARCHAR (50)  NULL,
    [MaintenanceScheduleDescription] NVARCHAR (100) NULL,
    [FKEquipmentId]                  BIGINT         NOT NULL,
    [AUDCreatedTs]                   DATETIME       CONSTRAINT [DF_MNTMaintenanceSchedules_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]               DATETIME       CONSTRAINT [DF_MNTMaintenanceSchedules_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                   NVARCHAR (255) CONSTRAINT [DF_MNTMaintenanceSchedules_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                 BIT            CONSTRAINT [DF_MNTMaintenanceSchedules_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                      BIT            CONSTRAINT [DF_MNTMaintenanceSchedules_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTMaintenanceSchedules] PRIMARY KEY CLUSTERED ([MaintenanceScheduleId] ASC),
    CONSTRAINT [FK_MNTMaintenanceSchedules_MNTEquipments] FOREIGN KEY ([FKEquipmentId]) REFERENCES [dbo].[MNTEquipments] ([EquipmentId])
);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MNTMaintenanceSchedules_Audit] ON [dbo].[MNTMaintenanceSchedules] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MaintenanceScheduleId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MaintenanceScheduleId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTMaintenanceSchedules, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTMaintenanceSchedules] SET AUDUpdatedBy = APP_NAME() WHERE MaintenanceScheduleId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTMaintenanceSchedules] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MaintenanceScheduleId = @RecordId
    END
END;