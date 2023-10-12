CREATE PROCEDURE [dbo].[SPKPIWorkOrderCompletion] @WorkOrderId     BIGINT, 
                                                 @KPIDefinitionId BIGINT   = NULL OUTPUT, 
                                                 @KPITime         DATETIME = NULL OUTPUT, 
                                                 @KPIValue        FLOAT    = NULL OUTPUT
AS

/*
EXECUTE dbo.SPKPIWorkOrderCompletion 176774;
*/

    BEGIN TRY
        SET NOCOUNT ON;
        SELECT @KPIValue = CASE
                               WHEN WorkOrderTargetWeight != 0
                               THEN WorkOrderProductWeight / WorkOrderTargetWeight
                               ELSE 0
                           END
        FROM dw.FactWorkOrder
        WHERE FactWorkOrderKey = @WorkOrderId;

        -- O U T P U T

        SET @KPIDefinitionId = 4;
        SET @KPITime = GETDATE();
        SET @KPIValue = ISNULL(@KPIValue, 0);
        PRINT @KPIDefinitionId;
        PRINT @KPITime;
        PRINT @KPIValue;
    END TRY
    BEGIN CATCH
        EXEC dbo.SPGetErrorInfo;
    END CATCH;
        RETURN;