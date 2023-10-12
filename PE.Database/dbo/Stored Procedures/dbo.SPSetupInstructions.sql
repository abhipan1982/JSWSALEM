CREATE PROCEDURE [dbo].[SPSetupInstructions] @SetupId    BIGINT = NULL, 
                                             @AssetId    BIGINT = NULL, 
                                             @IsSentToL1 BIT    = NULL
AS
    BEGIN
        SET NOCOUNT ON;
        WITH CTE
             AS (SELECT ST.SetupTypeId, 
                        ST.SetupTypeCode, 
                        ST.SetupTypeName, 
                        ST.IsRequired AS IsRequiredSetup, 
                        ST.OrderSeq AS OrderSeqSetup, 
                        S.SetupId, 
                        S.SetupCode, 
                        S.SetupName, 
                        P.ParameterCode, 
                        P.ParameterName, 
                        SP.ParameterValueId, 
                        SP.IsRequired AS IsRequiredParameter, 
                        STI.SetupTypeInstructionId, 
                        STI.OrderSeq, 
                        STI.IsRequired, 
                        STI.IsSentToL1, 
                        STI.RangeFrom, 
                        STI.RangeTo, 
                        I.InstructionId, 
                        I.InstructionCode, 
                        I.InstructionName, 
                        I.InstructionDescription, 
                        I.DefaultValue InstructionDefaultValue, 
                        SI.SetupInstructionId, 
                        SI.[Value], 
                        UOM.UnitId, 
                        UOM.UnitSymbol, 
                        DT.DataTypeId, 
                        DT.DataType, 
                        DT.DataTypeNameDotNet, 
                        A.AssetId, 
                        A.AssetName
                 FROM [dbo].[STPSetups] S
                      INNER JOIN [dbo].[STPSetupTypes] ST ON S.FKSetupTypeId = ST.SetupTypeId
                      INNER JOIN [dbo].[STPSetupTypeInstructions] STI
                      LEFT JOIN [dbo].[MVHAssets] A ON STI.FKAssetId = A.AssetId ON ST.SetupTypeId = STI.FKSetupTypeId
                      INNER JOIN [dbo].[STPInstructions] I ON STI.FKInstructionId = I.InstructionId
                      INNER JOIN [smf].[UnitOfMeasure] UOM ON I.FKUnitId = UOM.UnitId
                      INNER JOIN [dbo].[DBDataTypes] DT ON I.FKDataTypeId = DT.DataTypeId
                      INNER JOIN [dbo].[STPSetupInstructions] SI ON SI.FKSetupId = S.SetupId
                                                                    AND SI.FKSetupTypeInstructionId = STI.SetupTypeInstructionId
                      LEFT JOIN [dbo].[STPSetupParameters] SP
                      INNER JOIN [dbo].[STPParameters] P ON SP.FKParameterId = P.ParameterId ON SP.FKSetupId = S.SetupId
                 WHERE(@SetupId IS NULL
                       OR SetupId = @SetupId)
                      AND (@IsSentToL1 IS NULL
                           OR IsSentToL1 = @IsSentToL1)
                      AND (@AssetId IS NULL
                           OR AssetId = @AssetId))
             SELECT ROW_NUMBER() OVER(
                    ORDER BY OrderSeqSetup, 
                             SetupId, 
                             OrderSeq) Seq, 
                    SetupTypeId, 
                    SetupTypeCode, 
                    SetupTypeName, 
                    OrderSeqSetup, 
                    IsRequiredSetup, 
                    SetupId, 
                    SetupCode, 
                    SetupName, 
                    SetupTypeInstructionId, 
                    SetupInstructionId, 
                    OrderSeq, 
                    InstructionId, 
                    InstructionCode, 
                    InstructionName, 
                    InstructionDescription, 
                    InstructionDefaultValue, 
                    IsRequired, 
                    [Value], 
                    RangeFrom, 
                    RangeTo, 
                    UnitId, 
                    UnitSymbol, 
                    DataTypeId, 
                    DataType, 
                    DataTypeNameDotNet, 
                    IsSentToL1, 
                    AssetId, 
                    AssetName, 
                    MAX([Steelgrade]) [Steelgrade], 
                    MAX([Product Size]) [Product Size], 
                    MAX([Work Order]) [Work Order], 
                    MAX([Heat Number]) [Heat Number], 
                    MAX([Layout]) [Layout], 
                    MAX([Issue]) [Issue], 
                    MAX([Previous Steelgrade]) [Previous Steelgrade], 
                    MAX([Previous Product Size]) [Previous Product Size], 
                    MAX([Previous Layout]) [Previous Layout]
             FROM CTE PIVOT(MAX(ParameterValueId) FOR ParameterName IN([Steelgrade], 
                                                                       [Product Size], 
                                                                       [Work Order], 
                                                                       [Heat Number], 
                                                                       [Layout], 
                                                                       [Issue], 
                                                                       [Previous Steelgrade], 
                                                                       [Previous Product Size], 
                                                                       [Previous Layout])) AS PT
             WHERE(@SetupId IS NULL
                   OR PT.SetupId = @SetupId)
                  AND (@IsSentToL1 IS NULL
                       OR PT.IsSentToL1 = @IsSentToL1)
                  AND (@AssetId IS NULL
                       OR PT.AssetId = @AssetId)
             GROUP BY SetupTypeId, 
                      SetupTypeCode, 
                      SetupTypeName, 
                      OrderSeqSetup, 
                      IsRequiredSetup, 
                      OrderSeq, 
                      InstructionId, 
                      InstructionCode, 
                      InstructionName, 
                      InstructionDescription, 
                      InstructionDefaultValue, 
                      IsRequired, 
                      [Value], 
                      RangeFrom, 
                      RangeTo, 
                      UnitId, 
                      UnitSymbol, 
                      DataTypeId, 
                      DataType, 
                      DataTypeNameDotNet, 
                      IsSentToL1, 
                      AssetId, 
                      AssetName, 
                      SetupId, 
                      SetupCode, 
                      SetupName, 
                      SetupTypeInstructionId, 
                      SetupInstructionId;
    END;