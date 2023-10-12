CREATE TABLE [dbo].[EVTDaysOfYear] (
    [DaysOfYearId]     BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DateDay]          DATE           NOT NULL,
    [FKShiftLayoutId]  BIGINT         CONSTRAINT [DF_EVTDaysOfYear_FKShiftLayoutId] DEFAULT ((1)) NOT NULL,
    [Year]             AS             (datepart(year,[DateDay])),
    [Quarter]          AS             (datepart(quarter,[DateDay])),
    [Month]            AS             (datepart(month,[DateDay])),
    [Day]              AS             (datepart(day,[DateDay])),
    [WeekNo]           AS             (datepart(week,[DateDay])),
    [IsWeekend]        AS             (isnull(CONVERT([bit],case when datepart(weekday,[DateDay])=(7) OR datepart(weekday,[DateDay])=(1) then (1) else (0) end),(0))),
    [MonthName]        AS             (datename(month,[DateDay])),
    [WeekDayName]      AS             (datename(weekday,[DateDay])),
    [HalfOfYear]       AS             (case when datepart(quarter,[DateDay])<=(2) then (1) else (2) end),
    [DateANSI]         AS             (CONVERT([varchar](10),[DateDay],(102))),
    [DateUS]           AS             (CONVERT([varchar](10),[DateDay],(101))),
    [DateUK]           AS             (CONVERT([varchar](10),[DateDay],(103))),
    [DateDE]           AS             (CONVERT([varchar](10),[DateDay],(104))),
    [DateIT]           AS             (CONVERT([varchar](10),[DateDay],(105))),
    [DateISO]          AS             (CONVERT([varchar](8),[DateDay],(112))),
    [FirstOfMonth]     AS             (CONVERT([date],dateadd(month,datediff(month,(0),[DateDay]),(0)))),
    [LastOfMonth]      AS             (CONVERT([date],eomonth([DateDay]))),
    [FirstOfYear]      AS             (CONVERT([date],dateadd(year,datediff(year,(0),[DateDay]),(0)))),
    [LastOfYear]       AS             (CONVERT([date],dateadd(day,(-1),dateadd(year,datediff(year,(0),[DateDay])+(1),(0))))),
    [ISOWeekNumber]    AS             (datepart(iso_week,[DateDay])),
    [WeekNumber]       AS             (datepart(week,[DateDay])),
    [YearNumber]       AS             (datepart(year,[DateDay])),
    [MonthNumber]      AS             (datepart(month,[DateDay])),
    [DayYearNumber]    AS             (datepart(dayofyear,[DateDay])),
    [DayNumber]        AS             (datepart(day,[DateDay])),
    [WeekDayNumber]    AS             (datepart(weekday,[DateDay])),
    [AUDCreatedTs]     DATETIME       CONSTRAINT [DF_EVTDaysOfYear_AUDCreatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDLastUpdatedTs] DATETIME       CONSTRAINT [DF_EVTDaysOfYear_AUDLastUpdatedTs] DEFAULT (getdate()) NOT NULL,
    [AUDUpdatedBy]     NVARCHAR (255) CONSTRAINT [DF_EVTDaysOfYear_AUDUpdatedBy] DEFAULT (app_name()) NULL,
    [IsBeforeCommit]   BIT            CONSTRAINT [DF_EVTDaysOfYear_IsBeforeCommit] DEFAULT ((0)) NOT NULL,
    [IsDeleted]        BIT            CONSTRAINT [DF_EVTDaysOfYear_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DaysOfYear] PRIMARY KEY CLUSTERED ([DaysOfYearId] ASC),
    CONSTRAINT [FK_EVTDaysOfYear_EVTShiftLayouts] FOREIGN KEY ([FKShiftLayoutId]) REFERENCES [dbo].[EVTShiftLayouts] ([ShiftLayoutId])
);












GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_DateDay]
    ON [dbo].[EVTDaysOfYear]([DateDay] ASC);


GO

-- =============================================
-- Author:		Klakla
-- Create date: 2022
-- Description:	Audit Trigger
-- =============================================
CREATE   TRIGGER [dbo].[TR_EVTDaysOfYear_Audit] ON [dbo].[EVTDaysOfYear] AFTER INSERT, UPDATE, DELETE AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @RecordId BIGINT
	DECLARE @LogValue VARCHAR(100)
	SELECT @RecordId = INSERTED.DaysOfYearId FROM INSERTED;

    IF NOT EXISTS(SELECT 1 FROM INSERTED) 
	BEGIN
        -- DELETE ACTION
		SELECT @RecordId = DELETED.DaysOfYearId FROM DELETED;
		SET @LogValue = CONCAT('Deleted record from table: EVTDaysOfYear, Id: ',CAST(@RecordId AS VARCHAR),', by: ',APP_NAME());
        exec SPLogDB @LogValue;
	END;
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM DELETED)
            -- INSERT ACTION
            UPDATE [dbo].[EVTDaysOfYear] SET AUDUpdatedBy = APP_NAME() WHERE DaysOfYearId = @RecordId
        ELSE
            -- UPDATE ACTION
            UPDATE [dbo].[EVTDaysOfYear] SET AUDUpdatedBy = APP_NAME() , AUDLastUpdatedTs=getdate() WHERE DaysOfYearId = @RecordId
    END
END;