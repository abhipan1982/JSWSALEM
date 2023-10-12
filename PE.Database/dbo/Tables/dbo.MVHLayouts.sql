CREATE TABLE [dbo].[MVHLayouts] (
    [LayoutId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [LayoutName]       NVARCHAR (50)  NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MVHLayouts_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MVHLayouts_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MVHLayouts_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MVHLayouts_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MVHLayouts_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHLayouts] PRIMARY KEY CLUSTERED ([LayoutId] ASC)
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHLayouts_Audit] ON [dbo].[MVHLayouts] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.LayoutId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.LayoutId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHLayouts, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHLayouts] SET AUDUpdatedBy = APP_NAME() WHERE LayoutId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHLayouts] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE LayoutId = @RecordId
    END
END;