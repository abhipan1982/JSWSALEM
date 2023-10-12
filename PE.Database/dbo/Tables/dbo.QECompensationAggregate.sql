CREATE TABLE [dbo].[QECompensationAggregate] (
    [CompensationAggregateId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKCompensationId]        BIGINT         NOT NULL,
    [FKAssetId]               BIGINT         NOT NULL,
    [AUDCreatedTs]            DATETIME       CONSTRAINT [DF_QECompensationAggregate_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]        DATETIME       CONSTRAINT [DF_QECompensationAggregate_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]            NVARCHAR (255) CONSTRAINT [DF_QECompensationAggregate_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]          BIT            CONSTRAINT [DF_QECompensationAggregate_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_QECompensationAggregate_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QECompensationAggregate] PRIMARY KEY CLUSTERED ([CompensationAggregateId] ASC),
    CONSTRAINT [FK_QECompensationAggregate_QECompensation] FOREIGN KEY ([FKCompensationId]) REFERENCES [dbo].[QECompensation] ([CompensationId]) ON DELETE CASCADE
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QECompensationAggregate_Audit] ON [dbo].[QECompensationAggregate] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.CompensationAggregateId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.CompensationAggregateId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QECompensationAggregate, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QECompensationAggregate] SET AUDUpdatedBy = APP_NAME() WHERE CompensationAggregateId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QECompensationAggregate] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE CompensationAggregateId = @RecordId
    END
END;