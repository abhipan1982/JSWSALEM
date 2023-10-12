CREATE TABLE [dbo].[L1ABypassTypes] (
    [BypassTypeId]   SMALLINT       IDENTITY (1, 1) NOT NULL,
    [BypassTypeCode] SMALLINT       NOT NULL,
    [BypassTypeName] NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_BypassTypes] PRIMARY KEY CLUSTERED ([BypassTypeId] ASC)
);

