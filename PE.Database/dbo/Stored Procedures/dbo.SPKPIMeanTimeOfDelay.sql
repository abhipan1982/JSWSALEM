CREATE   PROCEDURE [dbo].[SPKPIMeanTimeOfDelay] @TimeFrom DATETIME = NULL, 
                                                       @TimeTo   DATETIME = NULL, 
                                                       @KPIValue FLOAT    = NULL OUTPUT
AS

/*
EXECUTE dbo.SPKPIMeanTimeOfDelay @TimeFrom = '2022-02-02 00:55:00.000', @TimeTo='2022-02-02 07:00:00.000'
select * from dw.FactEvent 
where eventstart between '2022-02-02 00:55:00.000' and '2022-02-02 07:00:00.000'
or eventend between '2022-02-02 00:55:00.000' and '2022-02-02 07:00:00.000'
order by EventStart
delete from evtevents where eventid in (24256,24261)
select * from dw.FactMaterial
select * from smf.unitofmeasure

*/

    BEGIN TRY
        IF @TimeTo IS NULL
            SET @TimeTo = GETDATE(); -- '2022-02-16 12:00:00.000'; --
        IF @TimeFrom IS NULL
            SELECT @TimeFrom = ShiftStartTime
            FROM dw.DimShift
            WHERE @TimeTo BETWEEN ShiftStartTime AND ShiftEndTime;
        WITH EventsBetween
             AS (SELECT SUM(DATEDIFF(SECOND, EventStart, EventEnd)) AS EventDuration, 
                        COUNT(FactEventKey) AS EventCounter
                 FROM dw.FactEvent
                 WHERE EventIsDelay = 1
                       AND EventStart >= @TimeFrom
                       AND EventStart < @TimeTo
                       AND EventEnd >= @TimeFrom
                       AND EventEnd < @TimeTo),
             EventsEndBetween
             AS (SELECT SUM(DATEDIFF(SECOND, @TimeFrom, EventEnd)) AS EventDuration, 
                        COUNT(FactEventKey) AS EventCounter
                 FROM dw.FactEvent
                 WHERE EventIsDelay = 1
                       AND EventStart < @TimeFrom
                       AND EventEnd >= @TimeFrom
                       AND EventEnd < @TimeTo),
             EventsStartBetween
             AS (SELECT SUM(DATEDIFF(SECOND, EventStart, @TimeTo)) AS EventDuration, 
                        COUNT(FactEventKey) AS EventCounter
                 FROM dw.FactEvent
                 WHERE EventIsDelay = 1
                       AND EventStart >= @TimeFrom
                       AND EventStart < @TimeTo
                       AND EventEnd > @TimeTo),
             EventsOuterBetween
             AS (SELECT SUM(DATEDIFF(SECOND, @TimeFrom, @TimeTo)) AS EventDuration, 
                        COUNT(FactEventKey) AS EventCounter
                 FROM dw.FactEvent
                 WHERE EventIsDelay = 1
                       AND EventStart < @TimeFrom
                       AND EventEnd > @TimeTo)
             SELECT @KPIValue = CASE
                                    WHEN SUM(ISNULL(EventCounter, 0)) != 0
                                    THEN SUM(ISNULL(EventDuration, 0)) / SUM(ISNULL(EventCounter, 0))
                                    ELSE 0
                                END
             FROM
             (
                 SELECT EventDuration, 
                        EventCounter
                 FROM EventsBetween
                 UNION ALL
                 SELECT EventDuration, 
                        EventCounter
                 FROM EventsEndBetween
                 UNION ALL
                 SELECT EventDuration, 
                        EventCounter
                 FROM EventsStartBetween
                 UNION ALL
                 SELECT EventDuration, 
                        EventCounter
                 FROM EventsOuterBetween
             ) QRY;

        -- O U T P U T

        SET @KPIValue = ISNULL(@KPIValue, 0);
        PRINT @KPIValue;
    END TRY
    BEGIN CATCH
        EXEC dbo.SPGetErrorInfo;
    END CATCH;
        RETURN;