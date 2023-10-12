CREATE TABLE [smf].[Roles] (
    [Id]               NVARCHAR (450) NOT NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    [Description]      NVARCHAR (128) NULL,
    CONSTRAINT [PK_NEW_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);



