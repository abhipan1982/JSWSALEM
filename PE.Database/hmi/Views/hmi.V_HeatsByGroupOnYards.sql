CREATE   VIEW [hmi].[V_HeatsByGroupOnYards]
AS

/*
select * from [hmi].[V_HeatsByGroupOnYards]
*/

SELECT ISNULL(ROW_NUMBER() OVER(
       ORDER BY AssetName, 
                GroupNo DESC), 0) AS OrderSeq, 
       CAST(CASE
                WHEN GroupNo = MaxGroupNoByAsset
                THEN 1
                ELSE 0
            END AS BIT) AS IsFirstInQueue, 
       AssetId, 
       AssetName, 
       AssetDescription, 
       AssetTypeName, 
       AreaId, 
       AreaName, 
       AreaDescription, 
       EnumYardType, 
       HeatId, 
       HeatName, 
       GroupNo, 
       SteelgradeId, 
       SteelgradeName, 
       HeatWeight, 
       SUM(MaterialWeight) AS WeightByGroupOnArea, 
       COUNT(MaterialId) AS MaterialsByGroupOnArea
FROM
(
    SELECT MaterialId, 
           MaterialName, 
           MaterialWeight, 
           HeatId, 
           HeatName, 
           HeatWeight, 
           SteelgradeId, 
           SteelgradeCode, 
           SteelgradeName, 
           WorkOrderId, 
           WorkOrderName, 
           AssetOrderSeq, 
           AssetId, 
           AssetName, 
           AssetDescription, 
           AssetTypeName, 
           AreaId, 
           AreaName, 
           AreaDescription, 
           ISNULL(EnumYardType, 0) AS EnumYardType, 
           PositionX, 
           PositionY, 
           GroupNo, 
           MAX(GroupNo) OVER(PARTITION BY AssetId) MaxGroupNoByAsset
    FROM dbo.PRMMaterials M
         INNER JOIN dbo.PRMMaterialSteps MS ON M.MaterialId = MS.FKMaterialId
                                               AND MS.StepNo = 0
         INNER JOIN dbo.PRMHeats H ON M.FKHeatId = H.HeatId
         INNER JOIN hmi.V_Assets A ON MS.FKAssetId = A.AssetId
         LEFT JOIN dbo.PRMSteelgrades S ON H.FKSteelgradeId = S.SteelgradeId
         LEFT JOIN dbo.PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
) QRY
GROUP BY AssetId, 
         AssetName, 
         AssetDescription, 
         AssetTypeName, 
         AreaId, 
         AreaName, 
         AreaDescription, 
         EnumYardType, 
         HeatId, 
         HeatName, 
         GroupNo, 
         SteelgradeId, 
         SteelgradeName, 
         HeatWeight, 
         MaxGroupNoByAsset;