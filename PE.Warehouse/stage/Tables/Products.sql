CREATE TABLE [stage].[Products] (
    [ProductId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]         DATETIME       NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (200) NULL,
    [IsDeleted]            BIT            NOT NULL,
    [IsBeforeCommit]       BIT            NOT NULL,
    [FKWorkOrderId]        BIGINT         NULL,
    [FKProductCatalogueId] BIGINT         NULL,
    [IsDummy]              BIT            NOT NULL,
    [IsAssigned]           BIT            NOT NULL,
    [ProductName]          NVARCHAR (50)  NOT NULL,
    [ProductWeight]        FLOAT (53)     NOT NULL,
    [ProductCreatedTs]     DATETIME       NOT NULL,
    [BarsCounter]          SMALLINT       NOT NULL,
    [EnumWeightSource]     SMALLINT       NOT NULL,
    [EnumQuality]          SMALLINT       NOT NULL
);

