﻿

CREATE     VIEW [dw].[DimInspectionResult] AS 
/*
	SELECT * FROM dw.DimInspectionResult
	select * from hmi.v_enums
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY EV.[Value]), 0) AS DimInspectionResultRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimInspectionResultIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(EV.[Value] AS NVARCHAR), ';') + 
			COALESCE(CAST(EV.Keyword AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimInspectionResultHash, 
          EV.[Value] AS DimInspectionResultKey, 
          EV.Keyword AS InspectionResult
   FROM smf.EnumNames AS EN
        INNER JOIN smf.EnumValues AS EV ON EN.EnumNameId = EV.FkEnumNameId
   WHERE(EN.EnumName = 'InspectionResult');