CREATE TABLE [dbo].[MVHFeatures] (
    [FeatureId]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKAssetId]                  BIGINT         NOT NULL,
    [FKUnitOfMeasureId]          BIGINT         NOT NULL,
    [FKExtUnitOfMeasureId]       BIGINT         NOT NULL,
    [FKDataTypeId]               BIGINT         NOT NULL,
    [FKParentFeatureId]          BIGINT         NULL,
    [FeatureCode]                INT            NOT NULL,
    [FeatureName]                NVARCHAR (75)  NOT NULL,
    [FeatureDescription]         NVARCHAR (100) NULL,
    [IsSampledFeature]           BIT            CONSTRAINT [DF_MVHFeatures_IsSampled] DEFAULT ((0)) NOT NULL,
    [IsMaterialRelated]          BIT            CONSTRAINT [DF_MVHFeatures_IsMaterialRelated] DEFAULT ((1)) NOT NULL,
    [IsLengthRelated]            BIT            CONSTRAINT [DF_MVHFeatures_IsLengthRelated] DEFAULT ((0)) NOT NULL,
    [IsQETrigger]                BIT            CONSTRAINT [DF_MVHFeatures_IsQETrigger] DEFAULT ((0)) NOT NULL,
    [IsDigital]                  BIT            CONSTRAINT [DF_MVHFeatures_IsDigital] DEFAULT ((1)) NOT NULL,
    [IsActive]                   BIT            CONSTRAINT [DF_MVHFeatures_IsActive] DEFAULT ((1)) NOT NULL,
    [IsOnHMI]                    BIT            CONSTRAINT [DF_MVHFeatures_OnHMI] DEFAULT ((0)) NOT NULL,
    [IsTrackingPoint]            AS             (isnull(CONVERT([bit],case when [EnumFeatureType]>=(100) AND [EnumFeatureType]<=(199) then (1) else (0) end),(0))),
    [IsMeasurementPoint]         AS             (isnull(CONVERT([bit],case when [EnumFeatureType]>=(200) AND [EnumFeatureType]<=(299) then (1) else (0) end),(0))),
    [IsConsumptionPoint]         AS             (isnull(CONVERT([bit],case when [EnumFeatureType]>=(280) AND [EnumFeatureType]<=(299) then (1) else (0) end),(0))),
    [SampleOffsetTime]           FLOAT (53)     NULL,
    [ConsumptionAggregationTime] FLOAT (53)     NULL,
    [MinValue]                   FLOAT (53)     NULL,
    [MaxValue]                   FLOAT (53)     NULL,
    [RetentionFactor]            INT            NULL,
    [EnumFeatureType]            SMALLINT       CONSTRAINT [DF_MVHFeatures_EnumFeatureType] DEFAULT ((0)) NOT NULL,
    [EnumFeatureProvider]        SMALLINT       CONSTRAINT [DF_MVHFeatures_EnumFeatureProvider] DEFAULT ((0)) NOT NULL,
    [EnumCommChannelType]        SMALLINT       CONSTRAINT [DF_MVHFeatures_EnumCommChannelType] DEFAULT ((0)) NOT NULL,
    [EnumAggregationStrategy]    SMALLINT       CONSTRAINT [DF_MVHFeatures_EnumAggregationStrategu] DEFAULT ((0)) NOT NULL,
    [EnumTagValidationResult]    SMALLINT       CONSTRAINT [DF_MVHFeatures_EnumValidationResult] DEFAULT ((0)) NOT NULL,
    [CommAttr1]                  NVARCHAR (350) NULL,
    [CommAttr2]                  NVARCHAR (350) NULL,
    [CommAttr3]                  NVARCHAR (350) NULL,
    [AUDCreatedTs]               DATETIME       CONSTRAINT [DF_MVHFeatures_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]           DATETIME       CONSTRAINT [DF_MVHFeatures_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]               NVARCHAR (255) CONSTRAINT [DF_MVHFeatures_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]             BIT            CONSTRAINT [DF_MVHFeatures_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                  BIT            CONSTRAINT [DF_MVHFeatures_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVFeatures] PRIMARY KEY CLUSTERED ([FeatureId] ASC),
    CONSTRAINT [FK_MVHFeatures_DataTypes] FOREIGN KEY ([FKDataTypeId]) REFERENCES [dbo].[DBDataTypes] ([DataTypeId]),
    CONSTRAINT [FK_MVHFeatures_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_MVHFeatures_MVHFeatures] FOREIGN KEY ([FKParentFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]),
    CONSTRAINT [FK_MVHFeatures_UnitOfMeasure] FOREIGN KEY ([FKUnitOfMeasureId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [FK_MVHFeatures_UnitOfMeasure1] FOREIGN KEY ([FKExtUnitOfMeasureId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);


























GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FeatureCode]
    ON [dbo].[MVHFeatures]([FeatureCode] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_UnitOfMeasureId]
    ON [dbo].[MVHFeatures]([FKUnitOfMeasureId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_DataTypeId]
    ON [dbo].[MVHFeatures]([FKDataTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_AssetId]
    ON [dbo].[MVHFeatures]([FKAssetId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHFeatures_Audit] ON [dbo].[MVHFeatures] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.FeatureId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.FeatureId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHFeatures, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHFeatures] SET AUDUpdatedBy = APP_NAME() WHERE FeatureId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHFeatures] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE FeatureId = @RecordId
    END
END;