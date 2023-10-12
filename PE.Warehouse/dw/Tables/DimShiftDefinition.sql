CREATE TABLE [dw].[DimShiftDefinition] (
    [SourceName]                  NVARCHAR (50)  NOT NULL,
    [SourceTime]                  DATETIME       NOT NULL,
    [DimShiftDefinitionIsDeleted] BIT            NOT NULL,
    [DimShiftDefinitionHash]      VARBINARY (16) NULL,
    [DimShiftDefinitionKey]       BIGINT         NOT NULL,
    [DimShiftDefinitionKeyNext]   BIGINT         NULL,
    [ShiftCode]                   NVARCHAR (10)  NOT NULL,
    [ShiftDefaultStartTime]       TIME (7)       NOT NULL,
    [ShiftDefaultEndTime]         TIME (7)       NOT NULL,
    [ShiftEndsNextDay]            BIT            NOT NULL,
    [DimShiftDefinitionRow]       INT            IDENTITY (1, 1) NOT NULL
);



