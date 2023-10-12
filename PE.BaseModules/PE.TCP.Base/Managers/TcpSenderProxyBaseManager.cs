using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.TCP;
using PE.BaseInterfaces.SendOffices.TCP;
using PE.BaseModels.DataContracts.External.TCP.Base;
using PE.TCP.Base.Handlers;
using PE.TCP.Base.Models.Configuration.Sender;
using PE.TCP.Base.Models.Entities.Sender;
using PE.TcpProxy.Base.Managers;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Infrastructure;
using SMF.Core.Interfaces;
using SMF.Core.Notification;

namespace PE.TCP.Base.Managers
{
  public class TcpSenderProxyBaseManager : BaseManager, ITcpSenderProxyBaseManager
  {
    #region members

    protected readonly ITcpProxyBaseSendOffice SendOffice;

    #endregion

    #region ctor

    public TcpSenderProxyBaseManager(IModuleInfo moduleInfo, ITcpProxyBaseSendOffice sendOffice) : base(moduleInfo)
    {
      SendOffice = sendOffice;
      ConfigSenders = (SenderSection)ConfigurationManager.GetSection("SenderSection");

      if (ConfigSenders == null)
      {
        NotificationController.Error("Can not read senders from configuration file! Application aborted.");
        return;
      }

      if (!InitializeSenders())
      {
      }
    }

    #endregion

    #region properties

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

    #endregion Properties

    #region func

    public virtual async Task Send(DataContractBase dc)
    {
      DataContractBase result = new DataContractBase();
      TcpSender sender = AllSenderInstances.GetSenderByTelId(dc.TelId);

      if (sender is null)
        throw new InternalModuleException($"Could not find sender with telegram [{dc.TelId}] in configuration", AlarmDefsBase.SenderNotFound);

      TcpSenderHandler tcpSenderHandler = new TcpSenderHandler(sender);

      BaseExternalTelegram externalTelegramObject = PrepareExternalTelegram(dc, sender);

      try
      {
        await tcpSenderHandler.OpenConnection();
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
          ex.Message, ex.AlarmParams);
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.TCPConnectionNotOpened,
          $"Unexpected error while sending telegram [{dc.TelId}] to {sender.BaseTcpSender.Ip}.", sender.BaseTcpSender.Ip, sender.BaseTcpSender.Port);
      }

