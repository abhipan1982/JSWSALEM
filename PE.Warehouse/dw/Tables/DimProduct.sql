CREATE TABLE [dw].[DimProduct] (
    [SourceName]                       NVARCHAR (50)  NOT NULL,
    [SourceTime]                       DATETIME       NOT NULL,
    [DimProductIsDeleted]              BIT            NOT NULL,
    [DimProductHash]                   VARBINARY (16) NULL,
    [DimProductKey]                    BIGINT         NOT NULL,
    [DimWorkOrderKey]                  BIGINT         NULL,
    [DimSteelgradeKey]                 BIGINT         NOT NULL,
    [DimHeatKey]                       BIGINT         NULL,
    [DimProductCatalogueKey]           BIGINT         NOT NULL,
    [ProductName]                      NVARCHAR (50)  NOT NULL,
    [ProductWeight]                    FLOAT (53)     NOT NULL,
    [ProductCreated]                   DATETIME       NOT NULL,
    [ProductIsAssignedWithRawMaterial] BIT            NOT NULL,
    [RawMaterialsAssigned]             INT            NULL,
    [DimProductRow]                    INT            IDENTITY (1, 1) NOT NULL
);



