CREATE TABLE [dbo].[PRMScrapGroups] (
    [ScrapGroupId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ScrapGroupCode]        NVARCHAR (10)  NOT NULL,
    [ScrapGroupName]        NVARCHAR (50)  NULL,
    [ScrapGroupDescription] NVARCHAR (200) NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_PRMScrapGroups_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_PRMScrapGroups_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_PRMScrapGroups_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_PRMScrapGroups_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_PRMScrapGroups_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMScrapGroups] PRIMARY KEY CLUSTERED ([ScrapGroupId] ASC)
);










GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMScrapGroups_Audit] ON [dbo].[PRMScrapGroups] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ScrapGroupId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ScrapGroupId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMScrapGroups, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMScrapGroups] SET AUDUpdatedBy = APP_NAME() WHERE ScrapGroupId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMScrapGroups] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ScrapGroupId = @RecordId
    END
END;