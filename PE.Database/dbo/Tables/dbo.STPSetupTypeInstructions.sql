CREATE TABLE [dbo].[STPSetupTypeInstructions] (
    [SetupTypeInstructionId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKSetupTypeId]          BIGINT         NOT NULL,
    [FKInstructionId]        BIGINT         NOT NULL,
    [FKAssetId]              BIGINT         NULL,
    [OrderSeq]               SMALLINT       CONSTRAINT [DF_STPSetupTypeInstructions_OrderSeq] DEFAULT ((0)) NOT NULL,
    [RangeFrom]              FLOAT (53)     NULL,
    [RangeTo]                FLOAT (53)     NULL,
    [IsRequired]             BIT            CONSTRAINT [DF_STPSetupTypeInstructions_IsRequired] DEFAULT ((1)) NOT NULL,
    [IsSentToL1]             BIT            CONSTRAINT [DF_STPSetupTypeInstructions_IsSentToL1] DEFAULT ((1)) NOT NULL,
    [EnumCommChannelType]    SMALLINT       CONSTRAINT [DF_STPSetupTypeInstructions_EnumCommChannelType] DEFAULT ((0)) NOT NULL,
    [CommAttr1]              NVARCHAR (50)  NULL,
    [CommAttr2]              NVARCHAR (50)  NULL,
    [CommAttr3]              NVARCHAR (50)  NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_STPSetupTypeInstructions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_STPSetupTypeInstructions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_STPSetupTypeInstructions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_STPSetupTypeInstructions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_STPSetupTypeInstructions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetupTypeInstructions] PRIMARY KEY CLUSTERED ([SetupTypeInstructionId] ASC),
    CONSTRAINT [FK_STPSetupTypeInstructions_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_STPSetupTypeInstructions_STPInstructions] FOREIGN KEY ([FKInstructionId]) REFERENCES [dbo].[STPInstructions] ([InstructionId]),
    CONSTRAINT [FK_STPSetupTypeInstructions_STPSetupTypes] FOREIGN KEY ([FKSetupTypeId]) REFERENCES [dbo].[STPSetupTypes] ([SetupTypeId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SetupTypeId_OrderSeq]
    ON [dbo].[STPSetupTypeInstructions]([FKSetupTypeId] ASC, [OrderSeq] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SetupTypeId_InstructionId_AssetId]
    ON [dbo].[STPSetupTypeInstructions]([FKSetupTypeId] ASC, [FKInstructionId] ASC, [FKAssetId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetupTypeInstructions_Audit] ON [dbo].[STPSetupTypeInstructions] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupTypeInstructionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupTypeInstructionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetupTypeInstructions, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetupTypeInstructions] SET AUDUpdatedBy = APP_NAME() WHERE SetupTypeInstructionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetupTypeInstructions] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupTypeInstructionId = @RecordId
    END
END;