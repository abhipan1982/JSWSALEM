CREATE TABLE [smf].[UserTokens] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [Name]          NVARCHAR (128) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_NEW_UserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC) WITH (FILLFACTOR = 80),
    CONSTRAINT [FK_NEW_UserTokens_NEW_Users] FOREIGN KEY ([UserId]) REFERENCES [smf].[Users] ([Id])
);

