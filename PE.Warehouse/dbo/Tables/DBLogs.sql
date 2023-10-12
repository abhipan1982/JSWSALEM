CREATE TABLE [dbo].[DBLogs] (
    [DBLogId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_DBLogs_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_DBLogs_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (200) NULL,
    [LogDateTs]        DATETIME       CONSTRAINT [DF_DBLogs_LogDate] DEFAULT (getdate()) NOT NULL,
    [LogType]          NVARCHAR (10)  NULL,
    [LogSource]        NVARCHAR (50)  NULL,
    [LogValue]         NVARCHAR (255) NULL,
    [LogMessage]       NVARCHAR (MAX) NULL,
    [ErrorMessage]     NVARCHAR (MAX) NULL
);

