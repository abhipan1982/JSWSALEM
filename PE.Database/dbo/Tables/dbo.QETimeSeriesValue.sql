CREATE TABLE [dbo].[QETimeSeriesValue] (
    [TimeSeriesValueId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKMappingValueId]  BIGINT         NOT NULL,
    [TimePosition]      BIGINT         NOT NULL,
    [TimeSeriesValue]   FLOAT (53)     NOT NULL,
    [AUDCreatedTs]      DATETIME       CONSTRAINT [DF_QETimeSeriesValue_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       CONSTRAINT [DF_QETimeSeriesValue_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (255) CONSTRAINT [DF_QETimeSeriesValue_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]    BIT            CONSTRAINT [DF_QETimeSeriesValue_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_QETimeSeriesValue_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QETimeSeriesValue] PRIMARY KEY CLUSTERED ([TimeSeriesValueId] ASC),
    CONSTRAINT [FK_QETimeSeriesValue_QEMappingValue] FOREIGN KEY ([FKMappingValueId]) REFERENCES [dbo].[QEMappingValue] ([MappingValueId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QETimeSeriesValue_Audit] ON [dbo].[QETimeSeriesValue] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.TimeSeriesValueId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.TimeSeriesValueId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QETimeSeriesValue, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QETimeSeriesValue] SET AUDUpdatedBy = APP_NAME() WHERE TimeSeriesValueId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QETimeSeriesValue] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE TimeSeriesValueId = @RecordId
    END
END;