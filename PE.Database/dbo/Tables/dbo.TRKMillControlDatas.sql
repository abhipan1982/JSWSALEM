CREATE TABLE [dbo].[TRKMillControlDatas] (
    [MillControlDataId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [MillControlCode]     INT            NOT NULL,
    [EnumCommChannelType] SMALLINT       CONSTRAINT [DF_TRKMillControlDatas_EnumCommChannelType] DEFAULT ((0)) NOT NULL,
    [CommAttr1]           NVARCHAR (350) NULL,
    [CommAttr2]           NVARCHAR (350) NULL,
    [CommAttr3]           NVARCHAR (350) NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_TRKMillControlDatas_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_TRKMillControlDatas_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_TRKMillControlDatas_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_TRKMillControlDatas_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_TRKMillControlDatas_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TRKMillControlDatas] PRIMARY KEY CLUSTERED ([MillControlDataId] ASC),
    CONSTRAINT [UQ_TRKMillControlCode] UNIQUE NONCLUSTERED ([MillControlCode] ASC)
);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_TRKMillControlDatas_Audit] ON [dbo].[TRKMillControlDatas] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MillControlDataId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MillControlDataId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: TRKMillControlDatas, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[TRKMillControlDatas] SET AUDUpdatedBy = APP_NAME() WHERE MillControlDataId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[TRKMillControlDatas] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MillControlDataId = @RecordId
    END
END;