CREATE TABLE [dbo].[TRKLayerRelations] (
    [ParentLayerId]    BIGINT         NOT NULL,
    [ChildLayerId]     BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_TRKLayerRelations_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_TRKLayerRelations_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_TRKLayerRelations_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_TRKLayerRelations_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_TRKLayerRelations_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TRKLayerRelations] PRIMARY KEY CLUSTERED ([ParentLayerId] ASC, [ChildLayerId] ASC),
    CONSTRAINT [FK_TRKLayerRelations_TRKRawMaterials] FOREIGN KEY ([ParentLayerId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TRKLayerRelations_TRKRawMaterials1] FOREIGN KEY ([ChildLayerId]) REFERENCES [dbo].[TRKRawMaterials] ([RawMaterialId])
);





