CREATE TABLE [dbo].[STPLayouts] (
    [LayoutId]         SMALLINT       IDENTITY (1, 1) NOT NULL,
    [Layout]           CHAR (1)       NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_STPLayouts_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_STPLayouts_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_STPLayouts_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_STPLayouts_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_STPLayouts_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPLayouts] PRIMARY KEY CLUSTERED ([LayoutId] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Layout]
    ON [dbo].[STPLayouts]([Layout] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPLayouts_Audit] ON [dbo].[STPLayouts] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.LayoutId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.LayoutId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPLayouts, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPLayouts] SET AUDUpdatedBy = APP_NAME() WHERE LayoutId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPLayouts] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE LayoutId = @RecordId
    END
END;