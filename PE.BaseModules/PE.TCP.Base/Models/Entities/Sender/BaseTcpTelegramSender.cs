using PE.TCP.Base.Models.Configuration.Sender;

namespace PE.TCP.Base.Models.Entities.Sender
{
  public class BaseTcpTelegramSender
  {
    public int AcknowledgeLength;
    public string AcknowledgeTelegramType;
    public string Assembly;
    public string Description;
    public bool HasAcknowledge;
    public bool IsAsciiMessage;
    public int TelegramId;
    public int TelegramLength;
    public string TelegramType;

    public BaseTcpTelegramSender(SenderTelegramElement senderTelegramElement)
    {
      TelegramId = senderTelegramElement.TelegramId;
      Description = senderTelegramElement.Description;
      TelegramLength = senderTelegramElement.TelegramLength;
      TelegramType = senderTelegramElement.TelegramType;
      Assembly = senderTelegramElement.Assembly;
      HasAcknowledge = senderTelegramElement.HasAcknowledge;
      AcknowledgeTelegramType = senderTelegramElement.AcknowledgeTelegramType;
      AcknowledgeLength = senderTelegramElement.AcknowledgeLength;
      IsAsciiMessage = senderTelegramElement.IsAsciiMessage;
    }
  }
}
