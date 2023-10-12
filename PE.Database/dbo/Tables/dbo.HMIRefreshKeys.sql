CREATE TABLE [dbo].[HMIRefreshKeys] (
    [HmiRefreshKeyId]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKEventId]        BIGINT         NOT NULL,
    [HmiRefreshKey]    NVARCHAR (150) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_HMIRefreshKeys_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_HMIRefreshKeys_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_HMIRefreshKeys_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_HMIRefreshKeys_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_HMIRefreshKeys_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HmiRefreshKeys] PRIMARY KEY CLUSTERED ([HmiRefreshKeyId] ASC)
);












GO
CREATE NONCLUSTERED INDEX [NCI_EventId]
    ON [dbo].[HMIRefreshKeys]([FKEventId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_HMIRefreshKeys_Audit] ON [dbo].[HMIRefreshKeys] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.HmiRefreshKeyId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.HmiRefreshKeyId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: HMIRefreshKeys, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[HMIRefreshKeys] SET AUDUpdatedBy = APP_NAME() WHERE HmiRefreshKeyId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[HMIRefreshKeys] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE HmiRefreshKeyId = @RecordId
    END
END;