using SMF.Core.DC;
using System.Threading.Tasks;

namespace PE.BaseInterfaces.Managers.L1A
{
  public interface IL1SignalManagerBase
  {
    Task<DataContractBase> ResendTrackingPointSignals();

    void Stop();
  }
}
