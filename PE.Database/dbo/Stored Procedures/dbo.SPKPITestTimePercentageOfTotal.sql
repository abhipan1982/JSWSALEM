CREATE PROCEDURE [dbo].[SPKPITestTimePercentageOfTotal] @TimeFrom DATETIME = NULL, 
                                                       @TimeTo   DATETIME = NULL, 
                                                       @KPIValue FLOAT    = NULL OUTPUT
AS

/*
EXECUTE dbo.SPKPITestTimePercentageOfTotal @TimeFrom = '2022-02-01 00:00:00.000', @TimeTo='2022-02-03 12:00:00.000'
select * from dw.FactWorkOrder
7224810
*/

    BEGIN TRY
        IF @TimeTo IS NULL
            SET @TimeTo = GETDATE(); --'2022-02-16 12:00:00.000'; --
        IF @TimeFrom IS NULL
            SELECT @TimeFrom = ShiftStartTime
            FROM dw.DimShift
            WHERE @TimeTo BETWEEN ShiftStartTime AND ShiftEndTime;
        --declare @TimeFrom datetime = '2022-02-01 00:00:00.000', @TimeTo datetime ='2022-02-03 12:00:00.000'
        SELECT --MaterialProductionDuration,MaterialIsTest

        @KPIValue = CASE
                        WHEN SUM(MaterialProductionDuration) != 0
                        THEN CAST(SUM(MaterialIsTest * MaterialProductionDuration) AS FLOAT) / SUM(MaterialProductionDuration)
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