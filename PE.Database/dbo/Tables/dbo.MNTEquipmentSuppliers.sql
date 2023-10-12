CREATE TABLE [dbo].[MNTEquipmentSuppliers] (
    [EquipmentSupplierId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]                DATETIME       NOT NULL,
    [EquipmentSupplierName]    NVARCHAR (50)  NOT NULL,
    [EquipmentSupplierAddress] NVARCHAR (200) NULL,
    [EquipmentSupplierEmail]   NVARCHAR (150) NULL,
    [EquipmentSupplierPhone]   NVARCHAR (20)  NULL,
    [IsActive]                 BIT            CONSTRAINT [DF_MNTEquipmentSuppliers_IsActive] DEFAULT ((1)) NOT NULL,
    [IsDefault]                BIT            CONSTRAINT [DF_MNTEquipmentSuppliers_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]             DATETIME       CONSTRAINT [DF_MNTEquipmentSuppliers_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]         DATETIME       CONSTRAINT [DF_MNTEquipmentSuppliers_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]             NVARCHAR (255) CONSTRAINT [DF_MNTEquipmentSuppliers_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]           BIT            CONSTRAINT [DF_MNTEquipmentSuppliers_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                BIT            CONSTRAINT [DF_MNTEquipmentSuppliers_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTEquipmentSuppliers] PRIMARY KEY CLUSTERED ([EquipmentSupplierId] ASC)
);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MNTEquipmentSuppliers_Audit] ON [dbo].[MNTEquipmentSuppliers] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EquipmentSupplierId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EquipmentSupplierId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTEquipmentSuppliers, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTEquipmentSuppliers] SET AUDUpdatedBy = APP_NAME() WHERE EquipmentSupplierId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTEquipmentSuppliers] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EquipmentSupplierId = @RecordId
    END
END;