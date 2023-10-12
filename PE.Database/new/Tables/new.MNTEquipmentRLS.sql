CREATE TABLE [new].[MNTEquipmentRLS] (
    [FKEquipmentId]        BIGINT     NOT NULL,
    [FKEquipmentRLSTypeId] BIGINT     NOT NULL,
    [EnumArrangement]      SMALLINT   CONSTRAINT [DF_MNTDeviceRLS_EnumRLSCasseteArrangement] DEFAULT ((0)) NOT NULL,
    [EnumScrapReason]      SMALLINT   CONSTRAINT [DF_MNTDeviceRLS_EnumScrapReason] DEFAULT ((0)) NOT NULL,
    [SequenceOrder]        SMALLINT   CONSTRAINT [DF_MNTDeviceRLS_SequenceOrder] DEFAULT ((1)) NULL,
    [NumberOfRols]         SMALLINT   CONSTRAINT [DF_MNTDeviceRLS_RLSCassetteNumberOfRols] DEFAULT ((1)) NULL,
    [NumberOfGrooves]      SMALLINT   CONSTRAINT [DF_MNTDeviceRLS_NumberOfGrooves] DEFAULT ((1)) NULL,
    [RollInitialDiameter]  FLOAT (53) NULL,
    [RollMinimumDiameter]  FLOAT (53) NULL,
    CONSTRAINT [PK_MNTDeviceRLS] PRIMARY KEY CLUSTERED ([FKEquipmentId] ASC),
    CONSTRAINT [FK_MNTDeviceRLS_MNTDeviceRLSTypes] FOREIGN KEY ([FKEquipmentRLSTypeId]) REFERENCES [new].[MNTEquipmentRLSTypes] ([EquipmentRLSTypeId]),
    CONSTRAINT [FK_MNTDeviceRLS_MNTDevices] FOREIGN KEY ([FKEquipmentId]) REFERENCES [new].[MNTEquipments] ([EquipmentId])
);

