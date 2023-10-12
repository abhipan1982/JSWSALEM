
CREATE     VIEW [hmi].[V_AS_Delays]
AS SELECT D.EventId DimDelayId, 
          FORMAT(D.EventStartTs, 'yyyy') AS DimYear, 
          FORMAT(D.EventStartTs, 'yyyy-MM') AS DimMonth, 
          CONCAT(FORMAT(D.EventStartTs, 'yyyy'), '-W', DATEPART(WEEK, D.EventStartTs)) AS DimWeek, 
          FORMAT(D.EventStartTs, 'yyyy-MM-dd') AS DimDate, 
          GS.ShiftCode AS DimShiftCode, 
          GS.ShiftKey AS DimShiftKey, 
          GS.CrewName AS DimCrewName, 
          DC.EventCatalogueCode AS DimDelayCatalogueCode, 
          DC.EventCatalogueName AS DimDelayCatalogueName, 
          DCC.EventCatalogueCategoryCode AS DimDelayCatalogueCategoryCode, 
          DCC.EventCatalogueCategoryName AS DimDelayCatalogueCategoryName, 
          D.IsPlanned AS DimIsPlanned, 
          D.EventStartTs, 
          D.EventEndTs, 
          24 * 60 * 60 * (CONVERT(FLOAT, ISNULL(D.EventEndTs, GETDATE())) - CONVERT(FLOAT, D.EventStartTs)) AS DelayDuration,
          CASE
              WHEN D.EventEndTs IS NULL
              THEN 1
              ELSE 0
          END IsOpen
   FROM EVTEvents D
        INNER JOIN EVTEventCatalogue DC ON D.FKEventCatalogueId = DC.EventCatalogueId
        INNER JOIN EVTEventCatalogueCategory DCC ON DC.FKEventCatalogueCategoryId = DCC.EventCatalogueCategoryId
        CROSS APPLY dbo.FNTGetShiftId(D.EventStartTs) GS
   WHERE D.FKEventTypeId = 1;