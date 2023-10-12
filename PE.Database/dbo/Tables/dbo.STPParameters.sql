CREATE TABLE [dbo].[STPParameters] (
    [ParameterId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ParameterCode]        NVARCHAR (10)  NOT NULL,
    [ParameterName]        NVARCHAR (50)  NOT NULL,
    [ParameterDescription] NVARCHAR (100) NULL,
    [TableName]            NVARCHAR (20)  NULL,
    [ColumnId]             NVARCHAR (20)  NULL,
    [ColumnValue]          NVARCHAR (20)  NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_STPParameters_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_STPParameters_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_STPParameters_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_STPParameters_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_STPParameters_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPParameters] PRIMARY KEY CLUSTERED ([ParameterId] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ParameterName]
    ON [dbo].[STPParameters]([ParameterName] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ParameterCode]
    ON [dbo].[STPParameters]([ParameterCode] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPParameters_Audit] ON [dbo].[STPParameters] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ParameterId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ParameterId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPParameters, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPParameters] SET AUDUpdatedBy = APP_NAME() WHERE ParameterId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPParameters] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ParameterId = @RecordId
    END
END;