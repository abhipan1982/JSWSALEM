using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PE.BaseInterfaces.Managers;
using PE.BaseInterfaces.Managers.L1A;
using PE.BaseInterfaces.SendOffices.L1A;
using PE.L1A.Base.Configuration;
using PE.L1A.Base.Handlers;
using PE.L1A.Base.Managers;
using PE.L1A.Base.Module.Communication;
using PE.L1A.Base.Providers.Abstract;
using PE.L1A.Base.Providers.Concrete;
using PE.L1A.L1Adapter.Managers.Concrete;
using SMF.Module.Core.Interfaces;

namespace PE.L1A.Base.Module
{
  public abstract class ProgramBase : IModule
  {
    public virtual void RegisterServices(ServiceCollection services)
    {
      services.AddSingleton<IL1MillControlDataManagerBase, L1MillControlDataManagerBase>();
      services.AddSingleton<IL1SignalSendOfficeBase, ModuleBaseSendOffice>();
      services.AddSingleton<IPlantConfigurationManagerBase, PlantConfigurationManagerBase>();
      services.AddSingleton<IL1MeasurementStorageProviderBase, L1MongoStorageProviderBase>();
      services.AddSingleton<IConfigurationStorageProviderBase, ConfigurationStorageProviderBase>();
      services.AddSingleton<IMeasurementStorageManagerBase, MeasurementStorageManagerBase>();
      services.AddSingleton<IMeasurementsDatabaseInstanceSettings>((MeasurementsDatabaseSection)ConfigurationManager
        .GetSection("MeasurementsDatabaseSection"));

      services.AddSingleton<IBypassStorageProviderBase, BypassStorageProviderBase>();
      services.AddSingleton<BypassHandler>();
      services.AddSingleton<IBypassManagerBase, BypassManagerBase>();

      services.AddSingleton<L1SignalManagerBase>();
      services.AddSingleton<IL1SignalManagerBase>((r) => r.GetRequiredService<L1SignalManagerBase>());
      services.AddSingleton<IL1OPCSignalManagerBase>((r) => r.GetRequiredService<L1SignalManagerBase>());

      services.AddSingleton<ModuleBaseExternalAdapterHandler>();
    }
  }
}
