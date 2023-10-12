CREATE TABLE [dbo].[EVTEventTypes] (
    [EventTypeId]          BIGINT         NOT NULL,
    [FKParentEvenTypeId]   BIGINT         NULL,
    [EventTypeCode]        SMALLINT       NOT NULL,
    [EventTypeName]        NVARCHAR (50)  NOT NULL,
    [EventTypeDescription] NVARCHAR (100) NULL,
    [IsVisibleOnHMI]       BIT            CONSTRAINT [DF_EventTypes_IsEditable] DEFAULT ((0)) NOT NULL,
    [HMIIcon]              NVARCHAR (50)  NULL,
    [HMIColor]             NVARCHAR (100) NULL,
    [HMILink]              NVARCHAR (255) NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_EVTEventTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_EVTEventTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_EVTEventTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_EVTEventTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_EVTEventTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EventTypes] PRIMARY KEY CLUSTERED ([EventTypeId] ASC),
    CONSTRAINT [FK_EVTEventTypes_EVTEventTypes] FOREIGN KEY ([FKParentEvenTypeId]) REFERENCES [dbo].[EVTEventTypes] ([EventTypeId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EventTypeCode]
    ON [dbo].[EVTEventTypes]([EventTypeCode] ASC);

