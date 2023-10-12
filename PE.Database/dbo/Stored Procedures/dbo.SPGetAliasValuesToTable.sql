CREATE   PROCEDURE dbo.SPGetAliasValuesToTable
(@AliasName NVARCHAR(50), 
 @Param1    NVARCHAR(50) = 0, 
 @Param2    NVARCHAR(50) = 0, 
 @Param3    NVARCHAR(50) = 0, 
 @GetSample BIT          = 0
)
AS

/*
EXEC [dbo].[SPGetAliasValuesToTable] @AliasName = 'STATIC'
EXEC [dbo].[SPGetAliasValuesToTable] @AliasName = 'QUERY', @Param1=22;
EXEC [dbo].[SPGetAliasValuesToTable] @AliasName = 'TABLE_COLUMN', @Param1=6;
EXEC [dbo].[SPGetAliasValuesToTable] @AliasName = 'PFM1.MEAS.VEL_ACT', @Param1=1066181;
EXEC [dbo].[SPGetAliasValuesToTable] @AliasName = 'TIMESTAMP', @Param1=7203166;
EXEC [dbo].[SPGetAliasValuesToTable] @AliasName = 'BOOL', @Param1=177068;
EXEC [dbo].[SPGetAliasValuesToTable] @AliasName = 'K_PR_REN.HMI_STAT.MSA', @Param1=1060330, @GetSample=1;
*/

    BEGIN
        CREATE TABLE #TableOfValues
        (ResultValue          NVARCHAR(2000), 
         ResultValueText      NVARCHAR(2000), 
         ResultValueBoolean   BIT, 
         ResultValueTimestamp DATETIME, 
         ResultValueNumber    FLOAT, 
         ResultValueSample    FLOAT, 
         LengthOffsetFromHead FLOAT NULL, 
         TimeOffsetFromHead   DATETIME NULL, 
         UnitId               BIGINT NULL
        );
        DECLARE @TableName NVARCHAR(50);
        DECLARE @ColumnName NVARCHAR(50);
        DECLARE @ColumnId NVARCHAR(50);
        DECLARE @Aggregation NVARCHAR(50);
        DECLARE @WhereClause NVARCHAR(4000);
        DECLARE @SQLQuery NVARCHAR(4000);
        DECLARE @UnitId BIGINT;
        DECLARE @FeatureId BIGINT;
        DECLARE @StaticValue NVARCHAR(2000);
        DECLARE @SignalType NVARCHAR(50);
        DECLARE @ResultValue NVARCHAR(2000);
        DECLARE @ResultValueText NVARCHAR(2000);
        DECLARE @ResultValueBoolean BIT;
        DECLARE @ResultValueTimestamp DATETIME;
        DECLARE @ResultValueNumber FLOAT;
        DECLARE @ErrorNumber NVARCHAR(4000);
        DECLARE @ErrorMessage NVARCHAR(4000);
        SELECT @TableName = TableName, 
               @ColumnName = ColumnName, 
               @Aggregation = ISNULL(Aggregation, ' '), 
               @WhereClause = ISNULL(WhereClause, ' 1 = 1'), 
               @ColumnId = ColumnId, 
               @SQLQuery = SQLQuery, 
               @StaticValue = StaticValue, 
               @UnitId = FKUnitId, 
               @SignalType = E.EnumKeyword
        FROM dbo.QEAliases A
             INNER JOIN hmi.V_Enums E ON A.EnumQESignalType = E.EnumValue
                                         AND E.EnumName = 'QESignalType'
        WHERE AliasName = @AliasName;

        -- Determine FeatureId
        SELECT @FeatureId = FeatureId
        FROM MVHFeatures
        WHERE FeatureName = @AliasName;

        -- Build a qeury for StaticValue mode
        IF @StaticValue IS NOT NULL
            BEGIN
                SET @SQLQuery = N'SELECT ResultValue = ''' + CAST(@StaticValue AS NVARCHAR(2000)) + '''';
            END;
            ELSE
            -- Build a query for TableName mode
            IF(@TableName IS NOT NULL
               AND @ColumnName IS NOT NULL
               AND @ColumnId IS NOT NULL)
                BEGIN
                    SET @SQLQuery = N'SELECT ResultValue =' + @Aggregation + '(' + @ColumnName + ') ' + N' FROM ' + @TableName + N' WHERE ' + @WhereClause + N' AND ' + @ColumnId + N' = ' + @Param1;
                END;
                ELSE
                -- Build a query for SQLQuery mode
                IF @SQLQuery IS NOT NULL
                    BEGIN
                        SET @SQLQuery = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@SQLQuery, '@Param1', @Param1), '@Param2', @Param2), '@Param3', @Param3), 'UPDATE', '--'), 'DROP', '--'), 'DELETE', '--'), 'CREATE', '--');
                        SET @SQLQuery = ISNULL(STUFF(@SQLQuery, CHARINDEX('SELECT', @SQLQuery), 6, 'SELECT ResultValue = '), @SQLQuery);
                    END;
                    ELSE
                    -- Build a query for Sample mode
                    IF @GetSample = 1
                        BEGIN
                            SET @SQLQuery = 'SELECT M.ValueAvg AS ResultValue, S.SampleValue AS ResultValueSample,
								CASE
									WHEN F.IsLengthRelated = 1 THEN S.OffsetFromHead
								END AS LengthOffsetFromHead,
								CASE
									WHEN F.IsLengthRelated = 0 THEN DATEADD(MILLISECOND, 1000 * S.OffsetFromHead, M.FirstMeasurementTs)
								END TimeOffsetFromHead
								FROM dbo.MVHSamples AS S
                                INNER JOIN dbo.MVHMeasurements AS M ON S.FKMeasurementId = M.MeasurementId
                                INNER JOIN dbo.MVHFeatures AS F ON M.FKFeatureId = F.FeatureId
								WHERE F.FeatureId = ' + CAST(@FeatureId AS NVARCHAR(50)) + ' AND M.FKRawMaterialId = ' + @Param1;
                        END;
                        ELSE
                        -- Build a query for NonSample mode
                        IF @GetSample = 0
                            BEGIN
                                SET @SQLQuery = 'SELECT ValueAvg AS ResultValue
									FROM dbo.MVHMeasurements AS M
									INNER JOIN dbo.MVHFeatures AS F ON M.FKFeatureId = F.FeatureId
									WHERE F.FeatureId = ' + CAST(@FeatureId AS NVARCHAR(50)) + ' AND M.FKRawMaterialId = ' + @Param1;
                            END;
                            ELSE
                            SET @SQLQuery = 'SELECT 0 AS ResultValue';

        -- Insert into temporary table
        BEGIN TRY
            IF @GetSample = 1
                INSERT INTO #TableOfValues
                (ResultValue, 
                 ResultValueSample, 
                 LengthOffsetFromHead, 
                 TimeOffsetFromHead
                )
                EXEC (@SQLQuery);
                ELSE
                INSERT INTO #TableOfValues(ResultValue)
            EXEC (@SQLQuery);
            UPDATE #TableOfValues
              SET 
                  UnitId = @UnitId;
            SELECT @ResultValue = ResultValue
            FROM #TableOfValues;
            IF @SignalType = 'NUMERIC'
                UPDATE #TableOfValues
                  SET 
                      ResultValueNumber = CAST(@ResultValue AS FLOAT);
                ELSE
                IF @SignalType = 'BOOLEAN'
                    UPDATE #TableOfValues
                      SET 
                          ResultValueBoolean = CAST(@ResultValue AS BIT);
                    ELSE
                    IF @SignalType = 'TIMESTAMP'
                        UPDATE #TableOfValues
                          SET 
                              ResultValueTimestamp = CAST(@ResultValue AS DATETIME);
                        ELSE
                        UPDATE #TableOfValues
                          SET 
                              ResultValueText = CAST(@ResultValue AS NVARCHAR(2000));
        END TRY
        BEGIN CATCH
            SELECT @ErrorNumber = CONVERT(NVARCHAR(4000), ERROR_NUMBER()), 
                   @ErrorMessage = ERROR_MESSAGE();
        END CATCH;
        -- Select temporary table

        SELECT *
        FROM #TableOfValues;
    END;