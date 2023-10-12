CREATE TABLE [smf].[RoleRights] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [RoleId]         NVARCHAR (450) NOT NULL,
    [ClaimType]      NVARCHAR (MAX) NULL,
    [ClaimValue]     NVARCHAR (MAX) NULL,
    [AccessUnitId]   BIGINT         NOT NULL,
    [PermissionType] SMALLINT       CONSTRAINT [DF_SMFRoleRights_PermissionType] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_NEW_RoleRights] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NEW_RoleRights_AccessUnits] FOREIGN KEY ([AccessUnitId]) REFERENCES [smf].[AccessUnits] ([AccessUnitId]) ON DELETE CASCADE,
    CONSTRAINT [FK_NEW_RoleRights_NEW_Roles] FOREIGN KEY ([RoleId]) REFERENCES [smf].[Roles] ([Id])
);









