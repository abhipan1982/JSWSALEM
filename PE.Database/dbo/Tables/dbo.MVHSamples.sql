CREATE TABLE [dbo].[MVHSamples] (
    [SampleId]        BIGINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FKMeasurementId] BIGINT     NOT NULL,
    [IsValid]         BIT        CONSTRAINT [DF_MVSamples_Valid] DEFAULT ((1)) NOT NULL,
    [SampleValue]     FLOAT (53) NOT NULL,
    [OffsetFromHead]  FLOAT (53) CONSTRAINT [DF_MVSamples_Offset] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVSamples] PRIMARY KEY CLUSTERED ([SampleId] ASC) ON [MV_SAMPLES],
    CONSTRAINT [FK_MVSamples_MVMeasurements] FOREIGN KEY ([FKMeasurementId]) REFERENCES [dbo].[MVHMeasurements] ([MeasurementId]) ON DELETE CASCADE
) ON [MV_SAMPLES];








GO
CREATE NONCLUSTERED INDEX [NCI_MeasurementId]
    ON [dbo].[MVHSamples]([FKMeasurementId] ASC)
    INCLUDE([IsValid], [OffsetFromHead])
    ON [MV_SAMPLES];



