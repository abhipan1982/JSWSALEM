CREATE TABLE [dw].[DimInspectionResult] (
    [SourceName]                   NVARCHAR (50)  NOT NULL,
    [SourceTime]                   DATETIME       NOT NULL,
    [DimInspectionResultIsDeleted] BIT            NOT NULL,
    [DimInspectionResultHash]      VARBINARY (16) NULL,
    [DimInspectionResultKey]       BIGINT         NOT NULL,
    [InspectionResult]             VARCHAR (50)   NOT NULL,
    [DimInspectionResultRow]       INT            IDENTITY (1, 1) NOT NULL
);

