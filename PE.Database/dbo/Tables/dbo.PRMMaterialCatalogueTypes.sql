CREATE TABLE [dbo].[PRMMaterialCatalogueTypes] (
    [MaterialCatalogueTypeId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaterialCatalogueTypeCode]        NVARCHAR (10)  NOT NULL,
    [MaterialCatalogueTypeName]        NVARCHAR (50)  NOT NULL,
    [MaterialCatalogueTypeDescription] NVARCHAR (200) NULL,
    [IsDefault]                        BIT            CONSTRAINT [DF_MaterialCatalogueTypes_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]                     DATETIME       CONSTRAINT [DF_PRMMaterialCatalogueTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]                 DATETIME       CONSTRAINT [DF_PRMMaterialCatalogueTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                     NVARCHAR (255) CONSTRAINT [DF_PRMMaterialCatalogueTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                   BIT            CONSTRAINT [DF_PRMMaterialCatalogueTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                        BIT            CONSTRAINT [DF_PRMMaterialCatalogueTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MaterialCatalogueTypeId] PRIMARY KEY CLUSTERED ([MaterialCatalogueTypeId] ASC),
    CONSTRAINT [UQ_MaterialCatalogueTypeName] UNIQUE NONCLUSTERED ([MaterialCatalogueTypeName] ASC)
);












GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_MaterialCatalogueTypeSymbol]
    ON [dbo].[PRMMaterialCatalogueTypes]([MaterialCatalogueTypeCode] ASC);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMMaterialCatalogueTypes_Audit] ON [dbo].[PRMMaterialCatalogueTypes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MaterialCatalogueTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MaterialCatalogueTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMMaterialCatalogueTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMMaterialCatalogueTypes] SET AUDUpdatedBy = APP_NAME() WHERE MaterialCatalogueTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMMaterialCatalogueTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MaterialCatalogueTypeId = @RecordId
    END
END;