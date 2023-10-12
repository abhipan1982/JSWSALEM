CREATE TABLE [dbo].[TRKLayerRawMaterialRelations] (
    [ParentLayerRawMaterialId] BIGINT         NOT NULL,
    [ChildLayerRawMaterialId]  BIGINT         NOT NULL,
    [IsActualRelation]         BIT            CONSTRAINT [DF_TRKLayerRawMaterialRelations_IsActualRelation] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]             DATETIME       CONSTRAINT [DF_TRKLayerRawMaterialRelations_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]         DATETIME       CONSTRAINT [DF_TRKLayerRawMaterialRelations_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]             NVARCHAR (255) CONSTRAINT [DF_TRKLayerRawMaterialRelations_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]           BIT            CONSTRAINT [DF_TRKLayerRawMaterialRelations_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                BIT            CONSTRAINT [DF_TRKLayerRawMaterialRelations_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TRKLayerRawMaterialRelations] PRIMARY KEY CLUSTERED ([ParentLayerRawMaterialId] ASC, [ChildLayerRawMaterialId] ASC),
    CONSTRAINT [FK_TRKLayerRawMaterialRelations_TRKRawMaterials] FOREIGN KEY ([ParentLayerRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TRKLayerRawMaterialRelations_TRKRawMaterials1] FOREIGN KEY ([ChildLayerRawMaterialId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId])
);





