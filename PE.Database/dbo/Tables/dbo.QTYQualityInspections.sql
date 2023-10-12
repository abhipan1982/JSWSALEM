CREATE TABLE [dbo].[QTYQualityInspections] (
    [QualityInspectionId]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKMaterialId]         BIGINT         NULL,
    [FKRawMaterialId]      BIGINT         NULL,
    [FKProductId]          BIGINT         NULL,
    [VisualInspection]     NVARCHAR (400) NULL,
    [DiameterMin]          FLOAT (53)     NULL,
    [DiameterMax]          FLOAT (53)     NULL,
    [EnumCrashTest]        SMALLINT       CONSTRAINT [DF_QTYQualityInspections_EnumCrashTest] DEFAULT ((0)) NOT NULL,
    [EnumInspectionResult] SMALLINT       CONSTRAINT [DF_QTYQualityInspections_EnumInspectionResult] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_QTYQualityInspections_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_QTYQualityInspections_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_QTYQualityInspections_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_QTYQualityInspections_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_QTYQualityInspections_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QTYQualityInspections_1] PRIMARY KEY CLUSTERED ([QualityInspectionId] ASC),
    CONSTRAINT [FK_QTYQualityInspections_PRMMaterials] FOREIGN KEY ([FKMaterialId]) REFERENCES [dbo].[PRMMaterials] ([MaterialId]),
    CONSTRAINT [FK_QTYQualityInspections_PRMProducts] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId]) ON DELETE SET NULL,
    CONSTRAINT [FK_QTYQualityInspections_TRKRawMaterials] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RawMaterialId]
    ON [dbo].[QTYQualityInspections]([FKRawMaterialId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_QTYQualityInspections_Audit] ON [dbo].[QTYQualityInspections] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.QualityInspectionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.QualityInspectionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: QTYQualityInspections, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[QTYQualityInspections] SET AUDUpdatedBy = APP_NAME() WHERE QualityInspectionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[QTYQualityInspections] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE QualityInspectionId = @RecordId
    END
END;