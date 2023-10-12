using PE.BaseInterfaces.Managers;
using PE.BaseInterfaces.Managers.L1A;
using PE.BaseInterfaces.SendOffices.L1A;
using PE.L1A.Base.Module;
using PE.L1A.Base.Providers.Abstract;
using SMF.Core.Interfaces;

namespace PE.L1A.L1Adapter.Module
{
  public class Worker : WorkerBase
  {
    public Worker(IL1MeasurementStorageProviderBase storageProvider,
      IConfigurationStorageProviderBase configurationStorageProvider,
      IPlantConfigurationManagerBase configurationManager,
      IL1SignalManagerBase l1SignalManager,
      IL1MillControlDataManagerBase l1MillControlDataManager,
      IL1OPCSignalManagerBase l1OPCSignalManager,
      IBypassManagerBase bypassManager)
      : base(storageProvider, configurationStorageProvider, configurationManager, l1SignalManager, l1MillControlDataManager, l1OPCSignalManager, bypassManager)
    {
    }
  }
}
