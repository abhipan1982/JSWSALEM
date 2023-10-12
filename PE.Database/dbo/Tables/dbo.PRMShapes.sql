CREATE TABLE [dbo].[PRMShapes] (
    [ShapeId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ShapeCode]        NVARCHAR (10)  NOT NULL,
    [ShapeName]        NVARCHAR (50)  NULL,
    [ShapeDescription] NVARCHAR (200) NULL,
    [IsDefault]        BIT            CONSTRAINT [DF_PRMShapes_IsDefault] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMShapes_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMShapes_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMShapes_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMShapes_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMShapes_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LRProductShape] PRIMARY KEY CLUSTERED ([ShapeId] ASC)
);














GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ShapeSymbol]
    ON [dbo].[PRMShapes]([ShapeCode] ASC);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_PRMShapes_Audit] ON [dbo].[PRMShapes] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.ShapeId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.ShapeId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: PRMShapes, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[PRMShapes] SET AUDUpdatedBy = APP_NAME() WHERE ShapeId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[PRMShapes] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE ShapeId = @RecordId
    END
END;