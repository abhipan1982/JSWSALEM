using Microsoft.Extensions.DependencyInjection;
using PE.MDB.Base.Module.Communication;
using SMF.Module.Core.Interfaces;

namespace PE.MDB.Base.Module
{
  public class ProgramBase : IModule
  {
    public virtual void RegisterServices(ServiceCollection services)
    {
      services.AddSingleton<ModuleBaseSendOffice>();
    }
  }
}
