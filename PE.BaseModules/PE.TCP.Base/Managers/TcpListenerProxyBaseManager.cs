using System;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.TCP;
using PE.BaseInterfaces.SendOffices.TCP;
using PE.BaseModels.DataContracts.External.TCP.Telegrams;
using PE.TCP.Base.Handlers;
using PE.TCP.Base.Models.Configuration.Listener;
using PE.TCP.Base.Models.Entities.Listener;
using PE.TcpProxy.Base.Managers;
using SMF.Core.DC;
using SMF.Core.ExceptionHelpers;
using SMF.Core.Extensions;
using SMF.Core.Helpers;
using SMF.Core.Notification;
using TcpListener = PE.TCP.Base.Models.Entities.Listener.TcpListener;

namespace PE.TCP.Base.Managers
{
  public class TcpListenerProxyBaseManager : ITcpListenerProxyBaseManager
  {
    #region members

    protected readonly ITcpProxyBaseSendOffice SendOffice;

    #endregion

    #region ctor

    public TcpListenerProxyBaseManager(ITcpProxyBaseSendOffice sendOffice)
    {
      this.SendOffice = sendOffice;
      ConfigListeners = (ListenerSection)ConfigurationManager.GetSection("ListenerSection");

      TcpListenerHandler.OnTcpReceivedTelegramEvent += TcpListenerHandler_OnTcpReceivedTelegramEvent;

      if (ConfigListeners == null)
      {
        NotificationController.Error("Can not read listeners from configuration file! Application aborted.");
      }
    }

    #endregion


    #region func

    /// <summary>
    ///   Starting listening for tcp telegrams
    /// </summary>
    public virtual void StartListening()
    {
      try
      {
        foreach (ListenerElement listenerElement in ConfigListeners.Listeners)
        {
          try
          {
            BaseTcpListener baseListener = new BaseTcpListener(listenerElement)
            {
              HtListeners = new Hashtable(listenerElement.Telegrams.Count)
            };
            foreach (ListenerTelegramElement listenerTelegramElement in listenerElement.Telegrams)
            {
              BaseTcpTelegramListener baseTcpTelegramListener = new BaseTcpTelegramListener(listenerTelegramElement);
              TcpListener listener = new TcpListener(baseListener, baseTcpTelegramListener);

              baseListener.AddListener(listener, listenerTelegramElement.TelegramId);
            }

            Task.Factory.StartNew(
              () => Listen(baseListener, listenerElement.Ip, listenerElement.Port),
              TaskCreationOptions.LongRunning);
          }
          catch (Exception ex)
          {
            NotificationController.LogException(ex,
              $"Something went wrong while local listening TCP for {listenerElement.Ip} : {listenerElement.Port}.");
            NotificationController.RegisterAlarm(AlarmDefsBase.ErrorDuringListeningAddress,
              $"Something went wrong while local listening TCP for {listenerElement.Ip} : {listenerElement.Port}.", listenerElement.Ip, listenerElement.Port);
          }
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Something went wrong while global listening of TCP");
      }
    }

    #endregion

    #region properties

    public static ListenerSection ConfigListeners
    {
      get;
      private set;
    }

    public static ListenersTable AllListenerInstances
    {
      get;
      private set;
    }
    public string ModuleName { get; private set; }

    #endregion Properties

    #region private

    /// <summary>
    ///   Single instance for listening on every IP + Port
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    private void Listen(BaseTcpListener listener, string ip, int port)
    {
      TcpListenerHandler tcpListenerHandler = new TcpListenerHandler(listener);

      try
      {
        tcpListenerHandler.OnTcpAcceptCallbackEvent += TcpListenerHandler_OnTcpAcceptCallbackEvent;
        tcpListenerHandler.OnTcpReadCallbackEvent += TcpListenerHandler_OnTcpReadCallbackEvent;
        tcpListenerHandler.InitializeClient();
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.TCPClientNotInitialized, "Error during client first initialization.");
        NotificationController.LogException(ex, "Error during client first initialization.");
      }

      while (true)
      {
        try
        {
          tcpListenerHandler.ManualResetEventReset();
          tcpListenerHandler.Listen();

          //if(tcpListenerHandler.AcceptClient())
          tcpListenerHandler.ManualResetEventWaitOne();
        }
        catch (Exception ex)
        {
          NotificationController.RegisterAlarm(AlarmDefsBase.TCPClientNotInitialized, "Communication disconnected exception.");
          NotificationController.LogException(ex, "Communication disconnected exception.");
          NotificationController.Warn($"Communication disconnected: unable to receive data from {ip} : {port}.");
        }
      }
    }

