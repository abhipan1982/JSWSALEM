using System.Threading.Tasks;
using PE.L1A.Base.Module.Communication;
using PE.Interfaces.Modules;
using SMF.Core.DC;

namespace PE.L1A.L1Adapter.Communication
{
  public class ExternalAdapter : ModuleBaseExternalAdapter<IL1Adapter>, IL1Adapter
  {
    //AP on 08072023
    private readonly ExternalAdapterHandler _handler;
    #region ctor
    public ExternalAdapter(ExternalAdapterHandler handler) : base(handler)
    {
      _handler= handler;
    }

    #endregion

    //AP on 08072023
    public Task<DataContractBase> ChargeAtGrid1(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessChargeAtGrid, dc);
    }

    //AP on 12092023
    public Task<DataContractBase> ProcessTestTelegramAsync(DataContractBase dc)
    {
      return HandleIncommingMethod(_handler.ProcessTestTelegramAsync, dc);
    }
  }
}
