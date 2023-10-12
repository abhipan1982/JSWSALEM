CREATE TABLE [dbo].[STPProductLayouts] (
    [ProductLayoutId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKProductCatalogueId] BIGINT         NOT NULL,
    [FKLayoutId]           SMALLINT       NOT NULL,
    [FKIssueId]            SMALLINT       NOT NULL,
    [ExitSpeed]            FLOAT (53)     NOT NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_STPProductLayouts_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_STPProductLayouts_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_STPProductLayouts_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_STPProductLayouts_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_STPProductLayouts_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPProductLayouts] PRIMARY KEY CLUSTERED ([ProductLayoutId] ASC),
    CONSTRAINT [FK_ProductLayouts_Issues] FOREIGN KEY ([FKIssueId]) REFERENCES [dbo].[STPIssues] ([IssueId]),
    CONSTRAINT [FK_ProductLayouts_Layouts] FOREIGN KEY ([FKLayoutId]) REFERENCES [dbo].[STPLayouts] ([LayoutId]),
    CONSTRAINT [FK_ProductLayouts_ProductCatalogue] FOREIGN KEY ([FKProductCatalogueId]) REFERENCES [dbo].[PRMProductCatalogue] ([ProductCatalogueId])
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPProductLayouts_Audit] ON [dbo].[STPProductLayouts] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ProductLayoutId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ProductLayoutId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPProductLayouts, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPProductLayouts] SET AUDUpdatedBy = APP_NAME() WHERE ProductLayoutId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPProductLayouts] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ProductLayoutId = @RecordId
    END
END;