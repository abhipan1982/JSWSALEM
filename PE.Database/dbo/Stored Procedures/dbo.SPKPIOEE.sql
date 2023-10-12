CREATE PROCEDURE [dbo].[SPKPIOEE] @TimeFrom DATETIME = NULL, 
                                 @TimeTo   DATETIME = NULL, 
                                 @KPIValue FLOAT    = NULL OUTPUT
AS

/*
EXEC [dbo].[SPKPIOEE] 
     @TimeFrom = '2022-02-01 00:00:00.000', 
     @TimeTo = '2022-02-03 12:00:00.000';
select * from hmi.v_delayOverview order by EventStartTs
delete from evtevents where eventid in (10301,10308,11162)
select * from trkrawmaterials rm
inner join prmproducts p on rm.fkproductid=p.productid
where rawmaterialcreatedts between '2021-10-14 14:00:00.000' and '2021-10-14 15:06:00.000'
177045
select * from trkrawmaterials where fkproductid in (
select productid from prmproducts where fkworkorderid=177045)
delete from trkrawmaterials where rawmaterialcreatedts between '2021-10-14 00:00:00.000' and '2021-10-14 14:00:00.000'
update trkrawmaterials set fkproductid=fkmaterialid-7159579 where fkmaterialid between 7213210 and 7213259
update trkrawmaterials set fkproductid=null where fkproductid in (53631,53632,53633,53634,53635,53636,53637,53638,53639,53640,53641,53642,53643,53644,53645,53646,53647,53648,53649,53650,53651,53652,53653,53654,53655,53656,53657,53658,53659,53660,53661,53662,53663,53664,53665,53666,53667,53668,53669,53670,53671,53672,53673,53674,53675,53676,53677,53678,53679,53680)
*/

     DECLARE @MaximumMillCapacity FLOAT;
     DECLARE @TimeFromStart BIGINT, @DelayTime BIGINT, @PlannedDelayTime BIGINT, @UnplannedDelayTime BIGINT;
     DECLARE @ActualProductionTime FLOAT, @PlannedProductionTime FLOAT;
     DECLARE @TotalMaterialWeight FLOAT, @TotalProductWeight FLOAT;
     DECLARE @Availability FLOAT, @Performance FLOAT, @Quality FLOAT;
    BEGIN
        SELECT @MaximumMillCapacity = UpperValueFloat * 0.0000001
        FROM smf.Limits
        WHERE Name = 'MillCapacity';
        IF @TimeTo IS NULL
            SET @TimeTo = GETDATE(); -- '2022-02-16 12:00:00.000'; --
        IF @TimeFrom IS NULL
            SELECT @TimeFrom = ShiftStartTime
            FROM dw.DimShift
            WHERE @TimeTo BETWEEN ShiftStartTime AND ShiftEndTime;
        SET @TimeFromStart = DATEDIFF(SECOND, @TimeFrom, @TimeTo);

        --SELECT @ShiftCalendarId = SC.ShiftCalendarId, 
        --     @TimeStart = SC.PlannedStartTime, 
        --      @TimeFromStart = DATEDIFF(SECOND, SC.PlannedStartTime, @TimeNow)
        --FROM EVTShiftCalendar SC
        --WHERE @TimeNow BETWEEN SC.PlannedStartTime AND SC.PlannedEndTime;

        SELECT @DelayTime = ISNULL(SUM(DelayTime), 0), 
               @UnplannedDelayTime = ISNULL(SUM(CASE
                                                    WHEN IsPlanned = 0
                                                    THEN DelayTime
                                                    ELSE 0
                                                END), 0), 
               @PlannedDelayTime = ISNULL(SUM(CASE
                                                  WHEN IsPlanned = 1
                                                  THEN DelayTime
                                                  ELSE 0
                                              END), 0)
        FROM
        (
            --DelayStart before TimeStart, DelayEnd after TimeStart but before TimeNow
            SELECT DATEDIFF(SECOND, @TimeFrom, EventEndTs) AS DelayTime, 
                   IsPlanned
            FROM EVTEvents E
                 INNER JOIN EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
            WHERE(ET.EventTypeId = 10
                  OR ET.FKParentEvenTypeId = 10)
                 AND EventStartTs <= @TimeFrom
                 AND EventEndTs >= @TimeFrom
                 AND EventEndTs <= @TimeTo
            UNION ALL
            --DelayStart before TimeStart, DelayEnd after TimeNow or is still active
            SELECT DATEDIFF(SECOND, @TimeFrom, @TimeTo) AS DelayTime, 
                   IsPlanned
            FROM EVTEvents E
                 INNER JOIN EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
            WHERE(ET.EventTypeId = 10
                  OR ET.FKParentEvenTypeId = 10)
                 AND EventStartTs <= @TimeFrom
                 AND ISNULL(EventEndTs, @TimeTo) >= @TimeTo
            UNION ALL
            -- DelayStart And DelayEnd are between TimeStart and TimeNow
            SELECT DATEDIFF(SECOND, EventStartTs, EventEndTs) AS DelayTime, 
                   IsPlanned
            FROM EVTEvents E
                 INNER JOIN EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
            WHERE(ET.EventTypeId = 10
                  OR ET.FKParentEvenTypeId = 10)
                 AND EventStartTs BETWEEN @TimeFrom AND @TimeTo
                 AND EventEndTs BETWEEN @TimeFrom AND @TimeTo
            UNION ALL
            -- DelayStart between TimeStart and TimeNow, DelayEnd after TimeNow or is still active
            SELECT DATEDIFF(SECOND, EventStartTs, @TimeTo) AS DelayTime, 
                   IsPlanned
            FROM EVTEvents E
                 INNER JOIN EVTEventTypes AS ET ON E.FKEventTypeId = ET.EventTypeId
            WHERE(ET.EventTypeId = 10
                  OR ET.FKParentEvenTypeId = 10)
                 AND EventStartTs BETWEEN @TimeFrom AND @TimeTo
                 AND ISNULL(EventEndTs, @TimeTo) >= @TimeTo
        ) AS Delays;
        -- Products & Materials
        SELECT @TotalMaterialWeight = ISNULL(SUM(MaterialWeight), 0), 
               @TotalProductWeight = ISNULL(SUM(ProductWeight), 0)
        FROM dw.FactMaterial
        WHERE MaterialProductionStart >= @TimeFrom
              AND MaterialProductionStart < @TimeTo
              AND MaterialProductionEnd >= @TimeFrom
              AND MaterialProductionEnd < @TimeTo;
        --
        SET @ActualProductionTime = @TimeFromStart - @PlannedDelayTime - @UnplannedDelayTime;
        SET @PlannedProductionTime = @TimeFromStart - @PlannedDelayTime;
        SET @Availability = @ActualProductionTime / @PlannedProductionTime;
        SET @Performance = @TotalMaterialWeight / (@MaximumMillCapacity * @ActualProductionTime);
        SET @Quality = CASE
                           WHEN @TotalMaterialWeight = 0
                           THEN 0
                           ELSE @TotalProductWeight / @TotalMaterialWeight
                       END;
        SET @KPIValue = @Availability * @Performance * @Quality;
        PRINT @KPIValue;
        -- PRINT 'TimeStart: ' + CONVERT(VARCHAR, @TimeStart);
        -- PRINT 'TimeNow: ' + CONVERT(VARCHAR, @TimeNow);
        PRINT 'TimeFromStart: ' + CONVERT(VARCHAR, @TimeFromStart);
        PRINT 'Planned Production Time: ' + CONVERT(VARCHAR, @PlannedProductionTime);
        PRINT 'DelayTime: ' + CONVERT(VARCHAR, @DelayTime);
        PRINT 'PlannedDelayTime: ' + CONVERT(VARCHAR, @PlannedDelayTime);
        PRINT 'UnplannedDelayTime: ' + CONVERT(VARCHAR, @UnplannedDelayTime);
        PRINT 'Actual Production Time: ' + CONVERT(VARCHAR, @ActualProductionTime);
        PRINT 'Total Material Weight: ' + CONVERT(VARCHAR, @TotalMaterialWeight);
        PRINT 'Total Product Weight: ' + CONVERT(VARCHAR, @TotalProductWeight);
        PRINT 'Availability: ' + CONVERT(VARCHAR, @Availability);
        PRINT 'Performance: ' + CONVERT(VARCHAR, @Performance);
        PRINT 'Quality: ' + CONVERT(VARCHAR, @Quality);
        PRINT 'OEE: ' + CONVERT(VARCHAR, @KPIValue);
    END;