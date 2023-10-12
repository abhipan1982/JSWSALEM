using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.STP;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.L1A
{
  public interface IL1SetupManager : IManagerBase
  {
    Task InitAsync();
    void Close();
    Task<DataContractBase> SendSetupToL1Async(DCCommonSetupStructure message);
    Task<DataContractBase> RequestSetupFromL1Async(DCCommonSetupStructure message);
  }
}
