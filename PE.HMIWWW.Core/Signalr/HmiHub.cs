using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using PE.BaseModels.DataContracts.Internal.HMI;
using SMF.HMIWWW.SignalR;

namespace PE.HMIWWW.Core.Signalr.Hubs
{
  public class HmiHub : BaseHub<Client>
  {
    public static ConnectionMapping<Client> UserConnections => Connections;

    public void SendPageInfo(string path)
    {
      Client connection = Connections.GetConnection(Context.ConnectionId);
      if (connection != null)
      {
        connection.Path = path;
      }
    }

    #region MODULES TO HMI Client

    public void L1MaterialPositionRefresh(DCMaterialPosition telegram)
    {
      try
      {
        Clients.Group("L1MaterialPositionRefresh")
          .SendAsync("L1MaterialPositionRefresh", telegram).GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {

      }
    }

    public void SendCurrentLabelPrinting(DCPrintingLabel telegram)
    {
      try
      {
        Clients.Group("CurrentLabelPrinting")
          .SendAsync("CurrentLabelPrinting", telegram).GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {

      }
    }

    #endregion

    protected override Client GetNewClient()
    {
      var feature = Context.Features.Get<IHttpConnectionFeature>();

      Client client = new()
      {
        ConnectionDate = DateTime.Now,
        Name = Context.User?.Identity?.Name,
        ConnectionId = Context.ConnectionId,
        IpAddress = feature?.RemoteIpAddress?.ToString()
      };

      return client;
    }
  }
  
  public class Client
  {
    public string Name { get; set; }
    public DateTime ConnectionDate { get; set; }
    public string ConnectionId { get; set; }
    public string IpAddress { get; set; }
    public string Path { get; set; }
  }
}
