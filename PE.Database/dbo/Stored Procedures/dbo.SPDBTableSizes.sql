﻿CREATE   PROCEDURE [dbo].[SPDBTableSizes]
AS
/*
	exec [dbo].[SPDBTableSizes]
*/
    BEGIN
        SET NOCOUNT ON;
        SELECT s.name AS SchemaName, 
               t.name AS TableName, 
               p.rows AS Rows, 
               CAST(ROUND(((SUM(a.total_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS TotalSpaceMB, 
               CAST(ROUND(((SUM(a.used_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS UsedSpaceMB, 
               CAST(ROUND(((SUM(a.total_pages) - SUM(a.used_pages)) * 8) / 1024.00, 2) AS NUMERIC(36, 2)) AS UnusedSpaceMB
        FROM sys.tables t
             INNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id
             INNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID
                                            AND i.index_id = p.index_id
             INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
             LEFT OUTER JOIN sys.schemas s ON t.schema_id = s.schema_id
        --WHERE t.NAME NOT LIKE 'dt%'
        --    AND t.is_ms_shipped = 0
        --  AND i.OBJECT_ID > 255
        GROUP BY t.name, 
                 s.name, 
                 p.rows
        ORDER BY TotalSpaceMB DESC, 
                 t.Name;
    END;