CREATE TABLE [dw].[FactRatingRootCause] (
    [SourceName]                   NVARCHAR (50)  NOT NULL,
    [SourceTime]                   DATETIME       NOT NULL,
    [FactRatingRootCauseIsDeleted] BIT            NOT NULL,
    [FactRatingRootCauseHash]      VARBINARY (16) NULL,
    [FactRatingRootCauseKey]       BIGINT         NOT NULL,
    [FactRatingKey]                BIGINT         NOT NULL,
    [DimMaterialKey]               BIGINT         NULL,
    [RootCauseName]                NVARCHAR (400) NOT NULL,
    [RootCauseType]                INT            NULL,
    [RootCausePriority]            FLOAT (53)     NULL,
    [RootCauseInfo]                NVARCHAR (400) NULL,
    [RootCauseCorrection]          NVARCHAR (400) NULL,
    [RootCauseVerification]        NVARCHAR (400) NULL,
    [RootCauseAggregates]          NVARCHAR (MAX) NULL,
    [FactRatingRootCauseRow]       BIGINT         IDENTITY (1, 1) NOT NULL
);

