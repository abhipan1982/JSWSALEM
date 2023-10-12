using System.Timers;
using PE.BaseInterfaces.Managers;
using PE.BaseInterfaces.Managers.L1A;
using PE.Helpers;
using PE.L1A.Base.Providers.Abstract;
using SMF.Module.Core;

namespace PE.L1A.Base.Module
{
  public class WorkerBase : BaseWorker
  {
    protected readonly IL1MeasurementStorageProviderBase StorageProvider;
    protected readonly IConfigurationStorageProviderBase ConfigurationStorageProvider;

    protected readonly IPlantConfigurationManagerBase ConfigurationManager;
    protected readonly IBypassManagerBase BypassManager;
    protected readonly IL1SignalManagerBase L1SignalManager;
    protected readonly IL1OPCSignalManagerBase L1OPCSignalManager;
    protected readonly IL1MillControlDataManagerBase L1MillControlDataManager;

    #region ctor

    public WorkerBase(IL1MeasurementStorageProviderBase storageProvider,
      IConfigurationStorageProviderBase configurationStorageProvider,
      IPlantConfigurationManagerBase configurationManager,
      IL1SignalManagerBase l1SignalManager,
      IL1MillControlDataManagerBase l1MillControlDataManager,
      IL1OPCSignalManagerBase l1OPCSignalManager,
      IBypassManagerBase bypassManager)
    {
      ConfigurationManager = configurationManager;
      BypassManager = bypassManager;
      L1SignalManager = l1SignalManager;
      L1OPCSignalManager = l1OPCSignalManager;
      L1MillControlDataManager = l1MillControlDataManager;
      StorageProvider = storageProvider;
      ConfigurationStorageProvider = configurationStorageProvider;
    }

    #endregion ctor

    #region module calls
    public override void ModuleInitialized(object sender, ModuleInitEventArgs e)
    {
      base.ModuleInitialized(sender, e);

      Init();
    }

    public override void ModuleStarted(object sender, ModuleStartEventArgs e)
    {
      base.ModuleStarted(sender, e);

      ReadConfiguration();
    }

    public override void ModuleClosed(object sender, ModuleCloseEventArgs e)
    {
      base.ModuleClosed(sender, e);

      L1SignalManager.Stop();
    }

    public override void TimerMethod(object sender, ElapsedEventArgs e)
    {
      TaskHelper.FireAndForget(() => StorageProvider.ProcessSaveMeasurements()
      .GetAwaiter()
      .GetResult());
    }
    #endregion

    #region protected methods
    protected virtual void Init()
    {
      TaskHelper.FireAndForget(() => BypassManager.Init(), "Something went wrong while init bypass manager");

      L1OPCSignalManager.Init();

      L1MillControlDataManager.Init();

      ConfigurationStorageProvider.IsInitialized = true;
    }    

    protected virtual void ReadConfiguration()
    {
      ConfigurationManager.ReadPlantConfiguration();
    }
    #endregion
  }
}
