CREATE TABLE [prj].[MVHAssetsEXT] (
    [FKAssetId]        BIGINT         NOT NULL,
    [MaxPassNo]        SMALLINT       CONSTRAINT [DF_MVHAssetsEXT_MaxPassNo] DEFAULT ((1)) NOT NULL,
    [TimeIn]           SMALLINT       NULL,
    [IsInitial]        BIT            CONSTRAINT [DF_MVHAssetsEXT_IsInitial] DEFAULT ((0)) NOT NULL,
    [IsLast]           BIT            CONSTRAINT [DF_MVHAssetsEXT_IsLast] DEFAULT ((0)) NOT NULL,
    [IsQueue]          BIT            CONSTRAINT [DF_MVHAssetsEXT_IsQueue] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MVHAssetsEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MVHAssetsEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MVHAssetsEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MVHAssetsEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MVHAssetsEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AssetsEXT] PRIMARY KEY CLUSTERED ([FKAssetId] ASC),
    CONSTRAINT [FK_AssetsEXT_Assets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]) ON DELETE CASCADE
);





