CREATE   PROCEDURE [dbo].[SPFillShiftCalendar]
AS
/*
	exec [dbo].[SPFillShiftCalendar]
	select * from [dbo].[EVTShiftCalendar]
	delete from [dbo].[EVTShiftCalendar]
*/
    BEGIN
        SET NOCOUNT ON;
        DECLARE @LastCrew AS BIGINT;
        DECLARE @NextCrew AS BIGINT;
        DECLARE @InsertDayId AS BIGINT;
        DECLARE @InsertDay AS DATE;
        DECLARE @DayCounter AS INT;
        DECLARE @ShiftCounter AS INT;
        DECLARE @StartDate AS DATE;
        DECLARE @LastStartTime AS DATETIME;
        DECLARE @DaysQty AS INT;
        DECLARE @ShiftQty AS INT;
        DECLARE @ShiftStart AS TIME;
        DECLARE @ShiftDef AS INT;
        DECLARE @PlannedStartTime AS TIME;
        DECLARE @PlannedEndTime AS TIME;
        DECLARE @ShiftEndsNextDay AS BIT;
        DECLARE @IsWeekend AS BIT;
        SELECT @StartDate = MAX(PlannedEndTime), 
               @LastStartTime = MAX(PlannedStartTime)
        FROM [dbo].[EVTShiftCalendar];
        --SET @StartDate = '2019-01-01 00:00:00'
        IF @StartDate IS NULL
            SET @StartDate = CAST(GETDATE() AS DATE);
        SET @ShiftQty =
        (
            SELECT COUNT([ShiftDefinitionId])
            FROM [dbo].[EVTShiftDefinitions] SD
			INNER JOIN [dbo].[EVTShiftLayouts] SL ON SD.FKShiftLayoutId = SL.ShiftLayoutId AND SL.IsDefaultLayout=1
        );
        --SET @ShiftQty = 3;
        SET @DaysQty =
        (
            SELECT ValueInt
            FROM [smf].[Parameters]
            WHERE [Name] = 'ShiftCalendarGenerate'
        );
        --SET @DaysQty = 7;
        SET @LastCrew =
        (
            SELECT FKCrewId
            FROM [dbo].[EVTShiftCalendar]
            WHERE PlannedStartTime = @LastStartTime
        );
        --SET @LastCrew = 4;
        SET @NextCrew =
        (
            SELECT NextCrewId
            FROM [dbo].[EVTCrews]
            WHERE CrewId = @LastCrew
                  AND NextCrewId IS NOT NULL
        );
		IF @NextCrew IS NULL
            SET @NextCrew =
            (
                SELECT MIN(CrewId)
                FROM dbo.EVTCrews
                WHERE OrderSeq IS NOT NULL
                      AND NextCrewId IS NOT NULL
            );
        SELECT @InsertDayId = DaysOfYearId, 
               @InsertDay = DateDay, 
               @IsWeekend = IsWeekend
        FROM [dbo].[EVTDaysOfYear]
        WHERE DateDay = @StartDate;
        --SELECT @InsertDayId, @NextCrew, @DaysQty
        /* DAYS LOOP */

        SET @DayCounter = 0;
        WHILE @DaysQty - @DayCounter > 0
            BEGIN
                SET @ShiftCounter = 0;
                SET @ShiftStart =
                (
                    SELECT MIN(DefaultStartTime)
                    FROM dbo.EVTShiftDefinitions SD
					INNER JOIN [dbo].[EVTShiftLayouts] SL ON SD.FKShiftLayoutId = SL.ShiftLayoutId AND SL.IsDefaultLayout=1
                ); --First Shift Starts
                /* SHIFT LOOP */

                WHILE @ShiftQty - @ShiftCounter > 0
                    BEGIN
                        SET @ShiftDef =
                        (
                            SELECT [ShiftDefinitionId]
                            FROM [dbo].[EVTShiftDefinitions] SD
							INNER JOIN [dbo].[EVTShiftLayouts] SL ON SD.FKShiftLayoutId = SL.ShiftLayoutId AND SL.IsDefaultLayout=1
                            WHERE [DefaultStartTime] = @ShiftStart
                        );
                        SELECT @PlannedStartTime = [DefaultStartTime], 
                               @PlannedEndTime = [DefaultEndTime], 
                               @ShiftEndsNextDay = [ShiftEndsNextDay]
                        FROM [dbo].[EVTShiftDefinitions] SD
						INNER JOIN [dbo].[EVTShiftLayouts] SL ON SD.FKShiftLayoutId = SL.ShiftLayoutId AND SL.IsDefaultLayout=1
                        WHERE [ShiftDefinitionId] = @ShiftDef;
                        IF NOT EXISTS
                        (
                            SELECT *
                            FROM [dbo].[EVTShiftCalendar]
                            WHERE((FKDaysOfYearId = @InsertDayId)
                                  AND (FKShiftDefinitionId = @ShiftDef))
                        )
                            BEGIN
                                INSERT INTO [dbo].[EVTShiftCalendar]
                                (FKShiftDefinitionId, 
                                 FKCrewId, 
                                 FKDaysOfYearId, 
                                 PlannedStartTime, 
                                 PlannedEndTime, 
                                 IsActive
                                )
                                VALUES
                                (@ShiftDef, 
                                 @NextCrew, 
                                 @InsertDayId, 
                                 CAST(@InsertDay AS DATETIME) + CAST(@PlannedStartTime AS DATETIME),
                                 CASE
                                     WHEN @ShiftEndsNextDay = 1
                                     THEN CAST(DATEADD(DAY, 1, @InsertDay) AS DATETIME)
                                     ELSE CAST(@InsertDay AS DATETIME)
                                 END + CAST(@PlannedEndTime AS DATETIME), 
								 CASE
                                     WHEN @ShiftQty = @ShiftCounter + 1
                                     THEN 0
                                     WHEN @IsWeekend = 1
                                     THEN 0
                                     ELSE 1
                                 END
                                );
                            END;
                        SET @ShiftCounter = @ShiftCounter + 1;
                        SET @ShiftStart = @PlannedEndTime;
                        SET @NextCrew =
                        (
                            SELECT NextCrewId
                            FROM EVTCrews
                            WHERE CrewId = @NextCrew
                        );
                    END;
                SET @DayCounter = @DayCounter + 1;
                SET @InsertDayId =
                (
                    SELECT [DaysOfYearId]
                    FROM [dbo].[EVTDaysOfYear]
                    WHERE [DateDay] = DATEADD(DAY, @DayCounter, @StartDate)
                );
                SELECT @InsertDay = [DateDay], 
                       @IsWeekend = IsWeekend
                FROM [dbo].[EVTDaysOfYear]
                WHERE [DaysOfYearId] = @InsertDayId;
            END;
        INSERT INTO DBLogs
        (LogType, 
         LogSource, 
         LogValue
        )
        VALUES
        ('SP', 
         '[SPFillShiftCalendar]', 
         'Day counter: ' + CAST(@DaysQty AS NVARCHAR(10)) + ' '
        );
    END;