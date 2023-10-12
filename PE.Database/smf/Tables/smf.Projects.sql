CREATE TABLE [smf].[Projects] (
    [ProjectId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [ProjectName] NVARCHAR (50) NOT NULL,
    [SMFVersion]  NVARCHAR (50) NULL,
    [PEVersion]   NVARCHAR (50) NULL,
    [DBVersion]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_SMFProjects] PRIMARY KEY CLUSTERED ([ProjectId] ASC)
);

