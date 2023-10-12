CREATE TABLE [new].[MNTEquipmentAttachments] (
    [EquipmentAttachmentId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [FKEquipmentId]         BIGINT          NULL,
    [FKEquipmentTypeId]     BIGINT          NULL,
    [FKSupplierId]          BIGINT          NULL,
    [FKAttachmentTypeId]    BIGINT          NOT NULL,
    [Attachement]           VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_MNTAttachments] PRIMARY KEY CLUSTERED ([EquipmentAttachmentId] ASC),
    CONSTRAINT [FK_MNTAttachments_MNTEquipments] FOREIGN KEY ([FKEquipmentId]) REFERENCES [new].[MNTEquipments] ([EquipmentId]),
    CONSTRAINT [FK_MNTEquipmentAttachments_DBAttachmentTypes] FOREIGN KEY ([FKAttachmentTypeId]) REFERENCES [dbo].[DBAttachmentTypes] ([AttachmentTypeId]),
    CONSTRAINT [FK_MNTEquipmentAttachments_MNTEquipmentTypes] FOREIGN KEY ([FKEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId]),
    CONSTRAINT [FK_MNTEquipmentAttachments_MNTSuppliers] FOREIGN KEY ([FKSupplierId]) REFERENCES [new].[MNTSuppliers] ([SupplierId])
);





