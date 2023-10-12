CREATE TABLE [dbo].[TRKTrackingInstructions] (
    [TrackingInstructionId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKFeatureId]                   BIGINT         NOT NULL,
    [FKAreaAssetId]                 BIGINT         NOT NULL,
    [FKPointAssetId]                BIGINT         NULL,
    [FKParentTrackingInstructionId] BIGINT         NULL,
    [SeqNo]                         SMALLINT       CONSTRAINT [DF_TRKTrackingInstructions_SeqNo] DEFAULT ((1)) NOT NULL,
    [TrackingInstructionValue]      SMALLINT       NULL,
    [EnumTrackingInstructionType]   SMALLINT       CONSTRAINT [DF_TRKTrackingInstructions_EnumTrackingInstructionType] DEFAULT ((0)) NOT NULL,
    [ChannelId]                     SMALLINT       CONSTRAINT [DF_TRKTrackingInstructions_ChannelId] DEFAULT ((1)) NOT NULL,
    [TimeFilter]                    FLOAT (53)     NULL,
    [IsAsync]                       BIT            CONSTRAINT [DF_TRKTrackingInstructions_IsAsync] DEFAULT ((0)) NOT NULL,
    [IsIgnoredIfSimulation]         BIT            CONSTRAINT [DF_TRKTrackingInstructions_IsIgnoredIfSimulation] DEFAULT ((0)) NOT NULL,
    [IsProcessedDuringAdjustment]   BIT            CONSTRAINT [DF_TRKTrackingInstructions_ShouldBeProcessedDuringAdjustment] DEFAULT ((1)) NOT NULL,
    [AUDCreatedTs]                  DATETIME       CONSTRAINT [DF_TRKTrackingInstructions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]              DATETIME       CONSTRAINT [DF_TRKTrackingInstructions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                  NVARCHAR (255) CONSTRAINT [DF_TRKTrackingInstructions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                BIT            CONSTRAINT [DF_TRKTrackingInstructions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                     BIT            CONSTRAINT [DF_TRKTrackingInstructions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TRKTrackingInstructions] PRIMARY KEY CLUSTERED ([TrackingInstructionId] ASC),
    CONSTRAINT [FK_TRKTrackingInstructions_MVHAssets1] FOREIGN KEY ([FKAreaAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TRKTrackingInstructions_MVHAssets2] FOREIGN KEY ([FKPointAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId]),
    CONSTRAINT [FK_TRKTrackingInstructions_MVHFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TRKTrackingInstructions_TRKTrackingInstructions] FOREIGN KEY ([FKParentTrackingInstructionId]) REFERENCES [dbo].[TRKTrackingInstructions] ([TrackingInstructionId])
);










GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TrackingInstruction]
    ON [dbo].[TRKTrackingInstructions]([FKFeatureId] ASC, [SeqNo] ASC, [TrackingInstructionValue] ASC, [EnumTrackingInstructionType] ASC, [FKAreaAssetId] ASC, [FKPointAssetId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_TRKTrackingInstructions_Audit] ON [dbo].[TRKTrackingInstructions] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.TrackingInstructionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.TrackingInstructionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: TRKTrackingInstructions, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[TRKTrackingInstructions] SET AUDUpdatedBy = APP_NAME() WHERE TrackingInstructionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[TRKTrackingInstructions] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE TrackingInstructionId = @RecordId
    END
END;