CREATE TABLE [dbo].[QEAliases] (
    [AliasId]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [AliasName]        NVARCHAR (50)   NOT NULL,
    [AliasDescription] NVARCHAR (200)  NULL,
    [EnumQESignalType] SMALLINT        CONSTRAINT [DF_QEAliases_EnumQESignalType] DEFAULT ((0)) NOT NULL,
    [StaticValue]      NVARCHAR (2000) NULL,
    [SQLQuery]         NVARCHAR (4000) NULL,
    [TableName]        NVARCHAR (50)   NULL,
    [ColumnName]       NVARCHAR (50)   NULL,
    [Aggregation]      NVARCHAR (10)   NULL,
    [WhereClause]      NVARCHAR (400)  NULL,
    [ColumnId]         NVARCHAR (50)   NULL,
    [LimitMin]         FLOAT (53)      NULL,
    [LimitMax]         FLOAT (53)      NULL,
    [FKUnitId]         BIGINT          NULL,
    [AUDCreatedTs]     DATETIME        CONSTRAINT [DF_QEAliases_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME        CONSTRAINT [DF_QEAliases_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255)  CONSTRAINT [DF_QEAliases_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT             CONSTRAINT [DF_QEAliases_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT             CONSTRAINT [DF_QEAliases_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Aliases] PRIMARY KEY CLUSTERED ([AliasId] ASC),
    CONSTRAINT [FK_QEAliases_UnitOfMeasure] FOREIGN KEY ([FKUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QEAliases_Audit] ON [dbo].[QEAliases] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.AliasId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.AliasId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QEAliases, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QEAliases] SET AUDUpdatedBy = APP_NAME() WHERE AliasId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QEAliases] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE AliasId = @RecordId
    END
END;
GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AliasName]
    ON [dbo].[QEAliases]([AliasName] ASC);

