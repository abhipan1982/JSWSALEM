CREATE TABLE [dbo].[EVTShiftDefinitions] (
    [ShiftDefinitionId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKShiftLayoutId]        BIGINT         CONSTRAINT [DF_EVTShiftDefinitions_FKShiftLayoutId] DEFAULT ((1)) NOT NULL,
    [ShiftCode]              NVARCHAR (10)  NOT NULL,
    [DefaultStartTime]       TIME (7)       NOT NULL,
    [DefaultEndTime]         TIME (7)       NOT NULL,
    [ShiftStartsPreviousDay] BIT            NOT NULL,
    [ShiftEndsNextDay]       BIT            NOT NULL,
    [NextShiftDefinitionId]  BIGINT         NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_EVTShiftDefinitions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_EVTShiftDefinitions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_EVTShiftDefinitions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_EVTShiftDefinitions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_EVTShiftDefinitions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ShiftDefinitions] PRIMARY KEY CLUSTERED ([ShiftDefinitionId] ASC),
    CONSTRAINT [FK_EVTShiftDefinitions_EVTShiftLayouts] FOREIGN KEY ([FKShiftLayoutId]) REFERENCES [dbo].[EVTShiftLayouts] ([ShiftLayoutId])
);












GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ShiftCode]
    ON [dbo].[EVTShiftDefinitions]([ShiftCode] ASC);


GO



GO



GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTShiftDefinitions_Audit] ON [dbo].[EVTShiftDefinitions] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ShiftDefinitionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ShiftDefinitionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTShiftDefinitions, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTShiftDefinitions] SET AUDUpdatedBy = APP_NAME() WHERE ShiftDefinitionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTShiftDefinitions] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ShiftDefinitionId = @RecordId
    END
END;