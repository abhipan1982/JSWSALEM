using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.RLS;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.RLS
{
  public interface IRollSetHistoryBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateGroovesToRollSetAsync(DCRollSetGrooveSetup dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateGroovesDataToRollSetAsync(DCRollSetGrooveSetup dc);
  }
}
