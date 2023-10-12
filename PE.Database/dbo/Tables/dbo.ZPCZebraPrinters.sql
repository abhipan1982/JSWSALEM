CREATE TABLE [dbo].[ZPCZebraPrinters] (
    [ZebraPrinterId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [ZebraPrinterName]     NVARCHAR (50)  NOT NULL,
    [ZebraPrinterHostname] VARCHAR (255)  NULL,
    [ZebraPrinterPort]     INT            NULL,
    [FKAssetId]            BIGINT         NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_ZPCZebraPrinters_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_ZPCZebraPrinters_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_ZPCZebraPrinters_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_ZPCZebraPrinters_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_ZPCZebraPrinters_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ZPCZebraPrinters] PRIMARY KEY CLUSTERED ([ZebraPrinterId] ASC),
    CONSTRAINT [FK_ZPCZebraPrinters_MVHAssets] FOREIGN KEY ([FKAssetId]) REFERENCES [dbo].[MVHAssets] ([AssetId])
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_ZPCZebraPrinters_Audit] ON [dbo].[ZPCZebraPrinters] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ZebraPrinterId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ZebraPrinterId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: ZPCZebraPrinters, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[ZPCZebraPrinters] SET AUDUpdatedBy = APP_NAME() WHERE ZebraPrinterId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[ZPCZebraPrinters] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ZebraPrinterId = @RecordId
    END
END;