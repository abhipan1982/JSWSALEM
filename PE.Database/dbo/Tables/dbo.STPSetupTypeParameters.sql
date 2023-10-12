CREATE TABLE [dbo].[STPSetupTypeParameters] (
    [SetupTypeParameterId]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKSetupTypeId]         BIGINT         NOT NULL,
    [FKParameterId]         BIGINT         NOT NULL,
    [DefaultParameterValue] NVARCHAR (100) NULL,
    [DefaultIsRequired]     BIT            CONSTRAINT [DF_STPSetupTypeParameters_DefaultIsRequired] DEFAULT ((0)) NOT NULL,
    [OrderSeq]              SMALLINT       NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_STPSetupTypeParameters_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_STPSetupTypeParameters_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_STPSetupTypeParameters_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_STPSetupTypeParameters_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_STPSetupTypeParameters_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPSetupTypeParameters] PRIMARY KEY CLUSTERED ([SetupTypeParameterId] ASC),
    CONSTRAINT [FK_STPSetupTypeParameters_STPParameters] FOREIGN KEY ([FKParameterId]) REFERENCES [dbo].[STPParameters] ([ParameterId]),
    CONSTRAINT [FK_STPSetupTypeParameters_STPSetupTypes] FOREIGN KEY ([FKSetupTypeId]) REFERENCES [dbo].[STPSetupTypes] ([SetupTypeId])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SetupTypeId_ParameterId]
    ON [dbo].[STPSetupTypeParameters]([FKSetupTypeId] ASC, [FKParameterId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPSetupTypeParameters_Audit] ON [dbo].[STPSetupTypeParameters] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SetupTypeParameterId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SetupTypeParameterId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPSetupTypeParameters, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPSetupTypeParameters] SET AUDUpdatedBy = APP_NAME() WHERE SetupTypeParameterId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPSetupTypeParameters] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SetupTypeParameterId = @RecordId
    END
END;