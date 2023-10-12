CREATE PROCEDURE [dbo].[SPAddLanguageToAlarmMessages] @LanguageCode NVARCHAR(10) = 'en-US', 
                                                     @Operation    NVARCHAR(10) = 'activate'
AS

/*
exec dbo.SPAddLanguageToAlarmMessages @LanguageCode='zh-CN', @Operation='deactivate'
select * from smf.Languages
insert into smf.Languages (LanguageCode, IconName, LanguageName, Active) 
	select Code, flag, Language, 0 from [148.56.68.36].PE_Lite_UP.dbo.IMPLanguages 
	where Code not in (select LanguageCode from smf.Languages)
	order by Code, flag

select AD.AlarmDefinitionId, AD.DefinitionCode, AM1.MessageText, AM2.MessageText, AM5.MessageText, AM137.MessageText
from smf.AlarmDefinitions AD
left join smf.AlarmMessages AM1 ON AD.AlarmDefinitionId = AM1.FKAlarmDefinitionId and AM1.FKLanguageId = 1
left join smf.AlarmMessages AM2 ON AD.AlarmDefinitionId = AM2.FKAlarmDefinitionId and AM2.FKLanguageId = 2
left join smf.AlarmMessages AM5 ON AD.AlarmDefinitionId = AM5.FKAlarmDefinitionId and AM5.FKLanguageId = 5
left join smf.AlarmMessages AM137 ON AD.AlarmDefinitionId = AM137.FKAlarmDefinitionId and AM137.FKLanguageId = 137

UPDATE smf.AlarmMessages
  SET 
      MessageText = qry.Translated
FROM
(
    SELECT FKAlarmDefinitionId, 
           FKLanguageId, 
           Translated
    FROM IMPAlamMessages
) qry
WHERE qry.FKAlarmDefinitionId = smf.AlarmMessages.FKAlarmDefinitionId
      AND qry.FKLanguageId = smf.AlarmMessages.FKLanguageId;
*/

    BEGIN
        DECLARE @LanguageId BIGINT;
        SELECT @LanguageId = LanguageId
        FROM smf.Languages
        WHERE LanguageCode = @LanguageCode;
        IF @LanguageId = 1
            BEGIN
                SELECT LanguageCode, 
                       LanguageName, 
                       IconName
                FROM smf.Languages
                ORDER BY LanguageCode;
            END;
        IF @LanguageId > 1
            BEGIN
                SELECT @LanguageId AS LanguageId,
                       CASE
                           WHEN @Operation = 'activate'
                           THEN ' activated'
                           ELSE ' deactivated'
                       END AS Operation;
                DECLARE @DefaultMessageText TABLE
                (AlarmDefinitionId BIGINT, 
                 MessageText       NVARCHAR(255)
                );
                INSERT INTO @DefaultMessageText
                       SELECT AD.AlarmDefinitionId, 
                              AM.MessageText
                       FROM smf.AlarmDefinitions AD
                            INNER JOIN smf.AlarmMessages AM ON AD.AlarmDefinitionId = AM.FKAlarmDefinitionId
                                                               AND AM.FKLanguageId = 1;
                UPDATE smf.Languages
                  SET 
                      Active = CASE
                                   WHEN @Operation = 'activate'
                                   THEN 1
                                   ELSE 0
                               END
                WHERE LanguageId = @LanguageId;
                INSERT INTO smf.AlarmMessages
                (FKLanguageId, 
                 FKAlarmDefinitionId, 
                 MessageText
                )
                       SELECT @LanguageId, 
                              AlarmDefinitionId, 
                              MessageText
                       FROM @DefaultMessageText
                       WHERE AlarmDefinitionId IN
                       (
                           SELECT FKAlarmDefinitionId
                           FROM smf.AlarmMessages
                           WHERE FKLanguageId = 1
                           EXCEPT
                           SELECT FKAlarmDefinitionId
                           FROM smf.AlarmMessages
                           WHERE FKLanguageId = @LanguageId
                       );
                SELECT FKAlarmDefinitionId AS AlarmDefinitionId, 
                       FKLanguageId AS LanguageId, 
                       MessageText
                FROM smf.AlarmMessages AM
                WHERE FKLanguageId = @LanguageId;
            END;
    END;