
CREATE   VIEW [dw].[DimEventType] AS 
/*
	SELECT * FROM dw.DimEventType
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY ET.EventTypeId), 0) AS DimEventTypeRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimEventTypeIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(ET.EventTypeId AS NVARCHAR), ';') + 
			COALESCE(CAST(ET.FKParentEvenTypeId AS NVARCHAR), ';') + 
			COALESCE(CAST(ET.EventTypeCode AS NVARCHAR), ';') + 
			COALESCE(CAST(ET.EventTypeName AS NVARCHAR), ';') + 
			COALESCE(CAST(ET.EventTypeDescription AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimEventTypeHash, 
          ET.EventTypeId AS DimEventTypeKey, 
          ET.FKParentEvenTypeId AS DimEventTypeKeyParent,
          CASE
              WHEN ET.EventTypeId = 10
                   OR ET.FKParentEvenTypeId = 10
              THEN 1
              ELSE 0
          END AS EventIsDelay, 
          ET.EventTypeCode, 
          ET.EventTypeName, 
          ET.EventTypeDescription, 
          ETP.EventTypeCode AS EventTypeCodeParent, 
          ETP.EventTypeName AS EventTypeNameParent, 
          ETP.EventTypeDescription AS EventTypeDescriptionParent
   FROM dbo.EVTEventTypes ET
        LEFT JOIN DBO.EVTEventTypes ETP ON ET.FKParentEvenTypeId = ETP.EventTypeId;