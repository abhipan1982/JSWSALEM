CREATE PROCEDURE [dbo].[SPUpdateShiftCalendar] @Days dbo.Date_ShiftLayoutId_List READONLY
AS

/*
	DECLARE @Days dbo.Date_ShiftLayoutId_List;
		INSERT INTO @Days
        VALUES
        ('2023-02-02', 2),
		('2023-02-04', 2);
	EXEC dbo.SPUpdateShiftCalendar @Days;
	SELECT * FROM dbo.EVTShiftCalendar WHERE FKDaysOfYearId IN (SELECT DaysOfYearId FROM dbo.EVTDaysOfYear WHERE DateDay IN (SELECT DateDay FROM @Days))
*/

    BEGIN
        SET NOCOUNT ON;
        DECLARE @DateDay DATE;
        DECLARE @ShiftLayoutId BIGINT;
        DECLARE @DayOfYearId BIGINT;
        DECLARE @CrewId BIGINT;
        DECLARE @ShiftDefinitionId BIGINT;
        DECLARE @DefaultStartTime DATETIME;
        DECLARE @DefaultEndTime DATETIME;
        DECLARE @ShiftEndsNextDay BIT;
        DECLARE @DaysCursor CURSOR, @ShiftsCursor CURSOR;
        DECLARE @DaysCursorStatus INT, @ShiftsCursorStatus INT;

        -- Days Loop
        SET @DaysCursor = CURSOR STATIC LOCAL
        FOR SELECT D.DateDay, 
                   D.ShiftLayoutId, 
                   DOY.DaysOfYearId
            FROM @Days D
                 INNER JOIN dbo.EVTDaysOfYear DOY ON D.DateDay = DOY.DateDay;
        OPEN @DaysCursor;
        FETCH NEXT FROM @DaysCursor INTO @DateDay, @ShiftLayoutId, @DayOfYearId;
        SET @DaysCursorStatus = @@FETCH_STATUS;
        WHILE @DaysCursorStatus = 0
            BEGIN
                IF @DateDay > GETDATE()
                    BEGIN
                        -- Update LayoutId in calendar
                        UPDATE dbo.EVTDaysOfYear
                          SET 
                              FKShiftLayoutId = @ShiftLayoutId
                        WHERE DaysOfYearId = @DayOfYearId;

                        -- Delete existing shifts at that day
                        DELETE FROM dbo.EVTShiftCalendar
                        WHERE FKDaysOfYearId = @DayOfYearId;

                        -- Last CrewId before selected day
                        SELECT @CrewId = FKCrewId
                        FROM dbo.EVTShiftCalendar
                        WHERE PlannedStartTime =
                        (
                            SELECT MAX(PlannedStartTime)
                            FROM dbo.EVTShiftCalendar
                            WHERE CAST(PlannedStartTime AS DATE) <= @DateDay
                        );

                        -- Shift Loop 
                        SET @ShiftsCursor = CURSOR STATIC LOCAL
                        FOR SELECT ShiftDefinitionId, 
                                   DefaultStartTime, 
                                   DefaultEndTime, 
                                   ShiftEndsNextDay
                            FROM dbo.EVTShiftDefinitions
                            WHERE FKShiftLayoutId = @ShiftLayoutId;
                        OPEN @ShiftsCursor;
                        FETCH NEXT FROM @ShiftsCursor INTO @ShiftDefinitionId, @DefaultStartTime, @DefaultEndTime, @ShiftEndsNextDay;
                        SET @ShiftsCursorStatus = @@FETCH_STATUS;
                        WHILE @ShiftsCursorStatus = 0
                            BEGIN

                                -- Next CrewId
                                SELECT @CrewId = NextCrewId
                                FROM EVTCrews
                                WHERE CrewId = @CrewId;

                                -- Insert
                                INSERT INTO dbo.EVTShiftCalendar
                                (FKDaysOfYearId, 
                                 FKShiftDefinitionId, 
                                 FKCrewId, 
                                 PlannedStartTime, 
                                 PlannedEndTime
                                )
                                VALUES
                                (@DayOfYearId, 
                                 @ShiftDefinitionId, 
                                 @CrewId, 
                                 CAST(@DateDay AS DATETIME) + CAST(@DefaultStartTime AS DATETIME),
                                 CASE
                                     WHEN @ShiftEndsNextDay = 1
                                     THEN CAST(DATEADD(DAY, 1, @DateDay) AS DATETIME)
                                     ELSE CAST(@DateDay AS DATETIME)
                                 END + CAST(@DefaultEndTime AS DATETIME)
                                );
                                FETCH NEXT FROM @ShiftsCursor INTO @ShiftDefinitionId, @DefaultStartTime, @DefaultEndTime, @ShiftEndsNextDay;
                                SET @ShiftsCursorStatus = @@FETCH_STATUS;
                            END;
                        CLOSE @ShiftsCursor;
                        DEALLOCATE @ShiftsCursor;
                    END;
                FETCH NEXT FROM @DaysCursor INTO @DateDay, @ShiftLayoutId, @DayOfYearId;
                SET @DaysCursorStatus = @@FETCH_STATUS;
            END;
        CLOSE @DaysCursor;
        DEALLOCATE @DaysCursor;
    END;