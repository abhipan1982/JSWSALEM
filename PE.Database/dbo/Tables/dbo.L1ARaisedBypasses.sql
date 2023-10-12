CREATE TABLE [dbo].[L1ARaisedBypasses] (
    [RaisedBypassId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [BypassTypeName]   NVARCHAR (250)  NOT NULL,
    [BypassName]       NVARCHAR (1000) NOT NULL,
    [OpcServerAddress] NVARCHAR (250)  NOT NULL,
    [OpcServerName]    NVARCHAR (250)  CONSTRAINT [DF_L1ARaisedBypasses_OpcServerName] DEFAULT (N' ') NOT NULL,
    [Timestamp]        DATETIME        NOT NULL,
    [Value]            BIT             CONSTRAINT [DF_L1ARaisedBypasses_Value] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RaisedBypasses] PRIMARY KEY CLUSTERED ([RaisedBypassId] ASC)
);

