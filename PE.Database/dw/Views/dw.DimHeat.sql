



CREATE         VIEW [dw].[DimHeat] AS 
/*
	SELECT * FROM dw.DimHeat
*/
SELECT ISNULL(CAST(DB_NAME() AS NVARCHAR(50)), 0) AS SourceName, 
          GETDATE() AS SourceTime, 
          ISNULL(ROW_NUMBER() OVER(
          ORDER BY H.HeatId), 0) AS DimHeatRow, 
          ISNULL(CAST(0 AS BIT), 0) AS DimHeatIsDeleted, 
		  CAST(HASHBYTES('MD5', 
			COALESCE(CAST(H.HeatId AS NVARCHAR), ';') + 
			COALESCE(CAST(H.FKSteelgradeId AS NVARCHAR), ';') + 
			COALESCE(CAST(H.FKHeatSupplierId AS NVARCHAR), ';') + 
			COALESCE(CAST(H.HeatName AS NVARCHAR), ';') + 
			COALESCE(CAST(H.HeatWeight AS NVARCHAR), ';') + 
			COALESCE(CAST(H.HeatCreatedTs AS NVARCHAR), ';')) AS VARBINARY(16)) AS DimHeatHash, 
          H.HeatId AS DimHeatKey, 
          H.FKSteelgradeId AS DimSteelgradeKey, 
          H.FKHeatSupplierId AS DimHeatSupplierKey, 
          H.HeatName, 
          H.HeatWeight, 
          H.HeatCreatedTs AS HeatCreated, 
          S.SteelgradeName, 
          HS.HeatSupplierName AS HeatSupplierName, 
          HS.HeatSupplierDescription AS HeatSupplierDescription, 
          HA.Fe AS HeatChFe, 
          HA.C AS HeatChC, 
          HA.Mn AS HeatChMn, 
          HA.Cr AS HeatChCr, 
          HA.Mo AS HeatChMo, 
          HA.V AS HeatChV, 
          HA.Ni AS HeatChNi, 
          HA.Co AS HeatChCo, 
          HA.Si AS HeatChSi, 
          HA.P AS HeatChP, 
          HA.S AS HeatChS, 
          HA.Cu AS HeatChCu, 
          HA.Nb AS HeatChNb, 
          HA.Al AS HeatChAl, 
          HA.N AS HeatChN, 
          HA.Ca AS HeatChCa, 
          HA.B AS HeatChB, 
          HA.Ti AS HeatChTi, 
          HA.Sn AS HeatChSn, 
          HA.O AS HeatChO, 
          HA.H AS HeatChH, 
          HA.W AS HeatChW, 
          HA.Pb AS HeatChPb, 
          HA.Zn AS HeatChZn, 
          HA.[As] AS HeatChAs, 
          HA.Mg AS HeatChMg, 
          HA.Sb AS HeatChSb, 
          HA.Bi AS HeatChBi, 
          HA.Ta AS HeatChTa, 
          HA.Zr AS HeatChZr, 
          HA.Ce AS HeatChCe, 
          HA.Te AS HeatChTe
   FROM dbo.PRMHeats AS H
        LEFT JOIN dbo.PRMSteelgrades S ON H.FKSteelgradeId = S.SteelgradeId
        LEFT OUTER JOIN dbo.PRMHeatChemicalAnalysis AS HA ON H.HeatId = HA.FKHeatId
        LEFT OUTER JOIN dbo.PRMHeatSuppliers AS HS ON H.FKHeatSupplierId = HS.HeatSupplierId;