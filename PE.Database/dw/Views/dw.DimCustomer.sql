

CREATE     VIEW [dw].[DimCustomer] AS 
/*
	SELECT * FROM dw.DimCustomer
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY C.CustomerId), 0) AS DimCustomerRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimCustomerIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(C.CustomerId AS NVARCHAR), ';') + 
			COALESCE(CAST(C.CustomerCode AS NVARCHAR), ';') + 
			COALESCE(CAST(C.CustomerName AS NVARCHAR), ';') + 
			COALESCE(CAST(C.CustomerAddress AS NVARCHAR), ';') + 
			COALESCE(CAST(C.Email AS NVARCHAR), ';') + 
			COALESCE(CAST(C.Phone AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimCustomerHash, 
          C.CustomerId AS DimCustomerKey, 
          C.CustomerCode AS CustomerCode, 
          C.CustomerName AS CustomerName, 
          C.CustomerAddress AS CustomerAddress, 
          C.Email AS CustomerEmail, 
          C.Phone AS CustomerPhone
   FROM dbo.PRMCustomers AS C
        LEFT JOIN dbo.PRMCustomers AS C2 ON 1 = 0;