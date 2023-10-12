CREATE    PROCEDURE [dbo].[SPLogProcsExecute]
AS
    BEGIN

        INSERT INTO DBLogProcsExecute
        (ProcName, 
         FirstExecutionTime
        )
               SELECT T.ProcName, 
               (
                   SELECT TOP (1) last_execution_time
                   FROM sys.dm_exec_procedure_stats
                   WHERE [object_id] = T.[object_id]
               ) AS last_execution_time
               FROM
               (
                   SELECT p.NAME ProcName, 
                          p.[object_id]
                   FROM sys.procedures AS p
                        INNER JOIN sys.dm_exec_procedure_stats AS s ON p.[object_id] = s.[object_id]
                   WHERE NOT EXISTS
                   (
                       SELECT 1
                       FROM DBLogProcsExecute pte
                       WHERE pte.ProcName = p.name
                   )
                         AND last_execution_time IS NOT NULL
                   GROUP BY p.name, 
                            p.[object_id]
               ) T;

        --If they do exist in this table, update the last execution time.
        UPDATE DBLogProcsExecute
          SET 
              lastExecutionTime = s.last_execution_time
        FROM DBLogProcsExecute pte
             INNER JOIN sys.procedures AS p ON pte.procName = p.name
             LEFT OUTER JOIN sys.dm_exec_procedure_stats AS s ON p.[object_id] = s.[object_id]
        WHERE s.last_execution_time IS NOT NULL;
    END;