using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.L1A;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.L1A
{
  public interface IL1MillControlDataManagerBase
  {
    void Init();
    Task<DataContractBase> SendMillControlDataMessage(DCMillControlMessage dc);
  }
}
