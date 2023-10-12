CREATE TABLE [new].[MNTIncidentTriggers] (
    [IncidentTriggerId]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKLimitCounterId]              BIGINT         NOT NULL,
    [FKIncidentTemplateId]          BIGINT         NOT NULL,
    [EnumLimitCounterThresholdType] SMALLINT       CONSTRAINT [DF_MNTLimitCounterIncidentTriggers_EnumValueType] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]                  DATETIME       CONSTRAINT [DF_MNTIncidentTypeLimitCounters_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]              DATETIME       CONSTRAINT [DF_MNTIncidentTypeLimitCounters_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                  NVARCHAR (255) CONSTRAINT [DF_MNTIncidentTypeLimitCounters_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                BIT            CONSTRAINT [DF_MNTIncidentTypeLimitCounters_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                     BIT            CONSTRAINT [DF_MNTIncidentTypeLimitCounters_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTIncidentTypeLimitCounters] PRIMARY KEY CLUSTERED ([IncidentTriggerId] ASC),
    CONSTRAINT [FK_MNTIncidentTriggers_MNTIncidentTemplates] FOREIGN KEY ([FKIncidentTemplateId]) REFERENCES [new].[MNTIncidentTemplates] ([IncidentTemplateId]),
    CONSTRAINT [FK_MNTIncidentTypeLimitCounters_MNTLimitCounters] FOREIGN KEY ([FKLimitCounterId]) REFERENCES [new].[MNTLimitCounters] ([LimitCounterId])
);

