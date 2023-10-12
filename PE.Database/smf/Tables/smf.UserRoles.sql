CREATE TABLE [smf].[UserRoles] (
    [UserId] NVARCHAR (450) NOT NULL,
    [RoleId] NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_NEW_UserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_NEW_UserRoles_NEW_Roles] FOREIGN KEY ([RoleId]) REFERENCES [smf].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NEW_UserRoles_NEW_Users] FOREIGN KEY ([UserId]) REFERENCES [smf].[Users] ([Id]) ON DELETE CASCADE
);







