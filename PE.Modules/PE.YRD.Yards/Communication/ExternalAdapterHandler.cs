using PE.BaseInterfaces.Managers.YRD;
using PE.YRD.Base.Module.Communication;

namespace PE.YRD.Yards.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    private readonly IBilletYardBaseManager _billetYardManager;
    private readonly IProductYardBaseManager _productYardManager;

    public ExternalAdapterHandler(IBilletYardBaseManager billetYardManager, IProductYardBaseManager productYardManager)
      : base(billetYardManager, productYardManager)
    {
    }
  }
}
