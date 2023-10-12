CREATE TABLE [dbo].[STPIssues] (
    [IssueId]          SMALLINT       IDENTITY (1, 1) NOT NULL,
    [Issue]            CHAR (1)       NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_STPIssues_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_STPIssues_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_STPIssues_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_STPIssues_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_STPIssues_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPIssues] PRIMARY KEY CLUSTERED ([IssueId] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Issue]
    ON [dbo].[STPIssues]([Issue] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPIssues_Audit] ON [dbo].[STPIssues] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.IssueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.IssueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPIssues, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPIssues] SET AUDUpdatedBy = APP_NAME() WHERE IssueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPIssues] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE IssueId = @RecordId
    END
END;