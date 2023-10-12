CREATE   VIEW [hmi].[V_SetupConfigurationSearchGrid]
AS

/*
select * from [hmi].[V_SetupConfigurationSearchGrid]
select * from STPConfigurations
*/

SELECT C.ConfigurationId, 
       C.ConfigurationName, 
       C.ConfigurationVersion, 
       C.ConfigurationCreatedTs, 
       C.ConfigurationLastSentTs
FROM dbo.STPConfigurations AS C;