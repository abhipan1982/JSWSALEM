CREATE TABLE [dbo].[QECompensationType] (
    [CompensationTypeId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [CompensationName]       NVARCHAR (400) NOT NULL,
    [CompensationRatingCode] FLOAT (53)     NOT NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_QECompensationType_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_QECompensationType_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_QECompensationType_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_QECompensationType_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_QECompensationType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QECompensationType] PRIMARY KEY CLUSTERED ([CompensationTypeId] ASC)
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QECompensationType_Audit] ON [dbo].[QECompensationType] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.CompensationTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.CompensationTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QECompensationType, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QECompensationType] SET AUDUpdatedBy = APP_NAME() WHERE CompensationTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QECompensationType] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE CompensationTypeId = @RecordId
    END
END;