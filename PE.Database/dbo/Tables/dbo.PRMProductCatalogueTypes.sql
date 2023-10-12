CREATE TABLE [dbo].[PRMProductCatalogueTypes] (
    [ProductCatalogueTypeId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductCatalogueTypeCode]        NVARCHAR (10)  NOT NULL,
    [ProductCatalogueTypeName]        NVARCHAR (50)  NOT NULL,
    [ProductCatalogueTypeDescription] NVARCHAR (200) NULL,
    [IsDefault]                       BIT            CONSTRAINT [DF_ProductCatalogueTypes_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]                    DATETIME       CONSTRAINT [DF_PRMProductCatalogueTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]                DATETIME       CONSTRAINT [DF_PRMProductCatalogueTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                    NVARCHAR (255) CONSTRAINT [DF_PRMProductCatalogueTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                  BIT            CONSTRAINT [DF_PRMProductCatalogueTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                       BIT            CONSTRAINT [DF_PRMProductCatalogueTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProductCatalogueTypeId] PRIMARY KEY CLUSTERED ([ProductCatalogueTypeId] ASC),
    CONSTRAINT [UQ_ProductCatalogueTypesName] UNIQUE NONCLUSTERED ([ProductCatalogueTypeName] ASC)
);












GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductCatalogueTypeSymbol]
    ON [dbo].[PRMProductCatalogueTypes]([ProductCatalogueTypeCode] ASC);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMProductCatalogueTypes_Audit] ON [dbo].[PRMProductCatalogueTypes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ProductCatalogueTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ProductCatalogueTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMProductCatalogueTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMProductCatalogueTypes] SET AUDUpdatedBy = APP_NAME() WHERE ProductCatalogueTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMProductCatalogueTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ProductCatalogueTypeId = @RecordId
    END
END;