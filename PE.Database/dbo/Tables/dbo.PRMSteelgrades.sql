CREATE TABLE [dbo].[PRMSteelgrades] (
    [SteelgradeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKSteelGroupId]        BIGINT         NULL,
    [FKScrapGroupId]        BIGINT         NULL,
    [FKParentSteelgradeId]  BIGINT         NULL,
    [IsDefault]             BIT            CONSTRAINT [DF_Steelgrades_IsDefault] DEFAULT ((0)) NOT NULL,
    [SteelgradeCode]        NVARCHAR (10)  NOT NULL,
    [SteelgradeName]        NVARCHAR (50)  NULL,
    [SteelgradeDescription] NVARCHAR (200) NULL,
    [CustomCode]            NVARCHAR (10)  NULL,
    [CustomName]            NVARCHAR (50)  NULL,
    [CustomDescription]     NVARCHAR (200) NULL,
    [Density]               FLOAT (53)     NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_PRMSteelgrades_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_PRMSteelgrades_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_PRMSteelgrades_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_PRMSteelgrades_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_PRMSteelgrades_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SteelgradeId] PRIMARY KEY CLUSTERED ([SteelgradeId] ASC),
    CONSTRAINT [FK_PRMSteelgrades_PRMScrapGroups] FOREIGN KEY ([FKScrapGroupId]) REFERENCES [dbo].[PRMScrapGroups] ([ScrapGroupId]),
    CONSTRAINT [FK_Steelgrades_Steelgrades] FOREIGN KEY ([FKParentSteelgradeId]) REFERENCES [dbo].[PRMSteelgrades] ([SteelgradeId]),
    CONSTRAINT [FK_Steelgrades_SteelGroupId] FOREIGN KEY ([FKSteelGroupId]) REFERENCES [dbo].[PRMSteelGroups] ([SteelGroupId]),
    CONSTRAINT [UQ_SteelgradeCode] UNIQUE NONCLUSTERED ([SteelgradeCode] ASC)
);
















GO
CREATE NONCLUSTERED INDEX [NCI_SteelGroupId]
    ON [dbo].[PRMSteelgrades]([FKSteelGroupId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ParentSteelgradeId]
    ON [dbo].[PRMSteelgrades]([FKParentSteelgradeId] ASC);




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SteelgradeName]
    ON [dbo].[PRMSteelgrades]([SteelgradeName] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_ScrapGroupId]
    ON [dbo].[PRMSteelgrades]([FKScrapGroupId] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMSteelgrades_Audit] ON [dbo].[PRMSteelgrades] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.SteelgradeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.SteelgradeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMSteelgrades, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMSteelgrades] SET AUDUpdatedBy = APP_NAME() WHERE SteelgradeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMSteelgrades] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE SteelgradeId = @RecordId
    END
END;