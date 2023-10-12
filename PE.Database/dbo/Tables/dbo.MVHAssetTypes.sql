CREATE TABLE [dbo].[MVHAssetTypes] (
    [AssetTypeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [AssetTypeCode]        NVARCHAR (10)  NOT NULL,
    [AssetTypeName]        NVARCHAR (50)  NOT NULL,
    [AssetTypeDescription] NVARCHAR (200) NULL,
    [EnumYardType]         SMALLINT       CONSTRAINT [DF_MVHAssetTypes_EnumYardType] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_MVHAssetTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_MVHAssetTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_MVHAssetTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_MVHAssetTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_MVHAssetTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AssetTypes] PRIMARY KEY CLUSTERED ([AssetTypeId] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AssetTypeCode]
    ON [dbo].[MVHAssetTypes]([AssetTypeCode] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHAssetTypes_Audit] ON [dbo].[MVHAssetTypes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.AssetTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.AssetTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHAssetTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHAssetTypes] SET AUDUpdatedBy = APP_NAME() WHERE AssetTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHAssetTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE AssetTypeId = @RecordId
    END
END;