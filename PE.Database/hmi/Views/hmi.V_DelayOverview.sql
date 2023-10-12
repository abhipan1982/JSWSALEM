CREATE   VIEW [hmi].[V_DelayOverview]
AS

/*
select * from [hmi].[V_DelayOverview]
*/

SELECT ISNULL(ROW_NUMBER() OVER(
       ORDER BY EventStartTs DESC), 0) AS OrderSeq, 
       CONCAT(CONVERT(VARCHAR(10), CONVERT(DATE, D.EventStartTs)), ' - ', GS.ShiftCode, ' - ', GS.CrewName) AS DelayHeader, 
       D.EventId, 
       CONVERT(DATE, D.EventStartTs) AS DateDay, 
       D.FKShiftCalendarId AS ShiftCalendarId, 
       dbo.FNGetShiftId(D.EventStartTs) AS ShiftIdDelayStart, 
       dbo.FNGetShiftId(D.EventEndTs) AS ShiftIdDelayEnd, 
       GS.ShiftCode, 
       DC.EventCatalogueCode, 
       DC.EventCatalogueName, 
       DCC.EventCatalogueCategoryCode, 
       DCC.EventCatalogueCategoryName, 
       DC.StdEventTime, 
       D.EventStartTs, 
       D.EventEndTs, 
       DATEDIFF(SECOND, D.EventStartTs, ISNULL(D.EventEndTs, GETDATE())) AS DelayDuration, 
       CONCAT(CAST(FLOOR(CAST(ISNULL(D.EventEndTs, GETDATE()) - D.EventStartTs AS FLOAT)) AS VARCHAR),
                                                                                                    CASE
                                                                                                        WHEN FLOOR(CAST(ISNULL(D.EventEndTs, GETDATE()) - D.EventStartTs AS FLOAT)) = 1
                                                                                                        THEN ' day '
                                                                                                        ELSE ' days '
                                                                                                    END, CONVERT(VARCHAR, ISNULL(D.EventEndTs, GETDATE()) - D.EventStartTs, 8)) AS DelayDurationFD, 
       D.IsPlanned AS IsPlanned,
       CASE
           WHEN D.EventEndTs IS NULL
           THEN 1
           ELSE 0
       END AS IsOpen, 
       D.FKWorkOrderId AS WorkOrderId, 
       WO.WorkOrderName, 
       PC.ProductCatalogueName, 
       S.SteelgradeCode, 
       S.SteelgradeName, 
       D.FKRawMaterialId AS RawMaterialId, 
       RM.RawMaterialName, 
       D.FKAssetId AS AssetId, 
       A.AssetName, 
       D.FKUserId AS UserId, 
       U.UserName, 
       D.UserComment
FROM EVTEvents AS D
     INNER JOIN EVTEventCatalogue AS DC ON D.FKEventCatalogueId = DC.EventCatalogueId
     LEFT JOIN EVTEventCatalogueCategory AS DCC ON DC.FKEventCatalogueCategoryId = DCC.EventCatalogueCategoryId
     LEFT OUTER JOIN smf.Users AS U ON D.FKUserId = U.Id
     LEFT OUTER JOIN PRMWorkOrders AS WO ON D.FKWorkOrderId = WO.WorkOrderId
     LEFT OUTER JOIN PRMProductCatalogue AS PC ON WO.FKProductCatalogueId = PC.ProductCatalogueId
     LEFT OUTER JOIN PRMSteelgrades AS S ON WO.FKSteelgradeId = S.SteelgradeId
     LEFT OUTER JOIN TRKRawMaterials AS RM ON D.FKRawMaterialId = RM.RawMaterialId
     LEFT OUTER JOIN MVHAssets AS A ON D.FKAssetId = A.AssetId
     OUTER APPLY dbo.FNTGetShiftId(D.EventStartTs) AS GS;