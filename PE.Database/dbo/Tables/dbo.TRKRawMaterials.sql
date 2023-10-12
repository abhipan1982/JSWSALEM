CREATE TABLE [dbo].[TRKRawMaterials] (
    [RawMaterialId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKShiftCalendarId]      BIGINT         NOT NULL,
    [FKLastAssetId]          BIGINT         NULL,
    [FKScrapAssetId]         BIGINT         NULL,
    [FKMaterialId]           BIGINT         NULL,
    [FKProductId]            BIGINT         NULL,
    [RawMaterialName]        NVARCHAR (50)  NOT NULL,
    [OldRawMaterialName]     NVARCHAR (50)  NULL,
    [RawMaterialCreatedTs]   DATETIME       CONSTRAINT [DF_TRKRawMaterials_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [RawMaterialStartTs]     DATETIME       NULL,
    [RawMaterialEndTs]       DATETIME       NULL,
    [RollingStartTs]         DATETIME       NULL,
    [RollingEndTs]           DATETIME       NULL,
    [ProductCreatedTs]       DATETIME       NULL,
    [IsDummy]                BIT            CONSTRAINT [DF_Billets_IsDummyBillet] DEFAULT ((0)) NOT NULL,
    [IsVirtual]              BIT            CONSTRAINT [DF_MVHRawMaterials_IsVirtual] DEFAULT ((0)) NOT NULL,
    [EnumRawMaterialType]    SMALLINT       CONSTRAINT [DF_TRKRawMaterials_EnumRawMaterialType] DEFAULT ((0)) NOT NULL,
    [EnumRawMaterialStatus]  SMALLINT       CONSTRAINT [DF_RawMaterialsIndex_Status] DEFAULT ((0)) NOT NULL,
    [EnumLayerStatus]        SMALLINT       CONSTRAINT [DF_TRKRawMaterials_EnumLayerStatus] DEFAULT ((0)) NOT NULL,
    [EnumChargeType]         SMALLINT       CONSTRAINT [DF_MVHRawMaterials_EnumChargeType] DEFAULT ((0)) NOT NULL,
    [EnumRejectLocation]     SMALLINT       CONSTRAINT [DF_TRKRawMaterials_EnumRejectLocation] DEFAULT ((0)) NOT NULL,
    [EnumTypeOfScrap]        SMALLINT       CONSTRAINT [DF_TRKRawMaterials_EnumTypeOfScrap] DEFAULT ((0)) NOT NULL,
    [EnumGradingSource]      SMALLINT       CONSTRAINT [DF_TRKRawMaterials_EnumGradingSource] DEFAULT ((0)) NOT NULL,
    [EnumCuttingTip]         SMALLINT       CONSTRAINT [DF_TRKRawMaterials_EnumCuttingTip] DEFAULT ((0)) NOT NULL,
    [CuttingSeqNo]           SMALLINT       CONSTRAINT [DF_MVHRawMaterials_CuttingSeqNo] DEFAULT ((0)) NOT NULL,
    [ChildsNo]               SMALLINT       CONSTRAINT [DF_MVHRawMaterials_ChildsNo] DEFAULT ((0)) NOT NULL,
    [OutputPieces]           SMALLINT       CONSTRAINT [DF_TRKRawMaterials_OutputPieces] DEFAULT ((0)) NOT NULL,
    [SlittingFactor]         SMALLINT       CONSTRAINT [DF_TRKRawMaterials_SlittingFactor] DEFAULT ((0)) NOT NULL,
    [LastWeight]             FLOAT (53)     NULL,
    [LastLength]             FLOAT (53)     NULL,
    [LastTemperature]        FLOAT (53)     NULL,
    [LastGrading]            FLOAT (53)     NULL,
    [WeighingStationWeight]  FLOAT (53)     NULL,
    [ScrapPercent]           FLOAT (53)     NULL,
    [ScrapRemarks]           NVARCHAR (200) NULL,
    [FurnaceExitTemperature] FLOAT (53)     NULL,
    [FurnaceHeatingDuration] INT            NULL,
    [IsAfterDelayPoint]      BIT            CONSTRAINT [DF_MVHRawMaterials_IsAfterDelayPoint] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_TRKRawMaterials_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_TRKRawMaterials_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_TRKRawMaterials_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_TRKRawMaterials_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_TRKRawMaterials_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RawMaterialId] PRIMARY KEY CLUSTERED ([RawMaterialId] ASC),
    CONSTRAINT [FK_TRKRawMaterials_EVTShiftCalendar] FOREIGN KEY ([FKShiftCalendarId]) REFERENCES [dbo].[EVTShiftCalendar] ([ShiftCalendarId]),
    CONSTRAINT [FK_TRKRawMaterials_MVHAssets] FOREIGN KEY ([FKScrapAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_TRKRawMaterials_MVHAssets_LastAsset] FOREIGN KEY ([FKLastAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_TRKRawMaterials_PRMMaterials] FOREIGN KEY ([FKMaterialId]) REFERENCES [dbo].[PRMMaterials] ([MaterialId]) ON DELETE SET NULL,
    CONSTRAINT [FK_TRKRawMaterials_PRMProducts] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId]) ON DELETE SET NULL
);
















GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RawMaterialName]
    ON [dbo].[TRKRawMaterials]([RawMaterialName] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CuttingSeqNo]
    ON [dbo].[TRKRawMaterials]([RawMaterialId] ASC, [CuttingSeqNo] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_Status]
    ON [dbo].[TRKRawMaterials]([EnumRawMaterialStatus] ASC)
    INCLUDE([RawMaterialId]);


GO
CREATE NONCLUSTERED INDEX [NCI_ShiftCalendarId]
    ON [dbo].[TRKRawMaterials]([FKShiftCalendarId] ASC)
    INCLUDE([RawMaterialId]);


GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [NCI_ProductId]
    ON [dbo].[TRKRawMaterials]([FKProductId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_MaterialId]
    ON [dbo].[TRKRawMaterials]([FKMaterialId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_TRKRawMaterials_Audit] ON [dbo].[TRKRawMaterials] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RawMaterialId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RawMaterialId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: TRKRawMaterials, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[TRKRawMaterials] SET AUDUpdatedBy = APP_NAME() WHERE RawMaterialId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[TRKRawMaterials] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RawMaterialId = @RecordId
    END
END;
GO
-- =============================================
-- Author:		Klakla
-- Create date: 2019
-- Description:	LastUpdateTs
-- =============================================
CREATE   TRIGGER [dbo].[TR_TRKRawMaterials_Update] ON [dbo].[TRKRawMaterials] AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @RawMaterialId AS BIGINT;
    DECLARE @FKMaterialId AS BIGINT;
    DECLARE @FKProductId AS BIGINT;
    SELECT @RawMaterialId = inserted.RawMaterialId, 
           @FKMaterialId = inserted.FKMaterialId, 
           @FKProductId = inserted.FKProductId
    FROM inserted;

    --when Material is assigned then update L3 Materials
    IF @FKMaterialId > 0
        BEGIN
            UPDATE [dbo].[PRMMaterials]
              SET 
                  IsAssigned = 1
            WHERE MaterialId = @FKMaterialId;
        END;
    --when Product is assigned then update Products
    IF @FKProductId > 0
        BEGIN
            UPDATE [dbo].[PRMProducts]
              SET 
                  IsAssigned = 1
            WHERE ProductId = @FKProductId;
        END;
END;