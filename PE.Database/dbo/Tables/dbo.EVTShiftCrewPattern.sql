CREATE TABLE [dbo].[EVTShiftCrewPattern] (
    [ShiftCrewPatternId]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [OrderSeq]            SMALLINT       NOT NULL,
    [FKShiftDefinitionId] BIGINT         NOT NULL,
    [FKCrewId]            BIGINT         NOT NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_ShiftCrewPattern_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_ShiftCrewPattern_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_EVTShiftCrewPattern_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_ShiftCrewPattern_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_ShiftCrewPattern_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EVTShiftCrewPattern] PRIMARY KEY CLUSTERED ([ShiftCrewPatternId] ASC),
    CONSTRAINT [FK_EVTShiftCalendar_EVTCrews] FOREIGN KEY ([FKCrewId]) REFERENCES [dbo].[EVTCrews] ([CrewId]),
    CONSTRAINT [FK_EVTShiftCrewPattern_EVTShiftDefinitions] FOREIGN KEY ([FKShiftDefinitionId]) REFERENCES [dbo].[EVTShiftDefinitions] ([ShiftDefinitionId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTShiftCrewPattern_Audit] ON [dbo].[EVTShiftCrewPattern] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ShiftCrewPatternId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ShiftCrewPatternId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTShiftCrewPattern, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTShiftCrewPattern] SET AUDUpdatedBy = APP_NAME() WHERE ShiftCrewPatternId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTShiftCrewPattern] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ShiftCrewPatternId = @RecordId
    END
END;