using PE.BaseInterfaces.Managers.PPL;
using PE.PPL.Base.Module.Communication;

namespace PE.PPL.ProdPlaning.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    private readonly IScheduleBaseManager _scheduleManager;

    public ExternalAdapterHandler(IScheduleBaseManager scheduleManager)
      : base(scheduleManager)
    {
      _scheduleManager = scheduleManager;
    }
  }
}
