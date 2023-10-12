CREATE TABLE [stage].[Measurements] (
    [MeasurementId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]       DATETIME       NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (200) NULL,
    [IsDeleted]          BIT            NOT NULL,
    [IsBeforeCommit]     BIT            NOT NULL,
    [FKFeatureId]        BIGINT         NOT NULL,
    [FKRawMaterialId]    BIGINT         NULL,
    [CreatedTs]          DATETIME       NOT NULL,
    [PassNo]             SMALLINT       NOT NULL,
    [IsLastPass]         BIT            NOT NULL,
    [IsValid]            BIT            NOT NULL,
    [NoOfSamples]        SMALLINT       NOT NULL,
    [ValueAvg]           FLOAT (53)     NOT NULL,
    [ValueMin]           FLOAT (53)     NULL,
    [ValueMax]           FLOAT (53)     NULL,
    [FirstMeasurementTs] DATETIME       NULL,
    [LastMeasurementTs]  DATETIME       NULL,
    [ActualLength]       FLOAT (53)     NULL
);

