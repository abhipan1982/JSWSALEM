namespace PE.TCP.Base.Models.Entities.Listener
{
  public class TcpListener
  {
    public TcpListener(BaseTcpListener baseTcpListener, BaseTcpTelegramListener baseTcpTelegramListener)
    {
      BaseTcpListener = baseTcpListener;
      BaseTcpTelegramListener = baseTcpTelegramListener;
    }

    public BaseTcpListener BaseTcpListener { get; set; }
    public BaseTcpTelegramListener BaseTcpTelegramListener { get; set; }
  }
}
