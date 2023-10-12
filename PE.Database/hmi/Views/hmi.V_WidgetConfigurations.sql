CREATE   VIEW [hmi].[V_WidgetConfigurations]
AS

/*
	select * from [hmi].[V_WidgetConfigurations]
*/

SELECT W.WidgetId, 
       W.WidgetName, 
       W.WidgetFileName, 
       WC.WidgetConfigurationId, 
       WC.OrderSeq, 
       --ISNULL(WC.IsActive, 0) AS IsActiveOld, 
       CAST(CASE
                WHEN WC.FKUserId IS NOT NULL
                THEN 1
                ELSE 0
            END AS BIT) AS IsActive, 
       U.Id AS UserId, 
       U.UserName
FROM smf.Users U
     OUTER APPLY HMIWidgets W
     LEFT JOIN HMIWidgetConfigurations WC ON W.WidgetId = WC.FKWidgetId
                                             AND U.Id = WC.FKUserId;