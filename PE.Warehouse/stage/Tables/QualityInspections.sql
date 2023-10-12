CREATE TABLE [stage].[QualityInspections] (
    [QualityInspectionId]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]         DATETIME       NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (200) NULL,
    [IsDeleted]            BIT            NOT NULL,
    [IsBeforeCommit]       BIT            NOT NULL,
    [FKRawMaterialId]      BIGINT         NOT NULL,
    [VisualInspection]     NVARCHAR (400) NULL,
    [DiameterMin]          FLOAT (53)     NULL,
    [DiameterMax]          FLOAT (53)     NULL,
    [EnumCrashTest]        SMALLINT       NULL,
    [EnumInspectionResult] SMALLINT       NULL
);

