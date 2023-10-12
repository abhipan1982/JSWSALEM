CREATE TABLE [prj].[PRMSteelgradesEXT] (
    [FKSteelgradeId]   BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMSteelgradesEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMSteelgradesEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMSteelgradesEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMSteelgradesEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMSteelgradesEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMSteelgradesEXT] PRIMARY KEY CLUSTERED ([FKSteelgradeId] ASC),
    CONSTRAINT [FK_PRMSteelgradesEXT_PRMSteelgrades] FOREIGN KEY ([FKSteelgradeId]) REFERENCES [dbo].[PRMSteelgrades] ([SteelgradeId]) ON DELETE CASCADE
);