      try
      {
        await tcpSenderHandler.Send(ObjectDump.RawSerialize(externalTelegramObject));
        if (sender.BaseTcpTelegramSender.HasAcknowledge)
        {
          Task tryReceiveAndForget = Task.Run(() =>
          {
            try
            {
              BaseExternalTelegram responseTelegram = tcpSenderHandler.Receive();

              //TODO add serialization to string for logs
              NotificationController.Debug(
                $"Acknowledge result: {responseTelegram.ToInternal()?.GetDataContractLogText()}");
            }
            catch (InternalModuleException ex)
            {
              ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, ex.AlarmCode,
                ex.Message, ex.AlarmParams);
            }
            catch (Exception ex)
            {
              ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.AcknowledgeNotReceived,
                $"Unable to receive acknowledge from {sender.BaseTcpSender.Ip}:{sender.BaseTcpSender.Port} for telegram [{dc.TelId}].", sender.BaseTcpSender.Ip, sender.BaseTcpSender.Port);
            }
          });
        }
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.TCPTelegramNotSent,
          $"Unexpected error while sending telegram [{dc.TelId}] to {sender.BaseTcpSender.Ip}:{sender.BaseTcpSender.Port}.", sender.BaseTcpSender.Ip, sender.BaseTcpSender.Port);
      }
      finally
      {
        try
        {
          if (!sender.BaseTcpTelegramSender.HasAcknowledge)
          {
            await tcpSenderHandler.CloseConnection();
          }
        }
        catch (Exception ex)
        {
          ex.ThrowModuleMessageException(ModuleName, MethodHelper.GetMethodName(), dc, AlarmDefsBase.TCPConnectionNotClosed,
            $"Unexpected error while closing connection with {sender.BaseTcpSender.Ip} for telegram [{dc.TelId}].", sender.BaseTcpSender.Ip, sender.BaseTcpSender.Port);
        }
      }
    }

    public virtual Task<DataContractBase> SendAsciiTelegram(DataContractBase message, int telId, int retryCount = 3)
    {
      bool shouldRetry = false;
      TcpSender sender;
      ASCIIBaseTelegram externalTelegram;
      TcpSenderHandler tcpSenderHandler;

      try
      {
        ExtractAsciiTelegram(message, telId, out sender, out tcpSenderHandler, out externalTelegram);

        ASCIIBaseTelegram telegram =
          SendAndReceiveAsciiAcknowledge(sender, externalTelegram.CommandsList, "SendSetupTelegramToGauge");
      }
      catch (Exception)
      {
        if (shouldRetry && retryCount > 0)
        {
          return SendAsciiTelegram(message, telId, retryCount - 1);
        }

        throw new InternalModuleException($"Something went wrong while sending ASCII telegram.", AlarmDefsBase.AsciiTelegramNotSent);
      }

      if (shouldRetry && retryCount > 0)
      {
        return SendAsciiTelegram(message, telId, retryCount - 1);
      }

      return Task.FromResult(new DataContractBase());
    }

    #endregion

    #region private

    private ASCIIBaseTelegram SendAndReceiveAsciiAcknowledge(TcpSender sender, List<string> commands, string methodName)
    {
      IPAddress[] ipAdd = Dns.GetHostAddresses(sender.BaseTcpSender.Ip);
      IPAddress ipAddress = ipAdd[0];
      IPEndPoint remoteEp = new IPEndPoint(ipAddress, sender.BaseTcpSender.Port);
      byte[] bytes = new byte[1024];

      // Create a TCP/IP  socket.
      Socket socket = new Socket(ipAddress.AddressFamily,
        SocketType.Stream, ProtocolType.Tcp);

      // Connect the socket to the remote endpoint. Catch any errors.
      try
      {
        socket.Connect(remoteEp);

        NotificationController.Warn("Connected IP: {0}, Port:{1}", sender.BaseTcpSender.Ip, sender.BaseTcpSender.Port);
        // Encode the data string into a byte array.

        string result = "";
        foreach (string telegram in commands)
        {
          int bytesRec = 0;
          byte[] msg = Encoding.ASCII.GetBytes(telegram);

          // Send the data through the socket.
          int bytesSent = socket.Send(msg);
          NotificationController.Warn("Method:{0}, Sent Telegram:{1}", methodName, $@"{telegram}");

          bytesRec = socket.Receive(bytes);
          result += Encoding.ASCII.GetString(bytes, 0, bytesRec);
        }

        NotificationController.Warn("Method: {0}, result: {1}", methodName, $@"{result}");

        return ExtractASCIIAcknowledgeTelegram(sender, result);
      }
      catch (ArgumentNullException ane)
      {
        NotificationController.LogException(ane);
        throw;
      }
      catch (SocketException se)
      {
        NotificationController.LogException(se);
        throw;
      }
      catch (Exception e)
      {
        NotificationController.LogException(e);
        throw;
      }
      finally
      {
        try
        {
          socket.Shutdown(SocketShutdown.Both);
          socket.Close();
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex,
            $"Error during closing connection for telegramId: {sender.BaseTcpTelegramSender.TelegramId} on ip: {sender.BaseTcpSender.Ip}",
            ex);
        }
      }
    }

    private static string SendCommand(string methodName, Socket socket, string result, string telegram,
      TcpSender sender, IPEndPoint remoteEp, int retryCount = 3)
    {
      int bytesRec = 0;
      string commandName = telegram.Contains(" ") ? telegram.Split(' ')[0] : telegram;
      byte[] msg = Encoding.ASCII.GetBytes(telegram);
      bool isOk = true;
      byte[] bytes = new byte[1024];

      try
      {
        if (!socket.Connected)
        {
          socket.Connect(remoteEp);
          NotificationController.Warn("Connected IP: {0}, Port:{1}", sender.BaseTcpSender.Ip,
            sender.BaseTcpSender.Port);
        }

        // Send the data through the socket.
        int bytesSent = socket.Send(msg);
        NotificationController.Warn("Method:{0}, Sent Telegram:{1}", methodName, $@"{telegram}");

        string response = "";
        Thread.Sleep(20);
        while (!response.Contains("\n") || !response.Contains("\r"))
        {
          bytesRec = socket.Receive(bytes);
          response += Encoding.ASCII.GetString(bytes, 0, bytesRec);
        }

        NotificationController.Warn("Response:" + response);

        if (!response.Contains("OK"))
        {
          NotificationController.Warn($"{commandName} - ERROR - {response}");
          isOk = false;
        }
        else
        {
          NotificationController.Warn($"{commandName} - OK");
          result += response;
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, $"Something went wrong while sending {commandName}");
        isOk = false;
      }

      if (!isOk)
      {
        if (retryCount > 0)
        {
          return SendCommand(methodName, socket, result, telegram, sender, remoteEp, retryCount - 1);
        }
      }

      return result;
    }

    /// <summary>
    ///   Initializing senders from configuration
    /// </summary>
    /// <returns></returns>
    private bool InitializeSenders()
    {
      AllSenderInstances = new SendersTable();
      foreach (SenderElement senderElement in ConfigSenders.Senders)
      {
        BaseTcpSender baseTcpSender = new BaseTcpSender(senderElement);

        foreach (SenderTelegramElement telegram in senderElement.Telegrams)
        {
          BaseTcpTelegramSender baseTcpTelegramSender = new BaseTcpTelegramSender(telegram);
          TcpSender sender = new TcpSender(baseTcpSender, baseTcpTelegramSender);

          AllSenderInstances.AddSender(sender, telegram.TelegramId);
        }
      }

      return true;
    }

    /// <summary>
    ///   Preparing external telegram from internal
    /// </summary>
    /// <param name="message"></param>
    /// <param name="sender"></param>
    /// <returns></returns>
    private BaseExternalTelegram PrepareExternalTelegram(DataContractBase message, TcpSender sender)
    {
      Assembly externalTelegramTypeAssembly = Assembly.Load(sender.BaseTcpTelegramSender.Assembly);

      if (externalTelegramTypeAssembly == null)
      {
        throw new Exception($"Could not find assembly: {sender.BaseTcpTelegramSender.Assembly}");
      }

      Type externalTelegramType = externalTelegramTypeAssembly.GetType(sender.BaseTcpTelegramSender.TelegramType);

      if (externalTelegramType == null)
      {
        throw new Exception(
          $"Could not find type: {sender.BaseTcpTelegramSender.TelegramType} in assembly: {sender.BaseTcpTelegramSender.Assembly}");
      }

      BaseExternalTelegram externalTelegramObject =
        (BaseExternalTelegram)Activator.CreateInstance(externalTelegramType);
      externalTelegramObject.ToExternal(message);

      return externalTelegramObject;
    }

    /// <summary>
    ///   Preparing external telegram from internal
    /// </summary>
    /// <param name="message"></param>
    /// <param name="sender"></param>
    /// <returns></returns>
    private ASCIIBaseTelegram PrepareASCIIExternalTelegram(DataContractBase message, TcpSender sender)
    {
      Assembly externalTelegramTypeAssembly = Assembly.Load(sender.BaseTcpTelegramSender.Assembly);

      if (externalTelegramTypeAssembly == null)
      {
        throw new Exception($"Could not find assembly: {sender.BaseTcpTelegramSender.Assembly}");
      }

      Type externalTelegramType = externalTelegramTypeAssembly.GetType(sender.BaseTcpTelegramSender.TelegramType);

      if (externalTelegramType == null)
      {
        throw new Exception(
          $"Could not find type: {sender.BaseTcpTelegramSender.TelegramType} in assembly: {sender.BaseTcpTelegramSender.Assembly}");
      }

      ASCIIBaseTelegram externalTelegramObject = (ASCIIBaseTelegram)Activator.CreateInstance(externalTelegramType);
      externalTelegramObject.ToExternal(message);

      return externalTelegramObject;
    }

    private static bool HandleAsciiAcknowledge(DataContractBase message, TcpSender sender,
      TcpSenderHandler tcpSenderHandlerGauge, out ASCIIBaseTelegram resultTelegram)
    {
      bool shouldRetry = false;
      resultTelegram = new ASCIIBaseTelegram();

      if (sender.BaseTcpTelegramSender.HasAcknowledge)
      {
        //  Task tryReceiveAndForget = Task.Run(async () =>
        //  {
        try
        {
          BaseExternalTelegram responseTelegram = tcpSenderHandlerGauge.Receive();

          resultTelegram = (ASCIIBaseTelegram)responseTelegram;
          //TODO add serialization to string for logs
          NotificationController.Warn($"Acknowledge result: {resultTelegram.TelegramString}");

          if (resultTelegram.TelegramString.Length == 1)
          {
            if (resultTelegram.TelegramString[0] != (char)13)
            {
              NotificationController.Warn("Retrying...");
              shouldRetry = true;
            }
          }
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex,
            $"Unable to receive acknowledge for  {sender.BaseTcpSender.Ip} : {sender.BaseTcpSender.Port} TelegramId: {message.TelId}, ex: {ex.Message}, inner: {ex.InnerException}");
        }
        finally
        {
          tcpSenderHandlerGauge.CloseConnection().GetAwaiter().GetResult();
        }
        //  });
      }

      return shouldRetry;
    }

    private void ExtractAsciiTelegram(DataContractBase message, int telId, out TcpSender sender,
      out TcpSenderHandler tcpSenderHandlerGauge, out ASCIIBaseTelegram externalTelegramObject)
    {
      sender = AllSenderInstances.GetSenderByTelId(telId);
      if (sender == null)
      {
        NotificationController.Error($"Could not find sender with telegramId {telId} in Configuration");
        throw new Exception($"Could not find sender with telegramId {telId} in Configuration");
      }

      tcpSenderHandlerGauge = new TcpSenderHandler(sender);
      externalTelegramObject = PrepareASCIIExternalTelegram(message, sender);
    }

    private ASCIIBaseTelegram ExtractASCIIAcknowledgeTelegram(TcpSender sender, string resultString)
    {
      Assembly externalTelegramTypeAssembly = Assembly.Load(sender.BaseTcpTelegramSender.Assembly);

      if (externalTelegramTypeAssembly == null)
      {
        throw new Exception($"Could not find assembly: {sender.BaseTcpTelegramSender.Assembly}");
      }

      Type externalTelegramType =
        externalTelegramTypeAssembly.GetType(sender.BaseTcpTelegramSender.AcknowledgeTelegramType);

      if (externalTelegramType == null)
      {
        throw new Exception(
          $"Could not find type: {sender.BaseTcpTelegramSender.AcknowledgeTelegramType} in assembly: {sender.BaseTcpTelegramSender.Assembly}");
      }

      ASCIIBaseTelegram externalTelegramObject = (ASCIIBaseTelegram)Activator.CreateInstance(externalTelegramType);

      externalTelegramObject.TelegramString = resultString;

      return externalTelegramObject;
    }

    #endregion
  }
}
