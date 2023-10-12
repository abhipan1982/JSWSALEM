CREATE FUNCTION [dbo].[FNGetEnumKeyword](@EnumName  VARCHAR(50), 
                                        @EnumValue BIGINT)
RETURNS VARCHAR(50)
AS

/*
select dbo.FNGetEnumKeyword ('FeatureType',3)
*/

     BEGIN
         DECLARE @EnumKeyword VARCHAR(50);
         SELECT @EnumKeyword = EV.Keyword
         FROM smf.EnumNames EN
              INNER JOIN smf.EnumValues EV ON EN.EnumNameId = EV.FkEnumNameId
         WHERE EN.EnumName = @EnumName
               AND EV.[Value] = @EnumValue;
         RETURN @EnumKeyword;
     END;