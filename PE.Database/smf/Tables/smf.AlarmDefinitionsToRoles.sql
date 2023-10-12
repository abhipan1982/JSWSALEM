CREATE TABLE [smf].[AlarmDefinitionsToRoles] (
    [AlarmDefinitionToRoleId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKAlarmDefinitionId]     BIGINT         NOT NULL,
    [FKRoleId]                NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AlarmDefinitionToRoleId] PRIMARY KEY CLUSTERED ([AlarmDefinitionToRoleId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_AlarmDefinitionsToRoles_AlarmDefinitions] FOREIGN KEY ([FKAlarmDefinitionId]) REFERENCES [smf].[AlarmDefinitions] ([AlarmDefinitionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AlarmDefinitionsToRoles_Roles] FOREIGN KEY ([FKRoleId]) REFERENCES [smf].[Roles] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AlarmDefinitionId_RoleId]
    ON [smf].[AlarmDefinitionsToRoles]([FKAlarmDefinitionId] ASC, [FKRoleId] ASC);

