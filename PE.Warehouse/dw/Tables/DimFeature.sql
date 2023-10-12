CREATE TABLE [dw].[DimFeature] (
    [SourceName]               NVARCHAR (50)  NOT NULL,
    [SourceTime]               DATETIME       NOT NULL,
    [DimFeatureIsDeleted]      BIT            NOT NULL,
    [DimFeatureHash]           VARBINARY (16) NULL,
    [DimFeatureKey]            BIGINT         NOT NULL,
    [DimAssetKey]              BIGINT         NOT NULL,
    [DimUnitKey]               BIGINT         NOT NULL,
    [DimDataTypeKey]           BIGINT         NOT NULL,
    [FeatureCode]              INT            NOT NULL,
    [FeatureName]              NVARCHAR (75)  NOT NULL,
    [FeatureDescription]       NVARCHAR (100) NULL,
    [FeatureIsMaterialRelated] BIT            NOT NULL,
    [FeatureIsLengthRelated]   BIT            NOT NULL,
    [AssetName]                NVARCHAR (50)  NOT NULL,
    [UOMSymbol]                NVARCHAR (50)  NOT NULL,
    [DataType]                 NVARCHAR (50)  NULL,
    [DimFeatureRow]            INT            IDENTITY (1, 1) NOT NULL
);



