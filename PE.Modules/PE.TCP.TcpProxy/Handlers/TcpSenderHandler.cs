using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.External.TCP.Base;
using PE.TCP.Base.Models.Entities.Sender;
using SMF.Core.DC;
using SMF.Core.Helpers;
using SMF.Core.Notification;

namespace PE.TCP.TcpProxy.Handlers
{
  public class TcpSenderHandler
  {
    private readonly Dictionary<Guid, ManualResetEvent> _pendingQueries
      = new Dictionary<Guid, ManualResetEvent>();

    private readonly TcpSender _sender;
    private readonly bool _ifConnectCallBackIsOk = true;
    private BaseExternalTelegram _ackTel;
    private Socket _client;

    public TcpSenderHandler(TcpSender sender)
    {
      _sender = sender;
    }

    public bool IsConnected => _client?.Connected ?? false;

    public async Task<bool> OpenConnection()
    {
      // Establish the remote endpoint for the socket.
      IPAddress[] ipAdd = Dns.GetHostAddresses(_sender.BaseTcpSender.Ip);
      IPAddress ipAddress = ipAdd[0];
      IPEndPoint remoteEp = new IPEndPoint(ipAddress, _sender.BaseTcpSender.Port);

      NotificationController.Info(
        $"Initializing telegram {_sender.BaseTcpTelegramSender.TelegramId}, remote address: {_sender.BaseTcpSender.Ip}:{_sender.BaseTcpSender.Port}");

      // Create a TCP/IP socket.
      _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      NotificationController.Info(
        $"Socket client has been created for IP: {_sender.BaseTcpSender.Ip}, Port: {_sender.BaseTcpSender.Port}");

      if (!_client.Connected)
      {
        await CloseConnection();
      }

      // Connect to the remote endpoint.
      if (!_client.Connected)
      {
        Guid guid = Guid.NewGuid();
        ManualResetEvent resetEvent = new ManualResetEvent(false);
        _pendingQueries.Add(guid, resetEvent);

        _client.BeginConnect(remoteEp, ConnectCallback, Tuple.Create(guid, _client)).AsyncWaitHandle
          .WaitOne(TimeSpan.FromSeconds(3));

        resetEvent.WaitOne();
      }

      //TODO check if inside callback throwes exception
      if (_client.Connected && _ifConnectCallBackIsOk)
      {
        NotificationController.Warn(
          $"The connection to {_sender.BaseTcpSender.Ip} : {_sender.BaseTcpSender.Port} has been established");

        return true;
      }

      throw new Exception(
        $"Could not establish the connection to the {_sender.BaseTcpSender.Ip} : {_sender.BaseTcpSender.Port}");
    }

    public Task CloseConnection()
    {
      if (_client.Connected)
      {
        _client.Disconnect(true);
        NotificationController.Info(
          $"Tcp connection was closed for ip: {_sender.BaseTcpSender.Ip} socket: {_sender.BaseTcpSender.Port} telegramId: {_sender.BaseTcpTelegramSender.TelegramId}");
      }
      else
      {
        NotificationController.Warn(
          $"Cannot close connection, propably connection was not opened: connectStatus: {_client.Connected}");
      }

      return Task.CompletedTask;
    }

    private void ConnectCallback(IAsyncResult ar)
    {
      // Retrieve the socket from the state object.
      Tuple<Guid, Socket> client = (Tuple<Guid, Socket>)ar.AsyncState;
      try
      {
        // Complete the connection.
        client.Item2.EndConnect(ar);
        if (_pendingQueries.TryGetValue(client.Item1, out ManualResetEvent resetEvent))
        {
          resetEvent.Set();
        }

        NotificationController.Info("Socket connected to {0}", client.Item2.RemoteEndPoint.ToString());
      }
      catch (Exception ex)
      {
        if (_pendingQueries.TryGetValue(client.Item1, out ManualResetEvent resetEvent))
        {
          resetEvent.Set();
        }

        NotificationController.LogException(ex, "Socket not connected");
      }
    }

    public Task Send(byte[] byteData)
    {
      if (_client.Connected)
      {
        NotificationController.Info("Sending telegram {0}", _sender.BaseTcpTelegramSender.TelegramId);
        _client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, out SocketError socketError, SendCallback,
          _client).AsyncWaitHandle.WaitOne();

        if (socketError == SocketError.Success)
        {
          NotificationController.Info($"Send telegram ended with {socketError}");
        }
        else
        {
          NotificationController.Error(
            $"Telegram {_sender.BaseTcpTelegramSender.TelegramId} not sent! TCP Error: {socketError}.");
          throw new Exception($"Sending of telegram ended with no success. TCP error: {socketError}.");
        }
      }
      else
      {
        NotificationController.Error(
          $"Telegram {_sender.BaseTcpTelegramSender.TelegramId} not sent! Channel is actually disconnected.");
        throw new Exception(
          $"Telegram {_sender.BaseTcpTelegramSender.TelegramId} not sent! Channel is actually disconnected.");
      }

      return Task.CompletedTask;
    }

