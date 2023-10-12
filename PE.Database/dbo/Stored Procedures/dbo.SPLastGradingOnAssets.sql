CREATE   PROCEDURE dbo.SPLastGradingOnAssets
AS
/*
exec dbo.SPLastGradingOnAssets
*/
    BEGIN TRY
        SELECT AssetId, 
               LastGrading
        FROM
        (
            SELECT ROW_NUMBER() OVER(PARTITION BY T.FKAssetId
                   ORDER BY RMV.RuleMappingValueId DESC) AS Ranking, 
                   T.FKAssetId AS AssetId, 
                   RM.LastGrading
            FROM QERuleMappingValue AS RMV
                 INNER JOIN QETrigger AS T ON RMV.FKTriggerId = T.TriggerId
                 INNER JOIN TRKRawMaterials AS RM ON RMV.FKRawMaterialId = RM.RawMaterialId
        ) QRY
        WHERE Ranking = 1;
    END TRY
    BEGIN CATCH
        EXEC dbo.SPGetErrorInfo;
    END CATCH;
        RETURN;