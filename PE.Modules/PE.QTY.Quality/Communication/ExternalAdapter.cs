using PE.Interfaces.Modules;
using PE.QTY.Base.Module.Communication;

namespace PE.QTY.Quality.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IQuality>, IQuality
  {
    #region ctor

    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion
  }
}
