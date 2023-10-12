CREATE TABLE [dw].[DimShift] (
    [SourceName]            NVARCHAR (50)  NOT NULL,
    [SourceTime]            DATETIME       NOT NULL,
    [DimShiftIsDeleted]     BIT            NOT NULL,
    [DimShiftHash]          VARBINARY (16) NULL,
    [DimShiftKey]           BIGINT         NOT NULL,
    [DimCalendarKey]        BIGINT         NOT NULL,
    [DimDateKey]            INT            NULL,
    [DimCrewKey]            BIGINT         NOT NULL,
    [DimShiftDefinitionKey] BIGINT         NOT NULL,
    [ShiftCode]             NVARCHAR (10)  NOT NULL,
    [ShiftDateWithCode]     NVARCHAR (21)  NOT NULL,
    [ShiftStartTime]        DATETIME       NOT NULL,
    [ShiftEndTime]          DATETIME       NOT NULL,
    [ShiftDurationH]        INT            NULL,
    [ShiftDurationM]        INT            NULL,
    [ShiftDurationS]        INT            NULL,
    [ShiftEndsNextDay]      BIT            NOT NULL,
    [CrewName]              NVARCHAR (50)  NOT NULL,
    [CrewDescription]       NVARCHAR (100) NULL,
    [DimShiftRow]           INT            IDENTITY (1, 1) NOT NULL
);

