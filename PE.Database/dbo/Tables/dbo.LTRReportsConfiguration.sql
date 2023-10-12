﻿CREATE TABLE [dbo].[LTRReportsConfiguration] (
    [ReportId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ReportName]        NVARCHAR (20)  NULL,
    [ServerURL]         NVARCHAR (100) NULL,
    [Login]             NVARCHAR (100) NULL,
    [Password]          NVARCHAR (100) NULL,
    [ReportPath]        NVARCHAR (100) NULL,
    [ReportFormat]      NVARCHAR (20)  NULL,
    [OutputPath]        NVARCHAR (200) NULL,
    [Extension]         NVARCHAR (10)  NULL,
    [DefaultParamValue] NVARCHAR (100) NULL,
    [IsActive]          BIT            CONSTRAINT [DF_LTRReportsConfiguration_IsActive] DEFAULT ((1)) NOT NULL,
    [AUDCreatedTs]      DATETIME       CONSTRAINT [DF_LTRReportsConfiguration_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]  DATETIME       CONSTRAINT [DF_LTRReportsConfiguration_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]      NVARCHAR (255) CONSTRAINT [DF_LTRReportsConfiguration_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]    BIT            CONSTRAINT [DF_LTRReportsConfiguration_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]         BIT            CONSTRAINT [DF_LTRReportsConfiguration_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LTRReportsConfiguration] PRIMARY KEY CLUSTERED ([ReportId] ASC)
);






GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_LTRReportsConfiguration_Audit] ON [dbo].[LTRReportsConfiguration] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ReportId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ReportId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: LTRReportsConfiguration, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[LTRReportsConfiguration] SET AUDUpdatedBy = APP_NAME() WHERE ReportId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[LTRReportsConfiguration] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ReportId = @RecordId
    END
END;