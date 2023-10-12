CREATE   VIEW [report].[V_QualityReport]
AS

/*
select * from [report].[V_QualityReport]
*/

SELECT ISNULL(ROW_NUMBER() OVER(PARTITION BY WO.WorkOrderId
       ORDER BY RM.RawMaterialId), 0) OrderSeq, 
       WO.WorkOrderId, 
       WO.WorkOrderName, 
       M.MaterialId, 
       M.MaterialName, 
       QI.DiameterMin, 
       QI.DiameterMax, 
       QI.VisualInspection, 
	   QI.EnumCrashTest,
       QI.EnumInspectionResult, 
       [dbo].[FNGetEnumKeyword]('CrashTest', QI.EnumCrashTest) AS CrashTest, 
       [dbo].[FNGetEnumKeyword]('InspectionResult', QI.EnumInspectionResult) AS InspectionResult
FROM QTYQualityInspections QI
     INNER JOIN TRKRawMaterials RM ON QI.FKRawMaterialId = RM.RawMaterialId
     INNER JOIN PRMMaterials M ON RM.FKMaterialId = M.MaterialId
     INNER JOIN PRMWorkOrders WO ON M.FKWorkOrderId = WO.WorkOrderId;