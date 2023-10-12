using System.Timers;
using PE.MDB.Base.Module.Communication;
using PE.Models.DataContracts.Internal.MDA;
using SMF.Core.Notification;
using SMF.Module.Core;

namespace PE.MDB.Base.Module
{
  public class WorkerBase : BaseWorker
  {
    private readonly ModuleBaseSendOffice _sendOfficeBase;

    public WorkerBase(ModuleBaseSendOffice sendOfficeBase)
    {
      _sendOfficeBase = sendOfficeBase;
    }

    public override void TimerMethod(object sender, ElapsedEventArgs e)
    {
      var message = new DCHelloMessage { Text = "Hello from module B" };
      var ack = _sendOfficeBase.SendHello(message).Result;
      NotificationController.Info($"Received ack: {ack.DataConctract.Ok}");
    }
  }
}
