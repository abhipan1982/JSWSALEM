using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.STP;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.STP
{
  public interface ISetupCalculationBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateSetupAsync(DCCommonSetupStructure message);
  }
}
