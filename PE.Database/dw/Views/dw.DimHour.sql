

CREATE     VIEW [dw].[DimHour] AS 
/*
	SELECT * FROM dw.DimHour
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
        AS (SELECT(D1.i + 10 * D2.i) + 100 * D3.i AS Expr1
            FROM Digits AS D1
                 CROSS JOIN Digits AS D2
                 CROSS JOIN Digits AS D3)
        SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
               GETDATE() AS SourceTime, 
               ISNULL(ROW_NUMBER() OVER(
               ORDER BY i), 0) AS DimHourRow, 
               ISNULL(CAST(0 AS BIT), 0) AS DimHourIsDeleted, 
			   CAST(HASHBYTES('MD5', 
				COALESCE(CAST(i AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimHourHash, 
               ISNULL(CONVERT(SMALLINT, ROW_NUMBER() OVER(
               ORDER BY i)), 0) DimHourKey, 
               ISNULL(DATEPART(hh, DateVal), 0) AS Hour24, 
               ISNULL(DATEPART(hh, DateVal) % 12 + CASE
                                                       WHEN DATEPART(hh, DateVal) % 12 = 0
                                                       THEN 12
                                                       ELSE 0
                                                   END, 0) AS Hour12,
               CASE
                   WHEN DATEPART(hh, DateVal) BETWEEN 0 AND 11
                   THEN 'AM'
                   ELSE 'PM'
               END AS HourAmPm, 
               ISNULL(RIGHT('0' + CAST(DATEPART(hh, DateVal) AS VARCHAR(2)), 2), 0) AS HourCode, 
               ISNULL(CONVERT(TIME(0), DateVal), '00:00:00') AS HourTime
        FROM
        (
            SELECT DATEADD(HOUR, i, '20000101') AS DateVal, 
                   i
            FROM Sequence AS Sequence
            WHERE(i BETWEEN 0 AND 23)
        ) AS DailyHours;