CREATE TABLE [dbo].[QECompensation] (
    [CompensationId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKRatingId]              BIGINT         NOT NULL,
    [FKCompensationTypeId]    BIGINT         NOT NULL,
    [CompensationName]        NVARCHAR (400) NULL,
    [CompensationAlternative] FLOAT (53)     NULL,
    [CompensationInfo]        NVARCHAR (400) NULL,
    [CompensationDetail]      NVARCHAR (400) NULL,
    [IsChosen]                BIT            CONSTRAINT [DF_QECompensation_IsChosen] DEFAULT ((0)) NOT NULL,
    [ChosenTs]                DATETIME       NULL,
    [AUDCreatedTs]            DATETIME       CONSTRAINT [DF_QECompensation_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]        DATETIME       CONSTRAINT [DF_QECompensation_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]            NVARCHAR (255) CONSTRAINT [DF_QECompensation_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]          BIT            CONSTRAINT [DF_QECompensation_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_QECompensation_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QECompensation] PRIMARY KEY CLUSTERED ([CompensationId] ASC),
    CONSTRAINT [FK_QECompensation_QECompensationType] FOREIGN KEY ([FKCompensationTypeId]) REFERENCES [dbo].[QECompensationType] ([CompensationTypeId]),
    CONSTRAINT [FK_QECompensation_QERating] FOREIGN KEY ([FKRatingId]) REFERENCES [dbo].[QERating] ([RatingId])
);










GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QECompensation_Audit] ON [dbo].[QECompensation] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.CompensationId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.CompensationId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QECompensation, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QECompensation] SET AUDUpdatedBy = APP_NAME() WHERE CompensationId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QECompensation] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE CompensationId = @RecordId
    END
END;