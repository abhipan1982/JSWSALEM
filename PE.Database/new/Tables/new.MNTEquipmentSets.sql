CREATE TABLE [new].[MNTEquipmentSets] (
    [EquipmentSetId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKParentEquipmentId] BIGINT         NOT NULL,
    [FKEquipmentId]       BIGINT         NOT NULL,
    [EquipmentQuantity]   SMALLINT       CONSTRAINT [DF_MNTEquipmentSets_EquipmentQuantity] DEFAULT ((1)) NOT NULL,
    [MountedTs]           DATETIME       CONSTRAINT [DF_MNTEquipmentSets_MountedTs] DEFAULT (getdate()) NOT NULL,
    [DismountedTs]        DATETIME       NULL,
    [Notes]               NVARCHAR (255) NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_MNTDeviceComponents_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_MNTDeviceComponents_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_MNTDeviceComponents_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_MNTDeviceComponents_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_MNTDeviceComponents_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTDeviceComponents] PRIMARY KEY CLUSTERED ([EquipmentSetId] ASC),
    CONSTRAINT [FK_MNTDeviceComponents_MNTDevices] FOREIGN KEY ([FKEquipmentId]) REFERENCES [new].[MNTEquipments] ([EquipmentId]),
    CONSTRAINT [FK_MNTDeviceComponents_MNTDevicesParent] FOREIGN KEY ([FKParentEquipmentId]) REFERENCES [new].[MNTEquipments] ([EquipmentId])
);




GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTDeviceComponents_Audit] ON new.MNTEquipmentSets AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EquipmentSetId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EquipmentSetId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTDeviceComponents, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTEquipmentSets] SET AUDUpdatedBy = APP_NAME() WHERE EquipmentSetId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTEquipmentSets] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EquipmentSetId = @RecordId
    END
END;