CREATE TABLE [dbo].[MNTCrewMembers] (
    [CrewMemberId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKCrewId]         BIGINT         NOT NULL,
    [FKMemberId]       BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MNTCrewMembers_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MNTCrewMembers_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MNTCrewMembers_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MNTCrewMembers_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MNTCrewMembers_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTCrewMembers] PRIMARY KEY CLUSTERED ([CrewMemberId] ASC),
    CONSTRAINT [FK_MNTCrewMembers_Crews] FOREIGN KEY ([FKCrewId]) REFERENCES [dbo].[EVTCrews] ([CrewId]),
    CONSTRAINT [FK_MNTCrewMembers_MNTMembers] FOREIGN KEY ([FKMemberId]) REFERENCES [dbo].[MNTMembers] ([MemberId])
);










GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MNTCrewMembers_Audit] ON [dbo].[MNTCrewMembers] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.CrewMemberId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.CrewMemberId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTCrewMembers, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTCrewMembers] SET AUDUpdatedBy = APP_NAME() WHERE CrewMemberId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTCrewMembers] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE CrewMemberId = @RecordId
    END
END;