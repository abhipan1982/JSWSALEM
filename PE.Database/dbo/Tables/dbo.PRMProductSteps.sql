CREATE TABLE [dbo].[PRMProductSteps] (
    [ProductStepId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKProductId]      BIGINT         NOT NULL,
    [FKAssetId]        BIGINT         NOT NULL,
    [StepCreatedTs]    DATETIME       CONSTRAINT [DF_PRMProductSteps_StepCreatedTs] DEFAULT (getdate()) NOT NULL,
    [StepNo]           SMALLINT       CONSTRAINT [DF_PRMProductSteps_StepNo] DEFAULT ((0)) NOT NULL,
    [PositionX]        SMALLINT       CONSTRAINT [DF_PRMProductSteps_PositionX] DEFAULT ((0)) NOT NULL,
    [PositionY]        SMALLINT       CONSTRAINT [DF_PRMProductSteps_positionY] DEFAULT ((0)) NOT NULL,
    [GroupNo]          SMALLINT       CONSTRAINT [DF_PRMProductSteps_GroupNo] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMProductSteps_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMProductSteps_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMProductSteps_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMProductSteps_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMProductSteps_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMProductSteps] PRIMARY KEY CLUSTERED ([ProductStepId] ASC),
    CONSTRAINT [FK_PRMProductSteps_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_PRMProductSteps_PRMProducts] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId]) ON DELETE CASCADE
);












GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMProductSteps_Audit] ON [dbo].[PRMProductSteps] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ProductStepId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ProductStepId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMProductSteps, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMProductSteps] SET AUDUpdatedBy = APP_NAME() WHERE ProductStepId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMProductSteps] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ProductStepId = @RecordId
    END
END;