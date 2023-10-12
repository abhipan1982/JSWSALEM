CREATE TABLE [dw].[DimMaterialStatus] (
    [SourceName]                 NVARCHAR (50)  NOT NULL,
    [SourceTime]                 DATETIME       NOT NULL,
    [DimMaterialStatusIsDeleted] BIT            NOT NULL,
    [DimMaterialStatusHash]      VARBINARY (16) NULL,
    [DimMaterialStatusKey]       BIGINT         NOT NULL,
    [MaterialStatus]             VARCHAR (50)   NOT NULL,
    [DimMaterialStatusRow]       INT            IDENTITY (1, 1) NOT NULL
);

