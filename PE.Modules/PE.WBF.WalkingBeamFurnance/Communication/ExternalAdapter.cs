using PE.Interfaces.Modules;
using PE.WBF.Base.Module.Communication;

namespace PE.WBF.WalkingBeamFurnance.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IWalkingBeamFurnace>, IWalkingBeamFurnace
  {
    #region ctor

    public ExternalAdapter(ModuleBaseExternalAdapterHandler handler) : base(handler)
    {
    }

    #endregion

  }
}
