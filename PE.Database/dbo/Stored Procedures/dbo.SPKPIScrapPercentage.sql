CREATE PROCEDURE [dbo].[SPKPIScrapPercentage] @TimeFrom DATETIME = NULL, 
                                             @TimeTo   DATETIME = NULL, 
                                             @KPIValue FLOAT    = NULL OUTPUT
AS

/*
EXECUTE dbo.SPKPIScrapPercentage @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
select * from dw.FactWorkOrder
select * from dw.FactMaterial
*/

    BEGIN TRY
        IF @TimeTo IS NULL
            SET @TimeTo = GETDATE(); -- '2022-02-16 12:00:00.000'; --
        IF @TimeFrom IS NULL
            SELECT @TimeFrom = ShiftStartTime
            FROM dw.DimShift
            WHERE @TimeTo BETWEEN ShiftStartTime AND ShiftEndTime;
        SELECT @KPIValue = CASE
                               WHEN SUM(MaterialWeight - MaterialRejectWeight) != 0
                               THEN SUM(MaterialScrapWeight) / SUM(MaterialWeight - MaterialRejectWeight)
                               ELSE 0
                           END
        FROM dw.FactMaterial
        WHERE MaterialProductionStart >= @TimeFrom
              AND MaterialProductionStart < @TimeTo
              AND MaterialProductionEnd >= @TimeFrom
              AND MaterialProductionEnd < @TimeTo;

        -- O U T P U T

        SET @KPIValue = ISNULL(@KPIValue, 0);
        PRINT @KPIValue;
    END TRY
    BEGIN CATCH
        EXEC dbo.SPGetErrorInfo;
    END CATCH;
        RETURN;