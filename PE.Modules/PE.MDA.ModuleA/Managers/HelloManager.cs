using PE.MDA.Base.Managers;
using PE.Models.DataContracts.Internal.MDA;
using PE.Models.DataContracts.Internal.MDB;
using SMF.Core.Interfaces;
using SMF.Core.Notification;

namespace PE.MDA.ModuleA.Managers
{
  public class HelloManager : HelloManagerBase
  {
    private readonly IParameterService _parameterService;

    public HelloManager(IModuleInfo moduleInfo, IParameterService parameterService) : base(moduleInfo)
    {
      _parameterService = parameterService;
    }

    public override Task<DCAckMessage> ProcessHello(DCHelloMessage message)
    {
      var param = _parameterService.GetParameter("Sample param");
      NotificationController.Info($"Param name: {param.Name}, Param value: {param.ValueInt}");
      return base.ProcessHello(message);
    }
  }
}
