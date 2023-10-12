CREATE TABLE [new].[MNTIncidentTemplates] (
    [IncidentTemplateId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKEquipmentTypeId]       BIGINT         NOT NULL,
    [FKIncidentTypeId]        BIGINT         NOT NULL,
    [IsPlanned]               BIT            CONSTRAINT [DF_MNTIncidentTemplatea_IsPlanned] DEFAULT ((0)) NOT NULL,
    [IsRequired]              BIT            CONSTRAINT [DF_MNTIncidentTemplatea_IsRequired] DEFAULT ((0)) NOT NULL,
    [OrderSequence]           SMALLINT       CONSTRAINT [DF_MNTIncidentTemplatea_OrderSequence] DEFAULT ((1)) NOT NULL,
    [DefaultIncidentDuration] INT            CONSTRAINT [DF_MNTIncidentTemplates_DefaultIncidentDuration] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]            DATETIME       CONSTRAINT [DF_MNTIncidentTemplates_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]        DATETIME       CONSTRAINT [DF_MNTIncidentTemplates_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]            NVARCHAR (255) CONSTRAINT [DF_MNTIncidentTemplates_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]          BIT            CONSTRAINT [DF_MNTIncidentTemplates_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_MNTIncidentTemplates_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTIncidentTemplates] PRIMARY KEY CLUSTERED ([IncidentTemplateId] ASC),
    CONSTRAINT [FK_MNTIncidentTemplatea_MNTDeviceTypes] FOREIGN KEY ([FKEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId]),
    CONSTRAINT [FK_MNTIncidentTemplatea_MNTIncidentTypes] FOREIGN KEY ([FKEquipmentTypeId]) REFERENCES [new].[MNTIncidentTypes] ([IncidentTypeId])
);

