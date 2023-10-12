
CREATE   VIEW [dw].[DimYear] AS 
/*
	SELECT * FROM dw.DimYear
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY [Year]), 0) AS DimYearRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimYearIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST([Year] AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimYearHash, 
          ISNULL([Year], 0) AS DimYearKey, 
          [Year] AS [Year]
   FROM
   (
       SELECT DISTINCT 
              [Year]
       FROM EVTDaysOfYear
   ) AS DOY;