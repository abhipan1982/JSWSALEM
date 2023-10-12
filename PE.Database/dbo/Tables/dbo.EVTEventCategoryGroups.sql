CREATE TABLE [dbo].[EVTEventCategoryGroups] (
    [EventCategoryGroupId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [EventCategoryGroupCode] NVARCHAR (10)  NOT NULL,
    [EventCategoryGroupName] NVARCHAR (50)  NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_EVTEventCategoryGroups_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_EVTEventCategoryGroups_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_EVTEventCategoryGroups_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_EVTEventCategoryGroups_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_EVTEventCategoryGroups_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DLSDelayCategoryGroups] PRIMARY KEY CLUSTERED ([EventCategoryGroupId] ASC)
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTEventCategoryGroups_Audit] ON [dbo].[EVTEventCategoryGroups] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EventCategoryGroupId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EventCategoryGroupId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTEventCategoryGroups, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTEventCategoryGroups] SET AUDUpdatedBy = APP_NAME() WHERE EventCategoryGroupId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTEventCategoryGroups] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EventCategoryGroupId = @RecordId
    END
END;