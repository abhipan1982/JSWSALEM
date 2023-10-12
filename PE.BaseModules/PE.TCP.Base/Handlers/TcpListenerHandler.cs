using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PE.TCP.Base.Models.Entities.Listener;
using SMF.Core.Notification;
using TcpListener = PE.TCP.Base.Models.Entities.Listener.TcpListener;

namespace PE.TCP.Base.Handlers
{
  public class TcpListenerHandler
  {
    public TcpListenerHandler(BaseTcpListener listener)
    {
      _listener = listener;
    }

    /// <summary>
    ///   Listening a telegram to receive
    /// </summary>
    public void Listen()
    {
      NotificationController.Info("Waiting for a connection...");
      _client.BeginAccept(
        AcceptCallback,
        _client);
    }

    public Task FurnaceAlive(string dateTime)
    {
      NotificationController.Info("Received furnace alive telegram at {0}", dateTime);
      return Task.CompletedTask;
    }

    public void ManualResetEventReset()
    {
      _allDone.Reset();
    }

    public void ManualResetEventWaitOne()
    {
      _allDone.WaitOne();
    }

    public void InitializeClient()
    {
      NotificationController.Info($"Starting listener for port: {_listener.Port}");
      IPAddress[] ipAdd = Dns.GetHostAddresses(_listener.Ip);
      IPAddress ipAddress = ipAdd[0];
      IPEndPoint remoteEp = new IPEndPoint(ipAddress, _listener.Port);

      _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      NotificationController.Info($"Socket client has been created for IP: {_listener.Ip}, Port: {_listener.Port}");

      _client.Bind(remoteEp);
      _client.Listen(_listener.Port);
    }

    public void Accept(IAsyncResult ar)
    {
      // Signal the main thread to continue.
      _allDone.Set();

      Socket listener = (Socket)ar.AsyncState;
      Socket handler = listener.EndAccept(ar);

      NotificationController.Info($"Client connected on IP: {_listener.Ip}, Port: {_listener.Port}");

      // Create the state object.  
      StateObject state = new StateObject();
      state.WorkSocket = handler;

      NotificationController.Info("Waiting for a receive telegram...");

      handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
        ReadCallback, state);
    }

    public void SetIsConnected(bool value)
    {
      _isConnected = value;
    }

    public void Read(IAsyncResult ar)
    {
      // Retrieve the state object and the handler socket
      // from the asynchronous state object.
      StateObject state = (StateObject)ar.AsyncState;
      Socket handler = state.WorkSocket;
      bool connected = handler.Available != 0 || !handler.Poll(1, SelectMode.SelectRead);

      if (!connected)
      {
        NotificationController.Warn($"Handler availability: {handler.Available}");
        NotificationController.Info("Client disconnected on port: {0} ",
          ((IPEndPoint)handler.LocalEndPoint).Port.ToString());
        _aliveCounter = 0;
        state.ActualSize = 0;

        CloseConnection(handler);

        return;
      }

      // Read data from the client socket. 
      int bytesRead = handler.EndReceive(ar);
      state.ActualSize += bytesRead;
      NotificationController.Info("Read {0} bytes from socket. Actual size {1}", bytesRead, state.ActualSize);

      CloseConnection(handler);

      int telId = ExtractTelegramId(state.Buffer, _listener.TelegramIdOffset, _listener.TelegramIdLength);

      TcpListener concreteListener = _listener.GetListenerByTelId(telId);

      if (concreteListener == null)
      {
        NotificationController.Error($"Received telegram Id {telId} which does not exist in Configuration section");
      }
      else if (state.ActualSize < concreteListener.BaseTcpTelegramListener.TelegramLength)
      {
        NotificationController.Error(
          $"Received telegram Id {telId} which  length is not the same as in Configuration section expected: {concreteListener.BaseTcpTelegramListener.TelegramLength}, actual: {state.ActualSize}");
      }
      else if (state.ActualSize >= concreteListener.BaseTcpTelegramListener.TelegramLength)
      {
        NotificationController.Info($"Successfully received {telId} telegram at {DateTime.Now}. Sending to Adapter");

        TriggerReceivedTelegram(telId, state, concreteListener);
      }

      _isConnected = true;
      _aliveCounter = 0;
    }

    private static void CloseConnection(Socket handler)
    {
      try
      {
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
      }
      catch (Exception)
      {
      }
    }

    public bool AcceptClient()
    {
      _aliveCounter += _sleepPeriod;

      if (_isConnected && _aliveCounter > _listener.AliveCycle)
      {
        //disconnect channel
        NotificationController.Error("Listen timed out! Disconnecting client...");
        _isConnected = false;
        try
        {
          _client.Shutdown(SocketShutdown.Both);
        }
        catch
        {
        }
        finally
        {
          InitializeClient();
        }

        return false;
      }

      return true;
    }

