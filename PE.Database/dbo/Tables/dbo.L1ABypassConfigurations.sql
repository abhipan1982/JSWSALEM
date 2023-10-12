CREATE TABLE [dbo].[L1ABypassConfigurations] (
    [BypassConfigurationId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKBypassTypeId]               SMALLINT       NOT NULL,
    [OpcServerAddress]             NVARCHAR (250) NOT NULL,
    [OpcServerName]                NVARCHAR (250) CONSTRAINT [DF_L1ABypassConfigurations_OpcServerName] DEFAULT (N' ') NOT NULL,
    [OpcBypassParentStructureNode] NVARCHAR (250) NOT NULL,
    [OpcBypassName]                NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_BypassConfigurations] PRIMARY KEY CLUSTERED ([BypassConfigurationId] ASC),
    CONSTRAINT [FK_BypassConfigurations_BypassTypes] FOREIGN KEY ([FKBypassTypeId]) REFERENCES [dbo].[L1ABypassTypes] ([BypassTypeId])
);