    public BaseExternalTelegram Receive(CancellationToken cancellationToken = new CancellationToken())
    {
      // Create the state object.
      StateObject state = new StateObject {Id = Guid.NewGuid(), WorkSocket = _client};

      ManualResetEvent resetEvent = new ManualResetEvent(false);
      _pendingQueries.Add(state.Id, resetEvent);

      // Begin receiving the data from the remote device.
      _client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, ReadCallback, state);

      // We need to give a time for result to be deserialized
      int waitResult = WaitHandle.WaitAny(new[] {resetEvent, cancellationToken.WaitHandle}, TimeSpan.FromMinutes(5));
      if (waitResult == WaitHandle.WaitTimeout)
      {
        throw new SocketException((int)SocketError.TimedOut);
      }

      if (waitResult != 0)
      {
        return null;
      }

      if (_ackTel == null)
      {
        throw new Exception(
          $"Couldn't deserialize acknowledge to {_sender.BaseTcpTelegramSender.AcknowledgeTelegramType}");
      }

      return _ackTel;
    }

    private void SendCallback(IAsyncResult ar)
    {
      // Retrieve the socket from the state object.
      Socket client = (Socket)ar.AsyncState;

      // Complete sending the data to the remote device.
      int bytesSent = client.EndSend(ar);
      NotificationController.Info("Sent {0} bytes to server.", bytesSent);
    }

    /// <summary>
    ///   Callback when listener has received a telegram
    /// </summary>
    /// <param name="ar"></param>
    public void ReadCallback(IAsyncResult ar)
    {
      // Retrieve the state object and the handler socket
      // from the asynchronous state object.
      StateObject state = (StateObject)ar.AsyncState;

      try
      {
        Socket handler = state.WorkSocket;
        bool connected = handler.Available != 0 || !handler.Poll(1, SelectMode.SelectRead);
        if (!connected)
        {
          NotificationController.Info("Client disconnected " /*, ((IPEndPoint)handler.LocalEndPoint).Port.ToString()*/);
          handler.Close();
          return;
        }

        // Read data from the client socket.
        int bytesRead = handler.EndReceive(ar);
        state.ActualSize += bytesRead;
        NotificationController.Info(" Read {0} bytes from socket. Actual size {1}", bytesRead, state.ActualSize);

        NotificationController.Trace($"IsASCII: {_sender.BaseTcpTelegramSender.IsAsciiMessage}");
        if (!_sender.BaseTcpTelegramSender.IsAsciiMessage &&
            state.ActualSize >= _sender.BaseTcpTelegramSender.AcknowledgeLength)
        {
          ExtractAcknowledgeTelegram(state.Buffer);
        }
        else if (_sender.BaseTcpTelegramSender.IsAsciiMessage)
        {
          ExtractAsciiAcknowledgeTelegram(state.Buffer, state.ActualSize);
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Tcp ReadCallback Exception");
      }
      finally
      {
        if (_pendingQueries.TryGetValue(state.Id, out ManualResetEvent resetEvent))
        {
          resetEvent.Set();
        }
      }
    }

    /// <summary>
    ///   Extracting acknowledge
    /// </summary>
    /// <param name="buffer">received buffer</param>
    /// <returns></returns>
    private void ExtractAcknowledgeTelegram(byte[] buffer)
    {
      Assembly externalTelegramTypeAssembly = Assembly.Load(_sender.BaseTcpTelegramSender.Assembly);

      if (externalTelegramTypeAssembly == null)
      {
        throw new Exception($"Could not find assembly: {_sender.BaseTcpTelegramSender.Assembly}");
      }

      Type externalTelegramType =
        externalTelegramTypeAssembly.GetType(_sender.BaseTcpTelegramSender.AcknowledgeTelegramType);

      if (externalTelegramType == null)
      {
        throw new Exception(
          $"Could not find type: {_sender.BaseTcpTelegramSender.AcknowledgeTelegramType} in assembly: {_sender.BaseTcpTelegramSender.Assembly}");
      }

      BaseExternalTelegram externalTelegramObject =
        (BaseExternalTelegram)Activator.CreateInstance(externalTelegramType);

      _ackTel = (BaseExternalTelegram)ObjectDump.GetObjectFromBytes(buffer, externalTelegramType);
    }

    private void ExtractAsciiAcknowledgeTelegram(byte[] buffer, int size)
    {
      Assembly externalTelegramTypeAssembly = Assembly.Load(_sender.BaseTcpTelegramSender.Assembly);

      if (externalTelegramTypeAssembly == null)
      {
        throw new Exception($"Could not find assembly: {_sender.BaseTcpTelegramSender.Assembly}");
      }

      Type externalTelegramType =
        externalTelegramTypeAssembly.GetType(_sender.BaseTcpTelegramSender.AcknowledgeTelegramType);

      if (externalTelegramType == null)
      {
        throw new Exception(
          $"Could not find type: {_sender.BaseTcpTelegramSender.AcknowledgeTelegramType} in assembly: {_sender.BaseTcpTelegramSender.Assembly}");
      }

      ASCIIBaseTelegram externalTelegramObject = (ASCIIBaseTelegram)Activator.CreateInstance(externalTelegramType);

      externalTelegramObject.TelegramString = Encoding.ASCII.GetString(buffer, 0, size);

      _ackTel = externalTelegramObject;
    }
  }
}
