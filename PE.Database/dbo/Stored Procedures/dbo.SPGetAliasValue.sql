CREATE PROCEDURE [dbo].[SPGetAliasValue]
(@AliasName            NVARCHAR(50), 
 @Param1               NVARCHAR(50)   = 0, --RawMaterialId
 @Param2               NVARCHAR(50)   = 0, 
 @Param3               NVARCHAR(50)   = 0, 
 @ResultSet            NVARCHAR(4000) = NULL OUTPUT, 
 @ResultValue          NVARCHAR(4000) = NULL OUTPUT, 
 @ResultValueNumber    FLOAT          = NULL OUTPUT, 
 @ResultValueBoolean   BIT            = NULL OUTPUT, 
 @ResultValueTimestamp DATETIME       = NULL OUTPUT, 
 @ResultValueText      NVARCHAR(4000) = NULL OUTPUT, 
 @UnitId               BIGINT         = NULL OUTPUT, 
 @ErrorText            NVARCHAR(4000) = NULL OUTPUT, 
 @ErrorCode            INT            = NULL OUTPUT, 
 @NORecords            INT            = NULL OUTPUT
)
AS

/*
EXEC [dbo].[SPGetAliasValue] @AliasName = 'STATIC'
EXEC [dbo].[SPGetAliasValue] @AliasName = 'QUERY', @Param1=2;
EXEC [dbo].[SPGetAliasValue] @AliasName = 'TABLE_COLUMN', @Param1=6;
EXEC [dbo].[SPGetAliasValue] @AliasName = 'PFM1.MEAS.VEL_ACT', @Param1=1066181;
EXEC [dbo].[SPGetAliasValue] @AliasName = 'TIMESTAMP', @Param1=7203166;
EXEC [dbo].[SPGetAliasValue] @AliasName = 'BOOL', @Param1=177068;
EXEC [dbo].[SPGetAliasValue] @AliasName = 'K_PR_REN.HMI_STAT.MSA', @Param1=1060330;
*/

    BEGIN
        DECLARE @SelectSt NVARCHAR(10)= N'SELECT ';
        DECLARE @FromSt NVARCHAR(10)= N' FROM ';
        DECLARE @WhereSt NVARCHAR(15)= N' WHERE ';
        DECLARE @GroupSt NVARCHAR(15)= N' GROUP BY ';
        DECLARE @Apostrophe CHAR= CHAR(39);
        DECLARE @LeftParenthesis CHAR= CHAR(40);
        DECLARE @RightParenthesis CHAR= CHAR(41);
        DECLARE @Comma CHAR= CHAR(44);
        DECLARE @ErrorNumber NVARCHAR(4000);
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @TableName NVARCHAR(50);
        DECLARE @ColumnName NVARCHAR(50);
        DECLARE @ColumnId NVARCHAR(50);
        DECLARE @Aggregation NVARCHAR(50);
        DECLARE @WhereClause NVARCHAR(4000);
        DECLARE @SQLQuery NVARCHAR(4000);
        DECLARE @FeatureId BIGINT;
        DECLARE @StaticValue NVARCHAR(2000);
        DECLARE @SignalType NVARCHAR(50);
        DECLARE @IsSampled BIT;
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

        -- Build a query for StaticValue mode
        IF @StaticValue IS NOT NULL
            BEGIN
                SET @SQLQuery = @SelectSt + N'@ResultValueInside = ''' + CAST(@StaticValue AS NVARCHAR(2000)) + '''';
            END;
            ELSE
            -- Build a query for TableName mode
            IF(@TableName IS NOT NULL
               AND @ColumnName IS NOT NULL
               AND @ColumnId IS NOT NULL)
                BEGIN
                    SET @SQLQuery = @SelectSt + N'@ResultValueInside =' + @Aggregation + '(' + @ColumnName + ')' + @FromSt + @TableName + @WhereSt + @WhereClause + ' AND ' + @ColumnId + '=' + @Param1;
                END;
                ELSE
                -- Build a query for SQLQuery mode
                IF @SQLQuery IS NOT NULL
                    BEGIN
                        SET @SQLQuery = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@SQLQuery, '@Param1', @Param1), '@Param2', @Param2), '@Param3', @Param3), 'UPDATE', '--'), 'DROP', '--'), 'DELETE', '--'), 'CREATE', '--');
                        SET @SQLQuery = ISNULL(STUFF(@SQLQuery, CHARINDEX('SELECT', @SQLQuery), 6, 'SELECT @ResultValueInside = '), @SQLQuery);
                    END;
                    ELSE
                    -- Build a query for Measurement mode
                    IF @FeatureId IS NOT NULL
                        BEGIN
                            SET @SQLQuery = 'SELECT ' + N'@ResultValueInside = ' + ' ValueAvg 
												FROM MVHMeasurements 
												WHERE FKFeatureId = ' + CAST(@FeatureId AS NVARCHAR(50)) + ' 
													AND FKRawMaterialId = ' + @Param1;
                        END;
                        ELSE
                        SET @SQLQuery = 'SELECT ' + N'@ResultValueInside = 0';
        BEGIN TRY
            EXECUTE @ResultSet = sp_executesql 
                    @SQLQuery, 
                    N'@ResultValueInside NVARCHAR(4000) OUTPUT', 
                    @ResultValueInside = @ResultValue OUTPUT;
            IF @SignalType = 'NUMERIC'
                SET @ResultValueNumber = CAST(@ResultValue AS FLOAT);
                ELSE
                IF @SignalType = 'BOOLEAN'
                    SET @ResultValueBoolean = CAST(@ResultValue AS BIT);
                    ELSE
                    IF @SignalType = 'TIMESTAMP'
                        SET @ResultValueTimestamp = CAST(@ResultValue AS DATETIME);
                        ELSE
                        SET @ResultValueText = @ResultValue;
            SELECT @NORecords = @@ROWCOUNT;
        END TRY
        BEGIN CATCH
            SELECT @ErrorNumber = CONVERT(NVARCHAR(4000), ERROR_NUMBER()), 
                   @ErrorMessage = ERROR_MESSAGE();
        END CATCH;
        IF @NORecords > 0
            BEGIN
                SET @ErrorText = 'Success';
                SET @ErrorCode = 0;
            END;
            ELSE
            IF @NORecords = 0
                BEGIN
                    SET @ErrorText = 'No data fetched';
                    SET @ErrorCode = 1;
                END;
                ELSE
                BEGIN
                    SET @ErrorText = CONCAT('Error number: ' , @ErrorNumber , '. Error message: ' , @ErrorMessage + '');
                    SET @ErrorCode = 2;
                END;
        PRINT 'ErrorText: ' + @ErrorText;
        PRINT 'ErrorCode: ' + CAST(@ErrorCode AS NVARCHAR(50));
        PRINT 'ResultValue: ' + ISNULL(@ResultValue, '');
        PRINT 'ResultValueNumber: ' + ISNULL(CAST(@ResultValueNumber AS NVARCHAR(50)), '');
        PRINT 'ResultValueText: ' + ISNULL(CAST(@ResultValueText AS NVARCHAR(50)), '');
        PRINT 'ResultValueBoolean: ' + ISNULL(CAST(@ResultValueBoolean AS NVARCHAR(50)), '');
        PRINT 'ResultValueTimestamp: ' + ISNULL(CAST(@ResultValueTimestamp AS NVARCHAR(50)), '');
        PRINT 'UnitId: ' + ISNULL(CAST(@UnitId AS NVARCHAR(50)), '');
        PRINT 'SQLQuery: ' + ISNULL(@SQLQuery, '');
    END;