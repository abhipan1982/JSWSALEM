CREATE PROCEDURE [dbo].[SPActualBypassWidget] @SetTime DATETIME = NULL, 
                                             @Value   SMALLINT = 0
AS

/*
	exec SPActualBypassWidget @SetTime = '2022-09-23 00:00:00', @Value = 0
*/

    BEGIN
        SET @SetTime = ISNULL(@SetTime, GETDATE());
        WITH Last0Switch
             AS (SELECT BypassName, 
                        [Value], 
                        MAX([Timestamp]) AS LastTimeStamp
                 FROM L1ARaisedBypasses
                 WHERE [Timestamp] <= @SetTime
                       AND [Value] = CASE @Value
                                         WHEN 0
                                         THEN 'FALSE'
                                         WHEN 1
                                         THEN 'TRUE'
                                         ELSE [Value]
                                     END
                 GROUP BY BypassName, 
                          [Value])
             SELECT RB.BypassName, 
                    RB.BypassTypeName, 
                    RB.OpcServerAddress, 
                    RB.OpcServerName, 
                    RB.[Value], 
                    L0S.LastTimeStamp
             FROM L1ARaisedBypasses RB
                  INNER JOIN Last0Switch L0S ON RB.BypassName = L0S.BypassName
             WHERE L0S.[LastTimeStamp] = RB.[Timestamp]
                   AND L0S.[Value] = RB.[Value]
             ORDER BY 1;
    END;