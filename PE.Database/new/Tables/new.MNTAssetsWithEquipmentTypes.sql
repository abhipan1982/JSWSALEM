CREATE TABLE [new].[MNTAssetsWithEquipmentTypes] (
    [FKAssetId]         BIGINT NOT NULL,
    [FKEquipmentTypeId] BIGINT NOT NULL,
    CONSTRAINT [PK_MNTAssetsWithEquipmentTypes] PRIMARY KEY CLUSTERED ([FKAssetId] ASC, [FKEquipmentTypeId] ASC),
    CONSTRAINT [FK_MNTAssetEquipmentTypeConfigurations_MNTEquipmentTypes] FOREIGN KEY ([FKEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId]),
    CONSTRAINT [FK_MNTAssetEquipmentTypeConfigurations_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId])
);

