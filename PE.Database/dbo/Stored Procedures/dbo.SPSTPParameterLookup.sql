CREATE PROCEDURE [dbo].[SPSTPParameterLookup]
(@ParameterCode    NVARCHAR(50), 
 @ParameterValueId BIGINT,
 @ResultValue      NVARCHAR(4000) = NULL OUTPUT 
--,@ResultSet        NVARCHAR(4000) = NULL OUTPUT
--,@ErrorText        NVARCHAR(4000) = NULL OUTPUT 
--,@ErrorCode        INT            = NULL OUTPUT
--,@NORecords        INT            = NULL OUTPUT
)
AS
    BEGIN
        DECLARE @SelectSt NVARCHAR(10)= N'SELECT ';
        DECLARE @FromSt NVARCHAR(10)= N' FROM ';
        DECLARE @WhereSt NVARCHAR(15)= N' WHERE 1=1 AND ';
        DECLARE @Apostrophe CHAR= CHAR(39);
        DECLARE @LeftParenthesis CHAR= CHAR(40);
        DECLARE @RightParenthesis CHAR= CHAR(41);
        DECLARE @Comma CHAR= CHAR(44);
        DECLARE @ErrorNumber NVARCHAR(4000);
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @TableName NVARCHAR(50);
        DECLARE @ColumnValue NVARCHAR(50);
        DECLARE @ColumnId NVARCHAR(50);
        DECLARE @WhereClause NVARCHAR(4000);
        DECLARE @SQLQuery NVARCHAR(4000);
        DECLARE @ResultSet NVARCHAR(4000)= NULL;
        --DECLARE @ResultValue NVARCHAR(4000)= NULL;
        DECLARE @ErrorText NVARCHAR(4000)= NULL;
        DECLARE @ErrorCode INT= NULL;
        DECLARE @NORecords INT= NULL;
        SELECT @TableName = TableName, 
               @ColumnId = ColumnId, 
               @ColumnValue = ColumnValue
        FROM dbo.STPParameters
        WHERE ParameterCode = @ParameterCode;
        SET @SQLQuery = N' SELECT @ResultValueInside=' + CONVERT(NVARCHAR(50), ISNULL(@ColumnValue, '')) + ' ' + ' FROM ' + @TableName + ' WHERE ' + @ColumnId + ' = ' + CONVERT(NVARCHAR(50), ISNULL(@ParameterValueId, '')) + '';
        BEGIN TRY
            EXECUTE @ResultSet = sys.sp_executesql 
                    @SQLQuery, 
                    N'@ResultValueInside NVARCHAR(4000) OUTPUT', 
                    @ResultValueInside = @ResultValue OUTPUT;
            SELECT @ResultValue;
            SELECT @NORecords = @@ROWCOUNT;
        END TRY
        BEGIN CATCH
            SELECT @ErrorNumber = CONVERT(NVARCHAR(MAX), ERROR_NUMBER()), 
                   @ErrorMessage = ERROR_MESSAGE();
        END CATCH;
        IF @NORecords = 0
            BEGIN
                SET @ErrorText = 'No data fetched';
                SET @ErrorCode = 0;
        END;
            ELSE
            IF @NORecords > 0
                BEGIN
                    SET @ErrorText = 'Success';
                    SET @ErrorCode = 1;
            END;
                ELSE
                BEGIN
                    SET @ErrorText = 'Error number: ' + @ErrorNumber + '. Error message: ' + @ErrorMessage + '';
                    SET @ErrorCode = 2;
            END;
        --PRINT 'ErrorCode: ' + CONVERT(NVARCHAR(30), @ErrorCode);
        --PRINT 'ErrorText: ' + @ErrorText;
        --PRINT 'SQLQuery: ' + @SQLQuery;
        --PRINT 'ResultSet: ' + @ResultSet;
        --PRINT 'ResultValue: ' + @ResultValue;
    END;