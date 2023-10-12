CREATE TABLE [dbo].[RLSRolls] (
    [RollId]              BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKRollTypeId]        BIGINT         NOT NULL,
    [RollName]            NVARCHAR (50)  NOT NULL,
    [RollDescription]     NVARCHAR (100) NULL,
    [InitialDiameter]     FLOAT (53)     NOT NULL,
    [ActualDiameter]      FLOAT (53)     NOT NULL,
    [MinimumDiameter]     FLOAT (53)     NULL,
    [DiameterOfMaterial]  FLOAT (53)     NULL,
    [DiameterOfTool]      FLOAT (53)     NULL,
    [GroovesNumber]       SMALLINT       CONSTRAINT [DF_Rolls_GroovesNumber] DEFAULT ((0)) NOT NULL,
    [Supplier]            NVARCHAR (50)  NULL,
    [ScrapTime]           DATETIME       NULL,
    [EnumRollScrapReason] SMALLINT       CONSTRAINT [DF_RLSRolls_EnumScrapReason] DEFAULT ((0)) NOT NULL,
    [EnumRollStatus]      SMALLINT       CONSTRAINT [DF_RLSRolls_Status] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_RLSRolls_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_RLSRolls_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_RLSRolls_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_RLSRolls_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_RLSRolls_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Rolls] PRIMARY KEY CLUSTERED ([RollId] ASC),
    CONSTRAINT [FK_Rolls_RollTypes] FOREIGN KEY ([FKRollTypeId]) REFERENCES [dbo].[RLSRollTypes] ([RollTypeId]),
    CONSTRAINT [UQ_RollName] UNIQUE NONCLUSTERED ([RollName] ASC)
);










GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_RLSRolls_Audit] ON [dbo].[RLSRolls] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.RollId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.RollId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: RLSRolls, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[RLSRolls] SET AUDUpdatedBy = APP_NAME() WHERE RollId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[RLSRolls] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE RollId = @RecordId
    END
END;