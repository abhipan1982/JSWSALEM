CREATE TABLE [new].[MNTRecommendedActions] (
    [RecommendedActionId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKIncidentTypeId]      BIGINT         NOT NULL,
    [FKActionTypeId]        BIGINT         NOT NULL,
    [FKEquipmentTypeId]     BIGINT         NULL,
    [OrderSequence]         SMALLINT       CONSTRAINT [DF_MNTRecomendedActions_OrderSequence] DEFAULT ((1)) NOT NULL,
    [IsRequired]            BIT            CONSTRAINT [DF_MNTRecomendedActions_IsRequired] DEFAULT ((1)) NOT NULL,
    [DefaultActionDuration] INT            CONSTRAINT [DF_MNTRecomendedActions_DefaultActionDuration] DEFAULT ((0)) NOT NULL,
    [ActionDescription]     NVARCHAR (100) NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_MNTRecomendedActions_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_MNTRecomendedActions_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_MNTRecomendedActions_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_MNTRecomendedActions_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_MNTRecomendedActions_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTRecomendedActions] PRIMARY KEY CLUSTERED ([RecommendedActionId] ASC),
    CONSTRAINT [FK_MNTRecomendedActions_MNTActionTypes] FOREIGN KEY ([FKActionTypeId]) REFERENCES [new].[MNTActionTypes] ([ActionTypeId]),
    CONSTRAINT [FK_MNTRecomendedActions_MNTEquipmentTypes] FOREIGN KEY ([FKEquipmentTypeId]) REFERENCES [new].[MNTEquipmentTypes] ([EquipmentTypeId]),
    CONSTRAINT [FK_MNTRecomendedActions_MNTIncidentTypes] FOREIGN KEY ([FKIncidentTypeId]) REFERENCES [new].[MNTIncidentTypes] ([IncidentTypeId])
);




GO
-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTRecommendedActions_Audit] ON [new].[MNTRecommendedActions] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @RecordId BIGINT;
    DECLARE @LogValue VARCHAR(100);
    SELECT @RecordId = INSERTED.RecommendedActionId
    FROM INSERTED;
    IF NOT EXISTS
    (
        SELECT 1
        FROM INSERTED
    )
        BEGIN
            -- DELETE ACTION
            SELECT @RecordId = DELETED.RecommendedActionId
            FROM DELETED;
            SET @LogValue = CONCAT('Deleted record from table: MNTRecommendedActions, Id: ', CAST(@RecordId AS VARCHAR), ', by: ', APP_NAME());
            EXEC SPLogDB 
                 @LogValue;
        END;
        ELSE
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1
                FROM DELETED
            )
                -- INSERT ACTION
                UPDATE [new].[MNTRecommendedActions]
                  SET 
                      AUDUpdatedBy = APP_NAME()
                WHERE RecommendedActionId = @RecordId;
                ELSE
                -- UPDATE ACTION
                UPDATE [new].[MNTRecommendedActions]
                  SET 
                      AUDUpdatedBy = APP_NAME(), 
                      AUDLastUpdatedTs = GETDATE()
                WHERE RecommendedActionId = @RecordId;
        END;
END;