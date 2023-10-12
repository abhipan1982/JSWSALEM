CREATE PROCEDURE [dbo].[SPWorkOrderTransferFromExcel]
AS;

/*
exec SPWorkOrderTransferFromExcel
*/
/*
     DECLARE @WorkOrderName NVARCHAR(50);
     DECLARE ExcelWorkOrders CURSOR
     FOR SELECT EWOD.[Work Order Name]
         FROM L3L2WorkOrderXFR...[WorkOrderData$] EWOD
              --LEFT JOIN xfr.L3L2WorkOrderDefinition WOD ON EWOD.[Work Order Name] = WOD.WorkOrderName
         WHERE EWOD.[Work Order Name] IS NOT NULL
               --AND WOD.WorkOrderName IS NULL;
     OPEN ExcelWorkOrders;
     FETCH NEXT FROM ExcelWorkOrders INTO @WorkOrderName;
     WHILE(@@FETCH_STATUS = 0)
         BEGIN
             INSERT INTO xfr.L3L2WorkOrderDefinition
             (WorkOrderName, 
              PreviousWorkOrderName, 
              OrderDeadline, 
              HeatName, 
              NumberOfBillets, 
              CustomerName, 
              BundleWeightMin, 
              BundleWeightMax, 
              TargetWorkOrderWeight, 
              TargetWorkOrderWeightMin, 
              TargetWorkOrderWeightMax, 
              SteelgradeCode, 
              InputThickness, 
              InputWidth, 
              InputShapeSymbol, 
              BilletWeight, 
              BilletLength, 
              OutputThickness, 
              OutputWidth, 
              OutputShapeSymbol
             )
                    SELECT [Work Order Name], 
                           [Previous Work Order Name], 
                           FORMAT([Order Deadline], 'ddMMyyyy'), 
                           [Heat Name], 
                           [Number Of Billets], 
                           [Customer Name], 
                           [Bundle Weight Min], 
                           [Bundle Weight Max], 
                           [Target Work Order Weight], 
                           [Target Work Order Weight Min], 
                           [Target Work Order Weight Max], 
                           [Steel Grade Code], 
                           [Input Thickness], 
                           [Input Width], 
                           [Input Shape Symbol], 
                           [Billet Weight], 
                           [Billet Length], 
                           [Output Thickness], 
                           [Output Width], 
                           [Output Shape Symbol]
                    FROM L3L2WorkOrderXFR...[WorkOrderData$]
                    WHERE [Work Order Name] = @WorkOrderName;
             --SELECT *
             --FROM xfr.L3L2WorkOrderDefinition
             --WHERE WorkOrderName = @WorkOrderName;
             FETCH NEXT FROM ExcelWorkOrders INTO @WorkOrderName;
         END;
     CLOSE ExcelWorkOrders;
     DEALLOCATE ExcelWorkOrders;
	 */