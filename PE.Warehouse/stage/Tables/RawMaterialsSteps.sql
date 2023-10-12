CREATE TABLE [stage].[RawMaterialsSteps] (
    [RawMaterialStepId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]      DATETIME       NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (200) NULL,
    [IsDeleted]         BIT            NOT NULL,
    [IsBeforeCommit]    BIT            NOT NULL,
    [ProcessingStepNo]  SMALLINT       NOT NULL,
    [ProcessingStepTs]  DATETIME       NOT NULL,
    [FKRawMaterialId]   BIGINT         NOT NULL,
    [FKAssetId]         BIGINT         NOT NULL,
    [PassNo]            SMALLINT       NOT NULL,
    [IsReversed]        BIT            NOT NULL,
    [IsAssetExit]       BIT            NOT NULL
);

