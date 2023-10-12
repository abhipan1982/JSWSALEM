CREATE   VIEW dw.DimRule
AS

/*
select * from dw.DimRule
*/

WITH Rules
     AS (SELECT DISTINCT 
                BINARY_CHECKSUM(RulesIdentifier, SignalIdentifier, EnumQEDirection, EnumQEParamType) AS RuleKey, 
                RulesIdentifier AS RuleIdentifier, 
                SignalIdentifier AS SignalIdentifier, 
                dbo.FNGetEnumKeyword('QEDirection', EnumQEDirection) AS RuleDirection, 
                dbo.FNGetEnumKeyword('QEParamType', EnumQEParamType) AS RuleParamType
         FROM dbo.QEMappingEntry AS ME)
     SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
            GETDATE() AS SourceTime, 
            ISNULL(ROW_NUMBER() OVER(
            ORDER BY R.RuleIdentifier, 
                     R.SignalIdentifier), 0) AS DimRuleRow, 
            ISNULL(CAST(0 AS BIT), 0) AS DimRuleIsDeleted, 
            ISNULL(CAST(1 AS BIT), 1) AS DimRuleIsCurrent, 
            CAST(HASHBYTES('MD5', COALESCE(CAST(R.RuleIdentifier AS NVARCHAR), ';') + COALESCE(CAST(R.SignalIdentifier AS NVARCHAR), ';') + COALESCE(CAST(R.RuleDirection AS NVARCHAR), ';') + COALESCE(CAST(R.RuleParamType AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimRuleHash, 
            R.RuleKey AS DimRuleKey, 
            R.RuleIdentifier, 
            R.SignalIdentifier, 
            R.RuleDirection, 
            R.RuleParamType
     FROM Rules AS R;