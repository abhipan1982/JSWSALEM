CREATE TABLE [prj].[TRKRawMaterialsStepsEXT] (
    [FKRawMaterialStepId] BIGINT         NOT NULL,
    [AUDCreatedTs]        DATETIME       CONSTRAINT [DF_TRKRawMaterialsStepsEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]    DATETIME       CONSTRAINT [DF_TRKRawMaterialsStepsEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]        NVARCHAR (255) CONSTRAINT [DF_TRKRawMaterialsStepsEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]      BIT            CONSTRAINT [DF_TRKRawMaterialsStepsEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            CONSTRAINT [DF_TRKRawMaterialsStepsEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHRawMaterialsStepsEXT] PRIMARY KEY CLUSTERED ([FKRawMaterialStepId] ASC),
    CONSTRAINT [FK_MVHRawMaterialsStepsEXT_MVHRawMaterialsSteps] FOREIGN KEY ([FKRawMaterialStepId]) REFERENCES [dbo].[TRKRawMaterialsSteps] ([RawMaterialStepId]) ON DELETE CASCADE
);





