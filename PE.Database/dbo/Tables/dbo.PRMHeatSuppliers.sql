CREATE TABLE [dbo].[PRMHeatSuppliers] (
    [HeatSupplierId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HeatSupplierName]        NVARCHAR (50)  NOT NULL,
    [HeatSupplierDescription] NVARCHAR (200) NULL,
    [IsDefault]               BIT            CONSTRAINT [DF_PRMHeatSuppliers_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]            DATETIME       CONSTRAINT [DF_PRMHeatSuppliers_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]        DATETIME       CONSTRAINT [DF_PRMHeatSuppliers_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]            NVARCHAR (255) CONSTRAINT [DF_PRMHeatSuppliers_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]          BIT            CONSTRAINT [DF_PRMHeatSuppliers_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_PRMHeatSuppliers_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HeatSuppliers] PRIMARY KEY CLUSTERED ([HeatSupplierId] ASC)
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMHeatSuppliers_Audit] ON [dbo].[PRMHeatSuppliers] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.HeatSupplierId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.HeatSupplierId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMHeatSuppliers, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMHeatSuppliers] SET AUDUpdatedBy = APP_NAME() WHERE HeatSupplierId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMHeatSuppliers] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE HeatSupplierId = @RecordId
    END
END;