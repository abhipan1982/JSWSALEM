CREATE TABLE [dbo].[MNTMembers] (
    [MemberId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [MemberName]       NVARCHAR (50)  NOT NULL,
    [FKMemberRoleId]   BIGINT         NOT NULL,
    [FKUserId]         NVARCHAR (450) NULL,
    [CostPerHour]      FLOAT (53)     CONSTRAINT [DF_MNTMembers_CostPerHour] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MNTMembers_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MNTMembers_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MNTMembers_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MNTMembers_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MNTMembers_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTMembers] PRIMARY KEY CLUSTERED ([MemberId] ASC),
    CONSTRAINT [FK_MNTMembers_MNTMemberRoles] FOREIGN KEY ([FKMemberRoleId]) REFERENCES [dbo].[MNTMemberRoles] ([MemberRoleId]),
    CONSTRAINT [FK_MNTMembers_Users] FOREIGN KEY ([FKUserId]) REFERENCES [smf].[Users] ([Id])
);








GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_MNTMembers_Audit] ON [dbo].[MNTMembers] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MemberId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MemberId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTMembers, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTMembers] SET AUDUpdatedBy = APP_NAME() WHERE MemberId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTMembers] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MemberId = @RecordId
    END
END;