CREATE TABLE [dbo].[MVHFeatureTemplates] (
    [FeatureTemplateId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKUnitOfMeasureId]          BIGINT         NOT NULL,
    [FKExtUnitOfMeasureId]       BIGINT         NOT NULL,
    [FKDataTypeId]               BIGINT         NOT NULL,
    [FeatureTemplateName]        NVARCHAR (75)  NOT NULL,
    [FeatureTemplateDescription] NVARCHAR (100) NULL,
    [EnumFeatureType]            SMALLINT       CONSTRAINT [DF_MVHFeatureTemplates_EnumFeatureType] DEFAULT ((0)) NOT NULL,
    [EnumCommChannelType]        SMALLINT       CONSTRAINT [DF_MVHFeatureTemplates_EnumCommChannelType] DEFAULT ((0)) NOT NULL,
    [EnumAggregationStrategy]    SMALLINT       CONSTRAINT [DF_MVHFeatureTemplates_EnumAggregationStrategu] DEFAULT ((0)) NOT NULL,
    [TemplateCommAttr1]          NVARCHAR (350) NULL,
    [TemplateCommAttr2]          NVARCHAR (350) NULL,
    [TemplateCommAttr3]          NVARCHAR (350) NULL,
    [AUDCreatedTs]               DATETIME       CONSTRAINT [DF_MVHFeatureTemplates_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]           DATETIME       CONSTRAINT [DF_MVHFeatureTemplates_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]               NVARCHAR (255) CONSTRAINT [DF_MVHFeatureTemplates_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]             BIT            CONSTRAINT [DF_MVHFeatureTemplates_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                  BIT            CONSTRAINT [DF_MVHFeatureTemplates_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHFeatureTemplatess] PRIMARY KEY CLUSTERED ([FeatureTemplateId] ASC),
    CONSTRAINT [FK_FeatureTemplates_DataTypes] FOREIGN KEY ([FKDataTypeId]) REFERENCES [dbo].[DBDataTypes] ([DataTypeId]),
    CONSTRAINT [FK_FeatureTemplates_ExtUnitOfMeasure] FOREIGN KEY ([FKExtUnitOfMeasureId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [FK_FeatureTemplates_UnitOfMeasure] FOREIGN KEY ([FKUnitOfMeasureId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FeatureTemplateName]
    ON [dbo].[MVHFeatureTemplates]([FeatureTemplateName] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHFeatureTemplates_Audit] ON [dbo].[MVHFeatureTemplates] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.FeatureTemplateId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.FeatureTemplateId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHFeatureTemplates, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHFeatureTemplates] SET AUDUpdatedBy = APP_NAME() WHERE FeatureTemplateId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHFeatureTemplates] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE FeatureTemplateId = @RecordId
    END
END;