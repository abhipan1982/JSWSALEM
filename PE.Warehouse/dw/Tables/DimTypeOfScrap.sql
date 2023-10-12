CREATE TABLE [dw].[DimTypeOfScrap] (
    [SourceName]              NVARCHAR (50)  NOT NULL,
    [SourceTime]              DATETIME       NOT NULL,
    [DimTypeOfScrapIsDeleted] BIT            NOT NULL,
    [DimTypeOfScrapHash]      VARBINARY (16) NULL,
    [DimTypeOfScrapKey]       BIGINT         NOT NULL,
    [TypeOfScrap]             VARCHAR (50)   NOT NULL,
    [DimTypeOfScrapRow]       INT            IDENTITY (1, 1) NOT NULL
);

