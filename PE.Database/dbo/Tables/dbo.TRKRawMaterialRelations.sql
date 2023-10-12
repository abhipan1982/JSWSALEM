CREATE TABLE [dbo].[TRKRawMaterialRelations] (
    [ParentRawMaterialId] BIGINT         NOT NULL,
    [ChildRawMaterialId]  BIGINT         NOT NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_TRKRawMaterialRelations_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_TRKRawMaterialRelations_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_TRKRawMaterialRelations_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_TRKRawMaterialRelations_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_TRKRawMaterialRelations_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TRKRawMaterialRelations] PRIMARY KEY CLUSTERED ([ParentRawMaterialId] ASC, [ChildRawMaterialId] ASC),
    CONSTRAINT [FK_TRKRawMaterialRelations_TRKRawMaterials] FOREIGN KEY ([ParentRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TRKRawMaterialRelations_TRKRawMaterials1] FOREIGN KEY ([ChildRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId])
);





