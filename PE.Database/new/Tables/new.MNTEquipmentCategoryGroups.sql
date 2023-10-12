CREATE TABLE [new].[MNTEquipmentCategoryGroups] (
    [EquipmentCategoryGroupId]         BIGINT        NOT NULL,
    [FKParentEquipmentCategoryGroupId] BIGINT        NULL,
    [EquipmentCategoryGroupName]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_MNTEquipmentCategoryGroups] PRIMARY KEY CLUSTERED ([EquipmentCategoryGroupId] ASC),
    CONSTRAINT [FK_MNTEquipmentCategoryGroups_MNTEquipmentCategoryGroups] FOREIGN KEY ([FKParentEquipmentCategoryGroupId]) REFERENCES [new].[MNTEquipmentCategoryGroups] ([EquipmentCategoryGroupId])
);

