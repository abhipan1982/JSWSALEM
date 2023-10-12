CREATE TABLE [dbo].[DBProperties] (
    [PropertyId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKDataTypeId]        BIGINT         NOT NULL,
    [PropertyCode]        NVARCHAR (10)  NOT NULL,
    [PropertyName]        NVARCHAR (50)  NOT NULL,
    [PropertyDescription] NVARCHAR (100) NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_DBProperties_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_DBProperties_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_DBProperties_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_DBProperties_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_DBProperties_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED ([PropertyId] ASC),
    CONSTRAINT [FK_Properties_DataTypes] FOREIGN KEY ([FKDataTypeId]) REFERENCES [dbo].[DBDataTypes] ([DataTypeId])
);






GO
CREATE NONCLUSTERED INDEX [NCI_DataTypeId]
    ON [dbo].[DBProperties]([FKDataTypeId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_DBProperties_Audit] ON [dbo].[DBProperties] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.PropertyId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.PropertyId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: DBProperties, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[DBProperties] SET AUDUpdatedBy = APP_NAME() WHERE PropertyId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[DBProperties] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE PropertyId = @RecordId
    END
END;