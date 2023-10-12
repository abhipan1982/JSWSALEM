CREATE PROCEDURE [dbo].[SPClearRawMaterial] @RawMaterialId BIGINT = NULL
AS

/*
select * from TRKRawMaterials
exec [dbo].[SPClearRawMaterial] 10674222
SELECT * FROM smf.Alarms ORDER BY 1 DESC
*/

    BEGIN
        DECLARE @LogValue NVARCHAR(300);
        DECLARE @RawMaterialName NVARCHAR(50);
        SELECT @RawMaterialName = RawMaterialName
        FROM [dbo].[TRKRawMaterials]
        WHERE RawMaterialId = @RawMaterialId;
        IF @@ROWCOUNT = 1
            BEGIN TRY
                -- Update Materials
                UPDATE [dbo].[PRMMaterials]
                  SET 
                      IsAssigned = 0
                WHERE MaterialId IN
                (
                    SELECT FKMaterialId
                    FROM TRKRawMaterials
                    WHERE RawMaterialId = @RawMaterialId
                );
                -- Delete RawMaterial
                DELETE FROM [dbo].[TRKRawMaterials]
                WHERE RawMaterialId = @RawMaterialId;
                -- Log operation
                SET @LogValue = 'Deleted RawMaterial with Id: ' + CAST(@RawMaterialId AS NVARCHAR(10)) + ', Name: ' + CAST(@RawMaterialName AS NVARCHAR(50)) + '';
                EXEC dbo.SPLogDB 
                     @LogValue;
            END TRY
            BEGIN CATCH
                EXEC dbo.SPLogDB 
                     @LogValue;
            END CATCH;
    END;