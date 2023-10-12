


CREATE     VIEW [dw].[DimSteelgrade] AS 
/*
	SELECT * FROM dw.DimSteelgrade
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY S.SteelgradeId), 0) AS DimSteelgradeRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimSteelgradeIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(S.SteelgradeId AS NVARCHAR), ';') + 
			COALESCE(CAST(S.FKParentSteelgradeId AS NVARCHAR), ';') + 
			COALESCE(CAST(S.FKSteelGroupId AS NVARCHAR), ';') + 
			COALESCE(CAST(S.FKScrapGroupId AS NVARCHAR), ';') + 
			COALESCE(CAST(S.SteelgradeCode AS NVARCHAR), ';') + 
			COALESCE(CAST(S.SteelgradeName AS NVARCHAR), ';') + 
			COALESCE(CAST(S.SteelgradeDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(S.CustomCode AS NVARCHAR), ';') + 
			COALESCE(CAST(S.CustomName AS NVARCHAR), ';') + 
			COALESCE(CAST(S.CustomDescription AS NVARCHAR), ';') + 
			COALESCE(CAST(S.Density AS NVARCHAR), ';') + 
			COALESCE(CAST(SG.SteelGroupCode AS NVARCHAR), ';') + 
			COALESCE(CAST(SG.SteelGroupName AS NVARCHAR), ';') + 
			COALESCE(CAST(SG.SteelGroupDescription AS NVARCHAR), ';') +
			COALESCE(CAST(SCG.ScrapGroupCode AS NVARCHAR), ';') + 
			COALESCE(CAST(SCG.ScrapGroupName AS NVARCHAR), ';') + 
			COALESCE(CAST(SCG.ScrapGroupDescription AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimSteelgradeHash, 
          S.SteelgradeId AS DimSteelgradeKey, 
          S.FKParentSteelgradeId AS DimSteelgradeKeyParent, 
          S.FKSteelGroupId AS DimSteelGroupKey, 
          S.FKScrapGroupId AS DimScrapGroupKey, 
          S.SteelgradeCode, 
          S.SteelgradeName, 
          S.SteelgradeDescription, 
          S.CustomCode AS SteelgradeCustomCode, 
          S.CustomName AS SteelgradeCustomName, 
          S.CustomDescription AS SteelgradeCustomDescription, 
          S.Density AS SteelgradeDensity, 
          SG.SteelGroupCode, 
          SG.SteelGroupName, 
          SG.SteelGroupDescription, 
          SCG.ScrapGroupCode, 
          SCG.ScrapGroupName, 
          SCG.ScrapGroupDescription, 
          SP.SteelgradeCode AS SteelgradeCodeParent, 
          SP.SteelgradeName AS SteelgradeNameParent, 
          SP.SteelgradeDescription AS SteelgradeDescriptionParent, 
          SCC.FeMin AS SteelgradeFeMin, 
          SCC.FeMax AS SteelgradeFeMax, 
          SCC.CMin AS SteelgradeCMin, 
          SCC.CMax AS SteelgradeCMax, 
          SCC.MnMin AS SteelgradeMnMin, 
          SCC.MnMax AS SteelgradeMnMax, 
          SCC.CrMin AS SteelgradeCrMin, 
          SCC.CrMax AS SteelgradeCrMax, 
          SCC.MoMin AS SteelgradeMoMin, 
          SCC.MoMax AS SteelgradeMoMax, 
          SCC.VMin AS SteelgradeVMin, 
          SCC.VMax AS SteelgradeVMax, 
          SCC.NiMin AS SteelgradeNiMin, 
          SCC.NiMax AS SteelgradeNiMax, 
          SCC.CoMin AS SteelgradeCoMin, 
          SCC.CoMax AS SteelgradeCoMax, 
          SCC.SiMin AS SteelgradeSiMin, 
          SCC.SiMax AS SteelgradeSiMax, 
          SCC.PMin AS SteelgradePMin, 
          SCC.PMax AS SteelgradePMax, 
          SCC.SMin AS SteelgradeSMin, 
          SCC.SMax AS SteelgradeSMax, 
          SCC.CuMin AS SteelgradeCuMin, 
          SCC.CuMax AS SteelgradeCuMax, 
          SCC.NbMin AS SteelgradeNbMin, 
          SCC.NbMax AS SteelgradeNbMax, 
          SCC.AlMin AS SteelgradeAlMin, 
          SCC.AlMax AS SteelgradeAlMax, 
          SCC.NMin AS SteelgradeNMin, 
          SCC.NMax AS SteelgradeNMax, 
          SCC.CaMin AS SteelgradeCaMin, 
          SCC.CaMax AS SteelgradeCaMax, 
          SCC.BMin AS SteelgradeBMin, 
          SCC.BMax AS SteelgradeBMax, 
          SCC.TiMin AS SteelgradeTiMin, 
          SCC.TiMax AS SteelgradeTiMax, 
          SCC.SnMin AS SteelgradeSnMin, 
          SCC.SnMax AS SteelgradeSnMax, 
          SCC.OMin AS SteelgradeOMin, 
          SCC.OMax AS SteelgradeOMax, 
          SCC.HMin AS SteelgradeHMin, 
          SCC.HMax AS SteelgradeHMax, 
          SCC.WMin AS SteelgradeWMin, 
          SCC.WMax AS SteelgradeWMax, 
          SCC.PbMin AS SteelgradePbMin, 
          SCC.PbMax AS SteelgradePbMax, 
          SCC.ZnMin AS SteelgradeZnMin, 
          SCC.ZnMax AS SteelgradeZnMax, 
          SCC.AsMin AS SteelgradeAsMin, 
          SCC.AsMax AS SteelgradeAsMax, 
          SCC.MgMin AS SteelgradeMgMin, 
          SCC.MgMax AS SteelgradeMgMax, 
          SCC.SbMin AS SteelgradeSbMin, 
          SCC.SbMax AS SteelgradeSbMax, 
          SCC.BiMin AS SteelgradeBiMin, 
          SCC.BiMax AS SteelgradeBiMax, 
          SCC.TaMin AS SteelgradeTaMin, 
          SCC.TaMax AS SteelgradeTaMax, 
          SCC.ZrMin AS SteelgradeZrMin, 
          SCC.ZrMax AS SteelgradeZrMax, 
          SCC.CeMin AS SteelgradeCeMin, 
          SCC.CeMax AS SteelgradeCeMax, 
          SCC.TeMin AS SteelgradeTeMin, 
          SCC.TeMax AS SteelgradeTeMax
   FROM dbo.PRMSteelgrades AS S
        LEFT JOIN dbo.PRMSteelGroups SG ON S.FKSteelGroupId = SG.SteelGroupId
        LEFT JOIN dbo.PRMScrapGroups SCG ON S.FKScrapGroupId = SCG.ScrapGroupId
        LEFT JOIN dbo.PRMSteelgradeChemicalCompositions SCC ON S.SteelgradeId = SCC.FKSteelgradeId
        LEFT JOIN dbo.PRMSteelgrades SP ON S.FKParentSteelgradeId = SP.SteelgradeId;