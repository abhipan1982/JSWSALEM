CREATE TABLE [smf].[Limits] (
    [LimitId]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedTs]              DATETIME       CONSTRAINT [DF_Limits_CreatedTs] DEFAULT (getdate()) NOT NULL,
    [LastUpdateTs]           DATETIME       CONSTRAINT [DF_Limits_LastUpdateTs] DEFAULT (getdate()) NOT NULL,
    [Name]                   NVARCHAR (50)  NOT NULL,
    [Description]            NVARCHAR (200) NULL,
    [EnumParameterValueType] SMALLINT       CONSTRAINT [DF_Limits_EnumParameterValueType] DEFAULT ((0)) NOT NULL,
    [ParameterGroupId]       BIGINT         NOT NULL,
    [UnitId]                 BIGINT         NULL,
    [LowerValueInt]          INT            NULL,
    [UpperValueInt]          INT            NULL,
    [LowerValueFloat]        FLOAT (53)     NULL,
    [UpperValueFloat]        FLOAT (53)     NULL,
    [LowerValueDate]         DATE           NULL,
    [UpperValueDate]         DATE           NULL,
    CONSTRAINT [PK_LimitId] PRIMARY KEY CLUSTERED ([LimitId] ASC),
    CONSTRAINT [CK_Limits_ValueDate] CHECK ([UpperValueDate]>=[LowerValueDate]),
    CONSTRAINT [CK_Limits_ValueFloat] CHECK ([UpperValueFloat]>=[LowerValueFloat]),
    CONSTRAINT [CK_Limits_ValueInt] CHECK ([UpperValueInt]>=[LowerValueInt]),
    CONSTRAINT [FK_Limits_LimitGroupId] FOREIGN KEY ([ParameterGroupId]) REFERENCES [smf].[ParameterGroup] ([ParameterGroupId]),
    CONSTRAINT [FK_Limits_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [smf].[UnitOfMeasure] ([UnitId]),
    CONSTRAINT [UQ_Limits_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);








GO
CREATE NONCLUSTERED INDEX [NCI_UnitId]
    ON [smf].[Limits]([UnitId] ASC);


GO
CREATE NONCLUSTERED INDEX [NCI_LimitGroupId]
    ON [smf].[Limits]([ParameterGroupId] ASC);



