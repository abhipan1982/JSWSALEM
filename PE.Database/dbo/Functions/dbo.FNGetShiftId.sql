CREATE FUNCTION [dbo].[FNGetShiftId](@DateTimeStamp DATETIME)
RETURNS INT

/*
SELECT [dbo].[FNGetShiftId]('2021-02-14 13:59:59')
*/

AS
     BEGIN
         DECLARE @ShiftCalendarId BIGINT;
         SELECT @ShiftCalendarId = MIN(ShiftCalendarId)
         FROM EVTShiftCalendar
         WHERE @DateTimeStamp >= ISNULL(StartTime, PlannedStartTime)
               AND @DateTimeStamp < ISNULL(EndTime, PlannedEndTime);
         RETURN @ShiftCalendarId;
     END;