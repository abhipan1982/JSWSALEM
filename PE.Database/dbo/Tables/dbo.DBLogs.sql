CREATE TABLE [dbo].[DBLogs] (
    [DBLogId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [LogDateTs]        DATETIME        CONSTRAINT [DF_DBLogs_LogDate] DEFAULT (getdate()) NOT NULL,
    [LogType]          NVARCHAR (10)   NULL,
    [LogSource]        NVARCHAR (50)   NULL,
    [LogValue]         NVARCHAR (50)   NULL,
    [ErrorMessage]     NVARCHAR (1000) NULL,
    [AUDCreatedTs]     DATETIME        CONSTRAINT [DF_DBLogs_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME        CONSTRAINT [DF_DBLogs_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255)  CONSTRAINT [DF_DBLogs_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT             CONSTRAINT [DF_DBLogs_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT             CONSTRAINT [DF_DBLogs_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DBLogs] PRIMARY KEY CLUSTERED ([DBLogId] ASC)
);










GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_DBLogs_Audit] ON [dbo].[DBLogs] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.DBLogId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.DBLogId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: DBLogs, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[DBLogs] SET AUDUpdatedBy = APP_NAME() WHERE DBLogId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[DBLogs] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE DBLogId = @RecordId
    END
END;