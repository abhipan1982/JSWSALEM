
CREATE   VIEW [dw].[DimUser] AS 
/*
	SELECT * FROM dw.DimUser
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY U.Id), 0) AS DimUserRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimUserIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(U.Id AS NVARCHAR), ';') + 
			COALESCE(CAST(U.UserName AS NVARCHAR), ';') + 
			COALESCE(CAST(U.FirstName AS NVARCHAR), ';') + 
			COALESCE(CAST(U.LastName AS NVARCHAR), ';') + 
			COALESCE(CAST(U.JobPosition AS NVARCHAR), ';') + 
			COALESCE(CAST(U.Email AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimUserHash, 
          U.Id AS DimUserKey, 
          U.UserName, 
          U.FirstName AS UserFirstName, 
          U.LastName AS UserLastName, 
          U.JobPosition AS UserJobPosition, 
		  CONVERT(NVARCHAR(100),CONCAT(U.FirstName,' ',U.LastName)) AS UserFullName,
          U.Email AS UserEmail
   FROM smf.Users AS u;