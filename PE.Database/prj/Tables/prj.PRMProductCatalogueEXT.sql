CREATE TABLE [prj].[PRMProductCatalogueEXT] (
    [FKProductCatalogueId] BIGINT         NOT NULL,
    [AUDCreatedTs]         DATETIME       CONSTRAINT [DF_PRMProductCatalogueEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]     DATETIME       CONSTRAINT [DF_PRMProductCatalogueEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]         NVARCHAR (255) CONSTRAINT [DF_PRMProductCatalogueEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]       BIT            CONSTRAINT [DF_PRMProductCatalogueEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]            BIT            CONSTRAINT [DF_PRMProductCatalogueEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMProductCatalogueEXT] PRIMARY KEY CLUSTERED ([FKProductCatalogueId] ASC),
    CONSTRAINT [FK_PRMProductCatalogueEXT_PRMProductCatalogue] FOREIGN KEY ([FKProductCatalogueId]) REFERENCES [dbo].[PRMProductCatalogue] ([ProductCatalogueId]) ON DELETE CASCADE
);





