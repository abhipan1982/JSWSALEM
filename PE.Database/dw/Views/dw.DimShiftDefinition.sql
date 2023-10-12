
CREATE   VIEW [dw].[DimShiftDefinition] AS 
/*
	SELECT * FROM dw.DimShiftDefinition
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY SD.ShiftDefinitionId), 0) AS DimShiftDefinitionRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimShiftDefinitionIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(SD.ShiftDefinitionId AS NVARCHAR), ';') + 
			COALESCE(CAST(SD.NextShiftDefinitionId AS NVARCHAR), ';') + 
			COALESCE(CAST(SD.ShiftCode AS NVARCHAR), ';') + 
			COALESCE(CAST(SD.DefaultStartTime AS NVARCHAR), ';') + 
			COALESCE(CAST(SD.DefaultEndTime AS NVARCHAR), ';') + 
			COALESCE(CAST(SD.ShiftEndsNextDay AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimShiftDefinitionHash, 
          SD.ShiftDefinitionId AS DimShiftDefinitionKey, 
          SD.NextShiftDefinitionId AS DimShiftDefinitionKeyNext, 
          SD.ShiftCode, 
          SD.DefaultStartTime AS ShiftDefaultStartTime, 
          SD.DefaultEndTime AS ShiftDefaultEndTime, 
          SD.ShiftEndsNextDay
   FROM EVTShiftDefinitions AS SD
        LEFT JOIN EVTShiftDefinitions AS SD2 ON 1 = 0;