
CREATE     VIEW [new].[V_EquipmentSets]

/*
	select * from new.V_EquipmentSets
*/

AS WITH Tree
        AS (SELECT [EquipmentId] = LU.FKEquipmentId, 
                   [Level] = 1, 
                   [Path] = CAST(FKEquipmentId AS VARCHAR(100)), 
                   [EquipmentSetId]
            FROM [new].[MNTEquipmentSets] AS LU
            WHERE FKEquipmentId = FKParentEquipmentId
            UNION ALL
            SELECT [EquipmentId] = LD.FKEquipmentId, 
                   [Level] = Tree.[Level] + 1, 
                   [Path] = CAST(Tree.Path + '/' + RIGHT('0000000000' + CAST(ROW_NUMBER() OVER(
                                                         ORDER BY LD.EquipmentSetId) AS VARCHAR(10)), 10) AS VARCHAR(100)), 
                   LD.[EquipmentSetId]
            FROM [new].[MNTEquipmentSets] AS LD
                 INNER JOIN Tree ON Tree.EquipmentId = LD.FKParentEquipmentId
                                    AND LD.FKParentEquipmentId != LD.FKEquipmentId)
        SELECT ISNULL(ROW_NUMBER() OVER(
               ORDER BY T.[Path]), 0) AS OrderSeq, 
               REPLICATE('    ', T.[Level] - 1) + E.EquipmentName AS Levels, 
               --E.EquipmentId, 
               E.EquipmentName, 
               ES.EquipmentQuantity
        FROM Tree T
             INNER JOIN [new].[MNTEquipmentSets] ES ON T.EquipmentSetId = ES.EquipmentSetId
             INNER JOIN [new].[MNTEquipments] E ON T.EquipmentId = E.EquipmentId;