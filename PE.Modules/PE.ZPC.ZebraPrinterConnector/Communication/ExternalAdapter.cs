using PE.Interfaces.Modules;
using PE.ZPC.Base.Module.Communication;

namespace PE.ZPC.ZebraPrinterConnector.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IZebraPC>, IZebraPC
  {
    private readonly ExternalAdapterHandler _handler;

    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
      _handler = handler;
    }

    #endregion
  }
}
