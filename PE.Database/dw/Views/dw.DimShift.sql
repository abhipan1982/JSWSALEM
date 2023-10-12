



CREATE         VIEW [dw].[DimShift] AS 
/*
	SELECT * FROM dw.DimShift
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY SC.ShiftCalendarId), 0) AS DimShiftRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimShiftIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(SC.ShiftCalendarId AS NVARCHAR), ';') + 
			COALESCE(CAST(SC.FKDaysOfYearId AS NVARCHAR), ';') + 
			COALESCE(CAST(SC.FKCrewId AS NVARCHAR), ';') + 
			COALESCE(CAST(SC.FKShiftDefinitionId AS NVARCHAR), ';') + 
			COALESCE(CAST(SC.PlannedStartTime AS NVARCHAR), ';') + 
			COALESCE(CAST(SC.PlannedEndTime AS NVARCHAR), ';') + 
			COALESCE(CAST(SC.PlannedEndTime AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimShiftHash, 
          SC.ShiftCalendarId AS DimShiftKey, 
          SC.FKDaysOfYearId AS DimCalendarKey, 
          CONVERT(INT, CONVERT(VARCHAR(8), SC.PlannedStartTime, 112)) AS DimDateKey, 
          SC.FKCrewId AS DimCrewKey, 
          SC.FKShiftDefinitionId AS DimShiftDefinitionKey, 
          SD.ShiftCode, 
          CONCAT(CONVERT(VARCHAR(10), SC.PlannedStartTime, 120), ' ', SD.ShiftCode) AS ShiftDateWithCode, 
          SC.PlannedStartTime AS ShiftStartTime, 
          SC.PlannedEndTime AS ShiftEndTime, 
          DATEDIFF(HOUR, SC.PlannedStartTime, SC.PlannedEndTime) AS ShiftDurationH, 
          DATEDIFF(MINUTE, SC.PlannedStartTime, SC.PlannedEndTime) AS ShiftDurationM, 
          DATEDIFF(SECOND, SC.PlannedStartTime, SC.PlannedEndTime) AS ShiftDurationS, 
          SD.ShiftEndsNextDay, 
          C.CrewName, 
          C.CrewDescription AS CrewDescription
   FROM EVTShiftCalendar AS SC
        INNER JOIN EVTDaysOfYear AS DOY ON SC.FKDaysOfYearId = DOY.DaysOfYearId
        INNER JOIN EVTShiftDefinitions AS SD ON SC.FKShiftDefinitionId = SD.ShiftDefinitionId
        INNER JOIN EVTCrews AS C ON SC.FKCrewId = C.CrewId;