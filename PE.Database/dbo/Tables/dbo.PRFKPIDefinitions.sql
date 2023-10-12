CREATE TABLE [dbo].[PRFKPIDefinitions] (
    [KPIDefinitionId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [KPICode]            NVARCHAR (10)  NOT NULL,
    [KPIName]            NVARCHAR (50)  NOT NULL,
    [KPIDescription]     NVARCHAR (100) NULL,
    [KPIFormula]         NVARCHAR (400) NULL,
    [KPIProcedure]       NVARCHAR (50)  NULL,
    [IsWorkOrderBased]   BIT            CONSTRAINT [DF_PRFKPIDefinitions_IsWorkOrderBased] DEFAULT ((0)) NOT NULL,
    [IsActive]           BIT            CONSTRAINT [DF_PRFKPIDefinitions_IsActive] DEFAULT ((1)) NOT NULL,
    [MinValue]           FLOAT (53)     NOT NULL,
    [AlarmTo]            FLOAT (53)     NOT NULL,
    [WarningTo]          FLOAT (53)     NOT NULL,
    [MaxValue]           FLOAT (53)     NOT NULL,
    [FKUnitId]           BIGINT         NOT NULL,
    [EnumGaugeDirection] SMALLINT       CONSTRAINT [DF_GaugeDirection] DEFAULT ((1)) NOT NULL,
    [AUDCreatedTs]       DATETIME       CONSTRAINT [DF_PRFKPIDefinitions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]   DATETIME       CONSTRAINT [DF_PRFKPIDefinitions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]       NVARCHAR (255) CONSTRAINT [DF_PRFKPIDefinitions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]     BIT            CONSTRAINT [DF_PRFKPIDefinitions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT            CONSTRAINT [DF_PRFKPIDefinitions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_KPIDefinitionId] PRIMARY KEY CLUSTERED ([KPIDefinitionId] ASC),
    CONSTRAINT [FK_UnitId] FOREIGN KEY ([FKUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);












GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRFKPIDefinitions_Audit] ON [dbo].[PRFKPIDefinitions] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.KPIDefinitionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.KPIDefinitionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRFKPIDefinitions, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRFKPIDefinitions] SET AUDUpdatedBy = APP_NAME() WHERE KPIDefinitionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRFKPIDefinitions] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE KPIDefinitionId = @RecordId
    END
END;