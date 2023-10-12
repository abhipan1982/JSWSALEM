CREATE   VIEW [hmi].[V_QualityExpertSearchGrid]
AS

/*
select * from hmi.V_Enums
SELECT * from [hmi].[V_QualityExpertSearchGrid]
*/

SELECT RM.RawMaterialId, 
       RM.EnumRawMaterialStatus, 
       RM.RawMaterialName, 
       RM.RawMaterialCreatedTs, 
       RM.RawMaterialStartTs, 
       RM.RawMaterialEndTs, 
       RM.RollingStartTs, 
       RM.RollingEndTs, 
       RM.ProductCreatedTs, 
       RM.LastGrading, 
       M.MaterialId, 
       M.MaterialName, 
       H.HeatId, 
       H.HeatName, 
       WO.WorkOrderId, 
       WO.WorkOrderName, 
       M.MaterialCreatedTs, 
       M.MaterialStartTs, 
       M.MaterialEndTs, 
       WO.WorkOrderStartTs, 
       WO.WorkOrderEndTs
FROM TRKRawMaterials RM
     INNER JOIN PRMMaterials M
     INNER JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId
     INNER JOIN PRMHeats H ON M.FKHeatId = H.HeatId ON RM.FKMaterialId = M.MaterialId
WHERE RM.EnumRawMaterialType = 0
      AND RM.CuttingSeqNo = 0;