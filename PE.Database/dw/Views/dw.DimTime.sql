

CREATE     VIEW [dw].[DimTime] AS 
/*
	SELECT * FROM dw.DimTime;
*/
WITH Digits(i)
        AS (SELECT 1 AS i
            UNION ALL
            SELECT 2 AS i
            UNION ALL
            SELECT 3 AS i
            UNION ALL
            SELECT 4 AS i
            UNION ALL
            SELECT 5 AS i
            UNION ALL
            SELECT 6 AS i
            UNION ALL
            SELECT 7 AS i
            UNION ALL
            SELECT 8 AS i
            UNION ALL
            SELECT 9 AS i
            UNION ALL
            SELECT 0 AS i),
        Sequence(i)
        AS (SELECT(((D1.i + 10 * D2.i) + 100 * D3.i) + 1000 * D4.i) + 10000 * D5.i AS Expr1
            FROM Digits AS D1
                 CROSS JOIN Digits AS D2
                 CROSS JOIN Digits AS D3
                 CROSS JOIN Digits AS D4
                 CROSS JOIN Digits AS D5)
        SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
               GETDATE() AS SourceTime, 
               ISNULL(ROW_NUMBER() OVER(
               ORDER BY i), 0) AS DimTimeRow, 
               ISNULL(CAST(0 AS BIT), 0) AS DimTimeIsDeleted, 
			   CAST(HASHBYTES('MD5', 
				COALESCE(CAST(i AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimTimeHash, 
               ISNULL(CONVERT(BIGINT, ROW_NUMBER() OVER(
               ORDER BY i)), 0) DimTimeKey, 
               ISNULL(DATEPART(hh, DateVal), 0) AS TimeHour, 
               ISNULL(DATEPART(mi, DateVal), 0) AS TimeMinute, 
               ISNULL(DATEPART(ss, DateVal), 0) AS TimeSecond, 
               ISNULL(i / 30 / 60 % 2 + 1, 0) AS TimeHalfHour, 
               ISNULL(i / 15 / 60 % 4 + 1, 0) AS TimeQuarterHour, 
               ISNULL(i + 1, 0) AS TimeSecondOfDay, 
               ISNULL(i / 60 + 1, 0) AS TimeMinuteOfDay, 
               ISNULL(i / 30 / 60 + 1, 0) AS TimeHalfHourOfDay, 
               ISNULL(i / 15 / 60 + 1, 0) AS TimeQuarterHourOfDay, 
               ISNULL(RIGHT('0' + CAST(DATEPART(hh, DateVal) AS VARCHAR(2)), 2) + ':' + RIGHT('0' + CAST(DATEPART(mi, DateVal) AS VARCHAR(2)), 2) + ':' + RIGHT('0' + CAST(DATEPART(ss, DateVal) AS VARCHAR(2)), 2), 0) AS TimeString, 
               ISNULL(RIGHT('0' + CAST(DATEPART(hh, DateVal) % 12 + CASE
                                                                        WHEN DATEPART(hh, DateVal) % 12 = 0
                                                                        THEN 12
                                                                        ELSE 0
                                                                    END AS VARCHAR(2)), 2) + ':' + RIGHT('0' + CAST(DATEPART(mi, DateVal) AS VARCHAR(2)), 2) + ':' + RIGHT('0' + CAST(DATEPART(ss, DateVal) AS VARCHAR(2)), 2), 0) AS TimeString12, 
               ISNULL(DATEPART(hh, DateVal) % 12 + CASE
                                                       WHEN DATEPART(hh, DateVal) % 12 = 0
                                                       THEN 12
                                                       ELSE 0
                                                   END, 0) AS TimeHour12,
               CASE
                   WHEN DATEPART(hh, DateVal) BETWEEN 0 AND 11
                   THEN 'AM'
                   ELSE 'PM'
               END AS TimeAmPm, 
               ISNULL(RIGHT('0' + CAST(DATEPART(hh, DateVal) AS VARCHAR(2)), 2), 0) AS TimeHourCode, 
               ISNULL(RIGHT('0' + CAST(DATEPART(hh, DateVal) AS VARCHAR(2)), 2) + ':' + RIGHT('0' + CAST(DATEPART(mi, DateVal) AS VARCHAR(2)), 2), 0) AS TimeMinuteCode, 
               ISNULL(CONVERT(TIME(0), DateVal), '00:00:00') AS TimeTime
        FROM
        (
            SELECT DATEADD(SECOND, i, '20000101') AS DateVal, 
                   i
            FROM Sequence AS Sequence
            WHERE(i BETWEEN 0 AND 86399)
        ) AS DailySeconds;