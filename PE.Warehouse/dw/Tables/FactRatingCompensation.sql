CREATE TABLE [dw].[FactRatingCompensation] (
    [SourceName]                      NVARCHAR (50)  NOT NULL,
    [SourceTime]                      DATETIME       NOT NULL,
    [FactRatingCompensationIsDeleted] BIT            NOT NULL,
    [FactRatingCompensationHash]      VARBINARY (16) NULL,
    [FactRatingCompensationKey]       BIGINT         NOT NULL,
    [FactRatingKey]                   BIGINT         NOT NULL,
    [DimMaterialKey]                  BIGINT         NULL,
    [CompensationName]                NVARCHAR (400) NULL,
    [CompensationTypeName]            NVARCHAR (400) NOT NULL,
    [CompensationAlternative]         FLOAT (53)     NULL,
    [CompensationInfo]                NVARCHAR (400) NULL,
    [CompensationDetail]              NVARCHAR (400) NULL,
    [CompensationIsChosen]            BIT            NOT NULL,
    [CompensationChosen]              DATETIME       NULL,
    [CompensationAggregates]          NVARCHAR (MAX) NULL,
    [FactRatingCompensationRow]       BIGINT         IDENTITY (1, 1) NOT NULL
);

