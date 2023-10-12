CREATE TABLE [prj].[PRMProductsEXT] (
    [FKProductId]      BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMProductsEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMProductsEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMProductsEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMProductsEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMProductsEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMProductsEXT] PRIMARY KEY CLUSTERED ([FKProductId] ASC),
    CONSTRAINT [FK_PRMProductsEXT_PRMProducts] FOREIGN KEY ([FKProductId]) REFERENCES [dbo].[PRMProducts] ([ProductId]) ON DELETE CASCADE
);





