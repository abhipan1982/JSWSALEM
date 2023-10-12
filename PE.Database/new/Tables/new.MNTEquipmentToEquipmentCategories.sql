CREATE TABLE [new].[MNTEquipmentToEquipmentCategories] (
    [FKEquipmentId]         BIGINT NOT NULL,
    [FKEquipmentCategoryId] BIGINT NOT NULL,
    CONSTRAINT [PK_MNTEquipmentToEquipmentType] PRIMARY KEY CLUSTERED ([FKEquipmentId] ASC, [FKEquipmentCategoryId] ASC),
    CONSTRAINT [FK_MNTEquipmentToEquipmentCategories_MNTEquipmentCategories] FOREIGN KEY ([FKEquipmentCategoryId]) REFERENCES [new].[MNTEquipmentCategories] ([EquipmentCategoryId]),
    CONSTRAINT [FK_MNTEquipmentToEquipmentType_MNTEquipments] FOREIGN KEY ([FKEquipmentId]) REFERENCES [new].[MNTEquipments] ([EquipmentId])
);

