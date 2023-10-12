CREATE TABLE [dbo].[PRMMaterials] (
    [MaterialId]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [SeqNo]                 SMALLINT       CONSTRAINT [DF_PRMMaterials_SeqNo] DEFAULT ((1)) NOT NULL,
    [MaterialCreatedTs]     DATETIME       CONSTRAINT [DF_PRMMaterials_MaterialCreatedTs] DEFAULT (getdate()) NOT NULL,
    [MaterialStartTs]       DATETIME       NULL,
    [MaterialEndTs]         DATETIME       NULL,
    [MaterialName]          NVARCHAR (50)  NOT NULL,
    [MaterialWeight]        FLOAT (53)     CONSTRAINT [DF_PRMMaterials_Weight] DEFAULT ((0)) NOT NULL,
    [MaterialThickness]     FLOAT (53)     NOT NULL,
    [MaterialWidth]         FLOAT (53)     NULL,
    [MaterialLength]        FLOAT (53)     NULL,
    [IsDummy]               BIT            CONSTRAINT [DF_Materials_IsDummy] DEFAULT ((0)) NOT NULL,
    [IsAssigned]            BIT            CONSTRAINT [DF_PRMMaterials_IsAssigned] DEFAULT ((0)) NOT NULL,
    [FKHeatId]              BIGINT         NOT NULL,
    [FKWorkOrderId]         BIGINT         NULL,
    [FKMaterialCatalogueId] BIGINT         NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_PRMMaterials_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_PRMMaterials_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_PRMMaterials_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_PRMMaterials_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_PRMMaterials_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED ([MaterialId] ASC),
    CONSTRAINT [FK_Materials_WorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId]),
    CONSTRAINT [FK_PRMMaterials_PRMHeats] FOREIGN KEY ([FKHeatId]) REFERENCES [dbo].[PRMHeats] ([HeatId]),
    CONSTRAINT [FK_PRMMaterials_PRMMaterialCatalogue] FOREIGN KEY ([FKMaterialCatalogueId]) REFERENCES [dbo].[PRMMaterialCatalogue] ([MaterialCatalogueId])
);




































GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_MaterialName]
    ON [dbo].[PRMMaterials]([MaterialName] ASC);


GO



GO



GO









GO



GO



GO
CREATE NONCLUSTERED INDEX [NCI_FKHeatId]
    ON [dbo].[PRMMaterials]([FKHeatId] ASC);






GO



GO
CREATE NONCLUSTERED INDEX [NCI_FKWorkOrderId]
    ON [dbo].[PRMMaterials]([FKWorkOrderId] ASC)
    INCLUDE([MaterialWeight]);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMMaterials_Audit] ON [dbo].[PRMMaterials] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MaterialId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MaterialId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMMaterials, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMMaterials] SET AUDUpdatedBy = APP_NAME() WHERE MaterialId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMMaterials] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MaterialId = @RecordId
    END
END;