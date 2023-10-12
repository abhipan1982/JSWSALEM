CREATE   VIEW [hmi].[V_HeatsOnYards]
AS

/*
select * from [hmi].[V_HeatsOnYards]
*/

SELECT ISNULL(ROW_NUMBER() OVER(
       ORDER BY AreaName), 0) AS OrderSeq, 
       AreaId, 
       AreaName, 
       AreaDescription, 
       AssetTypeName, 
       EnumYardType, 
       HeatId, 
       HeatName, 
       SteelgradeId, 
       SteelgradeName, 
       HeatWeight, 
       SUM(MaterialWeight) AS WeightOnArea, 
       COUNT(MaterialId) AS MaterialsOnArea
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
           AssetTypeName, 
           AreaId, 
           AreaName, 
           AreaDescription, 
           ISNULL(EnumYardType, 0) AS EnumYardType, 
           PositionX, 
           PositionY
    FROM dbo.PRMMaterials M
         INNER JOIN dbo.PRMHeats H ON M.FKHeatId = H.HeatId
         INNER JOIN dbo.PRMMaterialSteps MS ON M.MaterialId = MS.FKMaterialId
                                               AND MS.StepNo = 0
         INNER JOIN hmi.V_Assets A ON MS.FKAssetId = A.AssetId
         LEFT JOIN dbo.PRMSteelgrades S ON H.FKSteelgradeId = S.SteelgradeId
         LEFT JOIN dbo.PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
) QRY
GROUP BY AreaId, 
         AreaName, 
         AreaDescription, 
         AssetTypeName, 
         EnumYardType, 
         HeatId, 
         HeatName, 
         SteelgradeId, 
         SteelgradeName, 
         HeatWeight;