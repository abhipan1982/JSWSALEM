CREATE TABLE [new].[MNTEquipmentRLSTypes] (
    [EquipmentRLSTypeId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [EquipmentRLSTypeName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_MNTDeviceRLSTypes] PRIMARY KEY CLUSTERED ([EquipmentRLSTypeId] ASC)
);

