CREATE TABLE [dw].[DimYear] (
    [SourceName]       NVARCHAR (50)  NOT NULL,
    [SourceTime]       DATETIME       NOT NULL,
    [DimYearIsDeleted] BIT            NOT NULL,
    [DimYearHash]      VARBINARY (16) NULL,
    [DimYearKey]       INT            NOT NULL,
    [Year]             INT            NULL,
    [DimYearRow]       INT            IDENTITY (1, 1) NOT NULL
);

