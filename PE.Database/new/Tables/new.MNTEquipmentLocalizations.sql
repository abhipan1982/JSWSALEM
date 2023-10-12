CREATE TABLE [new].[MNTEquipmentLocalizations] (
    [EquipmentLocalizationId]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [FKParentEquipmentLocalizationId] BIGINT        NULL,
    [EquipmentLocalizationName]       NVARCHAR (50) NOT NULL,
    [EnumFillDirection]               SMALLINT      CONSTRAINT [DF_MNTEquipmentLocalizations_EnumFillDirection] DEFAULT ((0)) NOT NULL,
    [EnumFillPattern]                 SMALLINT      CONSTRAINT [DF_MNTEquipmentLocalizations_EnumFillPattern] DEFAULT ((0)) NOT NULL,
    [LocationX]                       SMALLINT      CONSTRAINT [DF_MNTEquipmentLocalizations_LocationX] DEFAULT ((0)) NOT NULL,
    [LocationY]                       SMALLINT      CONSTRAINT [DF_MNTEquipmentLocalizations_LocationY] DEFAULT ((0)) NOT NULL,
    [SizeX]                           SMALLINT      CONSTRAINT [DF_MNTEquipmentLocalizations_SizeX] DEFAULT ((0)) NOT NULL,
    [SizeY]                           SMALLINT      CONSTRAINT [DF_MNTEquipmentLocalizations_SizeY] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTEquipmentLocalizations] PRIMARY KEY CLUSTERED ([EquipmentLocalizationId] ASC),
    CONSTRAINT [FK_MNTEquipmentLocalizations_MNTEquipmentLocalizations] FOREIGN KEY ([FKParentEquipmentLocalizationId]) REFERENCES [new].[MNTEquipmentLocalizations] ([EquipmentLocalizationId])
);

