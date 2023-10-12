CREATE   VIEW [hmi].[V_RawMaterialHistory]
AS SELECT RM.RawMaterialId, 
          RM.RawMaterialName, 
          RM.FKShiftCalendarId AS ShiftCalendarId, 
          RM.LastWeight AS RawMaterialLastWeight, 
          RM.LastLength AS RawMaterialLastLength, 
          RMS.RawMaterialStepId, 
          RMS.FKAssetId AS AssetId, 
          RMS.ProcessingStepTs, 
          RMS.ProcessingStepNo, 
          RMS.PassNo AS RawMaterialPassNo, 
          RMS.IsReversed AS RawMaterialIsReversed, 
          RMS.IsAssetExit, 
          A.AssetName
   FROM dbo.TRKRawMaterials AS RM
        INNER JOIN dbo.TRKRawMaterialsSteps AS RMS ON RM.RawMaterialId = RMS.FKRawMaterialId
        INNER JOIN dbo.MVHAssets A ON RMS.FKAssetId = A.AssetId;