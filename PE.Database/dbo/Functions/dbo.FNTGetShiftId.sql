CREATE FUNCTION [dbo].[FNTGetShiftId](@DateTimeStamp DATETIME)
RETURNS TABLE
AS
     RETURN
     SELECT ShiftCalendarId, 
            SD.ShiftCode, 
            C.CrewName, 
            CONCAT(DOY.DateDay, ' ', SD.ShiftCode) AS ShiftKey
     FROM EVTShiftCalendar AS S
          INNER JOIN dbo.EVTDaysOfYear AS DOY ON S.FKDaysOfYearId = DOY.DaysOfYearId
          INNER JOIN dbo.EVTShiftDefinitions AS SD ON S.FKShiftDefinitionId = SD.ShiftDefinitionId
          INNER JOIN dbo.EVTCrews AS C ON S.FKCrewId = C.CrewId
     WHERE @DateTimeStamp >= PlannedStartTime
           AND @DateTimeStamp < PlannedEndTime;