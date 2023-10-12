CREATE TABLE [new].[MNTEquipmentCategories] (
    [EquipmentCategoryId]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [FKEquipmentCategoryGroupId] BIGINT        NOT NULL,
    [EquipmentCategoryName]      NVARCHAR (50) NULL,
    CONSTRAINT [PK_MNTEquipmentCategories] PRIMARY KEY CLUSTERED ([EquipmentCategoryId] ASC),
    CONSTRAINT [FK_MNTEquipmentCategories_MNTEquipmentCategoryGroups] FOREIGN KEY ([FKEquipmentCategoryGroupId]) REFERENCES [new].[MNTEquipmentCategoryGroups] ([EquipmentCategoryGroupId])
);





