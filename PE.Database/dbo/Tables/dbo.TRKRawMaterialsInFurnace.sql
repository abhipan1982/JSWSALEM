CREATE TABLE [dbo].[TRKRawMaterialsInFurnace] (
    [RawMaterialInFurnaceId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKRawMaterialId]        BIGINT         NULL,
    [OrderSeq]               SMALLINT       NOT NULL,
    [ChargingTs]             DATETIME       CONSTRAINT [DF_TRKRawMaterialsInFurnace_ChargingTs] DEFAULT (getdate()) NOT NULL,
    [IsTimerMeasurement]     BIT            CONSTRAINT [DF_TRKRawMaterialsInFurnace_IsTimerMeasurement] DEFAULT ((0)) NOT NULL,
    [TimeInFurnace]          INT            CONSTRAINT [DF_TRKRawMaterialsInFurnace_TimeInFurnace] DEFAULT ((0)) NOT NULL,
    [Temperature]            FLOAT (53)     NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_TRKRawMaterialsInFurnace_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_TRKRawMaterialsInFurnace_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_TRKRawMaterialsInFurnace_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_TRKRawMaterialsInFurnace_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_TRKRawMaterialsInFurnace_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TRKRawMaterialsInFurnace] PRIMARY KEY CLUSTERED ([RawMaterialInFurnaceId] ASC),
    CONSTRAINT [FK_TRKRawMaterialsInFurnace_MVHRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE SET NULL
);






GO
CREATE NONCLUSTERED INDEX [NCI_FKRawMaterialId]
    ON [dbo].[TRKRawMaterialsInFurnace]([FKRawMaterialId] ASC)
    INCLUDE([OrderSeq], [Temperature]);


GO
CREATE NONCLUSTERED INDEX [NCI_OrderSeq]
    ON [dbo].[TRKRawMaterialsInFurnace]([OrderSeq] ASC)
    INCLUDE([FKRawMaterialId], [Temperature]);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_TRKRawMaterialsInFurnace_Audit] ON [dbo].[TRKRawMaterialsInFurnace] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RawMaterialInFurnaceId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RawMaterialInFurnaceId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: TRKRawMaterialsInFurnace, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[TRKRawMaterialsInFurnace] SET AUDUpdatedBy = APP_NAME() WHERE RawMaterialInFurnaceId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[TRKRawMaterialsInFurnace] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RawMaterialInFurnaceId = @RecordId
    END
END;