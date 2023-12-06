using PE.BaseInterfaces.Managers.PRM;
using PE.PRM.Base.Module.Communication;
using PE.Interfaces.Managers.PRM;
using PE.PRM.ProdManager.Communication;
using PE.Models.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.PRM;
using SMF.Core.DC;
using System.Threading.Tasks;
using PE.Models.DataContracts.Internal.DBA;

namespace PE.PRM.ProdManager.Communication
{
  public class ExternalAdapterHandler :ModuleBaseExternalAdapterHandler
  {
    protected readonly ICatalogueManager CatalogueManager;
    protected readonly IProductManager ProductManager;
    protected readonly IWorkOrderManager WorkOrderManager;
    protected readonly IWorkOrderManager BatchDataManager;

    public ExternalAdapterHandler(ICatalogueManager catalogueManager, IProductManager productManager, IWorkOrderManager workOrderManager):base(workOrderManager, catalogueManager,productManager)
        
    {
      CatalogueManager = catalogueManager;
      ProductManager = productManager;
      WorkOrderManager = workOrderManager;
      BatchDataManager = workOrderManager;
    }
    

    public virtual Task<DataContractBase> CreateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue)
    {
      return CatalogueManager.CreateProductCatalogueEXTAsync(productCatalogue);
    }

    public virtual Task<DataContractBase> UpdateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue)
    {
      return CatalogueManager.UpdateProductCatalogueEXTAsync(productCatalogue);
    }

    public virtual Task<DataContractBase> DeleteProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue)
    {
      return CatalogueManager.DeleteProductCatalogueEXTAsync(productCatalogue);
    }

    public virtual Task<DataContractBase> UpdateSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade)
    {
      return CatalogueManager.UpdateSteelgradeCatalogueAsyncEXT(dcSteelgrade);
    }

    public virtual Task<DataContractBase> CreateSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade)
    {
      return CatalogueManager.CreateSteelgradeCatalogueAsyncEXT(dcSteelgrade);
    }


    public virtual Task<DataContractBase> DeleteSteelgradeAsyncEXT(DCSteelgradeEXT dcSteelgrade)
    {
      return CatalogueManager.DeleteSteelgradeAsyncEXT(dcSteelgrade);
    }


    public virtual Task<DataContractBase> UpdateSteelFamilyAsyncEXT(DCSteelFamilyEXT dc)
    {
      return CatalogueManager.UpdateSteelFamilyCatalogueAsyncEXT(dc);
    }

    public virtual Task<DataContractBase> CreateSteelFamilyAsyncEXT(DCSteelFamilyEXT dc)
    {
      return CatalogueManager.CreateSteelFamilyCatalogueAsyncEXT(dc);
    }


    public virtual Task<DataContractBase> DeleteSteelFamilyAsyncEXT(DCSteelFamilyEXT dc)
    {
      return CatalogueManager.DeleteSteelFamilyAsyncEXT(dc);
    }



    public virtual Task<DataContractBase> CreateHeatAsyncEXT(DCHeatEXT dcHeat)
    {
      return CatalogueManager.CreateHeatAsyncEXT(dcHeat);
    }



    public virtual Task<DataContractBase> EditHeatAsyncEXT(DCHeatEXT dcHeat)
    {
      return CatalogueManager.EditHeatAsyncEXT(dcHeat);
    }


    public virtual Task<DataContractBase> CreateWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder)
    {
      return WorkOrderManager.CreateWorkOrderAsyncEXT(dcWorkOrder);
    }



    public virtual Task<DataContractBase> UpdateWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder)
    {
      return WorkOrderManager.UpdateWorkOrderAsyncEXT(dcWorkOrder);
    }



    

    public virtual Task<DataContractBase> CreateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      return CatalogueManager.CreateMaterialCatalogueAsyncEXT(dcMaterialCatalogue);
    }

    public virtual Task<DataContractBase> UpdateMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      return CatalogueManager.UpdateMaterialCatalogueAsyncEXT(dcMaterialCatalogue);
    }

    public virtual Task<DataContractBase> DeleteMaterialCatalogueAsyncEXT(DCMaterialCatalogueEXT dcMaterialCatalogue)
    {
      return CatalogueManager.DeleteMaterialCatalogueAsyncEXT(dcMaterialCatalogue);
    }


    public virtual Task<DataContractBase> DeleteWorkOrderAsyncEXT(DCWorkOrderEXT dcWorkOrder)
    {
      return CatalogueManager.DeleteWorkOrderAsyncEXT(dcWorkOrder);
    }


    public virtual Task<DataContractBase> CreateMaterialAsyncEXT(DCMaterialEXT dcMaterial)
    {
      return CatalogueManager.CreateMaterialAsyncEXT(dcMaterial);
    }

    public virtual Task<DataContractBase> UpdateMaterialAsyncEXT(DCMaterialEXT dcMaterial)
    {
      return CatalogueManager.UpdateMaterialAsyncEXT(dcMaterial);
    }

    public virtual Task<DCBatchDataStatus> ProcessBatchDataAsync(DCL3L2BatchDataDefinition message)
    {
      return BatchDataManager.ProcessBatchDataAsync(message);
    }
  }
}
