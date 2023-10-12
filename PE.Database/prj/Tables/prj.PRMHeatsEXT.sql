CREATE TABLE [prj].[PRMHeatsEXT] (
    [FKHeatId]         BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMHeatsEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMHeatsEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMHeatsEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMHeatsEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMHeatsEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HeatsEXT] PRIMARY KEY CLUSTERED ([FKHeatId] ASC),
    CONSTRAINT [FK_HeatsEXT_Heats] FOREIGN KEY ([FKHeatId]) REFERENCES [dbo].[PRMHeats] ([HeatId]) ON DELETE CASCADE
);





