CREATE TABLE [dbo].[PRMHeatChemicalAnalysis] (
    [HeatChemAnalysisId] BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKHeatId]           BIGINT         NOT NULL,
    [SampleTakenTs]      DATETIME       NULL,
    [Laboratory]         SMALLINT       NULL,
    [Fe]                 FLOAT (53)     NULL,
    [C]                  FLOAT (53)     NULL,
    [Mn]                 FLOAT (53)     NULL,
    [Cr]                 FLOAT (53)     NULL,
    [Mo]                 FLOAT (53)     NULL,
    [V]                  FLOAT (53)     NULL,
    [Ni]                 FLOAT (53)     NULL,
    [Co]                 FLOAT (53)     NULL,
    [Si]                 FLOAT (53)     NULL,
    [P]                  FLOAT (53)     NULL,
    [S]                  FLOAT (53)     NULL,
    [Cu]                 FLOAT (53)     NULL,
    [Nb]                 FLOAT (53)     NULL,
    [Al]                 FLOAT (53)     NULL,
    [N]                  FLOAT (53)     NULL,
    [Ca]                 FLOAT (53)     NULL,
    [B]                  FLOAT (53)     NULL,
    [Ti]                 FLOAT (53)     NULL,
    [Sn]                 FLOAT (53)     NULL,
    [O]                  FLOAT (53)     NULL,
    [H]                  FLOAT (53)     NULL,
    [W]                  FLOAT (53)     NULL,
    [Pb]                 FLOAT (53)     NULL,
    [Zn]                 FLOAT (53)     NULL,
    [As]                 FLOAT (53)     NULL,
    [Mg]                 FLOAT (53)     NULL,
    [Sb]                 FLOAT (53)     NULL,
    [Bi]                 FLOAT (53)     NULL,
    [Ta]                 FLOAT (53)     NULL,
    [Zr]                 FLOAT (53)     NULL,
    [Ce]                 FLOAT (53)     NULL,
    [Te]                 FLOAT (53)     NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_PRMHeatChemicalAnalysis_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_PRMHeatChemicalAnalysis_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_PRMHeatChemicalAnalysis_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_PRMHeatChemicalAnalysis_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_PRMHeatChemicalAnalysis_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HeatChemAnalysis] PRIMARY KEY CLUSTERED ([HeatChemAnalysisId] ASC),
    CONSTRAINT [FK_PEHeatChemAnalysis_PEHeats] FOREIGN KEY ([FKHeatId]) REFERENCES [dbo].[PRMHeats] ([HeatId]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [NCI_HeatId]
    ON [dbo].[PRMHeatChemicalAnalysis]([FKHeatId] ASC)
    INCLUDE([C], [Mn], [Cr], [V], [Ni], [Al], [N], [Ti], [H], [Si], [P], [S], [Cu]);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMHeatChemicalAnalysis_Audit] ON [dbo].[PRMHeatChemicalAnalysis] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.HeatChemAnalysisId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.HeatChemAnalysisId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMHeatChemicalAnalysis, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMHeatChemicalAnalysis] SET AUDUpdatedBy = APP_NAME() WHERE HeatChemAnalysisId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMHeatChemicalAnalysis] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE HeatChemAnalysisId = @RecordId
    END
END;