CREATE TABLE [dbo].[RLSCassetteTypes] (
    [CassetteTypeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CassetteTypeName]        NVARCHAR (50)  NOT NULL,
    [CassetteTypeDescription] NVARCHAR (100) NULL,
    [NumberOfRolls]           SMALLINT       NULL,
    [EnumCassetteType]        SMALLINT       CONSTRAINT [DF_RLSCassetteTypes_EnumCassetteType] DEFAULT ((0)) NOT NULL,
    [IsInterCassette]         BIT            CONSTRAINT [DF_RLSCassetteTypes_IsInterCassette] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]            DATETIME       CONSTRAINT [DF_RLSCassetteTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]        DATETIME       CONSTRAINT [DF_RLSCassetteTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]            NVARCHAR (255) CONSTRAINT [DF_RLSCassetteTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]          BIT            CONSTRAINT [DF_RLSCassetteTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]               BIT            CONSTRAINT [DF_RLSCassetteTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CassetteTypes] PRIMARY KEY CLUSTERED ([CassetteTypeId] ASC)
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_RLSCassetteTypes_Audit] ON [dbo].[RLSCassetteTypes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.CassetteTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.CassetteTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: RLSCassetteTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[RLSCassetteTypes] SET AUDUpdatedBy = APP_NAME() WHERE CassetteTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[RLSCassetteTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE CassetteTypeId = @RecordId
    END
END;