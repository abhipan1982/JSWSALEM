CREATE   VIEW [hmi].[V_ShiftCalendar]
AS SELECT ISNULL(ROW_NUMBER() OVER(
          ORDER BY SC.ShiftCalendarId), 0) AS OrderSeq, 
          SC.ShiftCalendarId, 
          SD.ShiftCode, 
          C.CrewName, 
          SC.StartTime, 
          SC.EndTime, 
          SC.IsActualShift, 
          SC.PlannedStartTime, 
          SC.PlannedEndTime, 
          SD.ShiftDefinitionId, 
          C.CrewId, 
          IsActive, 
          DATEDIFF(SECOND, SC.PlannedStartTime, ISNULL(SC.PlannedEndTime, GETDATE())) AS ShiftDuration, 
          IsActive * DATEDIFF(SECOND, SC.PlannedStartTime, ISNULL(SC.PlannedEndTime, GETDATE())) AS ActiveShiftDuration
   FROM EVTShiftCalendar SC
        INNER JOIN EVTShiftDefinitions SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
        INNER JOIN EVTCrews C ON SC.FKCrewId = C.CrewId;