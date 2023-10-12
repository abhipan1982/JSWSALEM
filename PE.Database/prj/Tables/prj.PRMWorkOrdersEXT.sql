CREATE TABLE [prj].[PRMWorkOrdersEXT] (
    [FKWorkOrderId]    BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMWorkOrdersEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMWorkOrdersEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMWorkOrdersEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMWorkOrdersEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMWorkOrdersEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PEWorkOrdersExt] PRIMARY KEY CLUSTERED ([FKWorkOrderId] ASC),
    CONSTRAINT [FK_WorkOrdersEXT_WorkOrders] FOREIGN KEY ([FKWorkOrderId]) REFERENCES [dbo].[PRMWorkOrders] ([WorkOrderId]) ON DELETE CASCADE
);











