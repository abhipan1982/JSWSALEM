
CREATE     VIEW [hmi].[V_Enums]
AS

/*
select * from [hmi].[V_Enums]
*/

SELECT ROW_NUMBER() OVER(
       ORDER BY EN.EnumName, 
                EV.[Value]) AS OrderSeq, 
       EN.EnumNameId AS EnumNameId, 
       EN.EnumName AS EnumName, 
       EN.IsCustom AS EnumNameIsCustom, 
       EV.[Value] AS EnumValue, 
       EV.Keyword AS EnumKeyword, 
       EV.IsCustom AS EnumKeywordIsCustom
FROM smf.EnumNames AS EN
     INNER JOIN smf.EnumValues AS EV ON EN.EnumNameId = EV.FKEnumNameId;