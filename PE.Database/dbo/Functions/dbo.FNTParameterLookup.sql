CREATE   FUNCTION [dbo].[FNTParameterLookup](@ParameterCode    NVARCHAR(50), 
                                                    @ParameterValueId BIGINT) RETURNS TABLE AS RETURN
SELECT SetupId, 
CASE WHEN ParameterCode='SG' THEN SteelgradeName END Steelgrade,
CASE WHEN ParameterCode='PS' THEN ProductCatalogueName END [Product Size],
CASE WHEN ParameterCode='HN' THEN HeatName END [Heat Number]

/*
1 [Steelgrade], 
        1 [Product Size], 
        1 [Work Order], 
        1 [Heat Number], 
        1 [Layout], 
        1 [Issue]
		*/
FROM 
STPSetups S
LEFT JOIN STPSetupParameters SP ON S.SetupId = SP.FKSetupId
INNER JOIN STPParameters P ON SP.FKParameterId = P.ParameterId
LEFT JOIN PRMSteelgrades SG ON SP.ParameterValueId = SG.SteelgradeId 
LEFT JOIN PRMHeats H ON SP.ParameterValueId = H.HeatId 
LEFT JOIN PRMProductCatalogue PC ON SP.ParameterValueId = PC.ProductCatalogueId