using PE.BaseInterfaces.Managers.PRM;
using PE.PRM.Base.Module.Communication;

namespace PE.PRM.ProdManager.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    public ExternalAdapterHandler(IWorkOrderBaseManager workOrderManager,
      ICatalogueBaseManager catalogueManager,
      IProductBaseManager productManager)
      : base(workOrderManager, catalogueManager, productManager)
    {
    }
  }
}
