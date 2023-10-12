CREATE PROCEDURE [dbo].[SPWorkOrdersOnProductYards] @WorkOrderId BIGINT = NULL
AS

/*
exec dbo.SPWorkOrdersOnProductYards @WorkOrderId = 177061
177048
select * from prmproductsteps
*/

    BEGIN
        SELECT ISNULL(P.FKWorkOrderId, @WorkOrderId) AS WorkOrderId, 
               A.AssetId, 
               A.AssetName, 
               AL.PieceMaxCapacity, 
               AL.WeightMaxCapacity, 
               ISNULL(COUNT(P.ProductId), 0) AS ProductNumber, 
               ISNULL(SUM(P.ProductWeight), 0) AS ProductWeight, 
               ISNULL(CASE
                          WHEN WeightMaxCapacity > 0
                          THEN SUM(P.ProductWeight) / WeightMaxCapacity
                          ELSE 0
                      END, 0) AS FillIndex
        FROM hmi.V_Assets AS A
             INNER JOIN hmi.V_Assets AS PA ON A.ParentAssetId = PA.AssetId
             LEFT JOIN dbo.MVHAssetsLocation AS AL ON a.AssetId = AL.FKAssetId
             LEFT JOIN dbo.PRMProductSteps AS PS
             INNER JOIN dbo.PRMProducts AS P ON PS.FKProductId = P.ProductId ON A.AssetId = PS.FKAssetId
                                                                                AND PS.StepNo = 0
                                                                                AND P.FKWorkOrderId = @WorkOrderId
        WHERE PA.EnumYardType = 20
        GROUP BY A.AssetId, 
                 A.AssetName, 
                 P.FKWorkOrderId, 
                 AL.PieceMaxCapacity, 
                 AL.WeightMaxCapacity;
    END;