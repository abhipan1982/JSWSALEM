using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Opc.UaFx.Client;
using PE.L1A.Base.Managers;
using Polly;
using SMF.Core.Notification;
using SMF.Core.OPCClient;

namespace PE.L1A.Base.Providers.Abstract
{
  public abstract class OPCSignalProviderBase : IL1SignalProviderBase
  {
    protected readonly string OpcServerAddress;
    protected SmfOpcClient OpcClient;

    public OPCSignalProviderBase(string opcServerAddress)
    {
      OpcServerAddress = opcServerAddress;
    }

    public virtual void Init(int sleepPeriodWhenFail = 3000)
    {
      var policy = Policy
        .Handle<Exception>()
        .WaitAndRetryForever(sleepDuration => TimeSpan.FromMilliseconds(sleepPeriodWhenFail),
        (exception, timespan) =>
        {
          NotificationController.RegisterAlarm(AlarmDefsBase.CannotInitConnectionWithOPCServer,
          $"Something went wrong while Init connection for OPC Server: {OpcServerAddress} Retry...", OpcServerAddress);
          NotificationController.LogException(exception, $"Something went wrong while Init connection for OPC Server: {OpcServerAddress} Retry...");
        });

      policy.Execute(InitUsingPolly);
    }

    public virtual void Start(int sleepPeriodWhenFail = 3000)
    {
      var policy = Policy
        .Handle<Exception>()
        .WaitAndRetryForever(sleepDuration => TimeSpan.FromMilliseconds(sleepPeriodWhenFail),
        (exception, timespan) =>
        {
          NotificationController.RegisterAlarm(AlarmDefsBase.CannotStartConnectionWithOPCServer,
          $"Something went wrong while starting connection for OPC Server: {OpcServerAddress}.", OpcServerAddress);
          NotificationController.LogException(exception, $"Something went wrong while Start connection to OPC Server: {OpcServerAddress} Retry...");
        });

      policy.Execute(StartUsingPolly);
    }

    public virtual void Stop()
    {
      try
      {
        if (OpcClient != null)
          OpcClient.Close();
      }
      catch (Exception e)
      {
        NotificationController.LogException(e, $"Something went wrong while Stop connection to OPC Server: {OpcServerAddress}");
      }
    }

    protected abstract Dictionary<string, Action<object, OpcDataChangeReceivedEventArgs>> GetSubscriptions();

    protected virtual void StartUsingPolly()
    {
      if (OpcClient is not null)
      {
        OpcClient.Connect();

        OpcClient.StartSubscription(GetSubscriptions());
        NotificationController.RegisterAlarm(AlarmDefsBase.ConnectionWithOPCServerStarted,
          $"Started connection with OPC server {OpcServerAddress}.", OpcServerAddress);
      }
      else
      {
        NotificationController.RegisterAlarm(AlarmDefsBase.CannotStartConnectionWithOPCServer,
          $"Something went wrong while starting connection for OPC Server: {OpcServerAddress}.", OpcServerAddress);
        NotificationController.Fatal($"Init method wasn't run for OPC Server: {OpcServerAddress}!!!");
        NotificationController.Fatal($"Connection to OPC Server: {OpcServerAddress} wasn't established!!!");
      }
    }

    protected virtual void InitUsingPolly()
    {
      OpcClient = new SmfOpcClient(OpcServerAddress);
      OpcClient.Init(operationTimeout: 5000, disconnectTimeout: 100, reconnectTimeout: 500);
      OpcClient.CertificateConfigure();
      NotificationController.RegisterAlarm(AlarmDefsBase.ConnectionWithOPCServerInitialized,
        $"Initialized connection with OPC server {OpcServerAddress}.", OpcServerAddress);
    }
  }
}
