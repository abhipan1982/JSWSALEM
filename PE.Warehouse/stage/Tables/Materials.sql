CREATE TABLE [stage].[Materials] (
    [MaterialId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]          DATETIME       NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (200) NULL,
    [IsDeleted]             BIT            NOT NULL,
    [IsBeforeCommit]        BIT            NOT NULL,
    [FKWorkOrderId]         BIGINT         NULL,
    [FKMaterialCatalogueId] BIGINT         NULL,
    [FKHeatId]              BIGINT         NOT NULL,
    [IsDummy]               BIT            NOT NULL,
    [IsAssigned]            BIT            NOT NULL,
    [MaterialName]          NVARCHAR (50)  NOT NULL,
    [MaterialWeight]        FLOAT (53)     NOT NULL,
    [MaterialWidth]         FLOAT (53)     NULL,
    [MaterialThickness]     FLOAT (53)     NULL,
    [MaterialLength]        FLOAT (53)     NULL,
    [SeqNo]                 SMALLINT       NOT NULL
);

