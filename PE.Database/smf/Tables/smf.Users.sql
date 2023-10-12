CREATE TABLE [smf].[Users] (
    [Id]                   NVARCHAR (450)     NOT NULL,
    [Hometown]             NVARCHAR (MAX)     NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [Discriminator]        NVARCHAR (128)     NULL,
    [FirstName]            NVARCHAR (50)      NULL,
    [LastName]             NVARCHAR (50)      NULL,
    [JobPosition]          NVARCHAR (100)     NULL,
    [LeaderId]             BIGINT             NULL,
    [HMIViewOrientation]   SMALLINT           NULL,
    [LanguageId]           BIGINT             NULL,
    [ReportUser]           SMALLINT           NULL,
    CONSTRAINT [PK_NEW_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NEW_Users_Languages] FOREIGN KEY ([LanguageId]) REFERENCES [smf].[Languages] ([LanguageId])
);





