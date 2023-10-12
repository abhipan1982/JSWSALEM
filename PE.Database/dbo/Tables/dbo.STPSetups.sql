CREATE TABLE [dbo].[STPSetups] (
    [SetupId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKSetupTypeId]    BIGINT         NOT NULL,
    [SetupCode]        NVARCHAR (10)  NULL,
    [SetupName]        NVARCHAR (50)  NOT NULL,
    [SetupDescription] NVARCHAR (100) NULL,
    [CreatedTs]        DATETIME       CONSTRAINT [DF_STPSetups_CreatedTs_1] DEFAULT (getdate()) NOT NULL,
    [UpdatedTs]        DATETIME       CONSTRAINT [DF_STPSetups_UpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_STPSetups_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_STPSetups_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_STPSetups_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_STPSetups_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_STPSetups_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetups] PRIMARY KEY CLUSTERED ([SetupId] ASC),
    CONSTRAINT [FK_STPSetups_STPSetupTypes] FOREIGN KEY ([FKSetupTypeId]) REFERENCES [dbo].[STPSetupTypes] ([SetupTypeId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SetupName]
    ON [dbo].[STPSetups]([SetupName] ASC, [FKSetupTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_SetupType]
    ON [dbo].[STPSetups]([FKSetupTypeId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetups_Audit] ON [dbo].[STPSetups] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetups, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetups] SET AUDUpdatedBy = APP_NAME() WHERE SetupId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetups] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupId = @RecordId
    END
END;