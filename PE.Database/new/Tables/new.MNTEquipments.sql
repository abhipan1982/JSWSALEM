CREATE TABLE [new].[MNTEquipments] (
    [EquipmentId]               BIGINT         NOT NULL,
    [EquipmentName]             NVARCHAR (50)  NOT NULL,
    [EquipmentDescription]      NVARCHAR (100) NULL,
    [FKEquipmentTypeId]         BIGINT         NULL,
    [FKEquipmentLocalizationId] BIGINT         NULL,
    [FKSupplierId]              BIGINT         NULL,
    [FKDefaultAssetId]          BIGINT         NULL,
    [EquipmentQuantity]         SMALLINT       CONSTRAINT [DF_MNTEquipments_EquipmentQuantity] DEFAULT ((1)) NULL,
    [EquipmentModel]            NVARCHAR (50)  NULL,
    [EquipmentSupplierCode]     NVARCHAR (50)  NULL,
    [EquipmentSerialNumber]     NVARCHAR (50)  NULL,
    [IsAvailable]               BIT            CONSTRAINT [DF_MNTDevices_DeviceStatus] DEFAULT ((1)) NOT NULL,
    [EnumEquipmentStatus]       SMALLINT       CONSTRAINT [DF_MNTEquipments_EnumEquipmentStatus] DEFAULT ((0)) NOT NULL,
    [AcquiredDate]              DATE           CONSTRAINT [DF_MNTDevices_AcquiredDate] DEFAULT (getdate()) NOT NULL,
    [DisposedDate]              DATE           NULL,
    [AUDCreatedTs]              DATETIME       CONSTRAINT [DF_MNTDevices_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]          DATETIME       CONSTRAINT [DF_MNTDevices_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]              NVARCHAR (255) CONSTRAINT [DF_MNTDevices_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]            BIT            CONSTRAINT [DF_MNTDevices_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                 BIT            CONSTRAINT [DF_MNTDevices_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTDevices] PRIMARY KEY CLUSTERED ([EquipmentId] ASC),
    CONSTRAINT [FK_MNTDevices_MNTSuppliers] FOREIGN KEY ([FKSupplierId]) REFERENCES [new].[MNTSuppliers] ([SupplierId]),
    CONSTRAINT [FK_MNTEquipments_MNTEquipmentLocalizations] FOREIGN KEY ([FKEquipmentLocalizationId]) REFERENCES [new].[MNTEquipmentLocalizations] ([EquipmentLocalizationId]),
    CONSTRAINT [FK_MNTEquipments_MNTEquipmentTypes] FOREIGN KEY ([FKEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId])
);






GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTDevices_Audit] ON new.MNTEquipments AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EquipmentId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EquipmentId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTDevices, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTEquipments] SET AUDUpdatedBy = APP_NAME() WHERE EquipmentId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTEquipments] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EquipmentId = @RecordId
    END
END;