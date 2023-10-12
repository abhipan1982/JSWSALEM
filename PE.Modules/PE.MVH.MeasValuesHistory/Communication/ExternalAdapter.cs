using PE.Interfaces.Modules;
using PE.MVH.Base.Module.Communication;

namespace PE.MVH.MeasValuesHistory.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IMVHistory>, IMVHistory
  {
    private new readonly ExternalAdapterHandler _handler;

    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
      _handler = handler;
    }

    #endregion
  }
}
