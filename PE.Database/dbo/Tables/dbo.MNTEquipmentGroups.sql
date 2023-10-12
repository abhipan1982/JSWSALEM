CREATE TABLE [dbo].[MNTEquipmentGroups] (
    [EquipmentGroupId]          BIGINT         IDENTITY (100, 1) NOT NULL,
    [EquipmentGroupCode]        NVARCHAR (10)  NOT NULL,
    [EquipmentGroupName]        NVARCHAR (50)  NULL,
    [EquipmentGroupDescription] NVARCHAR (100) NULL,
    [FKParentEquipmentGroupId]  BIGINT         NULL,
    [IsDefault]                 BIT            CONSTRAINT [DF_MNTEquipmentGroups_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]              DATETIME       CONSTRAINT [DF_MNTEquipmentGroups_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]          DATETIME       CONSTRAINT [DF_MNTEquipmentGroups_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]              NVARCHAR (255) CONSTRAINT [DF_MNTEquipmentGroups_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]            BIT            CONSTRAINT [DF_MNTEquipmentGroups_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                 BIT            CONSTRAINT [DF_MNTEquipmentGroups_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTEquipmentGroups] PRIMARY KEY CLUSTERED ([EquipmentGroupId] ASC),
    CONSTRAINT [FK_MNTEquipmentGroups_MNTEquipmentGroups] FOREIGN KEY ([FKParentEquipmentGroupId]) REFERENCES [dbo].[MNTEquipmentGroups] ([EquipmentGroupId])
);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MNTEquipmentGroups_Audit] ON [dbo].[MNTEquipmentGroups] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.EquipmentGroupId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.EquipmentGroupId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTEquipmentGroups, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTEquipmentGroups] SET AUDUpdatedBy = APP_NAME() WHERE EquipmentGroupId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTEquipmentGroups] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE EquipmentGroupId = @RecordId
    END
END;