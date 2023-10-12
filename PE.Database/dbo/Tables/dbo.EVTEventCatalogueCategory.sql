CREATE TABLE [dbo].[EVTEventCatalogueCategory] (
    [EventCatalogueCategoryId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKEventCategoryGroupId]            BIGINT         NULL,
    [FKEventTypeId]                     BIGINT         NOT NULL,
    [EventCatalogueCategoryCode]        NVARCHAR (10)  NOT NULL,
    [EventCatalogueCategoryName]        NVARCHAR (50)  NOT NULL,
    [EventCatalogueCategoryDescription] NVARCHAR (100) NULL,
    [IsDefault]                         BIT            CONSTRAINT [DF_DelayCatalogueCategory_IsDefault] DEFAULT ((0)) NOT NULL,
    [EnumAssignmentType]                SMALLINT       CONSTRAINT [DF_DLSDelayCatalogueCategory_EnumAssignmentType] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]                      DATETIME       CONSTRAINT [DF_EVTEventCatalogueCategory_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]                  DATETIME       CONSTRAINT [DF_EVTEventCatalogueCategory_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                      NVARCHAR (255) CONSTRAINT [DF_EVTEventCatalogueCategory_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                    BIT            CONSTRAINT [DF_EVTEventCatalogueCategory_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                         BIT            CONSTRAINT [DF_EVTEventCatalogueCategory_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DelayCatalogueCategory] PRIMARY KEY CLUSTERED ([EventCatalogueCategoryId] ASC),
    CONSTRAINT [FK_DLSDelayCatalogueCategory_DLSDelayCategoryGroups] FOREIGN KEY ([FKEventCategoryGroupId]) REFERENCES [dbo].[EVTEventCategoryGroups] ([EventCategoryGroupId]),
    CONSTRAINT [FK_EVTEventCatalogueCategory_EVTEventTypes] FOREIGN KEY ([FKEventTypeId]) REFERENCES [dbo].[EVTEventTypes] ([EventTypeId])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DelayCatalogueCategoryCode]
    ON [dbo].[EVTEventCatalogueCategory]([EventCatalogueCategoryCode] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTEventCatalogueCategory_Audit] ON [dbo].[EVTEventCatalogueCategory] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EventCatalogueCategoryId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EventCatalogueCategoryId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTEventCatalogueCategory, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTEventCatalogueCategory] SET AUDUpdatedBy = APP_NAME() WHERE EventCatalogueCategoryId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTEventCatalogueCategory] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EventCatalogueCategoryId = @RecordId
    END
END;