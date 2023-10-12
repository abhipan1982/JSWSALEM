CREATE TABLE [dbo].[PRMCustomers] (
    [CustomerId]       BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CustomerCode]     NVARCHAR (10)  NOT NULL,
    [CustomerName]     NVARCHAR (50)  NOT NULL,
    [CustomerAddress]  NVARCHAR (200) NULL,
    [Email]            NVARCHAR (100) NULL,
    [Phone]            NVARCHAR (20)  NULL,
    [DocPatternName]   NVARCHAR (50)  NULL,
    [Country]          NVARCHAR (50)  NULL,
    [LogoName]         NVARCHAR (20)  NULL,
    [SAPKey]           NVARCHAR (20)  NULL,
    [IsActive]         BIT            CONSTRAINT [DF_PRMCustomers_Active] DEFAULT ((1)) NOT NULL,
    [IsDefault]        BIT            CONSTRAINT [DF_PRMCustomers_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMCustomers_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMCustomers_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMCustomers_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMCustomers_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMCustomers_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC) WITH (FILLFACTOR = 90)
);














GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CustomerName]
    ON [dbo].[PRMCustomers]([CustomerName] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMCustomers_Audit] ON [dbo].[PRMCustomers] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.CustomerId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.CustomerId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMCustomers, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMCustomers] SET AUDUpdatedBy = APP_NAME() WHERE CustomerId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMCustomers] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE CustomerId = @RecordId
    END
END;