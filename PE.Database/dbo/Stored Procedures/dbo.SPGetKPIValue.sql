CREATE PROCEDURE [dbo].[SPGetKPIValue]
(@KPICode          NVARCHAR(10), 
 @WorkOrderKey     BIGINT       = NULL, 
 @MaterialKey      BIGINT       = NULL, 
 @ShiftKey         BIGINT       = NULL, 
 @TimeFrom         DATETIME     = NULL, 
 @TimeTo           DATETIME     = NULL, 
 @KPIDefinitionKey BIGINT       = NULL OUTPUT, 
 @KPITime          DATETIME     = NULL OUTPUT, 
 @KPIValue         FLOAT        = NULL OUTPUT
)
AS

/*
EXEC dbo.SPGetKPIValue @KPICode='MY', @WorkOrderKey=177081;
EXEC dbo.SPGetKPIValue @KPICode='MY', @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
EXEC dbo.SPGetKPIValue @KPICode='QY', @WorkOrderKey=177081;
EXEC dbo.SPGetKPIValue @KPICode='QY', @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
EXEC dbo.SPGetKPIValue @KPICode='MTD', @WorkOrderKey=177081;
EXEC dbo.SPGetKPIValue @KPICode='MTD', @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
EXEC dbo.SPGetKPIValue @KPICode='PPV', @WorkOrderKey=177081;
EXEC dbo.SPGetKPIValue @KPICode='PPV', @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
EXEC dbo.SPGetKPIValue @KPICode='SP', @WorkOrderKey=177081;
EXEC dbo.SPGetKPIValue @KPICode='SP', @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
EXEC dbo.SPGetKPIValue @KPICode='TTP', @WorkOrderKey=177081;
EXEC dbo.SPGetKPIValue @KPICode='TTP', @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
EXEC dbo.SPGetKPIValue @KPICode='OEE', @WorkOrderKey=177081;
EXEC dbo.SPGetKPIValue @KPICode='OEE', @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
*/

     SET ANSI_WARNINGS OFF;
     DECLARE @KPIProcedure NVARCHAR(50);
     DECLARE @SQLQuery NVARCHAR(MAX);
     DECLARE @ResultSet NVARCHAR(MAX);
    BEGIN TRY
        SELECT @KPIProcedure = KPIProcedure, 
               @KPIDefinitionKey = KPIDefinitionId, 
               @KPITime = GETDATE()
        FROM dbo.PRFKPIDefinitions
        WHERE KPICode = @KPICode;
        IF @KPIProcedure IS NULL
            RAISERROR(N'There is no procedure for this KPI code.', 16, 127) WITH NOWAIT;
        IF @WorkOrderKey IS NOT NULL
            SELECT @TimeFrom = WorkOrderStart, 
                   @TimeTo = WorkOrderEnd
            FROM dw.DimWorkOrder
            WHERE DimWorkOrderKey = @WorkOrderKey;
        IF @ShiftKey IS NOT NULL
            SELECT @TimeFrom = ShiftStartTime, 
                   @TimeTo = ShiftEndTime
            FROM dw.DimShift
            WHERE DimShiftKey = @ShiftKey;
        IF @MaterialKey IS NOT NULL
            SELECT @TimeFrom = MaterialProductionStart, 
                   @TimeTo = MaterialProductionEnd
            FROM dw.DimMaterial
            WHERE DimMaterialKey = @MaterialKey;
        IF @TimeTo IS NULL
            SET @TimeTo = GETDATE();
        IF @TimeFrom IS NULL
            SELECT @TimeFrom = ShiftStartTime
            FROM dw.DimShift
            WHERE @TimeTo BETWEEN ShiftStartTime AND ShiftEndTime;
        SET @SQLQuery = 'exec ' + @KPIProcedure + ' @TimeFrom = ''' + CAST(@TimeFrom AS NVARCHAR(50)) + '''' + ', @TimeTo = ''' + CAST(@TimeTo AS NVARCHAR(50)) + '''';
        --PRINT @SQLQuery;
        PRINT @KPIDefinitionKey;
        PRINT @KPITime;
        EXECUTE @ResultSet = sp_executesql 
                @SQLQuery;
    END TRY
    BEGIN CATCH
    END CATCH;