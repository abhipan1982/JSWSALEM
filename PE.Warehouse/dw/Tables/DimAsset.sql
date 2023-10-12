CREATE TABLE [dw].[DimAsset] (
    [SourceName]             NVARCHAR (50)  NOT NULL,
    [SourceTime]             DATETIME       NOT NULL,
    [DimAssetIsDeleted]      BIT            NOT NULL,
    [DimAssetHash]           VARBINARY (16) NULL,
    [DimAssetKey]            BIGINT         NOT NULL,
    [DimAssetKeyParent]      BIGINT         NULL,
    [AssetCode]              INT            NOT NULL,
    [AssetName]              NVARCHAR (50)  NOT NULL,
    [AssetDescription]       NVARCHAR (100) NOT NULL,
    [AssetOrderSeq]          INT            NOT NULL,
    [AssetIsDelayCheckpoint] BIT            NOT NULL,
    [AssetIsArea]            BIT            NOT NULL,
    [AssetIsZone]            BIT            NOT NULL,
    [AssetIsReversible]      BIT            NOT NULL,
    [AssetCodeParent]        INT            NULL,
    [AssetNameParent]        NVARCHAR (50)  NULL,
    [AssetDescriptionParent] NVARCHAR (100) NULL,
    [DimAssetRow]            INT            IDENTITY (1, 1) NOT NULL
);



