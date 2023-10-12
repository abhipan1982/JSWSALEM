CREATE TABLE [new].[MNTEquipmentSetOnAssets] (
    [EquipmentSetOnAssetId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKEquipmentSetId]      BIGINT         NOT NULL,
    [FKAssetId]             BIGINT         NOT NULL,
    [OnLineTs]              DATETIME       CONSTRAINT [DF_MNTDeviceOnAsset_OnLineTs] DEFAULT (getdate()) NOT NULL,
    [OffLineTs]             DATETIME       NULL,
    [IsForcedDeactivated]   BIT            CONSTRAINT [DF_MNTDeviceOnAsset_IsForcedDeactivated] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_MNTDeviceOnAsset_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_MNTDeviceOnAsset_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_MNTDeviceOnAsset_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_MNTDeviceOnAsset_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_MNTDeviceOnAsset_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTDeviceOnAsset] PRIMARY KEY CLUSTERED ([EquipmentSetOnAssetId] ASC),
    CONSTRAINT [FK_MNTEquipmentSetOnAssets_MNTEquipmentSets] FOREIGN KEY ([FKEquipmentSetId]) REFERENCES [new].[MNTEquipmentSets] ([EquipmentSetId]),
    CONSTRAINT [FK_MNTEquipmentSetOnAssets_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId])
);




GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTDeviceOnAsset_Audit] ON new.MNTEquipmentSetOnAssets AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EquipmentSetOnAssetId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EquipmentSetOnAssetId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTEquipmentSetOnAssets, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTEquipmentSetOnAssets] SET AUDUpdatedBy = APP_NAME() WHERE EquipmentSetOnAssetId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTEquipmentSetOnAssets] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EquipmentSetOnAssetId = @RecordId
    END
END;