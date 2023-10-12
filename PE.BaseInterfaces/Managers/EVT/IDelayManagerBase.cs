using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.EVT
{
  public interface IDelayManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> UpdateDelayAsync(DCDelay delay);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CreateDelayAsync(DCDelay delay);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> DivideDelayAsync(DCDelayToDivide delayToDivide);

    void SetCurrentMode(int currentMode);
  }
}
