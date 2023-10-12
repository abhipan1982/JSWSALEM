
CREATE   VIEW [dw].[DimDate] AS 
/*
	SELECT * FROM dw.DimDate
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY DOY.DaysOfYearId), 0) AS DimDateRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimDateIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(DOY.DaysOfYearId AS NVARCHAR), ';') + 
			COALESCE(CAST(DOY.DateDay AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimDateHash, 
          ISNULL(CONVERT(INT, CONVERT(VARCHAR(8), DOY.DateDay, 112)), 0) AS DimDateKey, 
          DOY.DaysOfYearId AS DimCalendarKey, 
          ISNULL(DATEPART(YEAR, DOY.DateDay), 0) AS DimYearKey, 
          ISNULL(CONVERT(INT,
                         CASE
                             WHEN DATEPART(QUARTER, DOY.DateDay) <          = (2)
                             THEN '1'
                             ELSE '2'
                         END), 0) AS DateHalfOfYear, 
          ISNULL(DATEPART(QUARTER, DOY.DateDay), 0) AS DateQuarter, 
          ISNULL(DATEPART(MONTH, DOY.DateDay), 0) AS DateMonth, 
          ISNULL(DATEPART(WEEK, DOY.DateDay), 0) AS DateWeek, 
          ISNULL(DATEPART(ISO_WEEK, DOY.DateDay), 0) AS DateWeekISO, 
          ISNULL(DATEPART(DAYOFYEAR, DOY.DateDay), 0) AS DateDayOfYear, 
          ISNULL(DATEPART(DAY, DOY.DateDay), 0) AS DateDayOfMonth, 
          ISNULL(DATEPART(WEEKDAY, DOY.DateDay), 0) AS DateDayOfWeek, 
          ISNULL(CONVERT(BIT,
                         CASE
                             WHEN DATEPART(WEEKDAY, DOY.DateDay) IN(1, 7)
                             THEN 1
                             ELSE 0
                         END), 0) AS DateIsWeekend, 
          ISNULL(DATENAME(MONTH, DOY.DateDay), 0) AS DateMonthName, 
          ISNULL(CONVERT(VARCHAR(10), DATENAME(WEEKDAY, DOY.DateDay)), 0) AS DateWeekDayName, 
          ISNULL(CONVERT(DATETIME, DOY.DateDay), '1900-01-01') AS DateFullDateTime, 
          ISNULL(CONVERT(DATETIME2(3), DOY.DateDay), '1900-01-01') AS DateFullDateTime2, 
          ISNULL(CONVERT(DATE, DOY.DateDay), '1901-01-01') AS DateFullDate, 
          ISNULL(CONVERT(VARCHAR(10), DOY.DateDay, 102), 0) AS DateANSI, 
          ISNULL(CONVERT(VARCHAR(10), DOY.DateDay, 101), 0) AS DateUS, 
          ISNULL(CONVERT(VARCHAR(10), DOY.DateDay, 103), 0) AS DateUK, 
          ISNULL(CONVERT(VARCHAR(10), DOY.DateDay, 104), 0) AS DateDE, 
          ISNULL(CONVERT(VARCHAR(10), DOY.DateDay, 105), 0) AS DateIT, 
          ISNULL(CONVERT(VARCHAR(8), DOY.DateDay, 112), 0) AS DateISO, 
          ISNULL(CONVERT(DATE, DATEADD(YEAR, DATEDIFF(YEAR, 0, DOY.DateDay), 0)), '1901-01-01') AS DateFirstOfYear, 
          ISNULL(CONVERT(DATE, DATEADD(DD, -1, DATEADD(YY, DATEDIFF(YY, 0, DOY.DateDay) + 1, 0))), '1901-01-01') AS DateLastOfYear, 
          ISNULL(CONVERT(DATE, DATEADD(MONTH, DATEDIFF(MONTH, 0, DOY.DateDay), 0)), '1901-01-01') AS DateFirstOfMonth, 
          ISNULL(CONVERT(DATE, EOMONTH(DOY.DateDay)), '1901-01-01') AS DateLastOfMonth, 
          ISNULL(CONVERT(VARCHAR(7), FORMAT(DOY.DateDay, 'yyyy-MM')), 0) AS DateYearMonth, 
          ISNULL(CONVERT(VARCHAR(8), CONCAT(FORMAT(DOY.DateDay, 'yyyy'), '-W', DATEPART(WEEK, DOY.DateDay))), 0) AS DateYearWeek, 
          ISNULL(CONVERT(VARCHAR(7), CONCAT(FORMAT(DOY.DateDay, 'yyyy'), '-Q', DATEPART(QUARTER, DOY.DateDay))), 0) AS DateYearQuarter
   FROM dbo.EVTDaysOfYear AS DOY
        LEFT JOIN dbo.EVTDaysOfYear AS DOY2 ON 1 = 0;