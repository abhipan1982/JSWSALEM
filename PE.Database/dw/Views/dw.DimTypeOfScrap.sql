
CREATE   VIEW [dw].[DimTypeOfScrap] AS 
/*
	SELECT * FROM dw.DimTypeOfScrap
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY EV.[Value]), 0) AS DimTypeOfScrapRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimTypeOfScrapIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(EV.[Value] AS NVARCHAR), ';') + 
			COALESCE(CAST(EV.Keyword AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimTypeOfScrapHash, 
          EV.[Value] AS DimTypeOfScrapKey, 
          EV.Keyword AS TypeOfScrap
   FROM smf.EnumNames AS EN
        INNER JOIN smf.EnumValues AS EV ON EN.EnumNameId = EV.FkEnumNameId
   WHERE(EN.EnumName = 'TypeOfScrap');