CREATE TABLE [dbo].[QEAliasValues] (
    [AliasValueId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKAliasId]        BIGINT         NOT NULL,
    [AliasValue]       FLOAT (53)     NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_QEAliasValues_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_QEAliasValues_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_QEAliasValues_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_QEAliasValues_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_QEAliasValues_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AliasValues] PRIMARY KEY CLUSTERED ([AliasValueId] ASC),
    CONSTRAINT [FK_QEAliasValues_QEAliases] FOREIGN KEY ([FKAliasId]) REFERENCES [dbo].[QEAliases] ([AliasId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QEAliasValues_Audit] ON [dbo].[QEAliasValues] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.AliasValueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.AliasValueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QEAliasValues, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QEAliasValues] SET AUDUpdatedBy = APP_NAME() WHERE AliasValueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QEAliasValues] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE AliasValueId = @RecordId
    END
END;