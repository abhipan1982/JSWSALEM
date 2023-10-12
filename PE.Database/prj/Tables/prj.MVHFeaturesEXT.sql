CREATE TABLE [prj].[MVHFeaturesEXT] (
    [FKFeatureId]         BIGINT         NOT NULL,
    [ResNum]              SMALLINT       NULL,
    [ResDen]              SMALLINT       NULL,
    [MinValue]            FLOAT (53)     NULL,
    [MaxValue]            FLOAT (53)     NULL,
    [ListValues]          NVARCHAR (400) NULL,
    [IsLengthChange]      BIT            CONSTRAINT [DF_MVHFeaturesEXT_IsLengthChange] DEFAULT ((0)) NOT NULL,
    [IsWeightChange]      BIT            CONSTRAINT [DF_MVHFeaturesEXT_IsWeightChange] DEFAULT ((0)) NOT NULL,
    [IsPossibleInversion] BIT            CONSTRAINT [DF_FeaturesEXT_PossibleInversion] DEFAULT ((0)) NOT NULL,
    [OnAssetEntry]        BIT            CONSTRAINT [DF_MVHFeaturesEXT_OnAssetEntry] DEFAULT ((1)) NULL,
    [EnumTypeOfCut]       SMALLINT       CONSTRAINT [DF_MVHFeaturesEXT_EnumTypeOfCut] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_MVHFeaturesEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_MVHFeaturesEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_MVHFeaturesEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_MVHFeaturesEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_MVHFeaturesEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVFeaturesEXT] PRIMARY KEY CLUSTERED ([FKFeatureId] ASC),
    CONSTRAINT [FK_MVFeaturesEXT_MVFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]) ON DELETE CASCADE
);





