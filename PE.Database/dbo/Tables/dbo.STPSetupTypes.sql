CREATE TABLE [dbo].[STPSetupTypes] (
    [SetupTypeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [OrderSeq]             SMALLINT       NOT NULL,
    [SetupTypeCode]        NVARCHAR (10)  NOT NULL,
    [SetupTypeName]        NVARCHAR (50)  NOT NULL,
    [SetupTypeDescription] NVARCHAR (100) NULL,
    [IsRequired]           BIT            CONSTRAINT [DF_STPSetupTypes_IsRequired] DEFAULT ((1)) NOT NULL,
    [IsActive]             BIT            CONSTRAINT [DF_STPSetupTypes_IsProductSizeRelated] DEFAULT ((0)) NOT NULL,
    [IsSteelgradeRelated]  BIT            CONSTRAINT [DF_STPSetupTypes_IsSteelgrade] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_STPSetupTypes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_STPSetupTypes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_STPSetupTypes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_STPSetupTypes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_STPSetupTypes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetupTypes] PRIMARY KEY CLUSTERED ([SetupTypeId] ASC)
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SetupTypeName]
    ON [dbo].[STPSetupTypes]([SetupTypeName] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SetupTypeCode]
    ON [dbo].[STPSetupTypes]([SetupTypeCode] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OrderSeq]
    ON [dbo].[STPSetupTypes]([OrderSeq] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetupTypes_Audit] ON [dbo].[STPSetupTypes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupTypeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupTypeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetupTypes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetupTypes] SET AUDUpdatedBy = APP_NAME() WHERE SetupTypeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetupTypes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupTypeId = @RecordId
    END
END;