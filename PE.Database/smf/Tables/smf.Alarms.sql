CREATE TABLE [smf].[Alarms] (
    [AlarmId]             BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AlarmDate]           DATETIME       CONSTRAINT [DF_Alarms_AlarmDate] DEFAULT (getdate()) NOT NULL,
    [FKAlarmDefinitionId] BIGINT         NOT NULL,
    [FKUserIdConfirmed]   NVARCHAR (450) NULL,
    [IsConfirmed]         BIT            CONSTRAINT [DF_Alarms_IsConfirmed] DEFAULT ((0)) NOT NULL,
    [DefaultMessage]      NVARCHAR (255) NOT NULL,
    [Param1]              NVARCHAR (50)  NULL,
    [Param2]              NVARCHAR (50)  NULL,
    [Param3]              NVARCHAR (50)  NULL,
    [Param4]              NVARCHAR (50)  NULL,
    [ConfirmationDate]    DATETIME       NULL,
    [AlarmOwner]          NVARCHAR (50)  NULL,
    CONSTRAINT [PK_AlarmId] PRIMARY KEY CLUSTERED ([AlarmId] ASC),
    CONSTRAINT [FK_Alarms_AlarmDefinitions] FOREIGN KEY ([FKAlarmDefinitionId]) REFERENCES [smf].[AlarmDefinitions] ([AlarmDefinitionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Alarms_Users] FOREIGN KEY ([FKUserIdConfirmed]) REFERENCES [smf].[Users] ([Id]) ON DELETE SET NULL
);


















GO



GO







GO



GO



GO



GO



GO



GO



GO



GO


