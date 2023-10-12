CREATE   VIEW [hmi].[V_EventGroupSearchGrid]
AS

/*
select * from hmi.EventGroupSearchGrid
*/

SELECT ECG.EventCategoryGroupId, 
       ECG.EventCategoryGroupCode, 
       ECG.EventCategoryGroupName
FROM dbo.EVTEventCategoryGroups AS ECG;