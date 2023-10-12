CREATE TABLE [dw].[DimUser] (
    [SourceName]       NVARCHAR (50)  NOT NULL,
    [SourceTime]       DATETIME       NOT NULL,
    [DimUserIsDeleted] BIT            NOT NULL,
    [DimUserHash]      VARBINARY (16) NULL,
    [DimUserKey]       NVARCHAR (450) NOT NULL,
    [UserName]         NVARCHAR (256) NULL,
    [UserFirstName]    NVARCHAR (50)  NULL,
    [UserLastName]     NVARCHAR (50)  NULL,
    [UserJobPosition]  NVARCHAR (100) NULL,
    [UserFullName]     NVARCHAR (100) NULL,
    [UserEmail]        NVARCHAR (256) NULL,
    [DimUserRow]       INT            IDENTITY (1, 1) NOT NULL
);

