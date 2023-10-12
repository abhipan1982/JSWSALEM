CREATE TABLE [dbo].[PRMProducts] (
    [ProductId]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductCreatedTs] DATETIME       CONSTRAINT [DF_PRMProducts_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [ProductName]      NVARCHAR (50)  NOT NULL,
    [ProductWeight]    FLOAT (53)     NOT NULL,
    [BarsCounter]      SMALLINT       CONSTRAINT [DF_PRMProducts_BarsCounter] DEFAULT ((0)) NOT NULL,
    [EnumWeightSource] SMALLINT       CONSTRAINT [DF_PRMProducts_EnumWeightSource] DEFAULT ((0)) NOT NULL,
    [IsDummy]          BIT            CONSTRAINT [DF_PRMProducts_IsDummy] DEFAULT ((0)) NOT NULL,
    [IsAssigned]       BIT            CONSTRAINT [DF_PRMProducts_IsAssigned] DEFAULT ((0)) NOT NULL,
    [FKWorkOrderId]    BIGINT         NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMProducts_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMProducts_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMProducts_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMProducts_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMProducts_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [FK_Products_WorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId])
);


























GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductName]
    ON [dbo].[PRMProducts]([ProductName] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_WorkOrderId]
    ON [dbo].[PRMProducts]([FKWorkOrderId] ASC)
    INCLUDE([ProductWeight]);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMProducts_Audit] ON [dbo].[PRMProducts] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ProductId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ProductId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMProducts, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMProducts] SET AUDUpdatedBy = APP_NAME() WHERE ProductId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMProducts] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ProductId = @RecordId
    END
END;