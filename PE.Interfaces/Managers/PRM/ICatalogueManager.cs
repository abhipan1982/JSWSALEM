using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Managers;
using PE.BaseModels.DataContracts.Internal.PRM;
using SMF.Core.DC;
using PE.Models;
using PE.Models.DataContracts.Internal.PRM;
using PE.BaseInterfaces.Managers.PRM;

namespace PE.Interfaces.Managers.PRM
{
  public interface ICatalogueManager : ICatalogueBaseManager
  {
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateSteelgradeCatalogueAsync(DCSteelgrade steelgrade);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateSteelgradeCatalogueAsync(DCSteelgrade steelgrade);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> DeleteSteelgradeAsync(DCSteelgrade steelgrade);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateHeatAsync(DCHeat heat);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> EditHeatAsync(DCHeat heat);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateMaterialCatalogueAsync(DCMaterialCatalogue billetCatalogue);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> DeleteMaterialCatalogueAsync(DCMaterialCatalogue mCat);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue);//@Av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue);//@Av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSteelgradeCatalogueAsyncEXT(DCSteelgradeEXT dcSteelgrade);//@Av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSteelgradeCatalogueAsyncEXT(DCSteelgradeEXT dcSteelgrade);//@Av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade);//@Av

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateSteelFamilyCatalogueAsyncEXT(DCSteelFamilyEXT dc);//@av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateSteelFamilyCatalogueAsyncEXT(DCSteelFamilyEXT dc);//@av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteSteelFamilyAsyncEXT(DCSteelFamilyEXT dc);//@av



    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateHeatAsyncEXT(DCHeatEXT dc);//@av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> EditHeatAsyncEXT(DCHeatEXT dc);//@av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateMaterialAsyncEXT(DCMaterialEXT dcMaterial);//@av

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue);//@av


    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue);//@av

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue);//@av



    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DeleteWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder);//@av



    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateMaterialAsyncEXT(DCMaterialEXT dcMaterial);//@av



   


















    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> DeleteProductCatalogueAsync(DCProductCatalogue pCat);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> DeleteWorkOrderAsync(DCWorkOrder workOrder);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateMaterialAsync(DCMaterial material);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateMaterialAsync(DCMaterial material);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateScrapGroupCatalogueAsync(DCScrapGroup scrapGroup);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateScrapGroupCatalogueAsync(DCScrapGroup scrapGroup);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> DeleteScrapGroupAsync(DCScrapGroup scrapGroup);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateSteelFamilyCatalogueAsync(DCSteelFamily dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateSteelFamilyCatalogueAsync(DCSteelFamily dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> DeleteSteelFamilyAsync(DCSteelFamily dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> EditMaterialNumberAsync(DCWorkOrderMaterials workOrderMaterials);
  }
}
