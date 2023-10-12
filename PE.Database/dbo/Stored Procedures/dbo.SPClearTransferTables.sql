CREATE PROCEDURE [dbo].[SPClearTransferTables]
AS

/*
INSERT INTO smf.Parameters (Description,Name,ValueInt,EnumParameterValueType,ParameterGroupId) VALUES
('The number of month we keep data in L3L2/L2L3 Transfer Table','TransferTableKeptInMonths',1,1,5)
EXEC [dbo].[SPClearTransferTables]
SELECT * FROM smf.Alarms ORDER BY 1 DESC
*/

    BEGIN
        DECLARE @LogValue NVARCHAR(300);
        DECLARE @TransferTableKeptInMonths INT;
        SET @TransferTableKeptInMonths =
        (
            SELECT ValueInt
            FROM [smf].[Parameters]
            WHERE [Name] = 'TransferTableKeptInMonths'
        );
        IF @TransferTableKeptInMonths > 0
            BEGIN TRY
                -- Delete from TransferTable
                DELETE FROM [xfr].[L3L2WorkOrderDefinition]
                WHERE CreatedTs <= DATEADD(MONTH, -1 * @TransferTableKeptInMonths, GETDATE());
                -- Log operation
                SET @LogValue = CAST(ISNULL(@@ROWCOUNT, 0) AS NVARCHAR(10)) + ' rows from TransferTable, older than ' + CAST(@TransferTableKeptInMonths AS VARCHAR) + ' months have been deleted. ';
                EXEC dbo.SPLogDB 
                     @LogValue;
            END TRY
            BEGIN CATCH
                EXEC dbo.SPLogDB 
                     @LogValue;
            END CATCH;
    END;