using Microsoft.Extensions.DependencyInjection;
using PE.BaseDbEntity.Providers;
using PE.BaseInterfaces.Managers.DBA;
using PE.BaseInterfaces.SendOffices.DBA;
using PE.DBA.Base.Managers;
using PE.DBA.Base.Module.Communication;
using SMF.Module.Core.Interfaces;

namespace PE.DBA.Base.Module
{
  public abstract class ProgramBase : IModule
  {
    public virtual void RegisterServices(ServiceCollection services)
    {
      services.AddSingleton(typeof(IContextProvider<>), typeof(DefaultContextProvider<>));

      services.AddSingleton<IDbAdapterBaseSendOffice, ModuleBaseSendOffice>();

      services.AddSingleton<IL3DBCommunicationBaseManager, L3DbCommunicationBaseManager>();

      services.AddSingleton<ModuleBaseExternalAdapterHandler>();
    }
  }
}
