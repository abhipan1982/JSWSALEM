﻿CREATE TABLE [stage].[ProductSteps] (
    [ProductStepId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]     DATETIME       NOT NULL,
    [AUDLastUpdatedTs] DATETIME       NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (200) NULL,
    [IsDeleted]        BIT            NOT NULL,
    [IsBeforeCommit]   BIT            NOT NULL,
    [FKProductId]      BIGINT         NOT NULL,
    [FKAssetId]        BIGINT         NOT NULL,
    [StepNo]           SMALLINT       NOT NULL,
    [PositionX]        SMALLINT       NOT NULL,
    [PositionY]        SMALLINT       NOT NULL,
    [GroupNo]          SMALLINT       NOT NULL
);

