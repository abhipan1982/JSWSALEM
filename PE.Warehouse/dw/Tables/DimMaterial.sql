CREATE TABLE [dw].[DimMaterial] (
    [SourceName]                        NVARCHAR (50)  NOT NULL,
    [SourceTime]                        DATETIME       NOT NULL,
    [DimMaterialIsDeleted]              BIT            NOT NULL,
    [DimMaterialHash]                   VARBINARY (16) NULL,
    [DimMaterialKey]                    BIGINT         NOT NULL,
    [DimWorkOrderKey]                   BIGINT         NULL,
    [DimSteelgradeKey]                  BIGINT         NULL,
    [DimHeatKey]                        BIGINT         NOT NULL,
    [DimMaterialCatalogueKey]           BIGINT         NULL,
    [MaterialName]                      NVARCHAR (50)  NOT NULL,
    [MaterialSeqNo]                     SMALLINT       NOT NULL,
    [MaterialWeight]                    FLOAT (53)     NOT NULL,
    [MaterialLength]                    FLOAT (53)     NULL,
    [MaterialThickness]                 FLOAT (53)     NOT NULL,
    [MaterialWidth]                     FLOAT (53)     NULL,
    [MaterialCreated]                   DATETIME       NOT NULL,
    [MaterialProductionStart]           DATETIME       NULL,
    [MaterialProductionEnd]             DATETIME       NULL,
    [MaterialIsAssignedWithRawMaterial] BIT            NOT NULL,
    [RawMaterialsAssigned]              INT            NOT NULL,
    [DimMaterialRow]                    INT            IDENTITY (1, 1) NOT NULL
);





