CREATE TABLE [dw].[DimEventCatalogue] (
    [SourceName]                      NVARCHAR (50)  NOT NULL,
    [SourceTime]                      DATETIME       NOT NULL,
    [DimEventCatalogueIsDeleted]      BIT            NOT NULL,
    [DimEventCatalogueHash]           VARBINARY (16) NULL,
    [DimEventCatalogueKey]            BIGINT         NOT NULL,
    [DimEventTypeKey]                 BIGINT         NOT NULL,
    [DimEventCategoryKey]             BIGINT         NOT NULL,
    [DimEventGroupKey]                BIGINT         NULL,
    [DimEventCatalogueKeyParent]      BIGINT         NULL,
    [EventIsDelay]                    INT            NOT NULL,
    [EventTypeCode]                   SMALLINT       NOT NULL,
    [EventTypeName]                   NVARCHAR (50)  NOT NULL,
    [EventTypeDescription]            NVARCHAR (100) NULL,
    [EventCatalogueCode]              NVARCHAR (10)  NOT NULL,
    [EventCatalogueName]              NVARCHAR (50)  NOT NULL,
    [EventCatalogueDescription]       NVARCHAR (100) NULL,
    [EventStdTime]                    FLOAT (53)     NOT NULL,
    [EventIsPlanned]                  BIT            NOT NULL,
    [EventCategoryCode]               NVARCHAR (10)  NOT NULL,
    [EventCategoryName]               NVARCHAR (50)  NOT NULL,
    [EventCategoryDescription]        NVARCHAR (100) NULL,
    [EventCategoryAssignmentType]     VARCHAR (50)   NULL,
    [EventGroupCode]                  NVARCHAR (10)  NULL,
    [EventGroupName]                  NVARCHAR (50)  NULL,
    [EventCatalogueCodeParent]        NVARCHAR (10)  NULL,
    [EventCatalogueNameParent]        NVARCHAR (50)  NULL,
    [EventCatalogueDescriptionParent] NVARCHAR (100) NULL,
    [DimEventCatalogueRow]            INT            IDENTITY (1, 1) NOT NULL
);



