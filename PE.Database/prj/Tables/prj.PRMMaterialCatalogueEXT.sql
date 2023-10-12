CREATE TABLE [prj].[PRMMaterialCatalogueEXT] (
    [FKMaterialCatalogueId] BIGINT         NOT NULL,
    [AUDCreatedTs]          DATETIME       CONSTRAINT [DF_PRMMaterialCatalogueEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]      DATETIME       CONSTRAINT [DF_PRMMaterialCatalogueEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]          NVARCHAR (255) CONSTRAINT [DF_PRMMaterialCatalogueEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]        BIT            CONSTRAINT [DF_PRMMaterialCatalogueEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            CONSTRAINT [DF_PRMMaterialCatalogueEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMMaterialCatalogueEXT] PRIMARY KEY CLUSTERED ([FKMaterialCatalogueId] ASC),
    CONSTRAINT [FK_PRMMaterialCatalogueEXT_PRMMaterialCatalogue] FOREIGN KEY ([FKMaterialCatalogueId]) REFERENCES [dbo].[PRMMaterialCatalogue] ([MaterialCatalogueId]) ON DELETE CASCADE
);





