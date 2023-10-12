CREATE TABLE [prj].[TRKRawMaterialsEXT] (
    [FKRawMaterialId]  BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_TRKRawMaterialsEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_TRKRawMaterialsEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_TRKRawMaterialsEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_TRKRawMaterialsEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_TRKRawMaterialsEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RawMaterialsIndexEXT] PRIMARY KEY CLUSTERED ([FKRawMaterialId] ASC),
    CONSTRAINT [FK_RawMaterialsIndexEXT_RawMaterialsIndex] FOREIGN KEY ([FKRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE
);





