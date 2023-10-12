CREATE TABLE [dw].[DimDefectCatalogue] (
    [SourceName]                       NVARCHAR (50)   NOT NULL,
    [SourceTime]                       DATETIME        NOT NULL,
    [DimDefectCatalogueIsDeleted]      BIT             NOT NULL,
    [DimDefectCatalogueHash]           VARBINARY (16)  NULL,
    [DimDefectCatalogueKey]            BIGINT          NOT NULL,
    [DimDefectCatalogueKeyParent]      BIGINT          NULL,
    [DefectCatalogueCode]              NVARCHAR (10)   NOT NULL,
    [DefectCatalogueName]              NVARCHAR (50)   NOT NULL,
    [DefectCatalogueDescription]       NVARCHAR (200)  NULL,
    [DefectCategoryCode]               NVARCHAR (10)   NOT NULL,
    [DefectCategoryName]               NVARCHAR (50)   NULL,
    [DefectCategoryDescription]        NVARCHAR (200)  NULL,
    [DefectCategoryAssignmentType]     VARCHAR (50)    NULL,
    [DefectGroupCode]                  NVARCHAR (10)   NULL,
    [DefectGroupName]                  NVARCHAR (50)   NULL,
    [DefectGroupDescription]           NVARCHAR (2000) NULL,
    [DefectCatalogueCodeParent]        NVARCHAR (10)   NULL,
    [DefectCatalogueNameParent]        NVARCHAR (50)   NULL,
    [DefectCatalogueDescriptionParent] NVARCHAR (200)  NULL,
    [DimDefectCatalogueRow]            INT             IDENTITY (1, 1) NOT NULL
);



