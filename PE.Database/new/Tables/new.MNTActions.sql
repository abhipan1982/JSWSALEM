CREATE TABLE [new].[MNTActions] (
    [ActionId]          BIGINT         NOT NULL,
    [FKActionTypeId]    BIGINT         NOT NULL,
    [FKIncidentId]      BIGINT         NOT NULL,
    [FKEquipmentSetId]  BIGINT         NULL,
    [FKMemberId]        BIGINT         NULL,
    [ActionDescription] NVARCHAR (100) NULL,
    [EnumActionStatus]  SMALLINT       CONSTRAINT [DF_MNTActions_EnumActionStatus] DEFAULT ((0)) NOT NULL,
    [ActionStartTime]   DATETIME       CONSTRAINT [DF_MNTActions_ActionTs] DEFAULT (getdate()) NOT NULL,
    [ActionEndTime]     DATETIME       NULL,
    [AUDCreatedTs]      DATETIME       CONSTRAINT [DF_MNTActions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       CONSTRAINT [DF_MNTActions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (255) CONSTRAINT [DF_MNTActions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]    BIT            CONSTRAINT [DF_MNTActions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_MNTActions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTActions_1] PRIMARY KEY CLUSTERED ([ActionId] ASC, [ActionStartTime] ASC),
    CONSTRAINT [FK_MNTActions_MNTActionTypes] FOREIGN KEY ([FKActionTypeId]) REFERENCES [new].[MNTActionTypes] ([ActionTypeId]),
    CONSTRAINT [FK_MNTActions_MNTEquipmentSets] FOREIGN KEY ([FKEquipmentSetId]) REFERENCES [new].[MNTEquipmentSets] ([EquipmentSetId]),
    CONSTRAINT [FK_MNTActions_MNTIncidents] FOREIGN KEY ([FKIncidentId]) REFERENCES [new].[MNTIncidents] ([IncidentId]),
    CONSTRAINT [FK_MNTActions_MNTMembers] FOREIGN KEY ([FKMemberId]) REFERENCES [new].[MNTMembers] ([MemberId])
);




GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTActions_Audit] ON new.MNTActions AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ActionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ActionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTActions, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTActions] SET AUDUpdatedBy = APP_NAME() WHERE ActionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTActions] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ActionId = @RecordId
    END
END;