CREATE TABLE [prj].[MVHMeasurementsEXT] (
    [FKMeasurementId]  BIGINT         NOT NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_MVHMeasurementsEXT_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_MVHMeasurementsEXT_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_MVHMeasurementsEXT_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_MVHMeasurementsEXT_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_MVHMeasurementsEXT_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVMeasurementsEXT] PRIMARY KEY CLUSTERED ([FKMeasurementId] ASC) ON [MV_MEASUREMENTS],
    CONSTRAINT [FK_MeasurementsEXT_Measurements] FOREIGN KEY ([FKMeasurementId]) REFERENCES [dbo].[MVHMeasurements] ([MeasurementId]) ON DELETE CASCADE
) ON [MV_MEASUREMENTS];





