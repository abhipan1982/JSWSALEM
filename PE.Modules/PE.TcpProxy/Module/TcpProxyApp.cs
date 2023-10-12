using SMF.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PE.Interfaces.DC;
using SMF.Core.Log;
using SMF.Module.Notification;
using PE.TcpProxy.Module;
using SMF.Module.Parameter;

namespace PE.TcpProxy
{
  public static class TcpProxyApp
  {
    #region Properties
    public static ListenerSection ConfigListeners
    {
      get;
      private set;
    }



    public static SenderSection ConfigSenders
    {
      get;
      private set;
    }

    public static SendersTable AllSenderInstances
    {
      get;
      private set;
    }
    //public static int BytesDisplay
    //{
    //  get;
    //  private set;
    //}
    #endregion Properties

    #region Constructor

    static TcpProxyApp()
    {
      //BytesDisplay = (int)ParameterController.GetParameter("BytesDisplay").ValueInt;
      ConfigListeners = (TcpProxy.ListenerSection)ModuleController.ModuleConfiguration.GetSection("ListenerSection");
      ConfigSenders = (TcpProxy.SenderSection)ModuleController.ModuleConfiguration.GetSection("SenderSection");

      if (ConfigListeners == null)
      {
        ModuleController.Logger.Error("Can not read listeners from configuration file! Application aborted.");
        return;
      }
      if (ConfigSenders == null)
      {
        ModuleController.Logger.Error("Can not read senders from configuration file! Application aborted.");
        return;
      }
      if (!SpawnListeners())
      {
        return;
      }

      if (!SpawnSenders())
      {
        return;
      }
    }

    #endregion Constructor

    #region Methods

    public static void Init()
    {

    } 
    public static void SendMessage(DCExtCommonMessage message)
    {
      AsynchronousSocketClient sender = TcpProxyApp.AllSenderInstances.GetSenderByTelId(message.TelegramID);
      if (sender != null)
      {
        sender.Send(message._buffer);
      }
    }

    static bool SpawnListeners()
    {
      foreach (ListenerElement el in ConfigListeners.Listeners)
      {
        AsynchronousSocketListener listener = new AsynchronousSocketListener(el.TelegramID, el.TelegramLength, el.Socket, el.Description, el.Alive, el.AliveCycle, el.AliveID, el.AliveLength, el.AliveOffset);
        Thread t = new Thread(listener.StartListening);
        // t.Start();
      }
      return true;
    }

    static bool SpawnSenders()
    {
      AllSenderInstances = new SendersTable(ConfigSenders.Senders.Count);
      foreach (SenderElement el in ConfigSenders.Senders)
      {
        AsynchronousSocketClient sender = new AsynchronousSocketClient(el.TelegramID, el.TelegramIDCustomer, el.TelegramLength, el.IP, el.Socket, el.Description, el.Alive, el.AliveCycle, el.AliveID, el.AliveLength, el.AliveOffset);
        //Thread t = new Thread(sender.StartClient);
        //t.Start();    
        AllSenderInstances.AddSender(sender, el.TelegramID, el.TelegramIDCustomer);
      }
      return true;
    }

    public static void SendMessageWithOpenAndCloseConnection(DCExtCommonMessage message)
    {
      try
      {        
        AsynchronousSocketClient sender = TcpProxyApp.AllSenderInstances.GetSenderByTelId(message.TelegramID);
        if (sender != null)
        {
          if (sender.OpenConnection())
          {
            sender.Send(message._buffer);
            //sender.CloseConnection();
            NotificationController.RegisterAlarm(TcpProxyDefs.AlarmCode_TelegramSentToDevice, String.Format($"Setpoints was sent to device {message.DeviceName}"), message.DeviceName);
          }
          else
          {
            ModuleController.Logger.Error($"Error in TcpProxyApp::SendMessageWithOpenAndCloseConnection!  Cannot open connection for telegram :{message.TelegramID} to device :{message.DeviceName}");
            NotificationController.RegisterAlarm(TcpProxyDefs.AlarmCode_ErrorDuringSendingDataToDevices, String.Format($"Error during sending data to device {message.DeviceName}"), message.DeviceName);
          }
        }
      }
      catch (Exception ex)
      {
        ModuleController.Logger.Error($"Exception in TcpProxyApp::SendMessageWithOpenAndCloseConnection, ex:{ex.Message}, inner: {ex.InnerException}");
        NotificationController.RegisterAlarm(TcpProxyDefs.AlarmCode_ErrorDuringSendingDataToDevices, String.Format($"Error during sending data to device {message.DeviceName}"), message.DeviceName);       
      }
    }

    #endregion Methods
  }
}
