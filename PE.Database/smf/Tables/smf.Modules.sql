CREATE TABLE [smf].[Modules] (
    [ModuleId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [ModuleName] NVARCHAR (50) NOT NULL,
    [ModuleCode] VARCHAR (5)   NOT NULL,
    CONSTRAINT [PK_SMFModule] PRIMARY KEY CLUSTERED ([ModuleId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ModuleName]
    ON [smf].[Modules]([ModuleName] ASC);

