CREATE TABLE [stage].[Events] (
    [EventId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]       DATETIME       NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (200) NULL,
    [IsDeleted]          BIT            NOT NULL,
    [IsBeforeCommit]     BIT            NOT NULL,
    [FKEventTypeId]      BIGINT         NOT NULL,
    [FKEventCatalogueId] BIGINT         NULL,
    [FKShiftCalendarId]  BIGINT         NULL,
    [FKWorkOrderId]      BIGINT         NULL,
    [FKRawMaterialId]    BIGINT         NULL,
    [FKAssetId]          BIGINT         NULL,
    [FKParentEventId]    BIGINT         NULL,
    [FKUserId]           NVARCHAR (450) NULL,
    [UserCreatedTs]      DATETIME       NULL,
    [UserUpdatedTs]      DATETIME       NULL,
    [UserComment]        NVARCHAR (200) NULL,
    [EventStartTs]       DATETIME       NOT NULL,
    [EventEndTs]         DATETIME       NULL,
    [IsPlanned]          BIT            NOT NULL
);

