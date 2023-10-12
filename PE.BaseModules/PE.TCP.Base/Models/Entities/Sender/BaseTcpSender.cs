using PE.TCP.Base.Models.Configuration.Sender;

namespace PE.TCP.Base.Models.Entities.Sender
{
  public class BaseTcpSender
  {
    public int Alive;
    public int Alivecycle;
    public int AliveId;
    public int AliveLength;
    public int AliveOffset;
    public string Ip;
    public int Port;
    public int TelegramIdLength;
    public int TelegramIdOffset;

    public BaseTcpSender(SenderElement senderElement)
    {
      Ip = senderElement.Ip;
      Port = senderElement.Port;
      TelegramIdLength = senderElement.TelegramIdLength;
      TelegramIdOffset = senderElement.TelegramIdOffset;
      Alive = senderElement.Alive;
      Alivecycle = senderElement.AliveCycle;
      AliveOffset = senderElement.AliveOffset;
      AliveId = senderElement.AliveId;
      AliveLength = senderElement.AliveLength;
    }
  }
}
