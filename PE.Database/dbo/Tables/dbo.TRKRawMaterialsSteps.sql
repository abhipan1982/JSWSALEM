CREATE TABLE [dbo].[TRKRawMaterialsSteps] (
    [RawMaterialStepId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProcessingStepNo]  SMALLINT       CONSTRAINT [DF_MVHRawMaterialsSteps_ProcessingStepNo] DEFAULT ((0)) NOT NULL,
    [ProcessingStepTs]  DATETIME       CONSTRAINT [DF_MVHRawMaterialsSteps_ProcessingStepTs] DEFAULT (getdate()) NOT NULL,
    [FKRawMaterialId]   BIGINT         NOT NULL,
    [FKAssetId]         BIGINT         NOT NULL,
    [PassNo]            SMALLINT       CONSTRAINT [DF_MVHRawMaterialsSteps_PassNo] DEFAULT ((1)) NOT NULL,
    [IsReversed]        BIT            CONSTRAINT [DF_MVHRawMaterialsSteps_IsReversed] DEFAULT ((0)) NOT NULL,
    [IsAssetExit]       BIT            CONSTRAINT [DF_TRKRawMaterialsSteps_IsAssetExit] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]      DATETIME       CONSTRAINT [DF_TRKRawMaterialsSteps_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       CONSTRAINT [DF_TRKRawMaterialsSteps_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (255) CONSTRAINT [DF_TRKRawMaterialsSteps_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]    BIT            CONSTRAINT [DF_TRKRawMaterialsSteps_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_TRKRawMaterialsSteps_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RawMaterialStepId] PRIMARY KEY CLUSTERED ([RawMaterialStepId] ASC),
    CONSTRAINT [FK_MVHRawMaterialsSteps_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_PERawMaterialsStep_PERawMaterialsIndex] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [NCI_FKRawMaterialId_ProcessingStepNo]
    ON [dbo].[TRKRawMaterialsSteps]([FKRawMaterialId] ASC, [ProcessingStepNo] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PE.Core.Constants.MaterialShapeType', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TRKRawMaterialsSteps';


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_TRKRawMaterialsSteps_Audit] ON [dbo].[TRKRawMaterialsSteps] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RawMaterialStepId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RawMaterialStepId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: TRKRawMaterialsSteps, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[TRKRawMaterialsSteps] SET AUDUpdatedBy = APP_NAME() WHERE RawMaterialStepId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[TRKRawMaterialsSteps] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RawMaterialStepId = @RecordId
    END
END;