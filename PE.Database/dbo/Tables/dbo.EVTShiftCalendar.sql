CREATE TABLE [dbo].[EVTShiftCalendar] (
    [ShiftCalendarId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKDaysOfYearId]      BIGINT         NOT NULL,
    [FKShiftDefinitionId] BIGINT         NOT NULL,
    [FKCrewId]            BIGINT         NOT NULL,
    [IsActualShift]       BIT            CONSTRAINT [DF_ShiftCalendar_IsActualShift] DEFAULT ((0)) NOT NULL,
    [IsActive]            BIT            CONSTRAINT [DF_EVTShiftCalendar_IsActive] DEFAULT ((1)) NOT NULL,
    [PlannedStartTime]    DATETIME       NOT NULL,
    [PlannedEndTime]      DATETIME       NOT NULL,
    [StartTime]           DATETIME       NULL,
    [EndTime]             DATETIME       NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_EVTShiftCalendar_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_EVTShiftCalendar_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_EVTShiftCalendar_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_EVTShiftCalendar_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_EVTShiftCalendar_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PEShiftCalendar] PRIMARY KEY CLUSTERED ([ShiftCalendarId] ASC),
    CONSTRAINT [FK_PEShiftCalendar_PECrews] FOREIGN KEY ([FKCrewId]) REFERENCES [dbo].[EVTCrews] ([CrewId]),
    CONSTRAINT [FK_PEShiftCalendar_PEShiftDefinitions] FOREIGN KEY ([FKShiftDefinitionId]) REFERENCES [dbo].[EVTShiftDefinitions] ([ShiftDefinitionId]),
    CONSTRAINT [FK_ShiftCalendar_DaysOfYear] FOREIGN KEY ([FKDaysOfYearId]) REFERENCES [dbo].[EVTDaysOfYear] ([DaysOfYearId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PlannedStartTime]
    ON [dbo].[EVTShiftCalendar]([PlannedStartTime] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PlannedEndTime]
    ON [dbo].[EVTShiftCalendar]([PlannedEndTime] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_CrewId]
    ON [dbo].[EVTShiftCalendar]([FKCrewId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ShiftDefinitionId]
    ON [dbo].[EVTShiftCalendar]([FKShiftDefinitionId] ASC)
    INCLUDE([FKDaysOfYearId], [FKCrewId], [PlannedStartTime], [PlannedEndTime]);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTShiftCalendar_Audit] ON [dbo].[EVTShiftCalendar] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ShiftCalendarId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ShiftCalendarId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTShiftCalendar, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTShiftCalendar] SET AUDUpdatedBy = APP_NAME() WHERE ShiftCalendarId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTShiftCalendar] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ShiftCalendarId = @RecordId
    END
END;