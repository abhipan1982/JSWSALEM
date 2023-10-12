CREATE TABLE [dbo].[PRMWorkOrders] (
    [WorkOrderId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKProductCatalogueId]   BIGINT         NOT NULL,
    [FKMaterialCatalogueId]  BIGINT         NOT NULL,
    [FKSteelgradeId]         BIGINT         NOT NULL,
    [FKHeatId]               BIGINT         NULL,
    [FKCustomerId]           BIGINT         NULL,
    [FKParentWorkOrderId]    BIGINT         NULL,
    [EnumWorkOrderStatus]    SMALLINT       CONSTRAINT [DF_PRMWorkOrders_WorkOrderStatus] DEFAULT ((0)) NOT NULL,
    [WorkOrderName]          NVARCHAR (50)  NOT NULL,
    [WorkOrderCreatedInL3Ts] DATETIME       CONSTRAINT [DF_PRMWorkOrders_CreatedInL3] DEFAULT (getdate()) NOT NULL,
    [WorkOrderCreatedTs]     DATETIME       CONSTRAINT [DF_PRMWorkOrders_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [WorkOrderStartTs]       DATETIME       NULL,
    [WorkOrderEndTs]         DATETIME       NULL,
    [ToBeCompletedBeforeTs]  DATETIME       CONSTRAINT [DF_PRMWorkOrders_ToBeCompletedBefore] DEFAULT (getdate()) NOT NULL,
    [IsTestOrder]            BIT            CONSTRAINT [DF_WorkOrders_IsTestOrder] DEFAULT ((0)) NOT NULL,
    [IsSentToL3]             BIT            CONSTRAINT [DF_PRMWorkOrders_IsSentToL3] DEFAULT ((0)) NOT NULL,
    [IsBlocked]              BIT            CONSTRAINT [DF_PRMWorkOrders_IsBlocked] DEFAULT ((0)) NOT NULL,
    [TargetOrderWeight]      FLOAT (53)     NOT NULL,
    [TargetOrderWeightMin]   FLOAT (53)     NULL,
    [TargetOrderWeightMax]   FLOAT (53)     NULL,
    [BundleWeightMin]        FLOAT (53)     NULL,
    [BundleWeightMax]        FLOAT (53)     NULL,
    [L3NumberOfBillets]      SMALLINT       CONSTRAINT [DF_PRMWorkOrders_TargetMaterialNumber] DEFAULT ((1)) NOT NULL,
    [ExternalWorkOrderName]  NVARCHAR (50)  NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_PRMWorkOrders_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_PRMWorkOrders_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_PRMWorkOrders_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_PRMWorkOrders_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_PRMWorkOrders_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WorkOrders] PRIMARY KEY CLUSTERED ([WorkOrderId] ASC),
    CONSTRAINT [FK_PRMWorkOrders_PRMCustomers] FOREIGN KEY ([FKCustomerId]) REFERENCES [dbo].[PRMCustomers] ([CustomerId]),
    CONSTRAINT [FK_PRMWorkOrders_PRMHeats] FOREIGN KEY ([FKHeatId]) REFERENCES [dbo].[PRMHeats] ([HeatId]),
    CONSTRAINT [FK_PRMWorkOrders_PRMMaterialCatalogue] FOREIGN KEY ([FKMaterialCatalogueId]) REFERENCES [dbo].[PRMMaterialCatalogue] ([MaterialCatalogueId]),
    CONSTRAINT [FK_PRMWorkOrders_PRMSteelgrades] FOREIGN KEY ([FKSteelgradeId]) REFERENCES [dbo].[PRMSteelgrades] ([SteelgradeId]),
    CONSTRAINT [FK_PRMWorkOrders_PRMWorkOrders] FOREIGN KEY ([FKParentWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId]),
    CONSTRAINT [FK_WorkOrders_ProductCatalogue] FOREIGN KEY ([FKProductCatalogueId]) REFERENCES [dbo].[PRMProductCatalogue] ([ProductCatalogueId]),
    CONSTRAINT [UQ_WorkOrderName] UNIQUE NONCLUSTERED ([WorkOrderName] ASC)
);


























GO
CREATE NONCLUSTERED INDEX [NCI_WorkOrderStatus]
    ON [dbo].[PRMWorkOrders]([EnumWorkOrderStatus] ASC)
    INCLUDE([WorkOrderName]);








GO



GO
CREATE NONCLUSTERED INDEX [NCI_ProductCatalogueId]
    ON [dbo].[PRMWorkOrders]([FKProductCatalogueId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_MaterialCatalogueId]
    ON [dbo].[PRMWorkOrders]([FKMaterialCatalogueId] ASC);


GO







GO
CREATE NONCLUSTERED INDEX [NCI_CustomerId]
    ON [dbo].[PRMWorkOrders]([FKCustomerId] ASC);


GO



GO



GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMWorkOrders_Audit] ON [dbo].[PRMWorkOrders] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.WorkOrderId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.WorkOrderId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMWorkOrders, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMWorkOrders] SET AUDUpdatedBy = APP_NAME() WHERE WorkOrderId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMWorkOrders] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE WorkOrderId = @RecordId
    END
END;