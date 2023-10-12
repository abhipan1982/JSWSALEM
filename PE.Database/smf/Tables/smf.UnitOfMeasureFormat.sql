CREATE TABLE [smf].[UnitOfMeasureFormat] (
    [UnitFormatId] BIGINT       IDENTITY (1, 1) NOT NULL,
    [UnitFormat]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UnitOfMeasureFormat] PRIMARY KEY CLUSTERED ([UnitFormatId] ASC)
);

