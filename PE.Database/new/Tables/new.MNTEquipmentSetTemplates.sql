CREATE TABLE [new].[MNTEquipmentSetTemplates] (
    [EquipmentSetTemplateId]  BIGINT IDENTITY (1, 1) NOT NULL,
    [FKEquipmentTypeId]       BIGINT NOT NULL,
    [FKParentEquipmentTypeId] BIGINT NULL,
    CONSTRAINT [PK_MNTEquipmentSetTemplates] PRIMARY KEY CLUSTERED ([EquipmentSetTemplateId] ASC),
    CONSTRAINT [FK_MNTDeviceComponentTemplate_MNTDeviceTypes] FOREIGN KEY ([FKEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId]),
    CONSTRAINT [FK_MNTDeviceComponentTemplate_MNTDeviceTypes1] FOREIGN KEY ([FKParentEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId])
);

