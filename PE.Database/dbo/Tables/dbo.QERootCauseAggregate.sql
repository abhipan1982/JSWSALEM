CREATE TABLE [dbo].[QERootCauseAggregate] (
    [RootCauseAggregateId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKRootCauseId]        BIGINT         NOT NULL,
    [FKAssetId]            BIGINT         NOT NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_QERootCauseAggregate_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_QERootCauseAggregate_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_QERootCauseAggregate_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_QERootCauseAggregate_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_QERootCauseAggregate_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QERootCauseAggregate] PRIMARY KEY CLUSTERED ([RootCauseAggregateId] ASC),
    CONSTRAINT [FK_QERootCauseAggregate_QERootCause] FOREIGN KEY ([FKRootCauseId]) REFERENCES [dbo].[QERootCause] ([RootCauseId]) ON DELETE CASCADE
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QERootCauseAggregate_Audit] ON [dbo].[QERootCauseAggregate] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RootCauseAggregateId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RootCauseAggregateId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QERootCauseAggregate, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QERootCauseAggregate] SET AUDUpdatedBy = APP_NAME() WHERE RootCauseAggregateId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QERootCauseAggregate] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RootCauseAggregateId = @RecordId
    END
END;