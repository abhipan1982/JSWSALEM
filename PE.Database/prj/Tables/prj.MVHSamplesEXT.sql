CREATE TABLE [prj].[MVHSamplesEXT] (
    [FKSampleId]       BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MVHSamplesEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MVHSamplesEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MVHSamplesEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MVHSamplesEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MVHSamplesEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHSamplesEXT] PRIMARY KEY CLUSTERED ([FKSampleId] ASC),
    CONSTRAINT [FK_MVHSamplesEXT_MVHSamples] FOREIGN KEY ([FKSampleId]) REFERENCES [dbo].[MVHSamples] ([SampleId]) ON DELETE CASCADE
);





