CREATE   PROCEDURE [dbo].[SPRenumberAssets] @AssetIds dbo.AssetId_List READONLY
AS

/*
	DECLARE @AssetIds dbo.AssetId_list;
	--INSERT @AssetIds VALUES (105),(44),(72),(81),(12)
	INSERT @AssetIds select AssetId from MVHAssets order by OrderSeq
	EXEC [SPRenumberAssets] @AssetIds
	select * into MVHAssetsBAK2 from MVHAssets
*/

    BEGIN
        SET NOCOUNT ON;
        DECLARE @LogValue NVARCHAR(255);
        BEGIN TRY
            WITH NewOrderSeq
                 AS (SELECT ROW_NUMBER() OVER(
                            ORDER BY
                     (
                         SELECT NULL
                     )) OrderSeq, 
                            AssetId
                     FROM @AssetIds)
                 UPDATE MVHAssets
                   SET 
                       MVHAssets.OrderSeq = NewOrderSeq.OrderSeq
                 FROM NewOrderSeq
                 WHERE MVHAssets.AssetId = NewOrderSeq.AssetId
                       AND MVHAssets.AssetCode > 0
                       AND MVHAssets.OrderSeq > 0;
				 --select * from NewOrderSeq
            SET @LogValue = 'Succes';
            EXEC SPLogDB 
                 @LogValue;
        END TRY
        BEGIN CATCH
            EXEC dbo.SPLogDB 
                 @LogValue;
        END CATCH;
    END;