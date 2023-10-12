CREATE PROCEDURE [dbo].[SPAddRawMaterialLocations] @LineItems dbo.TRK_RML_ROW READONLY
AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @LogValue NVARCHAR(50);
        BEGIN TRY
            INSERT INTO TRKRawMaterialLocations
            (FkAssetId, 
             AssetCode, 
             PositionSeq, 
             OrderSeq, 
             FkRawMaterialId, 
             EnumAreaType, 
             IsVirtual, 
             IsOccupied, 
             FKCtrAssetId, 
             CorrelationId
            )
                   SELECT FkAssetId, 
                          AssetCode, 
                          PositionSeq, 
                          OrderSeq, 
                          FkRawMaterialId, 
                          EnumAreaType, 
                          IsVirtual, 
                          IsOccupied, 
                          FKCtrAssetId, 
                          CorrelationId
                   FROM @LineItems;
            --SET @LogValue = 'Succes';
            --EXEC SPLogError 'SP', '[SPAddRawMaterialLocations]', @LogValue;
        END TRY
        BEGIN CATCH
            EXEC SPLogError 
                 'SP', 
                 '[SPAddRawMaterialLocations]', 
                 @LogValue;
        END CATCH;
    END;