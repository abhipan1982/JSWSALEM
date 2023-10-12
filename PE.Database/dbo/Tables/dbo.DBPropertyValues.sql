CREATE TABLE [dbo].[DBPropertyValues] (
    [PropertyValueId]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKPropertyId]     BIGINT         NOT NULL,
    [Value]            NVARCHAR (50)  NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_DBPropertyValues_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_DBPropertyValues_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_DBPropertyValues_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_DBPropertyValues_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_DBPropertyValues_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PropertyValues] PRIMARY KEY CLUSTERED ([PropertyValueId] ASC),
    CONSTRAINT [FK_PropertyValues_Properties] FOREIGN KEY ([FKPropertyId]) REFERENCES [dbo].[DBProperties] ([PropertyId]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [NCI_PropertyId]
    ON [dbo].[DBPropertyValues]([FKPropertyId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_DBPropertyValues_Audit] ON [dbo].[DBPropertyValues] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.PropertyValueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.PropertyValueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: DBPropertyValues, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[DBPropertyValues] SET AUDUpdatedBy = APP_NAME() WHERE PropertyValueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[DBPropertyValues] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE PropertyValueId = @RecordId
    END
END;