CREATE TABLE [stage].[Defects] (
    [DefectId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]        DATETIME       NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (200) NULL,
    [IsDeleted]           BIT            NOT NULL,
    [IsBeforeCommit]      BIT            NOT NULL,
    [FKDefectCatalogueId] BIGINT         NOT NULL,
    [FKRawMaterialId]     BIGINT         NULL,
    [FKProductId]         BIGINT         NULL,
    [FKAssetId]           BIGINT         NULL,
    [DefectName]          NVARCHAR (50)  NULL,
    [DefectDescription]   NVARCHAR (200) NULL,
    [DefectPosition]      SMALLINT       NULL,
    [DefectFrequency]     SMALLINT       NULL,
    [DefectScale]         SMALLINT       NULL
);

