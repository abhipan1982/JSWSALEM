CREATE TABLE [dbo].[MVHFeatureCustoms] (
    [FeatureCustomId]         BIGINT IDENTITY (1, 1) NOT NULL,
    [FKFeatureId]             BIGINT NOT NULL,
    [FKLanguageId]            BIGINT NOT NULL,
    [FKUnitOfMeasureId]       BIGINT NOT NULL,
    [FKUnitOfMeasureFormatId] BIGINT NULL,
    CONSTRAINT [PK_MVHFeatureCustoms] PRIMARY KEY CLUSTERED ([FeatureCustomId] ASC),
    CONSTRAINT [FK_MVHFeatureCustoms_Languages] FOREIGN KEY ([FKLanguageId]) REFERENCES [smf].[Languages] ([LanguageId]),
    CONSTRAINT [FK_MVHFeatureCustoms_MVHFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MVHFeatureCustoms_UnitOfMeasure] FOREIGN KEY ([FKUnitOfMeasureId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [FK_MVHFeatureCustoms_UnitOfMeasureFormat] FOREIGN KEY ([FKUnitOfMeasureFormatId]) REFERENCES [smf].[UnitOfMeasureFormat] ([UnitFormatId])
);

