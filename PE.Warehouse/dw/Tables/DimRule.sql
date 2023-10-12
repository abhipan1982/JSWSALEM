CREATE TABLE [dw].[DimRule] (
    [SourceName]       NVARCHAR (50)  NOT NULL,
    [SourceTime]       DATETIME       NOT NULL,
    [DimRuleIsDeleted] BIT            NOT NULL,
    [DimRuleIsCurrent] BIT            NOT NULL,
    [DimRuleHash]      VARBINARY (16) NULL,
    [DimRuleKey]       INT            NULL,
    [RuleIdentifier]   NVARCHAR (400) NOT NULL,
    [SignalIdentifier] NVARCHAR (255) NOT NULL,
    [RuleDirection]    VARCHAR (50)   NULL,
    [RuleParamType]    VARCHAR (50)   NULL,
    [DimRuleRow]       INT            IDENTITY (1, 1) NOT NULL
);

