using System.Collections;
using SMF.Core.Notification;

namespace PE.TCP.Base.Models.Entities.Listener
{
  public class ListenerInfo
  {
    #region members

    #endregion

    #region constructor

    public ListenerInfo(TcpListener listener)
    {
      Listener = listener;
    }

    #endregion

    #region properties

    public TcpListener Listener { get; set; }

    #endregion
  }

  public class ListenersTable
  {
    #region members

    private readonly Hashtable _htListeners;

    #endregion

    #region constructor

    public ListenersTable()
    {
      _htListeners = new Hashtable();
    }

    #endregion

    #region methods

    public int AddListener(TcpListener listener, int telIdExternal)
    {
      ListenerInfo si = new ListenerInfo(listener);
      _htListeners.Add(telIdExternal, si);
      return 0;
    }

    public TcpListener GetListenerByTelId(int telIdExternal)
    {
      if (_htListeners.ContainsKey(telIdExternal))
      {
        ListenerInfo si = (ListenerInfo)_htListeners[telIdExternal];
        return si.Listener;
      }

      NotificationController.Error("Listener with telegram Id {0} not found", telIdExternal);
      return null;
    }

    #endregion
  }
}
