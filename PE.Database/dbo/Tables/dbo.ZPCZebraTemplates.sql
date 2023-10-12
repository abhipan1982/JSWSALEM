CREATE TABLE [dbo].[ZPCZebraTemplates] (
    [ZebraTemplateId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [ZebraTemplateCode] NVARCHAR (10)  NOT NULL,
    [ZebraTemplateName] NVARCHAR (50)  NOT NULL,
    [ZebraTemplate]     NVARCHAR (MAX) NULL,
    [IsDefault]         BIT            CONSTRAINT [DF_ZPCZebraTemplates_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]      DATETIME       CONSTRAINT [DF_ZPCZebraTemplates_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       CONSTRAINT [DF_ZPCZebraTemplates_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (255) CONSTRAINT [DF_ZPCZebraTemplates_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]    BIT            CONSTRAINT [DF_ZPCZebraTemplates_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_ZPCZebraTemplates_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ZPCZebraTemplates] PRIMARY KEY CLUSTERED ([ZebraTemplateId] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ZebraTemplateCode]
    ON [dbo].[ZPCZebraTemplates]([ZebraTemplateCode] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_ZPCZebraTemplates_Audit] ON [dbo].[ZPCZebraTemplates] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ZebraTemplateId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ZebraTemplateId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: ZPCZebraTemplates, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[ZPCZebraTemplates] SET AUDUpdatedBy = APP_NAME() WHERE ZebraTemplateId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[ZPCZebraTemplates] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ZebraTemplateId = @RecordId
    END
END;