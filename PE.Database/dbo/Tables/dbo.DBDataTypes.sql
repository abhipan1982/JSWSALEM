CREATE TABLE [dbo].[DBDataTypes] (
    [DataTypeId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [DataTypeName]       NVARCHAR (50)  NOT NULL,
    [DataTypeNameDotNet] NVARCHAR (50)  NULL,
    [DataType]           NVARCHAR (50)  NULL,
    [MaxLength]          SMALLINT       NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_DBDataTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_DBDataTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_DBDataTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_DBDataTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_DBDataTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DataTypes] PRIMARY KEY CLUSTERED ([DataTypeId] ASC)
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DataTypeName]
    ON [dbo].[DBDataTypes]([DataTypeName] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_DBDataTypes_Audit] ON [dbo].[DBDataTypes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.DataTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.DataTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: DBDataTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[DBDataTypes] SET AUDUpdatedBy = APP_NAME() WHERE DataTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[DBDataTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE DataTypeId = @RecordId
    END
END;