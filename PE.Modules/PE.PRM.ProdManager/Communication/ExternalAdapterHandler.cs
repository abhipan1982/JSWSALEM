using PE.BaseInterfaces.Managers.PRM;
using PE.PRM.Base.Module.Communication;
using PE.Interfaces.Managers.PRM;
using PE.PRM.ProdManager.Communication;
using PE.Models.DataContracts.Internal.PRM;
using SMF.Core.DC;
using System.Threading.Tasks;

namespace PE.PRM.ProdManager.Communication
{
  public class ExternalAdapterHandler :ModuleBaseExternalAdapterHandler
  {
    protected readonly ICatalogueManager CatalogueManager;
    protected readonly IProductManager ProductManager;
    protected readonly IWorkOrderManager WorkOrderManager;

    public ExternalAdapterHandler(ICatalogueManager catalogueManager, IProductManager productManager, IWorkOrderManager workOrderManager):base(workOrderManager, catalogueManager,productManager)
        
    {
      CatalogueManager = catalogueManager;
      ProductManager = productManager;
      WorkOrderManager = workOrderManager;
    }
    

    public virtual Task<DataContractBase> CreateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue)
    {
      return CatalogueManager.CreateProductCatalogueEXTAsync(productCatalogue);
    }

    public virtual Task<DataContractBase> UpdateProductCatalogueEXTAsync(DCProductCatalogueEXT productCatalogue)
    {
      return CatalogueManager.UpdateProductCatalogueEXTAsync(productCatalogue);
    }

  }
}
