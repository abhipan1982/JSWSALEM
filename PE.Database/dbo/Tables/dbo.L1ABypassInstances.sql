CREATE TABLE [dbo].[L1ABypassInstances] (
    [BypassInstanceId]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [FKBypassConfigurationId] BIGINT          NOT NULL,
    [OpcBypassNode]           NVARCHAR (1000) NOT NULL,
    CONSTRAINT [PK_BypassInstances] PRIMARY KEY CLUSTERED ([BypassInstanceId] ASC),
    CONSTRAINT [FK_BypassInstances_BypassConfigurations] FOREIGN KEY ([FKBypassConfigurationId]) REFERENCES [dbo].[L1ABypassConfigurations] ([BypassConfigurationId])
);

