CREATE TABLE [dbo].[RLSCassettes] (
    [CassetteId]          BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKCassetteTypeId]    BIGINT         NOT NULL,
    [FKStandId]           BIGINT         NULL,
    [CassetteName]        NVARCHAR (50)  NOT NULL,
    [CassetteDescription] NVARCHAR (100) NULL,
    [EnumCassetteStatus]  SMALLINT       CONSTRAINT [DF_RLSCassettes_Status] DEFAULT ((0)) NOT NULL,
    [NumberOfPositions]   SMALLINT       CONSTRAINT [DF_Cassettes_NumberOfPositions] DEFAULT ((1)) NOT NULL,
    [Arrangement]         SMALLINT       CONSTRAINT [DF_RLSCassettes_Arrangement] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_RLSCassettes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_RLSCassettes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_RLSCassettes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_RLSCassettes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_RLSCassettes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Cassettes] PRIMARY KEY CLUSTERED ([CassetteId] ASC),
    CONSTRAINT [FK_Cassettes_CassetteTypes] FOREIGN KEY ([FKCassetteTypeId]) REFERENCES [dbo].[RLSCassetteTypes] ([CassetteTypeId]),
    CONSTRAINT [FK_Cassettes_Stands] FOREIGN KEY ([FKStandId]) REFERENCES [dbo].[RLSStands] ([StandId])
);








GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 - undefined, 1 - horizontal, 2- veritical, 3- other', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSCassettes', @level2type = N'COLUMN', @level2name = N'Arrangement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SRC.Core.Constants.CassetteStatus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RLSCassettes', @level2type = N'COLUMN', @level2name = N'EnumCassetteStatus';


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_RLSCassettes_Audit] ON [dbo].[RLSCassettes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.CassetteId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.CassetteId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: RLSCassettes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[RLSCassettes] SET AUDUpdatedBy = APP_NAME() WHERE CassetteId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[RLSCassettes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE CassetteId = @RecordId
    END
END;