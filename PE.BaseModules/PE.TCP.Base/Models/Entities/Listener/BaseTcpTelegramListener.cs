using PE.TCP.Base.Models.Configuration.Listener;

namespace PE.TCP.Base.Models.Entities.Listener
{
  public class BaseTcpTelegramListener
  {
    public string Assembly;
    public string Description;
    public int TelegramId;
    public int TelegramLength;
    public string TelegramType;

    public BaseTcpTelegramListener(ListenerTelegramElement listenerTelegramElement)
    {
      TelegramId = listenerTelegramElement.TelegramId;
      Description = listenerTelegramElement.Description;
      TelegramLength = listenerTelegramElement.TelegramLength;
      TelegramType = listenerTelegramElement.TelegramType;
      Assembly = listenerTelegramElement.Assembly;
    }
  }
}
