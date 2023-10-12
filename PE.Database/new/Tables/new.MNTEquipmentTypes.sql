CREATE TABLE [new].[MNTEquipmentTypes] (
    [EquipmentTypeId]          BIGINT         NOT NULL,
    [EquipmentTypeName]        NVARCHAR (50)  NULL,
    [EquipmentTypeDescription] NVARCHAR (100) NULL,
    [FKParentEquipmentTypeId]  BIGINT         NULL,
    [IsCountable]              BIT            CONSTRAINT [DF_MNTEquipmentTypes_IsCountable] DEFAULT ((1)) NULL,
    [IsAssetSetPossible]       BIT            CONSTRAINT [DF_MNTEquipmentTypes_IsAssetSetPossible] DEFAULT ((0)) NOT NULL,
    [EquipmentMinimumQuantity] SMALLINT       CONSTRAINT [DF_MNTEquipmentTypes_EquipmentMinimumQuantity] DEFAULT ((1)) NOT NULL,
    [EnumMNTorRLSType]         SMALLINT       CONSTRAINT [DF_MNTEquipmentTypes_EnumMNToRLSCategory] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]             DATETIME       CONSTRAINT [DF_MNTDeviceGroups_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]         DATETIME       CONSTRAINT [DF_MNTDeviceGroups_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]             NVARCHAR (255) CONSTRAINT [DF_MNTDeviceGroups_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]           BIT            CONSTRAINT [DF_MNTDeviceGroups_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                BIT            CONSTRAINT [DF_MNTDeviceGroups_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTDeviceGroups] PRIMARY KEY CLUSTERED ([EquipmentTypeId] ASC),
    CONSTRAINT [FK_MNTDeviceGroups_MNTDeviceGroups] FOREIGN KEY ([FKParentEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId])
);








GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTDeviceGroups_Audit] ON new.MNTEquipmentTypes AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EquipmentTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EquipmentTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTDeviceGroups, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTEquipmentTypes] SET AUDUpdatedBy = APP_NAME() WHERE EquipmentTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTEquipmentTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EquipmentTypeId = @RecordId
    END
END;