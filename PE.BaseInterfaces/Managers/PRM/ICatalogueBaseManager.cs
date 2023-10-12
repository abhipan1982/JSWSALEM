using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.PRM;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.PRM
{
  public interface ICatalogueBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSteelgradeCatalogueAsync(DCSteelgrade steelgrade);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSteelgradeCatalogueAsync(DCSteelgrade steelgrade);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgrade);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateHeatAsync(DCHeat heat);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditHeatAsync(DCHeat heat);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue mCat);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateProductCatalogueAsync(DCProductCatalogue productCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateProductCatalogueAsync(DCProductCatalogue productCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue pCat);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateMaterialAsync(DCMaterial material);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateMaterialAsync(DCMaterial material);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateScrapGroupCatalogueAsync(DCScrapGroup scrapGroup);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateScrapGroupCatalogueAsync(DCScrapGroup scrapGroup);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteScrapGroupAsync(DCScrapGroup scrapGroup);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSteelFamilyCatalogueAsync(DCSteelFamily dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSteelFamilyCatalogueAsync(DCSteelFamily dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSteelFamilyAsync(DCSteelFamily dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditMaterialNumberAsync(DCWorkOrderMaterials workOrderMaterials);
  }
}
