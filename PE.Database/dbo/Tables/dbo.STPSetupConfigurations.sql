CREATE TABLE [dbo].[STPSetupConfigurations] (
    [SetupConfigurationId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKConfigurationId]            BIGINT         NOT NULL,
    [FKSetupId]                    BIGINT         NOT NULL,
    [SetupConfigurationLastSentTs] DATETIME       NULL,
    [AUDCreatedTs]                 DATETIME       CONSTRAINT [DF_STPSetupConfigurations_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]             DATETIME       CONSTRAINT [DF_STPSetupConfigurations_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                 NVARCHAR (255) CONSTRAINT [DF_STPSetupConfigurations_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]               BIT            CONSTRAINT [DF_STPSetupConfigurations_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                    BIT            CONSTRAINT [DF_STPSetupConfigurations_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetupGroup] PRIMARY KEY CLUSTERED ([SetupConfigurationId] ASC),
    CONSTRAINT [FK_STPSetupConfigurations_STPConfigurations] FOREIGN KEY ([FKConfigurationId]) REFERENCES [dbo].[STPConfigurations] ([ConfigurationId]),
    CONSTRAINT [FK_STPSetupConfigurations_STPSetups] FOREIGN KEY ([FKSetupId]) REFERENCES [dbo].[STPSetups] ([SetupId])
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetupConfigurations_Audit] ON [dbo].[STPSetupConfigurations] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupConfigurationId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupConfigurationId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetupConfigurations, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetupConfigurations] SET AUDUpdatedBy = APP_NAME() WHERE SetupConfigurationId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetupConfigurations] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupConfigurationId = @RecordId
    END
END;