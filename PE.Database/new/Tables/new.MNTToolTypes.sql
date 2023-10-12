CREATE TABLE [new].[MNTToolTypes] (
    [ToolTypeId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [ToolTypeName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_MNTToolTypes] PRIMARY KEY CLUSTERED ([ToolTypeId] ASC)
);

