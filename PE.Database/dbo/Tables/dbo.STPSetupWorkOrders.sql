CREATE TABLE [dbo].[STPSetupWorkOrders] (
    [SetupWorkOrderId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKWorkOrderId]    BIGINT         NOT NULL,
    [FKSetupId]        BIGINT         NOT NULL,
    [CalculatedTs]     DATETIME       CONSTRAINT [DF_STPSetupWorkOrders_CreatedTs] DEFAULT (getdate()) NULL,
    [SentTs]           DATETIME       NULL,
    [IsAmbiguous]      BIT            CONSTRAINT [DF_STPSetupWorkOrders_IsAmbiguous] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_STPSetupWorkOrders_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_STPSetupWorkOrders_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_STPSetupWorkOrders_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_STPSetupWorkOrders_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_STPSetupWorkOrders_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetupWorkOrders] PRIMARY KEY CLUSTERED ([SetupWorkOrderId] ASC),
    CONSTRAINT [FK_SetupWorkOrders_STPSetups] FOREIGN KEY ([FKSetupId]) REFERENCES [dbo].[STPSetups] ([SetupId]),
    CONSTRAINT [FK_STPSetupWorkOrders_PRMWorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId])
);








GO
CREATE NONCLUSTERED INDEX [NCI_WorkOrderId_SetupId]
    ON [dbo].[STPSetupWorkOrders]([FKWorkOrderId] ASC, [FKSetupId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetupWorkOrders_Audit] ON [dbo].[STPSetupWorkOrders] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupWorkOrderId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupWorkOrderId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetupWorkOrders, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetupWorkOrders] SET AUDUpdatedBy = APP_NAME() WHERE SetupWorkOrderId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetupWorkOrders] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupWorkOrderId = @RecordId
    END
END;