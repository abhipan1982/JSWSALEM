CREATE TABLE [dbo].[PRMMaterialSteps] (
    [MaterialStepId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKMaterialId]     BIGINT         NOT NULL,
    [FKAssetId]        BIGINT         NOT NULL,
    [StepCreatedTs]    DATETIME       CONSTRAINT [DF_PRMMaterialSteps_StepCreatedTs] DEFAULT (getdate()) NOT NULL,
    [StepNo]           SMALLINT       CONSTRAINT [DF_PRMMaterialSteps_LocationStepNo] DEFAULT ((0)) NOT NULL,
    [PositionX]        SMALLINT       CONSTRAINT [DF_PRMMaterialSteps_PositionX] DEFAULT ((0)) NOT NULL,
    [PositionY]        SMALLINT       CONSTRAINT [DF_PRMMaterialSteps_PositionY] DEFAULT ((0)) NOT NULL,
    [GroupNo]          SMALLINT       CONSTRAINT [DF_PRMMaterialSteps_GroupNo] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMMaterialSteps_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMMaterialSteps_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMMaterialSteps_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMMaterialSteps_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMMaterialSteps_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMMaterialSteps] PRIMARY KEY CLUSTERED ([MaterialStepId] ASC),
    CONSTRAINT [FK_PRMMaterialSteps_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_PRMMaterialSteps_PRMMaterials] FOREIGN KEY ([FKMaterialId]) REFERENCES [dbo].[PRMMaterials] ([MaterialId]) ON DELETE CASCADE
);












GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_MaterialId_StepNo]
    ON [dbo].[PRMMaterialSteps]([FKMaterialId] ASC, [StepNo] ASC, [PositionX] ASC, [PositionY] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMMaterialSteps_Audit] ON [dbo].[PRMMaterialSteps] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MaterialStepId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MaterialStepId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMMaterialSteps, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMMaterialSteps] SET AUDUpdatedBy = APP_NAME() WHERE MaterialStepId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMMaterialSteps] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MaterialStepId = @RecordId
    END
END;