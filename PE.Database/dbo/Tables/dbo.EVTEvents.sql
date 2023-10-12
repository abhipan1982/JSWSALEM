CREATE TABLE [dbo].[EVTEvents] (
    [EventId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKEventTypeId]      BIGINT         NOT NULL,
    [FKEventCatalogueId] BIGINT         NULL,
    [FKShiftCalendarId]  BIGINT         NULL,
    [FKWorkOrderId]      BIGINT         NULL,
    [FKRawMaterialId]    BIGINT         NULL,
    [FKAssetId]          BIGINT         NULL,
    [FKParentEventId]    BIGINT         NULL,
    [FKUserId]           NVARCHAR (450) NULL,
    [EventStartTs]       DATETIME       CONSTRAINT [DF_EVTMillEvents_LastUpdate] DEFAULT (getdate()) NOT NULL,
    [EventEndTs]         DATETIME       NULL,
    [UserCreatedTs]      DATETIME       NULL,
    [UserUpdatedTs]      DATETIME       NULL,
    [UserComment]        NVARCHAR (200) NULL,
    [IsPlanned]          BIT            CONSTRAINT [DF_EVTMillEvents_IsPlanned] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_EVTEvents_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_EVTEvents_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_EVTEvents_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_EVTEvents_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_EVTEvents_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EVTMillEvents] PRIMARY KEY CLUSTERED ([EventId] ASC),
    CONSTRAINT [FK_EVTEvents_EVTEventCatalogue] FOREIGN KEY ([FKEventCatalogueId]) REFERENCES [dbo].[EVTEventCatalogue] ([EventCatalogueId]),
    CONSTRAINT [FK_EVTEvents_EVTEvents] FOREIGN KEY ([FKParentEventId]) REFERENCES [dbo].[EVTEvents] ([EventId]),
    CONSTRAINT [FK_EVTEvents_Users] FOREIGN KEY ([FKUserId]) REFERENCES [smf].[Users] ([Id]),
    CONSTRAINT [FK_EVTMillEvents_EVTEventTypes] FOREIGN KEY ([FKEventTypeId]) REFERENCES [dbo].[EVTEventTypes] ([EventTypeId]),
    CONSTRAINT [FK_EVTMillEvents_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_EVTMillEvents_MVHRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EVTMillEvents_PRMWorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EVTMillEvents_ShiftCalendar] FOREIGN KEY ([FKShiftCalendarId]) REFERENCES [dbo].[EVTShiftCalendar] ([ShiftCalendarId])
);








GO
CREATE NONCLUSTERED INDEX [NCI_FKWorkOrderId]
    ON [dbo].[EVTEvents]([FKWorkOrderId] ASC)
    INCLUDE([FKEventTypeId], [FKEventCatalogueId], [FKShiftCalendarId], [FKRawMaterialId], [FKAssetId], [FKParentEventId], [FKUserId]);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTEvents_Audit] ON [dbo].[EVTEvents] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EventId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EventId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTEvents, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTEvents] SET AUDUpdatedBy = APP_NAME() WHERE EventId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTEvents] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EventId = @RecordId
    END
END;