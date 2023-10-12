CREATE TABLE [smf].[AlarmDefinitionParams] (
    [AlarmDefinitionParamId] BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKAlarmDefinitionId]    BIGINT        NOT NULL,
    [ParamKey]               SMALLINT      CONSTRAINT [DF_AlarmDefinitionParams_ParamKey] DEFAULT ((0)) NOT NULL,
    [ParamName]              NVARCHAR (50) NULL,
    CONSTRAINT [PK_AlarmDefinitionParamId] PRIMARY KEY CLUSTERED ([AlarmDefinitionParamId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_AlarmDefinitionParams_AlarmDefinitions] FOREIGN KEY ([FKAlarmDefinitionId]) REFERENCES [smf].[AlarmDefinitions] ([AlarmDefinitionId]) ON DELETE CASCADE
);

