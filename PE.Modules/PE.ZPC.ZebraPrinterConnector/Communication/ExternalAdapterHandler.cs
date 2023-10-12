using PE.BaseInterfaces.Managers.ZPC;
using PE.ZPC.Base.Module.Communication;

namespace PE.ZPC.ZebraPrinterConnector.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {

    public ExternalAdapterHandler(IZebraConnectionManagerBase zebraConnectionManager) : base(zebraConnectionManager)
    {
    }
  }
}
