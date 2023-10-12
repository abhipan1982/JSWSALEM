using PE.MDA.Base.Interfaces;
using PE.Models.DataContracts.Internal.MDA;
using PE.Models.DataContracts.Internal.MDB;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;

namespace PE.MDA.Base.Managers
{
  public class HelloManagerBase : BaseManager, IHelloManagerBase
  {
    public HelloManagerBase(IModuleInfo moduleInfo) : base(moduleInfo)
    {
    }

    public virtual Task<DCAckMessage> ProcessHello(DCHelloMessage message)
    {
      var ack = new DCAckMessage { Ok = true };

      NotificationController.Info(message.Text.ToString());

      return Task.FromResult(ack);
    }
  }
}
