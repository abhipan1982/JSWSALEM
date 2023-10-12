CREATE TABLE [dbo].[HMIUserConfigurations] (
    [ConfigurationId]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [FKUserId]             NVARCHAR (450)  NULL,
    [UserName]             NVARCHAR (256)  NULL,
    [ConfigurationName]    NVARCHAR (50)   NULL,
    [ConfigurationContent] NVARCHAR (4000) NULL,
    [ConfigurationType]    NVARCHAR (50)   NULL,
    [AUDCreatedTs]         DATETIME        CONSTRAINT [DF_HMIUserConfigurations_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME        CONSTRAINT [DF_HMIUserConfigurations_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255)  CONSTRAINT [DF_HMIUserConfigurations_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT             CONSTRAINT [DF_HMIUserConfigurations_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT             CONSTRAINT [DF_HMIUserConfigurations_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserComparisonSchemes] PRIMARY KEY CLUSTERED ([ConfigurationId] ASC),
    CONSTRAINT [FK_HMIUserConfigurations_Users] FOREIGN KEY ([FKUserId]) REFERENCES [smf].[Users] ([Id])
);












GO
CREATE NONCLUSTERED INDEX [NCI_UserId]
    ON [dbo].[HMIUserConfigurations]([FKUserId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_HMIUserConfigurations_Audit] ON [dbo].[HMIUserConfigurations] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ConfigurationId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ConfigurationId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: HMIUserConfigurations, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[HMIUserConfigurations] SET AUDUpdatedBy = APP_NAME() WHERE ConfigurationId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[HMIUserConfigurations] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ConfigurationId = @RecordId
    END
END;