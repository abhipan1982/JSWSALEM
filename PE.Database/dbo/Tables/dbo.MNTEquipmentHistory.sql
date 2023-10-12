CREATE TABLE [dbo].[MNTEquipmentHistory] (
    [EquipmentHistoryId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKEquipmentId]      BIGINT         NOT NULL,
    [MaterialsProcessed] BIGINT         NULL,
    [WeightProcessed]    FLOAT (53)     NULL,
    [EquipmentStatus]    SMALLINT       NOT NULL,
    [Remark]             NVARCHAR (200) NULL,
    [EnumServiceType]    SMALLINT       CONSTRAINT [DF_MNTEquipmentHistory_EnumServiceType] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_MNTEquipmentHistory_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_MNTEquipmentHistory_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_MNTEquipmentHistory_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_MNTEquipmentHistory_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_MNTEquipmentHistory_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTEquipmentHistory] PRIMARY KEY CLUSTERED ([EquipmentHistoryId] ASC),
    CONSTRAINT [FK_MNTEquipmentHistory_MNTEquipments] FOREIGN KEY ([FKEquipmentId]) REFERENCES [dbo].[MNTEquipments] ([EquipmentId]) ON DELETE CASCADE
);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MNTEquipmentHistory_Audit] ON [dbo].[MNTEquipmentHistory] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EquipmentHistoryId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EquipmentHistoryId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTEquipmentHistory, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTEquipmentHistory] SET AUDUpdatedBy = APP_NAME() WHERE EquipmentHistoryId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTEquipmentHistory] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EquipmentHistoryId = @RecordId
    END
END;