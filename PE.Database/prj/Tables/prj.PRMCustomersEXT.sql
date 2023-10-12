CREATE TABLE [prj].[PRMCustomersEXT] (
    [FKCustomerId]     BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMCustomersEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMCustomersEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMCustomersEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMCustomersEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMCustomersEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMCustomersEXT] PRIMARY KEY CLUSTERED ([FKCustomerId] ASC),
    CONSTRAINT [FK_PRMCustomersEXT_PRMCustomers] FOREIGN KEY ([FKCustomerId]) REFERENCES [dbo].[PRMCustomers] ([CustomerId]) ON DELETE CASCADE
);





