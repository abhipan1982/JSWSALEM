CREATE TABLE [dbo].[EVTShiftLayouts] (
    [ShiftLayoutId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ShiftLayoutCode]        NVARCHAR (10)  NOT NULL,
    [ShiftLayoutName]        NVARCHAR (50)  NULL,
    [ShiftLayoutDescription] NVARCHAR (255) NULL,
    [IsDefaultLayout]        BIT            CONSTRAINT [DF_EVTShiftLayouts_IsDefaultLayout] DEFAULT ((0)) NOT NULL,
    [AUDCreatedTs]           DATETIME       CONSTRAINT [DF_EVTShiftLayouts_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs]       DATETIME       CONSTRAINT [DF_EVTShiftLayouts_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]           NVARCHAR (255) CONSTRAINT [DF_EVTShiftLayouts_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]         BIT            CONSTRAINT [DF_EVTShiftLayouts_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_EVTShiftLayouts_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EVTShiftLayouts] PRIMARY KEY CLUSTERED ([ShiftLayoutId] ASC)
);



