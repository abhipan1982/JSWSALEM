CREATE TABLE [dbo].[RLSGrooveTemplates] (
    [GrooveTemplateId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Shape]                     NVARCHAR (5)   NOT NULL,
    [GrooveTemplateCode]        NVARCHAR (20)  NOT NULL,
    [GrooveTemplateName]        NVARCHAR (50)  NOT NULL,
    [GrooveTemplateDescription] NVARCHAR (100) NULL,
    [GrindingProgramName]       NVARCHAR (50)  NULL,
    [EnumGrooveTemplateStatus]  SMALLINT       CONSTRAINT [DF_RLSGrooveTemplates_Status] DEFAULT ((0)) NOT NULL,
    [EnumGrooveSetting]         SMALLINT       CONSTRAINT [DF_RLSGrooveTemplates_EnumGrooveSetting] DEFAULT ((0)) NOT NULL,
    [R1]                        FLOAT (53)     NULL,
    [R2]                        FLOAT (53)     NULL,
    [R3]                        FLOAT (53)     NULL,
    [D1]                        FLOAT (53)     NULL,
    [D2]                        FLOAT (53)     NULL,
    [W1]                        FLOAT (53)     NULL,
    [W2]                        FLOAT (53)     NULL,
    [Angle1]                    FLOAT (53)     NULL,
    [Angle2]                    FLOAT (53)     NULL,
    [Ds]                        FLOAT (53)     NULL,
    [Dw]                        FLOAT (53)     NULL,
    [SpreadFactor]              FLOAT (53)     NULL,
    [Plane]                     FLOAT (53)     NULL,
    [IsDefault]                 BIT            CONSTRAINT [DF_RLSGrooveTemplates_IsDefault] DEFAULT ((0)) NOT NULL,
    [AccBilletCntLimit]         BIGINT         NULL,
    [AccWeightLimit]            FLOAT (53)     NULL,
    [AUDCreatedTs]              DATETIME       CONSTRAINT [DF_RLSGrooveTemplates_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]          DATETIME       CONSTRAINT [DF_RLSGrooveTemplates_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]              NVARCHAR (255) CONSTRAINT [DF_RLSGrooveTemplates_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]            BIT            CONSTRAINT [DF_RLSGrooveTemplates_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                 BIT            CONSTRAINT [DF_RLSGrooveTemplates_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_GrooveTemplates] PRIMARY KEY CLUSTERED ([GrooveTemplateId] ASC)
);












GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'm2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'Plane';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Angle 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'Angle2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Angle 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'Angle1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Width 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'W2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Width 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'W1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depth 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'D2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depth 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'D1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Radius 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'R3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Radius 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'R2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Radius 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSGrooveTemplates', @level2type = N'COLUMN', @level2name = N'R1';


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_RLSGrooveTemplates_Audit] ON [dbo].[RLSGrooveTemplates] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.GrooveTemplateId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.GrooveTemplateId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: RLSGrooveTemplates, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[RLSGrooveTemplates] SET AUDUpdatedBy = APP_NAME() WHERE GrooveTemplateId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[RLSGrooveTemplates] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE GrooveTemplateId = @RecordId
    END
END;