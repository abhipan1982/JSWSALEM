namespace PE.TCP.Base.Models.Entities.Sender
{
  public class TcpSender
  {
    public TcpSender(BaseTcpSender baseTcpSender, BaseTcpTelegramSender baseTcpTelegramSender)
    {
      BaseTcpSender = baseTcpSender;
      BaseTcpTelegramSender = baseTcpTelegramSender;
    }

    public BaseTcpSender BaseTcpSender { get; set; }
    public BaseTcpTelegramSender BaseTcpTelegramSender { get; set; }
  }
}
