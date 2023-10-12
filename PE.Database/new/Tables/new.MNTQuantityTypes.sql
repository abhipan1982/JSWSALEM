CREATE TABLE [new].[MNTQuantityTypes] (
    [QuantityTypeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [QuantityTypeDescription] NVARCHAR (100) NOT NULL,
    [FKUnitId]                BIGINT         NULL,
    [FKHMIUnitId]             BIGINT         NULL,
    [AUDCreatedTs]            DATETIME       CONSTRAINT [DF_MNTQuantityTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]        DATETIME       CONSTRAINT [DF_MNTQuantityTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]            NVARCHAR (255) CONSTRAINT [DF_MNTQuantityTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]          BIT            CONSTRAINT [DF_MNTQuantityTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_MNTQuantityTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTQuantityTypes] PRIMARY KEY CLUSTERED ([QuantityTypeId] ASC),
    CONSTRAINT [FK_MNTQuantityTypes_UnitOfMeasure] FOREIGN KEY ([FKUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [FK_MNTQuantityTypes_UnitOfMeasure1] FOREIGN KEY ([FKHMIUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);




GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTQuantityTypes_Audit] ON new.MNTQuantityTypes AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.QuantityTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.QuantityTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTQuantityTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTQuantityTypes] SET AUDUpdatedBy = APP_NAME() WHERE QuantityTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTQuantityTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE QuantityTypeId = @RecordId
    END
END;