CREATE PROCEDURE [dbo].[SPGetAlarmsByLanguage] @LanguageId BIGINT = 1
AS

/*
@RoleId     NVARCHAR(450)
exec SPGetAlarmsByLanguage 3
select * from smf.Alarms
select * from smf.alarmdefinitions
select * from smf.AlarmMessages where fkalarmdefinitionid=230
*/

    BEGIN
        DECLARE @AlarmDefinitions TABLE(AlarmDefinitionId BIGINT);
        SELECT TOP 5 A.AlarmId, 
                      A.AlarmOwner, 
                      A.AlarmDate, 
                      A.DefaultMessage, 
                      A.Param1, 
                      A.Param2, 
                      A.Param3, 
                      A.Param4, 
                      A.ConfirmationDate, 
                      A.FKUserIdConfirmed AS UserIdConfirmed, 
                      U.UserName, 
                      AD.AlarmDefinitionId, 
                      AD.DefinitionCode, 
                      AD.DefinitionDescription, 
                      AD.IsStandard, 
                      AD.IsToConfirm, 
                      AD.IsPopupShow, 
                      AD.EnumAlarmType, 
                      AC.CategoryCode, 
                      ISNULL(AM.MessageText, A.DefaultMessage) AS MessageText, 
                      REPLACE(REPLACE(REPLACE(REPLACE(ISNULL(AM.MessageText, A.DefaultMessage), '{0}', ISNULL(Param1, '')), '{1}', ISNULL(Param2, '')), '{2}', ISNULL(Param3, '')), '{3}', ISNULL(Param4, '')) AS MessageTextFilled, 
                      ADP.ParamKeys, 
                      ADP.ParamNames
        FROM smf.Alarms A
             INNER JOIN smf.AlarmDefinitions AS AD ON A.FKAlarmDefinitionId = AD.AlarmDefinitionId
             INNER JOIN smf.AlarmCategories AS AC ON AD.FKAlarmCategoryId = AC.AlarmCategoryid
             LEFT JOIN smf.AlarmMessages AS AM ON AD.AlarmDefinitionId = AM.FKAlarmDefinitionId
                                                  AND FKLanguageId = @LanguageId
             LEFT JOIN smf.Users AS U ON A.FKUserIdConfirmed = U.Id
             LEFT JOIN
        (
            SELECT FKAlarmDefinitionId, 
                   STRING_AGG(ISNULL(ParamKey, ''), ',') ParamKeys, 
                   STRING_AGG(ISNULL(ParamName, ''), ',') ParamNames
            FROM smf.AlarmDefinitionParams
            GROUP BY FKAlarmDefinitionId
        ) AS ADP ON AD.AlarmDefinitionId = ADP.FKAlarmDefinitionId
        ORDER BY AlarmDate DESC;
    END;