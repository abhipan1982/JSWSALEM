CREATE TABLE [dbo].[PPLSchedules] (
    [ScheduleId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKWorkOrderId]    BIGINT         NOT NULL,
    [OrderSeq]         SMALLINT       CONSTRAINT [DF_PPLScheduleItems_OrderSeq] DEFAULT ((0)) NOT NULL,
    [PlannedDuration]  BIGINT         CONSTRAINT [DF_PPLScheduleItems_PlannedTime] DEFAULT ((0)) NOT NULL,
    [PlannedStartTime] DATETIME       NOT NULL,
    [PlannedEndTime]   DATETIME       NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PPLSchedules_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PPLSchedules_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PPLSchedules_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PPLSchedules_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PPLSchedules_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PPLScheduleItems] PRIMARY KEY CLUSTERED ([ScheduleId] ASC),
    CONSTRAINT [FK_PPLSchedules_PRMWorkOrders1] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId]) ON DELETE CASCADE
);


















GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderId]
    ON [dbo].[PPLSchedules]([FKWorkOrderId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PPLSchedules_Audit] ON [dbo].[PPLSchedules] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ScheduleId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ScheduleId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PPLSchedules, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PPLSchedules] SET AUDUpdatedBy = APP_NAME() WHERE ScheduleId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PPLSchedules] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ScheduleId = @RecordId
    END
END;