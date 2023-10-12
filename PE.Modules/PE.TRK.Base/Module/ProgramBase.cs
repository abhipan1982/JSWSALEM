using Microsoft.Extensions.DependencyInjection;
using PE.BaseInterfaces.SendOffices.TRK;
using PE.TRK.Base.Handlers;
using PE.TRK.Base.Managers.Abstract;
using PE.TRK.Base.Managers.Concrete;
using PE.TRK.Base.Models.Configuration.Concrete;
using PE.TRK.Base.Module.Communication;
using PE.TRK.Base.Providers.Abstract;
using PE.TRK.Base.Providers.Concrete;
using SMF.Module.Core.Interfaces;

namespace PE.TRK.Base.Module
{
  public abstract class ProgramBase : IModule
  {
    public virtual void RegisterServices(ServiceCollection services)
    {
      services.AddSingleton<ITrackingConfigurationManagerBase, TrackingConfigurationManagerBase>();
      services.AddSingleton<ITrackingDispatcherManagerBase, TrackingDispatcherManagerBase>();
      services.AddSingleton<ITrackingEventStorageProviderBase, TrackingEventStorageProviderBase>();

      services.AddSingleton<ModuleBaseSendOffice>();
      services.AddSingleton<ITrackingProcessMeasurementsSendOffice>((r) => r.GetRequiredService<ModuleBaseSendOffice>());
      services.AddSingleton<ITrackingHmiSendOfficeBase>((r) => r.GetRequiredService<ModuleBaseSendOffice>());
      services.AddSingleton<ITrackingFurnaceSendOffice>((r) => r.GetRequiredService<ModuleBaseSendOffice>());
      services.AddSingleton<ITrackingProcessMaterialEventSendOfficeBase>((r) => r.GetRequiredService<ModuleBaseSendOffice>());
      services.AddSingleton<ITrackingGetNdrMeasurementSendOfficeBase>((r) => r.GetRequiredService<ModuleBaseSendOffice>());
      services.AddSingleton<ITrackingProcessQualityExpertTriggersSendOffice>((r) => r.GetRequiredService<ModuleBaseSendOffice>());
      services.AddSingleton<ITrackingLabelPrinterSendOffice>((r) => r.GetRequiredService<ModuleBaseSendOffice>());
      services.AddSingleton<ITrackingL1AdapterSendOfficeBase>((r) => r.GetRequiredService<ModuleBaseSendOffice>());

      services.AddSingleton<TrackingStorageProviderBase>();
      services.AddSingleton<ITrackingStorageProviderBase>((r) => r.GetRequiredService<TrackingStorageProviderBase>());

      services.AddSingleton<TrackingManagerBase>();
      services.AddSingleton<ITrackingManagerBase>((r) => r.GetRequiredService<TrackingManagerBase>());

      services.AddSingleton<ITrackingEventHandlingManagerBase, TrackingEventHandlingManagerBase>();
      services.AddSingleton<ITrackingCtrMaterialProviderBase, TrackingCtrMaterialProviderBase>();
      services.AddSingleton<ITrackingMaterialCutProviderBase, TrackingMaterialCutProviderBase>();
      services.AddSingleton<ITrackingMaterialProcessingProviderBase, TrackingMaterialProcessingProviderBase>();

      services.AddSingleton<TrackingHandlerBase>();
      services.AddSingleton<TrackingRawMaterialHandlerBase>();
      services.AddSingleton<ModuleBaseExternalAdapterHandler>();

      services.AddSingleton<StandardMessageToL1>();
    }
  }
}
