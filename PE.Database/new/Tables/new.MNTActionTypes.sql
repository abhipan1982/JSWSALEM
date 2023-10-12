CREATE TABLE [new].[MNTActionTypes] (
    [ActionTypeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ActionTypeName]        NVARCHAR (50)  NOT NULL,
    [ActionTypeDescription] NVARCHAR (100) NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_MNTActionTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_MNTActionTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_MNTActionTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_MNTActionTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_MNTActionTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTActionTypes] PRIMARY KEY CLUSTERED ([ActionTypeId] ASC)
);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTActionTypes_Audit] ON [new].[MNTActionTypes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ActionTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ActionTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTActionTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [new].[MNTActionTypes] SET AUDUpdatedBy = APP_NAME() WHERE ActionTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [new].[MNTActionTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ActionTypeId = @RecordId
    END
END;