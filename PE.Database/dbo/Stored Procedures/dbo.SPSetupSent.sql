CREATE PROCEDURE [dbo].[SPSetupSent] @WorkOrderId BIGINT
AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @LogValue VARCHAR(128);
        BEGIN TRY
            DELETE FROM STPSetupSent
            WHERE FKWorkOrderId NOT IN
            (
                SELECT FKWorkOrderId
                FROM PPLSchedules
            );
            DELETE FROM STPSetupSent
            WHERE FKWorkOrderId = @WorkOrderId
                  AND EXISTS
            (
                SELECT 1
                FROM STPSetupWorkOrders
                WHERE FKWorkOrderId = @WorkOrderId
            );
            INSERT INTO STPSetupSent
            (FKWorkOrderId, 
             FKSetupId, 
             SentTs
            )
            (
                SELECT FKWorkOrderId, 
                       FKSetupId, 
                       GETDATE()
                FROM STPSetupWorkOrders
                WHERE FKWorkOrderId = @WorkOrderId
            );
            SET @LogValue = 'Succes';
        END TRY
        BEGIN CATCH
            EXEC SPLogError 
                 'SP', 
                 '[SPSetupSent]', 
                 @LogValue;
        END CATCH;
    END;