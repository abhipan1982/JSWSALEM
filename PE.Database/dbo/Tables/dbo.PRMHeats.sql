CREATE TABLE [dbo].[PRMHeats] (
    [HeatId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKSteelgradeId]   BIGINT         NULL,
    [FKHeatSupplierId] BIGINT         NULL,
    [HeatName]         NVARCHAR (50)  NOT NULL,
    [HeatWeight]       FLOAT (53)     NULL,
    [HeatCreatedTs]    DATETIME       CONSTRAINT [DF_PRMHeats_HeatCreatedTs] DEFAULT (getdate()) NULL,
    [IsDummy]          BIT            CONSTRAINT [DF_Heats_IsDummyHeat] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMHeats_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMHeats_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMHeats_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMHeats_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMHeats_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Heats] PRIMARY KEY CLUSTERED ([HeatId] ASC),
    CONSTRAINT [FK_PEHeats_PEHeatSuppliers] FOREIGN KEY ([FKHeatSupplierId]) REFERENCES [dbo].[PRMHeatSuppliers] ([HeatSupplierId]),
    CONSTRAINT [FK_PRMHeats_PRMSteelgrades] FOREIGN KEY ([FKSteelgradeId]) REFERENCES [dbo].[PRMSteelgrades] ([SteelgradeId]) ON DELETE SET NULL,
    CONSTRAINT [UQ_UniqueHeatName] UNIQUE NONCLUSTERED ([HeatName] ASC)
);




























GO



GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMHeats_Audit] ON [dbo].[PRMHeats] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.HeatId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.HeatId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMHeats, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMHeats] SET AUDUpdatedBy = APP_NAME() WHERE HeatId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMHeats] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE HeatId = @RecordId
    END
END;