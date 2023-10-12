
CREATE     VIEW [hmi].[V_AssetsLocationOverview] AS 
/*
select * from [hmi].[V_AssetsLocationOverview]
*/
WITH CTEAssets
        AS (SELECT A.OrderSeq, 
                   PA.AssetId AS ParentAssetId, 
                   PA.AssetName AS ParentAssetName, 
                   PA.AssetDescription AS ParentAssetDescription, 
                   PATS.AssetTypeName AS ParentAssetType, 
				   ISNULL(PATS.EnumYardType,0) AS ParentEnumYardType,
                   A.AssetId, 
                   A.AssetName, 
                   A.AssetDescription, 
                   ATS.AssetTypeName AS AssetType, 
				   ISNULL(ATS.EnumYardType,0) AS EnumYardType,
                   AL.LayersMaxNumber, 
                   AL.WeightMaxCapacity, 
                   AL.PieceMaxCapacity, 
                   AL.FillOrderSeq
            FROM MVHAssets A
                 INNER JOIN MVHAssetsLocation AL ON A.AssetId = AL.FKAssetId
                 INNER JOIN MVHAssets PA ON A.FKParentAssetId = PA.AssetId
                 LEFT JOIN MVHAssetTypes ATS ON A.FKAssetTypeId = ATS.AssetTypeId
                 LEFT JOIN MVHAssetTypes PATS ON PA.FKAssetTypeId = PATS.AssetTypeId),
        CTEMaterials
        AS (SELECT FKAssetId, 
                   SUM([MaterialWeight]) AS WeightMaterials, 
                   COUNT(MaterialId) AS CountMaterials, 
                   SUM(IsLastGroup) AS CountMaterialsInLastGroup, 
                   MAX(CASE
                           WHEN IsLastGroup = 1
                           THEN FKHeatId
                           ELSE NULL
                       END) HeatIdInLastGroup
            FROM
            (
                SELECT M.MaterialId, 
                       M.MaterialName, 
                       M.FKHeatId, 
                       M.MaterialWeight, 
                       MS.FKAssetId, 
                       MS.GroupNo,
                       CASE
                           WHEN MAX(MS.GroupNo) OVER(PARTITION BY MS.FKAssetId) = MS.GroupNo
                           THEN 1
                           ELSE 0
                       END IsLastGroup
                FROM PRMMaterials M
                     INNER JOIN PRMMaterialSteps MS ON M.MaterialId = MS.FKMaterialId
                                                       AND MS.StepNo = 0
            ) QRY
            GROUP BY FKAssetId),
        CTEProducts
        AS (SELECT FKAssetId, 
                   SUM(ProductWeight) AS WeightProducts, 
                   COUNT(ProductId) AS CountProducts, 
                   SUM(IsLastGroup) AS CountProductsInLastGroup, 
                   MAX(CASE
                           WHEN IsLastGroup = 1
                           THEN FKWorkOrderId
                           ELSE NULL
                       END) WorkOrderIdInLastGroup
            FROM
            (
                SELECT P.ProductId, 
                       P.ProductName, 
                       P.FKWorkOrderId, 
                       P.ProductWeight, 
                       PS.FKAssetId, 
                       PS.GroupNo,
                       CASE
                           WHEN MAX(PS.GroupNo) OVER(PARTITION BY PS.FKAssetId) = PS.GroupNo
                           THEN 1
                           ELSE 0
                       END IsLastGroup
                FROM PRMProducts P
                     INNER JOIN PRMProductSteps PS ON P.ProductId = PS.FKProductId
                                                      AND PS.StepNo = 0
            ) QRY
            GROUP BY FKAssetId)
        --Main Query
        SELECT ROW_NUMBER() OVER(
               ORDER BY A.OrderSeq) AS OrderSeq, 
               A.ParentAssetId, 
               A.ParentAssetName, 
               A.ParentAssetDescription, 
               A.ParentAssetType, 
			   A.ParentEnumYardType,
               A.AssetId, 
               A.AssetName, 
               A.AssetDescription, 
               A.AssetType, 
			   A.EnumYardType,
               A.LayersMaxNumber, 
               A.WeightMaxCapacity, 
               A.PieceMaxCapacity, 
               A.FillOrderSeq, 
               M.WeightMaterials, 
               M.CountMaterials, 
               M.CountMaterialsInLastGroup, 
               M.HeatIdInLastGroup, 
               H.HeatName AS HeatNameInLastGroup, 
               P.WeightProducts, 
               P.CountProducts, 
               P.CountProductsInLastGroup, 
               P.WorkOrderIdInLastGroup, 
               WO.WorkOrderName AS WorkOrderNameInLastGroup
        FROM CTEAssets A
             LEFT JOIN CTEMaterials M
             INNER JOIN PRMHeats H ON M.HeatIdInLastGroup = H.HeatId ON A.AssetId = M.FKAssetId
             LEFT JOIN CTEProducts P
             INNER JOIN PRMWorkOrders WO ON P.WorkOrderIdInLastGroup = WO.WorkOrderId ON A.AssetId = P.FKAssetId;

/*
YardId YardName LocationId LocationName TotalSpace UsedSpace LastHeatId LastHeatName
select * from V_AssetsLocationOverview
select * from prmProductsteps
*/