    private void TcpListenerHandler_OnTcpReadCallbackEvent(TcpCallbackArgument eventArgs)
    {
      try
      {
        eventArgs.TcpListenerHandler.Read(eventArgs.AsyncResult);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, "Something went wrong while ReadCallback.");
        NotificationController.LogException(ex, "Something went wrong while ReadCallback.");
        eventArgs.TcpListenerHandler.SetIsConnected(false);
      }
    }

    private void TcpListenerHandler_OnTcpAcceptCallbackEvent(TcpCallbackArgument eventArgs)
    {
      // Get the socket that handles the client request.
      try
      {
        eventArgs.TcpListenerHandler.Accept(eventArgs.AsyncResult);
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, "Communication error: AcceptCallback failed.");
        NotificationController.LogException(ex, "Communication error: AcceptCallback failed.");
      }
    }

    private void TcpListenerHandler_OnTcpReceivedTelegramEvent(ReceivedTelegramArgument eventArgs)
    {
      try
      {
        HandleReceivedTelegramByTelId(eventArgs.TelegramId, eventArgs.StateObject, eventArgs.Listener).GetAwaiter()
          .GetResult();
      }
      catch (Exception ex)
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.UnexpectedError, $"Something went wrong while HandleReceivedTelegramByTelId: {eventArgs.TelegramId}");
        NotificationController.LogException(ex,
          $"Something went wrong while HandleReceivedTelegramByTelId: {eventArgs.TelegramId}");
      }
    }

    private async Task HandleReceivedTelegramByTelId(int telId, StateObject state, TcpListener concreteListener)
    {
      switch (telId)
      {
        case 2001:
          await SendOffice.SendTestTelegramResponse(
            ((DCTCPTestCommunicationTelegramExt)ExtractTelegram(state.Buffer, concreteListener)).ToInternal());
          break;
      }
    }

    /// <summary>
    ///   Extracting right telegram from buffer
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="concreteListener"></param>
    /// <returns></returns>
    private BaseExternalTelegram ExtractTelegram(byte[] buffer, TcpListener concreteListener)
    {
      try
      {
        Type externalTelegramType = GetTelegramType(concreteListener);

        BaseExternalTelegram externalTelegramObject =
          (BaseExternalTelegram)Activator.CreateInstance(externalTelegramType);

        return (BaseExternalTelegram)ObjectDump.GetObjectFromBytes(buffer, externalTelegramType);
      }
      catch (InternalModuleException ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), ex.AlarmCode,
          ex.Message, ex.AlarmParams);
        throw;
      }
      catch (Exception ex)
      {
        ex.ThrowModuleMessageExceptionWithoutSerialization(ModuleName, MethodHelper.GetMethodName(), AlarmDefsBase.UnexpectedError,
          $"Convert error.");
        throw;
      }
    }

    private Type GetTelegramType(TcpListener concreteListener)
    {
      Assembly externalTelegramTypeAssembly = Assembly.Load(concreteListener.BaseTcpTelegramListener.Assembly);

      if (externalTelegramTypeAssembly is null)
        throw new InternalModuleException($"Could not find assembly: {concreteListener.BaseTcpTelegramListener.Assembly}.",
          AlarmDefsBase.AssemblyNotFOund, concreteListener.BaseTcpTelegramListener.Assembly);

      Type externalTelegramType =
        externalTelegramTypeAssembly.GetType(concreteListener.BaseTcpTelegramListener.TelegramType);

      if (externalTelegramType is null)
        throw new InternalModuleException($"Could not find type: {concreteListener.BaseTcpTelegramListener.TelegramType} in assembly: {concreteListener.BaseTcpTelegramListener.Assembly}.",
          AlarmDefsBase.AssemblyTypeNotFound, concreteListener.BaseTcpTelegramListener.TelegramType, concreteListener.BaseTcpTelegramListener.Assembly);

      return externalTelegramType;
    }

    #endregion
  }
}
