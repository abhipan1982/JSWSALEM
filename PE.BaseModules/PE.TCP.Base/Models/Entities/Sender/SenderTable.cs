using System.Collections;
using SMF.Core.Notification;

namespace PE.TCP.Base.Models.Entities.Sender
{
  public class SenderInfo
  {
    #region members

    #endregion

    #region constructor

    public SenderInfo(TcpSender sender)
    {
      Sender = sender;
    }

    #endregion

    #region properties

    public TcpSender Sender { get; set; }

    #endregion
  }

  public class SendersTable
  {
    #region members

    private readonly Hashtable _htSenders;

    #endregion

    #region constructor

    public SendersTable()
    {
      _htSenders = new Hashtable();
    }

    #endregion

    #region methods

    public int AddSender(TcpSender sender, int telIdExternal)
    {
      SenderInfo si = new SenderInfo(sender);
      _htSenders.Add(telIdExternal, si);
      return 0;
    }

    public TcpSender GetSenderByTelId(int telIdExternal)
    {
      if (_htSenders.ContainsKey(telIdExternal))
      {
        SenderInfo si = (SenderInfo)_htSenders[telIdExternal];
        return si.Sender;
      }

      NotificationController.Error("Sender with telegram Id {0} not found", telIdExternal);
      return null;
    }

    #endregion
  }
}
