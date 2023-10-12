CREATE TABLE [dbo].[STPSetupParameters] (
    [SetupParameterId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKSetupId]        BIGINT         NOT NULL,
    [FKParameterId]    BIGINT         NOT NULL,
    [ParameterValueId] BIGINT         NOT NULL,
    [IsRequired]       BIT            CONSTRAINT [DF_STPSetupParameters_IsRequired] DEFAULT ((1)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_STPSetupParameters_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_STPSetupParameters_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_STPSetupParameters_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_STPSetupParameters_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_STPSetupParameters_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetupParameters] PRIMARY KEY CLUSTERED ([SetupParameterId] ASC),
    CONSTRAINT [FK_STPSetupParameters_STPParameters] FOREIGN KEY ([FKParameterId]) REFERENCES [dbo].[STPParameters] ([ParameterId]),
    CONSTRAINT [FK_STPSetupParameters_STPSetups] FOREIGN KEY ([FKSetupId]) REFERENCES [dbo].[STPSetups] ([SetupId]) ON DELETE CASCADE
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SetupId_ParameterId]
    ON [dbo].[STPSetupParameters]([FKSetupId] ASC, [FKParameterId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParameterValueId]
    ON [dbo].[STPSetupParameters]([ParameterValueId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetupParameters_Audit] ON [dbo].[STPSetupParameters] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupParameterId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupParameterId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetupParameters, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetupParameters] SET AUDUpdatedBy = APP_NAME() WHERE SetupParameterId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetupParameters] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupParameterId = @RecordId
    END
END;