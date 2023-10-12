CREATE TABLE [new].[MNTIncidents] (
    [IncidentId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKIncidentTypeId]    BIGINT         NOT NULL,
    [FKEquipmentSetId]    BIGINT         NULL,
    [IsPlanned]           BIT            CONSTRAINT [DF_MNTIncidents_IsPlanned] DEFAULT ((0)) NOT NULL,
    [PrioritySequence]    SMALLINT       CONSTRAINT [DF_MNTIncidents_OrderSequence] DEFAULT ((1)) NOT NULL,
    [IncidentStartTime]   DATETIME       CONSTRAINT [DF_MNTIncidents_StartTime] DEFAULT (getdate()) NOT NULL,
    [IncidentEndTime]     DATETIME       NULL,
    [IncidentDescription] NVARCHAR (100) NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_MNTIncidents_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_MNTIncidents_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_MNTIncidents_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_MNTIncidents_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_MNTIncidents_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTIncidents] PRIMARY KEY CLUSTERED ([IncidentId] ASC),
    CONSTRAINT [FK_MNTIncidents_MNTDeviceComponents] FOREIGN KEY ([FKEquipmentSetId]) REFERENCES [new].[MNTEquipmentSets] ([EquipmentSetId]),
    CONSTRAINT [FK_MNTIncidents_MNTIncidentTypes] FOREIGN KEY ([FKIncidentTypeId]) REFERENCES [new].[MNTIncidentTypes] ([IncidentTypeId])
);




GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTIncidents_Audit] ON new.MNTIncidents AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.IncidentId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.IncidentId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTIncidents, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTIncidents] SET AUDUpdatedBy = APP_NAME() WHERE IncidentId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTIncidents] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE IncidentId = @RecordId
    END
END;