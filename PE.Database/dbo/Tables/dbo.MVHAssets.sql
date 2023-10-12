CREATE TABLE [dbo].[MVHAssets] (
    [AssetId]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [OrderSeq]               INT            CONSTRAINT [DF_MVHAssets_OrderSeq] DEFAULT ((0)) NOT NULL,
    [AssetCode]              INT            NOT NULL,
    [AssetName]              NVARCHAR (50)  NOT NULL,
    [AssetDescription]       NVARCHAR (100) NOT NULL,
    [FKParentAssetId]        BIGINT         NULL,
    [FKAssetTypeId]          BIGINT         NULL,
    [IsActive]               BIT            CONSTRAINT [DF_MVHAssets_IsActive] DEFAULT ((1)) NOT NULL,
    [IsArea]                 BIT            CONSTRAINT [DF_MVHAssets_IsArea] DEFAULT ((0)) NOT NULL,
    [IsZone]                 BIT            CONSTRAINT [DF_MVHAssets_IsZone] DEFAULT ((0)) NOT NULL,
    [IsTrackingPoint]        BIT            CONSTRAINT [DF_MVHAssets_IsTrackingPoint] DEFAULT ((1)) NOT NULL,
    [IsDelayCheckpoint]      BIT            CONSTRAINT [DF_MVHAssets_IsCheckpoint] DEFAULT ((0)) NOT NULL,
    [IsReversible]           BIT            CONSTRAINT [DF_MVHAssets_IsReversible] DEFAULT ((0)) NOT NULL,
    [IsVisibleOnMVH]         BIT            CONSTRAINT [DF_MVHAssets_IsVisibleOnMVH] DEFAULT ((0)) NOT NULL,
    [IsPositionBased]        BIT            CONSTRAINT [DF_MVHAssets_IsPositionBased] DEFAULT ((0)) NOT NULL,
    [PositionsNumber]        SMALLINT       NULL,
    [VirtualPositionsNumber] SMALLINT       NULL,
    [EnumTrackingAreaType]   SMALLINT       CONSTRAINT [DF_MVHAssets_EnumTrackingAreaType] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_MVHAssets_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_MVHAssets_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_MVHAssets_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_MVHAssets_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_MVHAssets_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED ([AssetId] ASC) WITH (FILLFACTOR = 90, PAD_INDEX = ON),
    CONSTRAINT [sprawdz] CHECK ([OrderSeq]>(0) OR [OrderSeq]=NULL),
    CONSTRAINT [FK_Assets_ParentAsset] FOREIGN KEY ([FKParentAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_MVHAssets_MVHAssetTypes] FOREIGN KEY ([FKAssetTypeId]) REFERENCES [dbo].[MVHAssetTypes] ([AssetTypeId]),
    CONSTRAINT [UQ_AssetName] UNIQUE NONCLUSTERED ([AssetName] ASC) WITH (FILLFACTOR = 90, PAD_INDEX = ON)
);






























GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OrderSeq]
    ON [dbo].[MVHAssets]([OrderSeq] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AssetCode]
    ON [dbo].[MVHAssets]([AssetCode] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHAssets_Audit] ON [dbo].[MVHAssets] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.AssetId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.AssetId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHAssets, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHAssets] SET AUDUpdatedBy = APP_NAME() WHERE AssetId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHAssets] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE AssetId = @RecordId
    END
END;