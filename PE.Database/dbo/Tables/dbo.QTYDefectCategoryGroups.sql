CREATE TABLE [dbo].[QTYDefectCategoryGroups] (
    [DefectCategoryGroupId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [DefectCategoryGroupCode]        NVARCHAR (10)   NOT NULL,
    [DefectCategoryGroupName]        NVARCHAR (50)   NULL,
    [DefectCategoryGroupDescription] NVARCHAR (2000) NULL,
    [AUDCreatedTs]                   DATETIME        CONSTRAINT [DF_QTYDefectCategoryGroups_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]               DATETIME        CONSTRAINT [DF_QTYDefectCategoryGroups_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                   NVARCHAR (255)  CONSTRAINT [DF_QTYDefectCategoryGroups_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                 BIT             CONSTRAINT [DF_QTYDefectCategoryGroups_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                      BIT             CONSTRAINT [DF_QTYDefectCategoryGroups_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHDefectCategoryGroups] PRIMARY KEY CLUSTERED ([DefectCategoryGroupId] ASC)
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QTYDefectCategoryGroups_Audit] ON [dbo].[QTYDefectCategoryGroups] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.DefectCategoryGroupId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.DefectCategoryGroupId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QTYDefectCategoryGroups, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QTYDefectCategoryGroups] SET AUDUpdatedBy = APP_NAME() WHERE DefectCategoryGroupId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QTYDefectCategoryGroups] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE DefectCategoryGroupId = @RecordId
    END
END;