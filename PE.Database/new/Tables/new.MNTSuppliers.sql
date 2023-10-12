CREATE TABLE [new].[MNTSuppliers] (
    [SupplierId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [SupplierName]     NVARCHAR (50)  NOT NULL,
    [SupplierAddress]  NVARCHAR (200) NULL,
    [SupplierEmail]    NVARCHAR (150) NULL,
    [SupplierPhone]    NVARCHAR (20)  NULL,
    [IsActive]         BIT            CONSTRAINT [DF_MNTSuppliers_IsActive] DEFAULT ((1)) NOT NULL,
    [IsDefault]        BIT            CONSTRAINT [DF_MNTSuppliers_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MNTSuppliers_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MNTSuppliers_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MNTSuppliers_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MNTSuppliers_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MNTSuppliers_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTSuppliers] PRIMARY KEY CLUSTERED ([SupplierId] ASC)
);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTSuppliers_Audit] ON [new].[MNTSuppliers] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SupplierId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SupplierId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTSuppliers, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTSuppliers] SET AUDUpdatedBy = APP_NAME() WHERE SupplierId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTSuppliers] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SupplierId = @RecordId
    END
END;