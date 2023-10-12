CREATE TABLE [dbo].[MNTEquipments] (
    [EquipmentId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [EquipmentCode]         NVARCHAR (10)  NOT NULL,
    [EquipmentName]         NVARCHAR (50)  NULL,
    [EquipmentDescription]  NVARCHAR (100) NULL,
    [FKEquipmentGroupId]    BIGINT         NOT NULL,
    [FKEquipmentSupplierId] BIGINT         NULL,
    [FKAssetId]             BIGINT         NULL,
    [FKParentEquipmentId]   BIGINT         NULL,
    [EquipmentStatus]       SMALLINT       CONSTRAINT [DF_MNTEquipments_EquipmentStatus] DEFAULT ((0)) NOT NULL,
    [AccumulationType]      SMALLINT       CONSTRAINT [DF_MNTEquipments_AccumulationType] DEFAULT ((0)) NOT NULL,
    [ActualValue]           FLOAT (53)     NULL,
    [WarningValue]          FLOAT (53)     NULL,
    [AlarmValue]            FLOAT (53)     NULL,
    [IsActive]              BIT            CONSTRAINT [DF_MNTEquipments_IsActive] DEFAULT ((1)) NOT NULL,
    [ServiceExpires]        DATETIME       NULL,
    [CntMatsProcessed]      BIGINT         NULL,
    [EnumServiceType]       SMALLINT       CONSTRAINT [DF_MNTEquipments_EnumServiceType] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_MNTEquipments_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_MNTEquipments_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_MNTEquipments_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_MNTEquipments_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_MNTEquipments_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTEquipments] PRIMARY KEY CLUSTERED ([EquipmentId] ASC),
    CONSTRAINT [FK_MNTEquipments_MNTEquipmentGroups] FOREIGN KEY ([FKEquipmentGroupId]) REFERENCES [dbo].[MNTEquipmentGroups] ([EquipmentGroupId]),
    CONSTRAINT [FK_MNTEquipments_MNTEquipments] FOREIGN KEY ([FKParentEquipmentId]) REFERENCES [dbo].[MNTEquipments] ([EquipmentId]),
    CONSTRAINT [FK_MNTEquipments_MNTEquipmentSuppliers] FOREIGN KEY ([FKEquipmentSupplierId]) REFERENCES [dbo].[MNTEquipmentSuppliers] ([EquipmentSupplierId]),
    CONSTRAINT [FK_MNTEquipments_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId])
);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MNTEquipments_Audit] ON [dbo].[MNTEquipments] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EquipmentId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EquipmentId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTEquipments, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTEquipments] SET AUDUpdatedBy = APP_NAME() WHERE EquipmentId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTEquipments] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EquipmentId = @RecordId
    END
END;