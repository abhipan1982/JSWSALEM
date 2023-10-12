CREATE TABLE [smf].[AlarmDefinitions] (
    [AlarmDefinitionId]     BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKAlarmCategoryId]     BIGINT         NOT NULL,
    [FKProjectId]           BIGINT         NULL,
    [DefinitionCode]        NVARCHAR (10)  NOT NULL,
    [DefinitionDescription] NVARCHAR (200) NULL,
    [DefinitionCreated]     DATETIME       CONSTRAINT [DF_AlarmDefinitions_DefinitionCreated] DEFAULT (getdate()) NOT NULL,
    [IsStandard]            BIT            CONSTRAINT [DF_AlarmDefinitions_IsStandard] DEFAULT ((1)) NOT NULL,
    [IsToConfirm]           BIT            NOT NULL,
    [IsPopupShow]           BIT            CONSTRAINT [DF_AlarmDefinitions_IsPopupShow] DEFAULT ((0)) NOT NULL,
    [EnumAlarmType]         SMALLINT       NOT NULL,
    CONSTRAINT [PK_AlarmDefinitionId] PRIMARY KEY CLUSTERED ([AlarmDefinitionId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_AlarmDefinitions_AlarmCategories] FOREIGN KEY ([FKAlarmCategoryId]) REFERENCES [smf].[AlarmCategories] ([AlarmCategoryId]),
    CONSTRAINT [FK_AlarmDefinitions_Projects] FOREIGN KEY ([FKProjectId]) REFERENCES [smf].[Projects] ([ProjectId]),
    CONSTRAINT [UQ_AlarmDefinitions_DefinitionCode] UNIQUE NONCLUSTERED ([DefinitionCode] ASC) WITH (FILLFACTOR = 90)
);








GO


