CREATE FUNCTION [dbo].[FNTGetActualGrooveInformations](@RollSetHistoryId BIGINT)
RETURNS TABLE
AS
     RETURN
     SELECT DISTINCT 
            RGH.FKRollSetHistoryId, 
            RGH.AccWeight, 
            RGH.AccBilletCnt, 
            RGH.GrooveNumber, 
            RGH.EnumRollGrooveStatus, 
            GT.GrooveTemplateId, 
            GT.GrooveTemplateName, 
            GT.EnumGrooveSetting
     FROM dbo.RLSRollSetHistory AS RSH
          INNER JOIN dbo.RLSRollGroovesHistory AS RGH ON RSH.RollSetHistoryId = RGH.FKRollSetHistoryId
          INNER JOIN dbo.RLSGrooveTemplates AS GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
     WHERE RSH.EnumRollSetHistoryStatus = 1
           AND RGH.EnumRollGrooveStatus = 2
           AND RGH.FKRollSetHistoryId = @RollSetHistoryId;