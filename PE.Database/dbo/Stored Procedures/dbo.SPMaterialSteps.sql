CREATE   PROCEDURE [dbo].[SPMaterialSteps] (@MaterialId BIGINT)
AS
     SELECT FKRawMaterialId AS RawMaterialId,
	        FKMaterialId AS MaterialId,
	        ProcessingStepNo, 
            ProcessingStepTs, 
            PassNo, 
            IsReversed, 
            IsAssetExit, 
            A.AssetName, 
            A.AreaName
     FROM TRKRawMaterialsSteps RMS
	      INNER JOIN TRKRawMaterials RM ON RMS.FKRawMaterialId = RM.RawMaterialId
          INNER JOIN hmi.V_Assets A ON RMS.FKAssetId = A.AssetId
     WHERE 1=1
	 AND RM.FKMaterialId = @MaterialId
	 AND ProcessingStepNo > 0;