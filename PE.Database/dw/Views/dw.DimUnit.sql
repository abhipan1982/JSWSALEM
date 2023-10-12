



CREATE         VIEW [dw].[DimUnit] AS 
/*
SELECT * FROM dw.DimUnit
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY U.UnitId), 0) AS DimUnitRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimUnitIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(U.UnitId AS NVARCHAR), ';') + 
			COALESCE(CAST(U.UnitCategoryId AS NVARCHAR), ';') + 
			COALESCE(CAST(UC.CategoryName AS NVARCHAR), ';') + 
			COALESCE(CAST(U.UnitSymbol AS NVARCHAR), ';') + 
			COALESCE(CAST(U.[Name] AS NVARCHAR), ';') + 
			COALESCE(CAST(U.Factor AS NVARCHAR), ';') + 
			COALESCE(CAST(U.[Shift] AS NVARCHAR), ';') + 
			COALESCE(CAST(U.SIUnitId AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimUnitHash, 
          U.UnitId AS DimUnitKey, 
		  U.UnitCategoryId AS DimUnitCategoryKey,
          ISNULL(CAST(CASE
                          WHEN U.SIUnitId IS NULL
                          THEN 1
                          ELSE 0
                      END AS BIT), 0) AS UnitIsSI, 
          UC.CategoryName AS UnitCategory, 
          U.UnitSymbol AS UnitSymbol, 
          U.[Name] AS UnitName, 
          U.Factor AS UnitFactor, 
          U.[Shift] AS UnitShift, 
          ISNULL(USI.UnitSymbol, U.UnitSymbol) AS UnitSISymbol, 
          ISNULL(USI.[Name], U.[Name]) AS UnitSIName
   FROM smf.UnitOfMeasure AS U
        INNER JOIN smf.UnitOfMeasureCategory AS UC ON U.UnitCategoryId = UC.UnitCategoryId
        LEFT OUTER JOIN smf.UnitOfMeasure AS USI ON U.SIUnitId = USI.UnitId;