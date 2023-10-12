CREATE   FUNCTION [dbo].[FNCamelCase](@Keyword NVARCHAR(255)) RETURNS NVARCHAR(255) AS

/*
select dbo.FNCamelCase ('Litwo, 23 55 (Ojczyzno) moja! ty jesteś jak zdrowie;
Ile cię trzeba cenić, ten tylko się dowie,
Kto cię stracił. Dziś piękność twą w całej ozdobie
Widzę i opisuję, bo tęsknię po tobie.')
*/

BEGIN
DECLARE @CapitalizeFirstLetterOnly NVARCHAR(255);
DECLARE @KeepValues AS VARCHAR(50);
SET @KeepValues = '%[^a-z 0-9]%';
WHILE PATINDEX(@KeepValues, @Keyword) > 0
    SET @Keyword = STUFF(@Keyword, PATINDEX(@KeepValues, @Keyword), 1, '');
SELECT @CapitalizeFirstLetterOnly = REPLACE([CapitalizeFirstLetterOnly], ' ', '')
FROM
(
    SELECT STUFF(
    (
        SELECT ' ' + UPPER(LEFT(T3.V, 1)) + LOWER(STUFF(T3.V, 1, 1, ''))
        FROM
        (
            SELECT CAST(replace(
            (
                SELECT @Keyword AS '*' FOR XML PATH('')
            ), ' ', '<X/>') AS XML).query('.')
        ) AS T1(X)
        CROSS APPLY T1.X.nodes('text()') AS T2(X)
        CROSS APPLY
        (
            SELECT T2.X.value('.', 'varchar(255)')
        ) AS T3(V) FOR XML PATH(''), TYPE
    ).value('text()[1]', 'varchar(255)'), 1, 1, '') AS [CapitalizeFirstLetterOnly]
) AS QRY;
RETURN @CapitalizeFirstLetterOnly;
END;