CREATE TABLE [dbo].[QERootCause] (
    [RootCauseId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKRatingId]            BIGINT         NOT NULL,
    [RootCauseName]         NVARCHAR (400) NOT NULL,
    [RootCauseType]         INT            NULL,
    [RootCausePriority]     FLOAT (53)     NULL,
    [RootCauseInfo]         NVARCHAR (400) NULL,
    [RootCauseVerification] NVARCHAR (400) NULL,
    [RootCauseCorrection]   NVARCHAR (400) NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_QERootCause_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_QERootCause_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_QERootCause_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_QERootCause_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_QERootCause_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QERootCause] PRIMARY KEY CLUSTERED ([RootCauseId] ASC),
    CONSTRAINT [FK_QERootCause_QERating] FOREIGN KEY ([FKRatingId]) REFERENCES [dbo].[QERating] ([RatingId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QERootCause_Audit] ON [dbo].[QERootCause] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RootCauseId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RootCauseId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QERootCause, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QERootCause] SET AUDUpdatedBy = APP_NAME() WHERE RootCauseId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QERootCause] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RootCauseId = @RecordId
    END
END;