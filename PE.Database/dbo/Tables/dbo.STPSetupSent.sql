CREATE TABLE [dbo].[STPSetupSent] (
    [SetupSentId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKSetupId]        BIGINT         NOT NULL,
    [FKWorkOrderId]    BIGINT         NOT NULL,
    [SentTs]           DATETIME       NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_STPSetupSent_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_STPSetupSent_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_STPSetupSent_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_STPSetupSent_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_STPSetupSent_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetupSent] PRIMARY KEY CLUSTERED ([SetupSentId] ASC),
    CONSTRAINT [FK_SetupSent_STPSetups] FOREIGN KEY ([FKSetupId]) REFERENCES [dbo].[STPSetups] ([SetupId]),
    CONSTRAINT [FK_STPSetupSent_PRMWorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetupSent_Audit] ON [dbo].[STPSetupSent] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupSentId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupSentId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetupSent, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetupSent] SET AUDUpdatedBy = APP_NAME() WHERE SetupSentId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetupSent] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupSentId = @RecordId
    END
END;