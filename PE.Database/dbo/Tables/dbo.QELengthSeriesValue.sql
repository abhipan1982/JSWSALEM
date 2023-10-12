CREATE TABLE [dbo].[QELengthSeriesValue] (
    [LengthSeriesValueId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKMappingValueId]    BIGINT         NOT NULL,
    [LengthPosition]      FLOAT (53)     NOT NULL,
    [LengthSeriesValue]   FLOAT (53)     NOT NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_QELengthSeriesValue_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_QELengthSeriesValue_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_QELengthSeriesValue_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_QELengthSeriesValue_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_QELengthSeriesValue_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QELengthSeriesValue] PRIMARY KEY CLUSTERED ([LengthSeriesValueId] ASC),
    CONSTRAINT [FK_QELengthSeriesValue_QEMappingValue] FOREIGN KEY ([FKMappingValueId]) REFERENCES [dbo].[QEMappingValue] ([MappingValueId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QELengthSeriesValue_Audit] ON [dbo].[QELengthSeriesValue] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.LengthSeriesValueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.LengthSeriesValueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QELengthSeriesValue, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QELengthSeriesValue] SET AUDUpdatedBy = APP_NAME() WHERE LengthSeriesValueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QELengthSeriesValue] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE LengthSeriesValueId = @RecordId
    END
END;