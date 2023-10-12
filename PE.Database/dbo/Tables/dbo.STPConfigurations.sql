CREATE TABLE [dbo].[STPConfigurations] (
    [ConfigurationId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [ConfigurationName]       NVARCHAR (50)  NOT NULL,
    [ConfigurationComment]    NVARCHAR (400) NULL,
    [ConfigurationVersion]    SMALLINT       CONSTRAINT [DF_STPGroups_GroupVersion] DEFAULT ((0)) NOT NULL,
    [ConfigurationCreatedTs]  DATETIME       CONSTRAINT [DF_STPGroups_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [ConfigurationLastSentTs] DATETIME       NULL,
    [AUDCreatedTs]            DATETIME       CONSTRAINT [DF_STPConfigurations_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]        DATETIME       CONSTRAINT [DF_STPConfigurations_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]            NVARCHAR (255) CONSTRAINT [DF_STPConfigurations_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]          BIT            CONSTRAINT [DF_STPConfigurations_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_STPConfigurations_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPConfigurations] PRIMARY KEY CLUSTERED ([ConfigurationId] ASC)
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPConfigurations_Audit] ON [dbo].[STPConfigurations] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ConfigurationId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ConfigurationId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPConfigurations, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPConfigurations] SET AUDUpdatedBy = APP_NAME() WHERE ConfigurationId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPConfigurations] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ConfigurationId = @RecordId
    END
END;