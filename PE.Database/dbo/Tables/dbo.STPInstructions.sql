CREATE TABLE [dbo].[STPInstructions] (
    [InstructionId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKDataTypeId]           BIGINT         NOT NULL,
    [FKUnitId]               BIGINT         NOT NULL,
    [InstructionCode]        NVARCHAR (10)  NOT NULL,
    [InstructionName]        NVARCHAR (50)  NOT NULL,
    [InstructionDescription] NVARCHAR (100) NULL,
    [DefaultValue]           NVARCHAR (255) NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_STPInstructions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_STPInstructions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_STPInstructions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_STPInstructions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_STPInstructions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_STPInstructions] PRIMARY KEY CLUSTERED ([InstructionId] ASC),
    CONSTRAINT [FK_STPInstructions_DataTypes] FOREIGN KEY ([FKDataTypeId]) REFERENCES [dbo].[DBDataTypes] ([DataTypeId]),
    CONSTRAINT [FK_STPInstructions_UnitOfMeasure] FOREIGN KEY ([FKUnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId])
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_STPInstructions_Audit] ON [dbo].[STPInstructions] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.InstructionId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.InstructionId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: STPInstructions, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[STPInstructions] SET AUDUpdatedBy = APP_NAME() WHERE InstructionId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[STPInstructions] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE InstructionId = @RecordId
    END
END;