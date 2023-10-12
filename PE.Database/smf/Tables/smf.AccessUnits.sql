CREATE TABLE [smf].[AccessUnits] (
    [AccessUnitId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [AccessUnitName]     NVARCHAR (50) NOT NULL,
    [EnumAccessUnitType] SMALLINT      CONSTRAINT [DF_SMFAccessUnits_AccessUnitType] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SMFAccessUnits] PRIMARY KEY CLUSTERED ([AccessUnitId] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [UQ_AccessUnits_Name] UNIQUE NONCLUSTERED ([AccessUnitName] ASC) WITH (FILLFACTOR = 90)
);



