


CREATE       VIEW [dw].[DimCrew] AS 
/*
	SELECT * FROM dw.DimCrew;
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY C.CrewId), 0) AS DimCrewRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimCrewIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(C.CrewId AS NVARCHAR), ';') + 
			COALESCE(CAST(C.CrewName AS NVARCHAR), ';') + 
			COALESCE(CAST(C.CrewDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(C.LeaderName AS NVARCHAR), ';') + 
			COALESCE(CAST(C.DfltCrewSize AS NVARCHAR), ';') + 
			COALESCE(CAST(C.OrderSeq AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimCrewHash, 
          C.CrewId AS DimCrewKey, 
          C.CrewName, 
          C.CrewDescription AS CrewDescription, 
          C.LeaderName AS CrewLeaderName, 
          C.DfltCrewSize AS CrewDefaultSize, 
          C.OrderSeq AS CrewOrderSeq
   FROM EVTCrews AS C
        LEFT JOIN EVTCrews AS C2 ON 1 = 0;