CREATE TABLE [dbo].[MVHAssetsLocation] (
    [FKAssetId]         BIGINT         NOT NULL,
    [EnumFillDirection] SMALLINT       CONSTRAINT [DF_MVHAssetsLocation_EnumFillDirection] DEFAULT ((0)) NOT NULL,
    [EnumFillPattern]   SMALLINT       CONSTRAINT [DF_MVHAssetsLocation_EnumFillPattern] DEFAULT ((0)) NOT NULL,
    [LocationX]         SMALLINT       CONSTRAINT [DF_MVHAssetsLocation_LocationX] DEFAULT ((0)) NOT NULL,
    [LocationY]         SMALLINT       CONSTRAINT [DF_MVHAssetsLocation_LocationY] DEFAULT ((0)) NOT NULL,
    [SizeX]             SMALLINT       CONSTRAINT [DF_MVHAssetsLocation_SizeX] DEFAULT ((0)) NOT NULL,
    [SizeY]             SMALLINT       CONSTRAINT [DF_MVHAssetsLocation_SizeY] DEFAULT ((0)) NOT NULL,
    [LayersMaxNumber]   SMALLINT       CONSTRAINT [DF_MVHAssetsLocation_LayersMaxNumber] DEFAULT ((1)) NOT NULL,
    [PieceMaxCapacity]  INT            CONSTRAINT [DF_MVHAssetsLocation_PieceCapacity] DEFAULT ((0)) NOT NULL,
    [WeightMaxCapacity] INT            CONSTRAINT [DF_MVHAssetsLocation_WeightCapacity] DEFAULT ((0)) NOT NULL,
    [VolumeMaxCapacity] INT            CONSTRAINT [DF_MVHAssetsLocation_VolumeCapacity] DEFAULT ((0)) NOT NULL,
    [FillOrderSeq]      SMALLINT       CONSTRAINT [DF_MVHAssetsLocation_FillOrderSeq] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]      DATETIME       CONSTRAINT [DF_MVHAssetsLocation_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       CONSTRAINT [DF_MVHAssetsLocation_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (255) CONSTRAINT [DF_MVHAssetsLocation_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]    BIT            CONSTRAINT [DF_MVHAssetsLocation_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_MVHAssetsLocation_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHAssetsLocation] PRIMARY KEY CLUSTERED ([FKAssetId] ASC),
    CONSTRAINT [FK_MVHAssetsLocation_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId])
);









