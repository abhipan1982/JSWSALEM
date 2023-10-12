




CREATE           VIEW [dw].[DimWorkOrder] AS 
/*
	SELECT * FROM dw.DimWorkOrder
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY WO.WorkOrderId), 0) AS DimWorkOrderRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimWorkOrderIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(WO.WorkOrderId AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.EnumWorkOrderStatus AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.FKHeatId AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.FKSteelgradeId AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.FKMaterialCatalogueId AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.FKProductCatalogueId AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.FKCustomerId AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.FKParentWorkOrderId AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.WorkOrderName AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.IsTestOrder AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.WorkOrderCreatedTs AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.WorkOrderCreatedInL3Ts AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.ToBeCompletedBeforeTs AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.WorkOrderStartTs AS NVARCHAR), ';') + 
			COALESCE(CAST(WO.WorkOrderEndTs AS NVARCHAR), ';') + 
			COALESCE(CAST(DATEDIFF(SECOND, WO.WorkOrderStartTs, ISNULL(WO.WorkOrderEndTs, GETDATE())) AS NVARCHAR), ';') +
			COALESCE(CAST(WO.ExternalWorkOrderName AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimWorkOrderHash, 
          WO.WorkOrderId AS DimWorkOrderKey, 
          WO.EnumWorkOrderStatus AS DimWorkOrderStatusKey, 
          WO.FKHeatId AS DimHeatKey, 
          WO.FKSteelgradeId AS DimSteelgradeKey, 
          WO.FKMaterialCatalogueId AS DimMaterialCatalogueKey, 
          WO.FKProductCatalogueId AS DimProductCatalogueKey, 
          WO.FKCustomerId AS DimCustomerKey, 
          WO.FKParentWorkOrderId AS DimWorkOrderKeyParent, 
          WO.WorkOrderName AS WorkOrderName, 
          WO.IsTestOrder AS WorkOrderIsTest, 
          WO.WorkOrderCreatedTs AS WorkOrderCreated, 
          WO.WorkOrderCreatedInL3Ts AS WorkOrderCreatedInL3, 
          WO.ToBeCompletedBeforeTs AS WorkOrderDueDate, 
          WO.WorkOrderStartTs AS WorkOrderStart, 
          WO.WorkOrderEndTs AS WorkOrderEnd, 
          WO.ExternalWorkOrderName AS WoorkOrderExternalName, 
          WOP.WorkOrderName AS WorkOrderNameParent
   FROM PRMWorkOrders WO
        LEFT JOIN PRMWorkOrders AS WOP ON WO.FKParentWorkOrderId = WOP.WorkOrderId;