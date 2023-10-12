using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.L1A;
using PE.L1A.Base.Module.Communication;
using PE.L1A.Base.Providers.Abstract;
using SMF.Core.DC;
using PE.Interfaces.Managers;

namespace PE.L1A.L1Adapter.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    #region members
   
    private readonly ITrackingManager _trackingManager;

    #endregion


    //AP on 07072023
    private readonly ITcpTelegramCommunicationManager _tcpTelegramCommunicationManager;

    public ExternalAdapterHandler(IL1SignalManagerBase l1SignalManager, IMeasurementStorageManagerBase measurementStorageManager,
      IL1MillControlDataManagerBase millControlDataManager, IBypassManagerBase bypassManager) 
      : base(l1SignalManager, measurementStorageManager, millControlDataManager, bypassManager)
    {
    }

    //AP on 07072023
    internal Task<DataContractBase> TcpTelegramSendAsync(DataContractBase message)
    {
      return _tcpTelegramCommunicationManager.ProcessTcpTelegramSendAsync(message);
    }

    //AP on 15072023
    internal async Task<DataContractBase> ProcessChargeAtGrid(DataContractBase dc)
    {
      await _trackingManager.ProcessChargeAtGrid(dc);

      return new DataContractBase();
    }

    //AP on 12092023  Data contract accept from TCP Proxy Module
    internal async Task<DataContractBase> ProcessTestTelegramAsync(DataContractBase dc)
    {
      await _trackingManager.ProcessChargeAtGrid(dc);

      return new DataContractBase();
    }    
  }
}
