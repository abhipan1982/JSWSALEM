CREATE PROCEDURE [dbo].[SPUserMenu] @UserName NVARCHAR(256) = NULL, 
                                   @IsAdmin  BIT           = 0
AS

/*
exec SPUserMenu @UserName='ivan.kopytko@primetals.com',@IsAdmin=1
*/

     DECLARE @HmiClientMenuIds TABLE(HmiClientMenuId BIGINT);
     IF @IsAdmin = 1
         BEGIN
             INSERT INTO @HmiClientMenuIds
                    SELECT HmiClientMenuId
                    FROM smf.HmiClientMenu;
         END;
         ELSE
         BEGIN
             INSERT INTO @HmiClientMenuIds
                    SELECT DISTINCT 
                           HmiClientMenuId
                    FROM smf.Users AS U
                         INNER JOIN smf.UserRoles AS UR ON U.Id = UR.UserId
                         INNER JOIN smf.RoleRights AS RR ON UR.RoleId = RR.RoleId
                         INNER JOIN smf.HmiClientMenu AS HCM ON RR.AccessUnitId = HCM.FKAccessUnitId
                    WHERE UserName = @UserName
                    UNION ALL
                    SELECT HmiClientMenuId
                    FROM smf.HmiClientMenu
                    WHERE FKAccessUnitId IS NULL;
         END;
     SELECT @UserName AS UserName, 
            AU.AccessUnitName, 
            HCM.HmiClientMenuId, 
            ParentHmiClientMenuId, 
            HmiClientMenuName, 
            ControllerName, 
            Method, 
            MethodParameter, 
            DisplayOrder, 
            IconName, 
            Area, 
            IsActive, 
            FKAccessUnitId AS AccessUnitId
     FROM smf.HmiClientMenu HCM
          INNER JOIN @HmiClientMenuIds HCMI ON HCM.HmiClientMenuId = HCMI.HmiClientMenuId
          LEFT JOIN smf.AccessUnits AS AU ON HCM.FKAccessUnitId = AU.AccessUnitId
     ORDER BY ParentHmiClientMenuId, 
              DisplayOrder;