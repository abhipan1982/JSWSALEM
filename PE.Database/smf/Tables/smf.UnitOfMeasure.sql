CREATE TABLE [smf].[UnitOfMeasure] (
    [UnitId]         BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UnitSymbol]     NVARCHAR (50) NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [Factor]         FLOAT (53)    CONSTRAINT [DF_UnitOfMeasure_Factor] DEFAULT ((1)) NOT NULL,
    [Shift]          FLOAT (53)    CONSTRAINT [DF_UnitOfMeasure_Shift] DEFAULT ((0)) NOT NULL,
    [UnitCategoryId] BIGINT        NOT NULL,
    [SIUnitId]       BIGINT        NULL,
    CONSTRAINT [PK_UnitId] PRIMARY KEY CLUSTERED ([UnitId] ASC),
    CONSTRAINT [FK_UnitOfMeasure_UnitOfMeasure] FOREIGN KEY ([SIUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [FK_UnitOfMeasure_UnitOfMeasureCategory] FOREIGN KEY ([UnitCategoryId]) REFERENCES [smf].[UnitOfMeasureCategory] ([UnitCategoryId]),
    CONSTRAINT [UQ_UOM_UnitSymbol] UNIQUE NONCLUSTERED ([UnitSymbol] ASC)
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'e', @level0type = N'SCHEMA', @level0name = N'smf', @level1type = N'TABLE', @level1name = N'UnitOfMeasure', @level2type = N'COLUMN', @level2name = N'UnitCategoryId';


GO


