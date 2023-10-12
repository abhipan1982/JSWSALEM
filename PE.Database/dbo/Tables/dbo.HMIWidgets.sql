CREATE TABLE [dbo].[HMIWidgets] (
    [WidgetId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [WidgetName]       NVARCHAR (50)  NOT NULL,
    [WidgetFileName]   NVARCHAR (250) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_HMIWidgets_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_HMIWidgets_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_HMIWidgets_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_HMIWidgets_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_HMIWidgets_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HMIWidgets] PRIMARY KEY CLUSTERED ([WidgetId] ASC)
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_HMIWidgets_Audit] ON [dbo].[HMIWidgets] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.WidgetId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.WidgetId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: HMIWidgets, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[HMIWidgets] SET AUDUpdatedBy = APP_NAME() WHERE WidgetId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[HMIWidgets] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE WidgetId = @RecordId
    END
END;