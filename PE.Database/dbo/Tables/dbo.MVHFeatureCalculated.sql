CREATE TABLE [dbo].[MVHFeatureCalculated] (
    [FeatureCalculatedId]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [FKFeatureId]                         BIGINT         NOT NULL,
    [CalculatedValue]                     INT            NOT NULL,
    [IsVirtual]                           BIT            NOT NULL,
    [Seq]                                 SMALLINT       NOT NULL,
    [FKFeatureId_1]                       BIGINT         NOT NULL,
    [Value_1]                             INT            NOT NULL,
    [EnumCompareOperator_1]               SMALLINT       NOT NULL,
    [EnumLogicalOperator]                 SMALLINT       NOT NULL,
    [FKFeatureId_2]                       BIGINT         NULL,
    [Value_2]                             INT            NULL,
    [EnumCompareOperator_2]               SMALLINT       NOT NULL,
    [TimeFilter]                          REAL           NULL,
    [EnumLogicalOperator_ForNextSequence] SMALLINT       NOT NULL,
    [AUDCreatedTs]                        DATETIME       CONSTRAINT [DF_MVHFeatureCalculated_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]                    DATETIME       CONSTRAINT [DF_MVHFeatureCalculated_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]                        NVARCHAR (255) CONSTRAINT [DF_MVHFeatureCalculated_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]                      BIT            CONSTRAINT [DF_MVHFeatureCalculated_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]                           BIT            CONSTRAINT [DF_MVHFeatureCalculated_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MVHFeatureCalculated] PRIMARY KEY CLUSTERED ([FeatureCalculatedId] ASC),
    CONSTRAINT [FK_MVHFeatureCalculated_MVHFeatures] FOREIGN KEY ([FKFeatureId]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]),
    CONSTRAINT [FK_MVHFeatureCalculated_MVHFeatures_1] FOREIGN KEY ([FKFeatureId_1]) REFERENCES [dbo].[MVHFeatures] ([FeatureId]),
    CONSTRAINT [FK_MVHFeatureCalculated_MVHFeatures_2] FOREIGN KEY ([FKFeatureId_2]) REFERENCES [dbo].[MVHFeatures] ([FeatureId])
);



