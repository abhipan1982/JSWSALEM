CREATE TABLE [dbo].[QERating] (
    [RatingId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [RatingCode]         FLOAT (53)     NULL,
    [RatingValue]        FLOAT (53)     NULL,
    [RatingValueForced]  FLOAT (53)     NULL,
    [RatingAlarm]        NVARCHAR (400) NULL,
    [RatingAffectedArea] FLOAT (53)     NULL,
    [RatingGroup]        INT            NULL,
    [RatingType]         INT            NULL,
    [RatingCreated]      DATETIME       CONSTRAINT [DF_QERating_RatingCreated] DEFAULT (getdate()) NOT NULL,
    [RatingModified]     DATETIME       CONSTRAINT [DF_QERating_RatingModified] DEFAULT (getdate()) NOT NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_QERating_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_QERating_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_QERating_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_QERating_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_QERating_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QERating] PRIMARY KEY CLUSTERED ([RatingId] ASC)
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QERating_Audit] ON [dbo].[QERating] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RatingId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RatingId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QERating, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QERating] SET AUDUpdatedBy = APP_NAME() WHERE RatingId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QERating] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RatingId = @RecordId
    END
END;