using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface ICustomRollGroovesBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SelectActiveGrooves(DCRollSetGrooveSetup dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AccumulateRollsUsageAsync(DCRollsAccu dc);
  }
}
