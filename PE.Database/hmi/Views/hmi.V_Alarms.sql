CREATE   VIEW [hmi].[V_Alarms]
AS

/*
	select * from [hmi].[V_Alarms]
*/

SELECT A.AlarmId, 
       A.AlarmOwner, 
       A.AlarmDate, 
       A.ConfirmationDate, 
       A.FKUserIdConfirmed AS UserIdConfirmed, 
       U.UserName, 
       AM.FKLanguageId AS LanguageId, 
       AD.IsToConfirm, 
       AD.EnumAlarmType, 
       AC.CategoryCode, 
       REPLACE(REPLACE(REPLACE(REPLACE(ISNULL(AM.MessageText, A.DefaultMessage), '{0}', ISNULL(Param1, '')), '{1}', ISNULL(Param2, '')), '{2}', ISNULL(Param3, '')), '{3}', ISNULL(Param4, '')) AS MessageTextFilled
FROM smf.Alarms A
     INNER JOIN smf.AlarmDefinitions AS AD ON A.FKAlarmDefinitionId = AD.AlarmDefinitionId
     INNER JOIN smf.AlarmCategories AS AC ON AD.FKAlarmCategoryId = AC.AlarmCategoryid
     LEFT JOIN smf.AlarmMessages AS AM
     INNER JOIN smf.Languages AS L ON AM.FKLanguageId = L.LanguageId
                                      AND L.Active = 1 ON AD.AlarmDefinitionId = AM.FKAlarmDefinitionId
     LEFT JOIN smf.Users AS U ON A.FKUserIdConfirmed = U.Id;