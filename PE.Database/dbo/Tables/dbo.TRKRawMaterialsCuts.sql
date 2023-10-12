CREATE TABLE [dbo].[TRKRawMaterialsCuts] (
    [RawMaterialCutId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKRawMaterialId]  BIGINT         NOT NULL,
    [FKAssetId]        BIGINT         NOT NULL,
    [CuttingTs]        DATETIME       CONSTRAINT [DF_MVHRawMaterialsCuts_CuttingTs] DEFAULT (getdate()) NOT NULL,
    [CuttingLength]    FLOAT (53)     NOT NULL,
    [EnumTypeOfCut]    SMALLINT       CONSTRAINT [DF_MVHRawMaterialsCuts_EnumTypeOfCut] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_TRKRawMaterialsCuts_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_TRKRawMaterialsCuts_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_TRKRawMaterialsCuts_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_TRKRawMaterialsCuts_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_TRKRawMaterialsCuts_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RawMaterialCutId] PRIMARY KEY CLUSTERED ([RawMaterialCutId] ASC),
    CONSTRAINT [FK_MVHRawMaterialsCuts_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_PERawMaterialsCut_TRKRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_TRKRawMaterialsCuts_Audit] ON [dbo].[TRKRawMaterialsCuts] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RawMaterialCutId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RawMaterialCutId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: TRKRawMaterialsCuts, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[TRKRawMaterialsCuts] SET AUDUpdatedBy = APP_NAME() WHERE RawMaterialCutId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[TRKRawMaterialsCuts] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RawMaterialCutId = @RecordId
    END
END;