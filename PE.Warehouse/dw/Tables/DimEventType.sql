CREATE TABLE [dw].[DimEventType] (
    [SourceName]                 NVARCHAR (50)  NOT NULL,
    [SourceTime]                 DATETIME       NOT NULL,
    [DimEventTypeIsDeleted]      BIT            NOT NULL,
    [DimEventTypeHash]           VARBINARY (16) NULL,
    [DimEventTypeKey]            BIGINT         NOT NULL,
    [DimEventTypeKeyParent]      BIGINT         NULL,
    [EventIsDelay]               INT            NOT NULL,
    [EventTypeCode]              SMALLINT       NOT NULL,
    [EventTypeName]              NVARCHAR (50)  NOT NULL,
    [EventTypeDescription]       NVARCHAR (100) NULL,
    [EventTypeCodeParent]        SMALLINT       NULL,
    [EventTypeNameParent]        NVARCHAR (50)  NULL,
    [EventTypeDescriptionParent] NVARCHAR (100) NULL,
    [DimEventTypeRow]            INT            IDENTITY (1, 1) NOT NULL
);

