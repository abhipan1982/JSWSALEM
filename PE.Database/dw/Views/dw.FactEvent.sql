


CREATE         VIEW [dw].[FactEvent]
AS

/*
	All events and delays
	select * from [dw].[FactEvent]
	SELECT sys.fn_varbintohexstr(HashCode),* FROM dw.FactEvent
*/

SELECT ISNULL(CONVERT(NVARCHAR(50), DB_NAME()), 0) AS SourceName, 
       GETDATE() AS SourceTime, 
       ISNULL(CONVERT(BIT, 0), 0) AS FactEventIsDeleted, 
       ISNULL(ROW_NUMBER() OVER(
       ORDER BY E.EventId), 0) AS FactEventRow, 
       CAST(HASHBYTES('MD5', COALESCE(CAST(E.EventId AS NVARCHAR), ';') + COALESCE(CAST(E.FKEventTypeId AS NVARCHAR), ';') + COALESCE(CAST(E.FKEventCatalogueId AS NVARCHAR), ';') + COALESCE(CAST(E.FKEventCatalogueId AS NVARCHAR), ';') + COALESCE(CAST(E.FKShiftCalendarId AS NVARCHAR), ';') + COALESCE(CAST(E.FKWorkOrderId AS NVARCHAR), ';') + COALESCE(CAST(M.MaterialId AS NVARCHAR), ';') + COALESCE(CAST(DATEDIFF(SECOND, E.EventStartTs, ISNULL(E.EventEndTs, GETDATE())) AS NVARCHAR), ';') + COALESCE(CAST(E.EventStartTs AS NVARCHAR), ';') + COALESCE(CAST(E.EventEndTs AS NVARCHAR), ';')) AS VARBINARY(16)) AS FactEventHash, 
       E.EventId AS FactEventKey, 
       E.FKParentEventId AS FactEventKeyParent, 
       E.FKEventTypeId AS DimEventTypeKey, 
       ISNULL(ETP.EventTypeId, ET.EventTypeId) AS DimRootEventTypeKey, 
       E.FKEventCatalogueId AS DimEventCatalogueKey, 
       ISNULL(DATEPART(YEAR, E.EventStartTs), 0) AS DimYearKey, 
       ISNULL(CONVERT(INT, CONVERT(VARCHAR(8), E.EventStartTs, 112)), 0) AS DimDateKey, 
       E.FKShiftCalendarId AS DimShiftKey, 
       ISNULL(E.FKWorkOrderId, M.FKWorkOrderId) AS DimWorkOrderKey, 
       E.FKAssetId AS DimAssetKey, 
	   RM.RawMaterialId AS DimRawMaterialKey,
       M.MaterialId AS DimMaterialKey, 
       E.FKUserId AS DimUserKey, 
	   CONCAT(CONVERT(VARCHAR(10), CONVERT(DATE, E.EventStartTs)), ' - ', GS.ShiftCode, ' - ', GS.CrewName) AS EventDayShiftCrew, 

	   GS.ShiftCode AS EventShiftCode, 
       ISNULL(CONVERT(BIT,
                      CASE
                          WHEN ET.EventTypeId = 10
                               OR ET.FKParentEvenTypeId = 10
                          THEN 1
                          ELSE 0
                      END), 0) AS EventIsDelay, 
       ISNULL(E.IsPlanned, 0) AS EventIsPlanned, 
       ISNULL(CONVERT(BIT,
                      CASE
                          WHEN E.EventEndTs IS NULL
                          THEN 1
                          ELSE 0
                      END), 0) AS EventIsOpen, 
       ET.EventTypeCode, 
       ET.EventTypeName, 
       EC.EventCatalogueCode, 
       EC.EventCatalogueName, 
       ECC.EventCatalogueCategoryCode, 
       ECC.EventCatalogueCategoryName, 
	   RM.ScrapPercent,
       M.MaterialName, 
	   M.MaterialWeight,
	   P.ProductWeight,
       WO.WorkOrderName, 
	   A.AssetCode,
	   A.AssetName,
       ISNULL(ETP.EventTypeCode, ET.EventTypeCode) AS RootEventTypeCode, 
       ISNULL(ETP.EventTypeName, ET.EventTypeName) AS RootEventTypeName, 
       CAST(E.EventStartTs AS DATE) AS EventDate, 
       E.EventStartTs AS EventStart, 
       E.EventEndTs AS EventEnd, 
       EC.StdEventTime AS EventStdTime, 
       E.UserComment AS EventUserComment, 
       E.UserUpdatedTs AS EventUserUpdated, 
       ISNULL(DATEDIFF(SECOND, E.EventStartTs, ISNULL(E.EventEndTs, GETDATE())), 0) AS EventDuration, 
       CONCAT(CONVERT(NVARCHAR(10), FLOOR(CAST(ISNULL(E.EventEndTs, GETDATE()) - E.EventStartTs AS FLOAT))), CONVERT(NVARCHAR(10),
                                                                                                                                CASE
                                                                                                                                    WHEN FLOOR(CAST(ISNULL(E.EventEndTs, GETDATE()) - E.EventStartTs AS FLOAT)) = 1
                                                                                                                                    THEN ' day '
                                                                                                                                    ELSE ' days '
                                                                                                                                END), CONVERT(NVARCHAR(10), ISNULL(E.EventEndTs, GETDATE()) - E.EventStartTs, 8)) AS EventDurationFD
FROM dbo.EVTEvents AS E
     INNER JOIN dbo.EVTEventTypes AS ET
     LEFT JOIN dbo.EVTEventTypes AS ETP ON ET.FKParentEvenTypeId = ETP.EventTypeId ON E.FKEventTypeId = ET.EventTypeId
     LEFT JOIN dbo.EVTEventCatalogue AS EC
     INNER JOIN dbo.EVTEventCatalogueCategory AS ECC ON EC.FKEventCatalogueCategoryId = ECC.EventCatalogueCategoryId ON E.FKEventCatalogueId = EC.EventCatalogueId
     LEFT JOIN dbo.EVTEvents AS EP ON E.FKParentEventId = EP.EventId
     LEFT JOIN dbo.PRMWorkOrders AS WO ON E.FKWorkOrderId = WO.WorkOrderId
	 LEFT JOIN dbo.MVHAssets AS A ON E.FKAssetId = A.AssetId
     LEFT JOIN dbo.TRKRawMaterials AS RM
     INNER JOIN dbo.PRMMaterials AS M ON RM.FKMaterialId = M.MaterialId 
	 LEFT JOIN dbo.PRMProducts AS P ON RM.FKProductId = P.ProductId
	 ON E.FKRawMaterialId = RM.RawMaterialId
     OUTER APPLY dbo.FNTGetShiftId(E.EventStartTs) AS GS;