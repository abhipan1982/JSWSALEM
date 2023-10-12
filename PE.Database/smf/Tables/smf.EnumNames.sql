CREATE TABLE [smf].[EnumNames] (
    [EnumNameId] BIGINT       NOT NULL,
    [EnumName]   VARCHAR (50) NOT NULL,
    [EnumType]   VARCHAR (10) CONSTRAINT [DF_EnumNames_EnumType] DEFAULT ('short') NOT NULL,
    [IsSMF]      BIT          CONSTRAINT [DF_EnumNames_IsSMF] DEFAULT ((0)) NOT NULL,
    [IsCustom]   BIT          CONSTRAINT [DF_EnumNames_IsCustom] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_smf.EnumNames] PRIMARY KEY CLUSTERED ([EnumNameId] ASC),
    CONSTRAINT [ChkEnumType] CHECK ([EnumType]='long' OR [EnumType]='int' OR [EnumType]='short' OR [EnumType]='byte'),
    CONSTRAINT [ChkIsSMFAndIsCustomNotTrueBoth] CHECK ((CONVERT([int],[IsSMF])+CONVERT([int],[IsCustom]))<(2))
);










GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EnumName]
    ON [smf].[EnumNames]([EnumName] ASC);

