CREATE   PROCEDURE [dbo].[SPBypassReport]
(@DateFrom   DATETIME, 
 @DateTo     DATETIME, 
 @Value      SMALLINT       = 2, 
 @BypassName NVARCHAR(1000) = NULL
)
AS

/*
	exec dbo.SPBypassReport @DateFrom = '2022-09-10 12:00:00', @DateTo = '2022-09-30 16:00:00', @Value = 2
*/

    BEGIN
        --DECLARE @RaisedBypassIds TABLE
        --(RaisedBypassId BIGINT, 
        -- Previous       BIT
        --);
        WITH BypassesWithPrevious
             AS (SELECT BypassTypeName, 
                        BypassName, 
                        OpcServerAddress, 
                        OpcServerName, 
                        Value, 
                        Timestamp, 
                        LAG(Value) OVER(PARTITION BY BypassTypeName, 
                                                     BypassName, 
                                                     OpcServerAddress, 
                                                     OpcServerName
                        ORDER BY Timestamp) PreviousValue
                 FROM [dbo].[L1ARaisedBypasses]),
             BypassesOnlyChanges
             AS (SELECT BypassTypeName, 
                        BypassName, 
                        OpcServerAddress, 
                        OpcServerName, 
                        Value, 
                        PreviousValue, 
                        Timestamp
                 FROM BypassesWithPrevious
                 WHERE Value != PreviousValue
                       OR PreviousValue IS NULL),
             Bypasses
             AS (SELECT RB.RaisedBypassId, 
                        RB.BypassTypeName, 
                        RB.BypassName, 
                        RB.OpcServerAddress, 
                        RB.OpcServerName, 
                        RB.Value, 
                        RB.Timestamp
                 FROM [dbo].[L1ARaisedBypasses] RB
                      INNER JOIN BypassesOnlyChanges BF ON RB.BypassName = BF.BypassName
                                                           AND RB.OpcServerAddress = BF.OpcServerAddress
                                                           AND RB.Value = BF.Value
                                                           AND RB.Timestamp = BF.Timestamp)
             SELECT RaisedBypassId, 
                    BypassName, 
                    Timestamp, 
                    OpcServerAddress, 
                    OpcServerName, 
                    BypassTypeName, 
                    Value, 
                    0 AS Previous
             FROM Bypasses
             WHERE [Timestamp] >= @DateFrom
                   AND [Timestamp] <= @DateTo
                   AND [Value] = CASE @Value
                                     WHEN 0
                                     THEN 'FALSE'
                                     WHEN 1
                                     THEN 'TRUE'
                                     ELSE [Value]
                                 END
                   AND [BypassName] LIKE('%' + ISNULL(@BypassName, '') + '%')
             UNION ALL
             SELECT TOP 1 RaisedBypassId, 
                          BypassName, 
                          Timestamp, 
                          OpcServerAddress, 
                          OpcServerName, 
                          BypassTypeName, 
                          Value, 
                          1 AS Previous
             FROM Bypasses
             WHERE [Timestamp] <= @DateFrom
                   AND [Value] = CASE @Value
                                     WHEN 0
                                     THEN 'FALSE'
                                     WHEN 1
                                     THEN 'TRUE'
                                     ELSE [Value]
                                 END
                   AND [BypassName] LIKE('%' + ISNULL(@BypassName, '') + '%')
             ORDER BY [Timestamp] DESC;

/*
             INSERT INTO @RaisedBypassIds
                    SELECT RaisedBypassId, 
                           Previous
                    FROM
                    (
                        SELECT RaisedBypassId, 
                               0 AS Previous
                        FROM Bypasses
                        WHERE [Timestamp] >= @DateFrom
                              AND [Timestamp] <= @DateTo
                              --AND [Value] = @Value
                              AND [Value] = CASE @Value
                                                WHEN 0
                                                THEN 'FALSE'
                                                WHEN 1
                                                THEN 'TRUE'
                                                ELSE [Value]
                                            END
                              AND [BypassName] LIKE('%' + ISNULL(@BypassName, '') + '%')
                        UNION ALL
                        SELECT TOP 1 RaisedBypassId, 
                                     1 AS Previous
                        FROM Bypasses
                        WHERE [Timestamp] <= @DateFrom
                              --AND [Value] = @Value
                              AND [Value] = CASE @Value
                                                WHEN 0
                                                THEN 'FALSE'
                                                WHEN 1
                                                THEN 'TRUE'
                                                ELSE [Value]
                                            END
                              AND [BypassName] LIKE('%' + ISNULL(@BypassName, '') + '%')
                        ORDER BY [Timestamp] DESC
                    ) QRY;

        SELECT RB.[RaisedBypassId], 
               RB.[BypassName], 
               RB.[Timestamp], 
               RB.[OpcServerAddress], 
               RB.[OpcServerName], 
               RB.[BypassTypeName], 
               RB.[Value], 
               RBI.Previous
        FROM [dbo].[L1ARaisedBypasses] AS RB
             INNER JOIN @RaisedBypassIds AS RBI ON RB.RaisedBypassId = RBI.RaisedBypassId
        ORDER BY [Timestamp] DESC;
		*/

    END;