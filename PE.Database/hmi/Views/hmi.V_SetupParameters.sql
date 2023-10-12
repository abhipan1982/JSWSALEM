



CREATE       VIEW [hmi].[V_SetupParameters] AS
     WITH CTE
          AS (SELECT ISNULL(ROW_NUMBER() OVER(
                     ORDER BY ST.SetupTypeId, 
                              S.SetupId), 0) AS OrderSeq, 
                     ST.SetupTypeId, 
                     ST.SetupTypeCode, 
                     ST.SetupTypeName, 
					 ST.IsActive,
					 ST.IsSteelgradeRelated,
                     S.SetupId, 
                     S.SetupCode, 
                     S.SetupName, 
					 --C.ConfigurationId,
					 --C.ConfigurationName,
					 --C.ConfigurationVersion,
					 --SC.SetupConfigurationLastSentTs,
                     P.ParameterCode, 
                     P.ParameterName, 
                     SP.ParameterValueId,
                     CASE P.ParameterCode
                         WHEN 'SG'
                         THEN SG.SteelgradeCode
                         WHEN 'PSG'
                         THEN PSG.SteelgradeCode
                         WHEN 'PS'
                         THEN PC.ProductCatalogueName
                         WHEN 'PPS'
                         THEN PPC.ProductCatalogueName
                         WHEN 'WO'
                         THEN WorkOrderName
                         WHEN 'HN'
                         THEN HeatName
                         WHEN 'L'
                         THEN L.Layout
                         WHEN 'PL'
                         THEN PL.Layout
                         WHEN 'I'
                         THEN IU.Issue
                         WHEN 'NAME'
                         THEN ST.SetupTypeName                     
						 END ParameterValue, 
                     SP.IsRequired AS IsRequiredParameter
              FROM [dbo].[STPSetups] S
                   LEFT JOIN [dbo].[STPSetupTypes] ST ON S.FKSetupTypeId = ST.SetupTypeId
				   --LEFT JOIN [dbo].[STPSetupConfigurations] SC 
				   --INNER JOIN [dbo].[STPConfigurations] C ON SC.FKConfigurationId = C.ConfigurationId ON S.SetupId = SC.FKSetupId
                   LEFT JOIN [dbo].[STPSetupParameters] SP
                   INNER JOIN [dbo].[STPParameters] P ON SP.FKParameterId = P.ParameterId
                   LEFT JOIN PRMSteelgrades SG ON SP.ParameterValueId = SG.SteelgradeId
                   LEFT JOIN PRMSteelgrades PSG ON SP.ParameterValueId = PSG.SteelgradeId
                   LEFT JOIN PRMProductCatalogue PC ON SP.ParameterValueId = PC.ProductCatalogueId
                   LEFT JOIN PRMProductCatalogue PPC ON SP.ParameterValueId = PPC.ProductCatalogueId
                   LEFT JOIN PRMWorkOrders WO ON SP.ParameterValueId = WO.WorkOrderId
                   LEFT JOIN PRMHeats H ON SP.ParameterValueId = H.HeatId
                   LEFT JOIN STPLayouts L ON SP.ParameterValueId = L.LayoutId
                   LEFT JOIN STPLayouts PL ON SP.ParameterValueId = PL.LayoutId
                   LEFT JOIN STPIssues IU ON SP.ParameterValueId = IU.IssueId ON SP.FKSetupId = S.SetupId)
          SELECT ISNULL(ROW_NUMBER() OVER(
                 ORDER BY SetupTypeId, 
                          SetupId), 0) AS OrderSeq, 
                 SetupId, 
                 SetupName, 
                 SetupTypeId, 
                 SetupTypeCode, 
                 SetupTypeName, 
				 --ConfigurationName,
				 --ConfigurationVersion,
				 --SetupConfigurationLastSentTs,
				 IsActive,
				 IsSteelgradeRelated,
                 MAX([Steelgrade]) [Steelgrade], 
                 MAX([Product Size]) [Product Size], 
                 MAX([Work Order]) [Work Order], 
                 MAX([Heat Number]) [Heat Number], 
                 MAX([Layout]) [Layout], 
                 MAX([Issue]) [Issue], 
                 MAX([Previous Steelgrade]) [Previous Steelgrade], 
                 MAX([Previous Product Size]) [Previous Product Size], 
                 MAX([Previous Layout]) [Previous Layout],
				 MAX([Parameter Name]) [Parameter Name]
          FROM CTE PIVOT(MAX(ParameterValue) FOR ParameterName IN([Steelgrade], 
                                                                  [Product Size], 
                                                                  [Work Order], 
                                                                  [Heat Number], 
                                                                  [Layout], 
                                                                  [Issue], 
                                                                  [Previous Steelgrade], 
                                                                  [Previous Product Size], 
                                                                  [Previous Layout],
																  [Parameter Name])) AS PT
          GROUP BY SetupId, 
                   SetupName, 
                   SetupTypeId, 
                   SetupTypeCode, 
                   SetupTypeName,
				   --ConfigurationName,
				   --ConfigurationVersion,
				   --SetupConfigurationLastSentTs,
				   IsActive,
				   IsSteelgradeRelated