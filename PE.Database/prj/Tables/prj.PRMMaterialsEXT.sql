CREATE TABLE [prj].[PRMMaterialsEXT] (
    [FKMaterialId]     BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_PRMMaterialsEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_PRMMaterialsEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_PRMMaterialsEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_PRMMaterialsEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_PRMMaterialsEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PRMMaterialsEXT] PRIMARY KEY CLUSTERED ([FKMaterialId] ASC),
    CONSTRAINT [FK_PRMMaterialsEXT_PRMMaterials] FOREIGN KEY ([FKMaterialId]) REFERENCES [dbo].[PRMMaterials] ([MaterialId]) ON DELETE CASCADE
);





