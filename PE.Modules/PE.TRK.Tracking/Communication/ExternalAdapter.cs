using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using PE.Interfaces.Modules;
using PE.TRK.Base.Module.Communication;
using SMF.Core.DC;

namespace PE.TRK.Tracking.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<ITracking>, ITracking
  {
    #region ctor

    public ExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base(handler)
    {
    }
    #endregion
  }
}
