CREATE TABLE [dbo].[PRMSteelGroups] (
    [SteelGroupId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SteelGroupCode]        NVARCHAR (10)  NOT NULL,
    [SteelGroupName]        NVARCHAR (50)  NOT NULL,
    [SteelGroupDescription] NVARCHAR (200) NULL,
    [IsDefault]             BIT            CONSTRAINT [DF_SteelGroups_IsDefault] DEFAULT ((0)) NOT NULL,
    [WearCoefficient]       FLOAT (53)     CONSTRAINT [DF_PRMSteelGroups_WearCoefficient] DEFAULT ((1)) NOT NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_PRMSteelGroups_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_PRMSteelGroups_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_PRMSteelGroups_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_PRMSteelGroups_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_PRMSteelGroups_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SteelGroupId] PRIMARY KEY CLUSTERED ([SteelGroupId] ASC),
    CONSTRAINT [UQ_SteelGroupName] UNIQUE NONCLUSTERED ([SteelGroupName] ASC)
);














GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMSteelGroups_Audit] ON [dbo].[PRMSteelGroups] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SteelGroupId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SteelGroupId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMSteelGroups, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMSteelGroups] SET AUDUpdatedBy = APP_NAME() WHERE SteelGroupId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMSteelGroups] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SteelGroupId = @RecordId
    END
END;