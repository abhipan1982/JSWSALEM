CREATE TABLE [dbo].[MVHMeasurements] (
    [MeasurementId]      BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKFeatureId]        BIGINT         NOT NULL,
    [FKRawMaterialId]    BIGINT         NULL,
    [CreatedTs]          DATETIME       CONSTRAINT [DF_MVHMeasurements_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [IsValid]            BIT            CONSTRAINT [DF_MVMeasurements_Valid] DEFAULT ((1)) NOT NULL,
    [NoOfSamples]        SMALLINT       CONSTRAINT [DF_MVMeasurements_NoOfSamples] DEFAULT ((0)) NOT NULL,
    [ValueAvg]           FLOAT (53)     NOT NULL,
    [ValueMin]           FLOAT (53)     NULL,
    [ValueMax]           FLOAT (53)     NULL,
    [FirstMeasurementTs] DATETIME       NULL,
    [LastMeasurementTs]  DATETIME       NULL,
    [ActualLength]       FLOAT (53)     NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_MVHMeasurements_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_MVHMeasurements_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_MVHMeasurements_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_MVHMeasurements_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_MVHMeasurements_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVMeasurements] PRIMARY KEY CLUSTERED ([MeasurementId] ASC) ON [MV_MEASUREMENTS],
    CONSTRAINT [FK_MVHMeasurements_MVHFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MVHMeasurements_MVHRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE
) ON [MV_MEASUREMENTS];






























GO



GO







GO



GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId_IsValid_CreatedTs]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC, [IsValid] ASC, [CreatedTs] ASC)
    INCLUDE([ValueAvg])
    ON [MV_MEASUREMENTS];


GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId_IsValid]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC, [IsValid] ASC)
    INCLUDE([ValueAvg])
    ON [MV_MEASUREMENTS];




GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId_CreatedTs]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC, [CreatedTs] ASC)
    INCLUDE([IsValid], [ValueAvg], [ValueMin], [ValueMax])
    ON [MV_MEASUREMENTS];




GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC)
    INCLUDE([FKRawMaterialId], [IsValid], [FirstMeasurementTs], [LastMeasurementTs], [ActualLength], [NoOfSamples], [ValueAvg], [ValueMin], [ValueMax])
    ON [MV_MEASUREMENTS];




GO
CREATE NONCLUSTERED INDEX [NCI_CreatedTs]
    ON [dbo].[MVHMeasurements]([CreatedTs] ASC)
    INCLUDE([FKFeatureId], [IsValid], [ValueAvg], [ValueMin], [ValueMax])
    ON [MV_MEASUREMENTS];




GO
CREATE NONCLUSTERED INDEX [NCI_FKRawMaterialId]
    ON [dbo].[MVHMeasurements]([FKRawMaterialId] ASC)
    INCLUDE([FKFeatureId], [IsValid], [ValueAvg], [ValueMin], [ValueMax])
    ON [MV_MEASUREMENTS];






GO
CREATE NONCLUSTERED INDEX [NCI_FKFeatureId_FKRawMaterialId]
    ON [dbo].[MVHMeasurements]([FKFeatureId] ASC, [FKRawMaterialId] ASC)
    INCLUDE([IsValid], [ValueAvg], [ValueMin], [ValueMax])
    ON [MV_MEASUREMENTS];




GO
CREATE NONCLUSTERED INDEX [NCI_NoOfSamples]
    ON [dbo].[MVHMeasurements]([NoOfSamples] ASC)
    INCLUDE([FKFeatureId], [FKRawMaterialId], [IsValid], [FirstMeasurementTs], [LastMeasurementTs], [ActualLength], [ValueAvg], [ValueMin], [ValueMax])
    ON [MV_MEASUREMENTS];




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MVHMeasurements_Audit] ON [dbo].[MVHMeasurements] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MeasurementId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MeasurementId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MVHMeasurements, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MVHMeasurements] SET AUDUpdatedBy = APP_NAME() WHERE MeasurementId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MVHMeasurements] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MeasurementId = @RecordId
    END
END;