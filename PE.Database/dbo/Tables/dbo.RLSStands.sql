CREATE TABLE [dbo].[RLSStands] (
    [StandId]          BIGINT         NOT NULL,
    [StandNo]          SMALLINT       NOT NULL,
    [StandName]        NVARCHAR (50)  NOT NULL,
    [EnumStandStatus]  SMALLINT       CONSTRAINT [DF_RLSStands_Status] DEFAULT ((0)) NOT NULL,
    [IsOnLine]         BIT            CONSTRAINT [DF_RLSStands_IsOnLine] DEFAULT ((1)) NOT NULL,
    [IsCalibrated]     BIT            CONSTRAINT [DF_RLSStands_IsCalibrated] DEFAULT ((0)) NOT NULL,
    [NumberOfRolls]    SMALLINT       CONSTRAINT [DF_RLSStands_NumberOfRolls] DEFAULT ((0)) NOT NULL,
    [Arrangement]      SMALLINT       CONSTRAINT [DF_RLSStands_Arrangement] DEFAULT ((0)) NOT NULL,
    [StandZoneName]    NVARCHAR (30)  NULL,
    [Position]         SMALLINT       NULL,
    [FKAssetId]        BIGINT         NULL,
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_RLSStands_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_RLSStands_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_RLSStands_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_RLSStands_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_RLSStands_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Stands] PRIMARY KEY CLUSTERED ([StandId] ASC)
);











