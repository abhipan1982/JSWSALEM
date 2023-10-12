CREATE   PROCEDURE [dbo].[SPMaterialGenealogy](@MaterialId BIGINT)
AS

/*
	exec SPMaterialGenealogy 7224180
*/

     WITH RawMaterials
          AS (SELECT RawMaterialId
              FROM TRKRawMaterials
              WHERE FKMaterialId = @MaterialId),
          Relations
          AS (SELECT ChildRawMaterialId, 
                     MAX(ParentRawMaterialId) ParentRawMaterialId
              FROM TRKRawMaterialRelations
              GROUP BY ChildRawMaterialId)
          SELECT DISTINCT 
                 RM.RawMaterialId, 
                 RM.RawMaterialName, 
                 RMR.ParentRawMaterialId, 
                 RM.LastWeight, 
                 RM.LastLength, 
                 RM.EnumRawMaterialStatus, 
                 dbo.FNGetEnumKeyword('RawMaterialStatus', RM.EnumRawMaterialStatus) AS MaterialStatus, 
                 RM.EnumTypeOfScrap, 
                 dbo.FNGetEnumKeyword('TypeOfScrap', RM.EnumTypeOfScrap) AS TypeOfScrap, 
                 RM.EnumRejectLocation, 
                 dbo.FNGetEnumKeyword('RejectLocation', RM.EnumRejectLocation) AS RejectLocation, 
                 RM.CuttingSeqNo, 
                 RM.ChildsNo
          FROM TRKRawMaterials RM
               LEFT JOIN Relations AS RMR ON RM.RawMaterialId = RMR.ChildRawMaterialId
               INNER JOIN RawMaterials RMC ON RMC.RawMaterialId = RM.RawMaterialId
                                              OR RMC.RawMaterialId = RMR.ParentRawMaterialId
          ORDER BY RM.CuttingSeqNo;