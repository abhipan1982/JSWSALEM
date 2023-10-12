CREATE TABLE [dw].[DimUnit] (
    [SourceName]         NVARCHAR (50)  NOT NULL,
    [SourceTime]         DATETIME       NOT NULL,
    [DimUnitIsDeleted]   BIT            NOT NULL,
    [DimUnitHash]        VARBINARY (16) NULL,
    [DimUnitKey]         BIGINT         NOT NULL,
    [DimUnitCategoryKey] BIGINT         NOT NULL,
    [UnitIsSI]           BIT            NOT NULL,
    [UnitCategory]       NVARCHAR (50)  NOT NULL,
    [UnitSymbol]         NVARCHAR (50)  NOT NULL,
    [UnitName]           NVARCHAR (50)  NOT NULL,
    [UnitFactor]         FLOAT (53)     NOT NULL,
    [UnitShift]          FLOAT (53)     NOT NULL,
    [UnitSISymbol]       NVARCHAR (50)  NOT NULL,
    [UnitSIName]         NVARCHAR (50)  NOT NULL,
    [DimUnitRow]         INT            IDENTITY (1, 1) NOT NULL
);

