using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.EVT
{
  public interface IDelayProcessManagerBase
  {
    Task<DataContractBase> ProcessHeadEnterAsync(DCDelayEvent dcDelayEvent);
    Task<DataContractBase> ProcessTailLeavesAsync(DCDelayEvent dcDelayEvent);
    Task<DataContractBase> CheckActiveDelayAfterShiftClose();
    void SetCurrentMode(int currentMode);
    void InitDelay();
    Task<DataContractBase> UpdateDelayStatusAsync(int? currentDelayMode = null, bool isEndOfWorkShop = false, bool isEndedByInactiveShift = false);
    void Stop();
  }
}
