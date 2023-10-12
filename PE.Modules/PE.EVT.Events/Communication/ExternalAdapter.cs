using System.Threading.Tasks;
using PE.EVT.Base.Module.Communication;
using PE.Interfaces.Modules;
using SMF.Core.DC;

namespace PE.EVT.Events.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IEvents>, IEvents
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
