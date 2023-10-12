CREATE TABLE [new].[MNTTools] (
    [ToolId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKToolTypeId]    BIGINT         NOT NULL,
    [ToolName]        NVARCHAR (50)  NOT NULL,
    [TollQuantity]    SMALLINT       CONSTRAINT [DF_MNTTools_TollQuantity] DEFAULT ((1)) NOT NULL,
    [ToolDescription] NVARCHAR (255) NULL,
    CONSTRAINT [PK_MNTTools] PRIMARY KEY CLUSTERED ([ToolId] ASC),
    CONSTRAINT [FK_MNTTools_MNTToolTypes] FOREIGN KEY ([FKToolTypeId]) REFERENCES [new].[MNTToolTypes] ([ToolTypeId])
);



