CREATE TABLE [dw].[DimHour] (
    [SourceName]       NVARCHAR (50)  NOT NULL,
    [SourceTime]       DATETIME       NOT NULL,
    [DimHourIsDeleted] BIT            NOT NULL,
    [DimHourHash]      VARBINARY (16) NULL,
    [DimHourKey]       SMALLINT       NOT NULL,
    [Hour24]           INT            NOT NULL,
    [Hour12]           INT            NOT NULL,
    [HourAmPm]         VARCHAR (2)    NOT NULL,
    [HourCode]         VARCHAR (2)    NOT NULL,
    [HourTime]         TIME (0)       NOT NULL,
    [DimHourRow]       INT            IDENTITY (1, 1) NOT NULL
);

