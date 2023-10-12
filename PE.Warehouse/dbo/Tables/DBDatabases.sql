CREATE TABLE [dbo].[DBDatabases] (
    [ServerId]            INT            IDENTITY (1, 1) NOT NULL,
    [ServerName]          VARCHAR (50)   NOT NULL,
    [ServerAddress]       VARCHAR (50)   NOT NULL,
    [DatabaseName]        VARCHAR (50)   NOT NULL,
    [DatabaseSchema]      VARCHAR (50)   NOT NULL,
    [DatabaseDescription] NVARCHAR (50)  NOT NULL,
    [IsWarehouse]         BIT            CONSTRAINT [DF_DBDatabases_IsWarehouse] DEFAULT ((0)) NOT NULL,
    [ConnectionString]    NVARCHAR (255) NULL,
    CONSTRAINT [PK_DatabaseServers] PRIMARY KEY CLUSTERED ([ServerId] ASC)
);



