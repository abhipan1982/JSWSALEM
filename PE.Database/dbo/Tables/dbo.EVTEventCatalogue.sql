CREATE TABLE [dbo].[EVTEventCatalogue] (
    [EventCatalogueId]           BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKParentEventCatalogueId]   BIGINT         NULL,
    [FKEventCatalogueCategoryId] BIGINT         NOT NULL,
    [EventCatalogueCode]         NVARCHAR (10)  NOT NULL,
    [EventCatalogueName]         NVARCHAR (50)  NOT NULL,
    [EventCatalogueDescription]  NVARCHAR (100) NULL,
    [IsActive]                   BIT            CONSTRAINT [DF_DelayCatalogue_IsActive] DEFAULT ((1)) NOT NULL,
    [IsDefault]                  BIT            CONSTRAINT [DF_DelayCatalogue_IsDefault] DEFAULT ((0)) NOT NULL,
    [StdEventTime]               FLOAT (53)     CONSTRAINT [DF_DelayCatalogue_StdDelayTime] DEFAULT ((0)) NOT NULL,
    [IsPlanned]                  BIT            CONSTRAINT [DF_DLSDelayCatalogue_IsPlanned] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]               DATETIME       CONSTRAINT [DF_EVTEventCatalogue_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]           DATETIME       CONSTRAINT [DF_EVTEventCatalogue_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]               NVARCHAR (255) CONSTRAINT [DF_EVTEventCatalogue_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]             BIT            CONSTRAINT [DF_EVTEventCatalogue_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                  BIT            CONSTRAINT [DF_EVTEventCatalogue_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DelayCatalogue] PRIMARY KEY CLUSTERED ([EventCatalogueId] ASC),
    CONSTRAINT [FK_DelayCatalogue] FOREIGN KEY ([FKParentEventCatalogueId]) REFERENCES [dbo].[EVTEventCatalogue] ([EventCatalogueId]),
    CONSTRAINT [FK_DelayCatalogue_DelayCatalogueCategory] FOREIGN KEY ([FKEventCatalogueCategoryId]) REFERENCES [dbo].[EVTEventCatalogueCategory] ([EventCatalogueCategoryId])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DelayCatalogueCode]
    ON [dbo].[EVTEventCatalogue]([EventCatalogueCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParentDelayCatalogueId]
    ON [dbo].[EVTEventCatalogue]([FKParentEventCatalogueId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DelayCatalogueCategoryId]
    ON [dbo].[EVTEventCatalogue]([FKEventCatalogueCategoryId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTEventCatalogue_Audit] ON [dbo].[EVTEventCatalogue] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EventCatalogueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EventCatalogueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTEventCatalogue, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTEventCatalogue] SET AUDUpdatedBy = APP_NAME() WHERE EventCatalogueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTEventCatalogue] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EventCatalogueId = @RecordId
    END
END;