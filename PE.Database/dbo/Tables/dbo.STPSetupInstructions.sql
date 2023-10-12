CREATE TABLE [dbo].[STPSetupInstructions] (
    [SetupInstructionId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKSetupId]                BIGINT         NOT NULL,
    [FKSetupTypeInstructionId] BIGINT         NULL,
    [Value]                    NVARCHAR (255) NULL,
    [AUDCreatedTs]             DATETIME       CONSTRAINT [DF_STPSetupInstructions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]         DATETIME       CONSTRAINT [DF_STPSetupInstructions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]             NVARCHAR (255) CONSTRAINT [DF_STPSetupInstructions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]           BIT            CONSTRAINT [DF_STPSetupInstructions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                BIT            CONSTRAINT [DF_STPSetupInstructions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetupInstructions] PRIMARY KEY CLUSTERED ([SetupInstructionId] ASC),
    CONSTRAINT [FK_STPSetupInstructions_STPSetups] FOREIGN KEY ([FKSetupId]) REFERENCES [dbo].[STPSetups] ([SetupId]) ON DELETE CASCADE,
    CONSTRAINT [FK_STPSetupInstructions_STPSetupTypeInstructions] FOREIGN KEY ([FKSetupTypeInstructionId]) REFERENCES [dbo].[STPSetupTypeInstructions] ([SetupTypeInstructionId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SetupId_SetupTypeInstructionId]
    ON [dbo].[STPSetupInstructions]([FKSetupId] ASC, [FKSetupTypeInstructionId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_SetupTypeInstructionId]
    ON [dbo].[STPSetupInstructions]([FKSetupTypeInstructionId] ASC)
    INCLUDE([FKSetupId], [Value]);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetupInstructions_Audit] ON [dbo].[STPSetupInstructions] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupInstructionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupInstructionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetupInstructions, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetupInstructions] SET AUDUpdatedBy = APP_NAME() WHERE SetupInstructionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetupInstructions] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupInstructionId = @RecordId
    END
END;