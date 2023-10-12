CREATE   PROCEDURE [dbo].[SPKPICorrectivePreventiveMaintenance] @TimeFrom DATETIME = NULL, 
                                                                       @TimeTo   DATETIME = NULL, 
                                                                       @KPIValue FLOAT    = NULL OUTPUT
AS

/*
EXECUTE dbo.[SPKPICorrectivePreventiveMaintenance] @TimeFrom = '2023-01-20 00:00:00.000', @TimeTo='2023-01-21 00:00:00.000'
select * from dw.FactEvent 
select * from evtEventCatalogue
where eventstart between '2022-02-03 00:00:00.000' and '2022-02-04 00:00:00.000'
or eventend between '2022-02-03 00:00:00.000' and '2022-02-04 00:00:00.000'
order by EventStart
delete from evtevents where eventid in (24256,24261)
select * from dw.FactMaterial
select * from smf.unitofmeasure

*/

    BEGIN TRY
        DECLARE @EventCatalogueCodePlanned nvarchar(10) = 'MT1';
		DECLARE @EventCatalogueCodeNotPlanned nvarchar(10) = 'MT2';
        IF @TimeTo IS NULL
            SET @TimeTo = GETDATE(); -- '2022-02-16 12:00:00.000'; --
        IF @TimeFrom IS NULL
            SELECT @TimeFrom = ShiftStartTime
            FROM dw.DimShift
            WHERE @TimeTo BETWEEN ShiftStartTime AND ShiftEndTime;
        WITH EventsBetween
             AS (SELECT SUM(CASE
                                WHEN EventCatalogueCode = @EventCatalogueCodePlanned
                                THEN DATEDIFF(SECOND, EventStart, EventEnd)
                                ELSE 0
                            END) AS EventPlanned, 
                        SUM(CASE
                                WHEN EventCatalogueCode = @EventCatalogueCodeNotPlanned
                                THEN DATEDIFF(SECOND, EventStart, EventEnd)
                                ELSE 0
                            END) AS EventNotPlanned
                 FROM dw.FactEvent
                 WHERE EventIsDelay = 1
                       AND EventStart >= @TimeFrom
                       AND EventStart < @TimeTo
                       AND EventEnd >= @TimeFrom
                       AND EventEnd < @TimeTo),
             EventsEndBetween
             AS (SELECT SUM(CASE
                                WHEN EventCatalogueCode = @EventCatalogueCodePlanned
                                THEN DATEDIFF(SECOND, EventStart, EventEnd)
                                ELSE 0
                            END) AS EventPlanned, 
                        SUM(CASE
                                WHEN EventCatalogueCode = @EventCatalogueCodeNotPlanned
                                THEN DATEDIFF(SECOND, EventStart, EventEnd)
                                ELSE 0
                            END) AS EventNotPlanned
                 FROM dw.FactEvent
                 WHERE EventIsDelay = 1
                       AND EventStart < @TimeFrom
                       AND EventEnd >= @TimeFrom
                       AND EventEnd < @TimeTo),
             EventsStartBetween
             AS (SELECT SUM(CASE
                                WHEN EventCatalogueCode = @EventCatalogueCodePlanned
                                THEN DATEDIFF(SECOND, EventStart, EventEnd)
                                ELSE 0
                            END) AS EventPlanned, 
                        SUM(CASE
                                WHEN EventCatalogueCode = @EventCatalogueCodeNotPlanned
                                THEN DATEDIFF(SECOND, EventStart, EventEnd)
                                ELSE 0
                            END) AS EventNotPlanned
                 FROM dw.FactEvent
                 WHERE EventIsDelay = 1
                       AND EventStart >= @TimeFrom
                       AND EventStart < @TimeTo
                       AND EventEnd > @TimeTo),
             EventsOuterBetween
             AS (SELECT SUM(CASE
                                WHEN EventCatalogueCode = @EventCatalogueCodePlanned
                                THEN DATEDIFF(SECOND, EventStart, EventEnd)
                                ELSE 0
                            END) AS EventPlanned, 
                        SUM(CASE
                                WHEN EventCatalogueCode = @EventCatalogueCodeNotPlanned
                                THEN DATEDIFF(SECOND, EventStart, EventEnd)
                                ELSE 0
                            END) AS EventNotPlanned
                 FROM dw.FactEvent
                 WHERE EventIsDelay = 1
                       AND EventStart < @TimeFrom
                       AND EventEnd > @TimeTo)
             SELECT @KPIValue = CASE
                                    WHEN SUM(ISNULL(EventPlanned, 0)) != 0
                                    THEN SUM(ISNULL(EventNotPlanned, 0)) / SUM(ISNULL(EventPlanned, 0))
                                    ELSE 0
                                END
             FROM
             (
                 SELECT EventPlanned, 
                        EventNotPlanned
                 FROM EventsBetween
                 UNION ALL
                 SELECT EventPlanned, 
                        EventNotPlanned
                 FROM EventsEndBetween
                 UNION ALL
                 SELECT EventPlanned, 
                        EventNotPlanned
                 FROM EventsStartBetween
                 UNION ALL
                 SELECT EventPlanned, 
                        EventNotPlanned
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