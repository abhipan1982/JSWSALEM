CREATE TABLE [smf].[AlarmMessages] (
    [AlarmMessageId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKAlarmDefinitionId] BIGINT         NOT NULL,
    [FKLanguageId]        BIGINT         CONSTRAINT [DF_AlarmMessages_FKLanguageId] DEFAULT ((1)) NOT NULL,
    [MessageText]         NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_AlarmMessageId] PRIMARY KEY CLUSTERED ([AlarmMessageId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_AlarmMessages_AlarmDefinitions] FOREIGN KEY ([FKAlarmDefinitionId]) REFERENCES [smf].[AlarmDefinitions] ([AlarmDefinitionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AlarmMessages_Languages] FOREIGN KEY ([FKLanguageId]) REFERENCES [smf].[Languages] ([LanguageId])
);



