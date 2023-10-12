CREATE TABLE [stage].[Samples] (
    [SampleId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKMeasurementId]  BIGINT         NOT NULL,
    [IsValid]          BIT            NOT NULL,
    [AUDCreatedTs]     DATETIME       NOT NULL,
    [Value]            FLOAT (53)     NOT NULL,
    [Offset]           FLOAT (53)     NOT NULL,
    [AUDLastUpdatedTs] DATETIME       NOT NULL,
    [IsDeleted]        BIT            NOT NULL,
    [IsBeforeCommit]   BIT            NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (200) NULL
);

