CREATE TABLE [dbo].[TRKRawMaterialLocations] (
    [RawMaterialLocationId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKAssetId]             BIGINT         NOT NULL,
    [AssetCode]             INT            NOT NULL,
    [PositionSeq]           SMALLINT       CONSTRAINT [DF_TRKRawMaterialLocations_PositionSeq] DEFAULT ((1)) NOT NULL,
    [OrderSeq]              SMALLINT       CONSTRAINT [DF_TRKRawMaterialLocations_OrderSeq] DEFAULT ((1)) NOT NULL,
    [FKRawMaterialId]       BIGINT         NULL,
    [CorrelationId]         NVARCHAR (50)  NULL,
    [EnumAreaType]          SMALLINT       CONSTRAINT [DF_TRKRawMaterialLocations_EnumAreaType] DEFAULT ((0)) NOT NULL,
    [IsVirtual]             BIT            CONSTRAINT [DF_TRKRawMaterialLocation_IsVirtual] DEFAULT ((0)) NOT NULL,
    [IsOccupied]            BIT            CONSTRAINT [DF_TRKRawMaterialLocations_IsOccupied] DEFAULT ((0)) NOT NULL,
    [FKCtrAssetId]          BIGINT         NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_TRKRawMaterialLocations_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_TRKRawMaterialLocations_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_TRKRawMaterialLocations_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_TRKRawMaterialLocations_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_TRKRawMaterialLocations_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TRKRawMaterialLocations] PRIMARY KEY CLUSTERED ([RawMaterialLocationId] ASC),
    CONSTRAINT [FK_TRKRawMaterialLocations_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TRKRawMaterialLocations_MVHAssets1] FOREIGN KEY ([FKCtrAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_TRKRawMaterialLocations_MVHRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE SET NULL
);




















GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AssetPositionSeqOrderSeq]
    ON [dbo].[TRKRawMaterialLocations]([AssetCode] ASC, [PositionSeq] ASC, [OrderSeq] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_RawMaterialId]
    ON [dbo].[TRKRawMaterialLocations]([FKRawMaterialId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AssetId]
    ON [dbo].[TRKRawMaterialLocations]([FKAssetId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AssetCode]
    ON [dbo].[TRKRawMaterialLocations]([AssetCode] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_TRKRawMaterialLocations_Audit] ON [dbo].[TRKRawMaterialLocations] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RawMaterialLocationId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RawMaterialLocationId FROM DELETED;
		/*
		SET @LogValue = CONCAT('Deleted record from table: TRKRawMaterialLocations, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
		*/
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[TRKRawMaterialLocations] SET AUDUpdatedBy = APP_NAME() WHERE RawMaterialLocationId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[TRKRawMaterialLocations] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RawMaterialLocationId = @RecordId
    END
END;