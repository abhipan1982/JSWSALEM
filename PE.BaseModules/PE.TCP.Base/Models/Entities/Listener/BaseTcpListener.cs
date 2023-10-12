using System.Collections;
using PE.TCP.Base.Models.Configuration.Listener;
using SMF.Core.Notification;

namespace PE.TCP.Base.Models.Entities.Listener
{
  public class BaseTcpListener
  {
    public int Alive;
    public int AliveCycle;
    public int AliveId;
    public int AliveLength;
    public int AliveOffset;
    public Hashtable HtListeners;
    public string Ip;
    public int Port;
    public int TelegramIdLength;
    public int TelegramIdOffset;

    #region constructor

    public BaseTcpListener(ListenerElement listenerElement)
    {
      Ip = listenerElement.Ip;
      Port = listenerElement.Port;
      Alive = listenerElement.Alive;
      AliveCycle = listenerElement.AliveCycle;
      AliveOffset = listenerElement.AliveOffset;
      AliveId = listenerElement.AliveId;
      AliveLength = listenerElement.AliveLength;
      TelegramIdLength = listenerElement.TelegramIdLength;
      TelegramIdOffset = listenerElement.TelegramIdOffset;
    }

    #endregion

    public int AddListener(TcpListener listener, int telIdExternal)
    {
      ListenerInfo si = new ListenerInfo(listener);
      HtListeners.Add(telIdExternal, si);
      return 0;
    }

    public TcpListener GetListenerByTelId(int telIdExternal)
    {
      if (HtListeners.ContainsKey(telIdExternal))
      {
        ListenerInfo si = (ListenerInfo)HtListeners[telIdExternal];
        return si.Listener;
      }

      NotificationController.Error("Listener with telegram Id {0} not found", telIdExternal);
      return null;
    }
  }
}
