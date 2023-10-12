CREATE TABLE [dw].[DimCrew] (
    [SourceName]       NVARCHAR (50)  NOT NULL,
    [SourceTime]       DATETIME       NOT NULL,
    [DimCrewIsDeleted] BIT            NOT NULL,
    [DimCrewHash]      VARBINARY (16) NULL,
    [DimCrewKey]       BIGINT         NOT NULL,
    [CrewName]         NVARCHAR (50)  NOT NULL,
    [CrewDescription]  NVARCHAR (100) NULL,
    [CrewLeaderName]   NVARCHAR (100) NULL,
    [CrewDefaultSize]  SMALLINT       NULL,
    [CrewOrderSeq]     SMALLINT       NULL,
    [DimCrewRow]       INT            IDENTITY (1, 1) NOT NULL
);

