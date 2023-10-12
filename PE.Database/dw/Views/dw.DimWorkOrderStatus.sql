CREATE   VIEW [dw].[DimWorkOrderStatus]
AS

/*
	SELECT * FROM dw.DimWorkOrderStatus
*/

SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
       GETDATE() AS SourceTime, 
       ISNULL(ROW_NUMBER() OVER(
       ORDER BY EV.[Value]), 0) AS DimWorkOrderStatusRow, 
       ISNULL(CAST(0 AS BIT), 0) AS DimWorkOrderStatusIsDeleted, 
       CAST(HASHBYTES('MD5', COALESCE(CAST(EV.[Value] AS NVARCHAR), ';') + COALESCE(CAST(EV.Keyword AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimWorkOrderStatusHash, 
       EV.[Value] AS DimWorkOrderStatusKey, 
       EV.Keyword AS WorkOrderStatus
FROM smf.EnumNames AS EN
     INNER JOIN smf.EnumValues AS EV ON EN.EnumNameId = EV.FkEnumNameId
WHERE(EN.EnumName = 'WorkOrderStatus');