    #region Members

    private event TcpCallbackDelegate TcpAcceptCallbackEvent;
    private event TcpCallbackDelegate TcpReadCallbackEvent;
    private static event ReceivedTelegramDelegate TcpReceivedTelegramEvent;


    private int _aliveCounter;
    private bool _isConnected;
    private readonly int _sleepPeriod = 10;
    private Socket _client;
    private readonly BaseTcpListener _listener;
    private readonly ManualResetEvent _allDone = new ManualResetEvent(false);

    #endregion


    #region Events

    /// <summary>
    ///   OnTcpAcceptCallbackEvent
    /// </summary>
    public event TcpCallbackDelegate OnTcpAcceptCallbackEvent
    {
      add => TcpAcceptCallbackEvent += value;
      remove => TcpAcceptCallbackEvent -= value;
    }

    /// <summary>
    ///   OnTcpReadCallbackEvent
    /// </summary>
    public event TcpCallbackDelegate OnTcpReadCallbackEvent
    {
      add => TcpReadCallbackEvent += value;
      remove => TcpReadCallbackEvent -= value;
    }

    /// <summary>
    ///   OnTcpReceivedTelegramEvent
    /// </summary>
    public static event ReceivedTelegramDelegate OnTcpReceivedTelegramEvent
    {
      add => TcpReceivedTelegramEvent += value;
      remove => TcpReceivedTelegramEvent -= value;
    }

    #endregion

    #region private methods

    /// <summary>
    ///   Callback when client was been connected to server
    /// </summary>
    /// <param name="ar"></param>
    private void AcceptCallback(IAsyncResult ar)
    {
      TriggerAcceptCallback(ar);
    }

    private void TriggerAcceptCallback(IAsyncResult ar)
    {
      TcpAcceptCallbackEvent?.Invoke(new TcpCallbackArgument(ar, this));
    }

    private void TriggerReadCallback(IAsyncResult ar)
    {
      TcpReadCallbackEvent?.Invoke(new TcpCallbackArgument(ar, this));
    }

    /// <summary>
    ///   Callback when listener has received a telegram
    /// </summary>
    /// <param name="ar"></param>
    private void ReadCallback(IAsyncResult ar)
    {
      TriggerReadCallback(ar);
    }

    private void TriggerReceivedTelegram(int telId, StateObject state, TcpListener concreteListener)
    {
      TcpReceivedTelegramEvent?.Invoke(new ReceivedTelegramArgument(telId, state, concreteListener));
    }

    /// <summary>
    ///   Decode telegramId
    /// </summary>
    /// <param name="buffer">received buffer</param>
    /// <param name="telIdOffset">offset to read telId</param>
    /// <param name="telIdLength">telId length</param>
    /// <returns></returns>
    private int ExtractTelegramId(byte[] buffer, int telIdOffset, int telIdLength)
    {
      byte[] telIdChar = new byte[telIdLength];
      for (int i = 0; i < telIdLength; i++)
      {
        telIdChar[i] = buffer[i + telIdOffset];
      }

      try
      {
        string telId = Encoding.UTF8.GetString(telIdChar, 0, telIdChar.Length);

        if (string.IsNullOrEmpty(telId))
        {
          throw new Exception("Cannot deserialize telId");
        }

        return Convert.ToInt32(telId);
      }
      catch (Exception ex)
      {
        throw new Exception("Convert error", ex);
      }
    }

    #endregion
  }

  #region Extension

  public delegate void TcpCallbackDelegate(TcpCallbackArgument eventArgs);

  public delegate void ReceivedTelegramDelegate(ReceivedTelegramArgument eventArgs);

  public class TcpCallbackArgument : EventArgs
  {
    public TcpCallbackArgument(IAsyncResult asyncResult, TcpListenerHandler tcpListenerHandler)
    {
      AsyncResult = asyncResult;
      TcpListenerHandler = tcpListenerHandler;
    }

    public IAsyncResult AsyncResult { get; }
    public TcpListenerHandler TcpListenerHandler { get; }
  }

  public class ReceivedTelegramArgument : EventArgs
  {
    public ReceivedTelegramArgument(int telegramId, StateObject stateObject, TcpListener listener)
    {
      TelegramId = telegramId;
      StateObject = stateObject;
      Listener = listener;
    }

    public int TelegramId { get; }
    public StateObject StateObject { get; }
    public TcpListener Listener { get; }
  }

  public class StateObject
  {
    // Size of receive buffer.
    public const int BufferSize = 1000;

    public int ActualSize;

    // Receive buffer.
    public byte[] Buffer = new byte[BufferSize];

    public Guid Id;

    // Client  socket.
    public Socket WorkSocket;
  }

  #endregion
}
