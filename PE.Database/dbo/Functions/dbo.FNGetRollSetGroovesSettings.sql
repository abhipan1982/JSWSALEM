CREATE FUNCTION [dbo].[FNGetRollSetGroovesSettings](@RollSetHistoryId BIGINT)
RETURNS SMALLINT
AS

/*
SELECT [dbo].[FNGetRollSetGroovesSettings] (10733)
*/

     BEGIN
         DECLARE @RollSetGroovesSettings SMALLINT;
         SELECT @RollSetGroovesSettings =
         (
             SELECT DISTINCT 
                    '' + CAST(EnumGrooveSetting AS NVARCHAR)
             FROM dbo.RLSRollGroovesHistory AS RGH
                  INNER JOIN dbo.RLSGrooveTemplates GT ON RGH.FKGrooveTemplateId = GT.GrooveTemplateId
             WHERE 1 = 1
                   AND FKRollSetHistoryId = @RollSetHistoryId
             ORDER BY 1 FOR XML PATH('')
         );
         RETURN @RollSetGroovesSettings;
     END;