CREATE TABLE [new].[MNTMemberRoles] (
    [MemberRoleId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [MemberRoleName]        NVARCHAR (50)  NOT NULL,
    [MemberRoleDescription] NVARCHAR (100) NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_MNTMemberRoles_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_MNTMemberRoles_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_MNTMemberRoles_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_MNTMemberRoles_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_MNTMemberRoles_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MNTMemberRoles] PRIMARY KEY CLUSTERED ([MemberRoleId] ASC)
);




GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [new].[TR_MNTMemberRoles_Audit] ON [new].[MNTMemberRoles] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.MemberRoleId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.MemberRoleId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: MNTMemberRoles, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[MNTMemberRoles] SET AUDUpdatedBy = APP_NAME() WHERE MemberRoleId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[MNTMemberRoles] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE MemberRoleId = @RecordId
    END
END;