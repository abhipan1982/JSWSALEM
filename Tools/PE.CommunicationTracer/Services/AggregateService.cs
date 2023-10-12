using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using PE.CommunicationTracer.Core;
using Prism.Events;
using ServiceModel.Grpc.Client;
using SMF.Core.Communication;
using SMF.Core.Helpers;
using SMF.Core.Interfaces;

namespace PE.CommunicationTracer.Services
{
  public class AggregateService
  {
    private readonly IEventAggregator _eventAggregator;
    ConcurrentDictionary<string, ModuleClient> _listeners;

    public AggregateService(IEventAggregator eventAggregator)
    {
      _listeners = new ConcurrentDictionary<string, ModuleClient>();
      _eventAggregator = eventAggregator;

    }

    public void RegisterListener(string moduleName, Action<MessageDump> addMessage)
    {
      _ = Task.Run(async () =>
        {
          if (_listeners.ContainsKey(moduleName))
            return;

          //var channel = new Channel("localhost", PortHelper.GetPort(moduleName, true), ChannelCredentials.Insecure);
          //var clientFactory = new ClientFactory();
          //IClient client = clientFactory.CreateClient<IClient>(channel);

          IClient client = WatchdogInterfaceHelper.GetWdogFactoryChannel<IClient>(moduleName);

          if (client == null)
          {
            Debug.WriteLine("Error Client not awailable");
            _eventAggregator.GetEvent<ModuleStatusEvent>().Publish(new ModuleDate { Name = moduleName , Status = ModuleStatus.Disconnected});
            return;
          }
          CancellationTokenSource cts = new CancellationTokenSource();
          _listeners.TryAdd(moduleName, new ModuleClient { Client = client, Token = cts });

          _eventAggregator.GetEvent<ModuleStatusEvent>().Publish(new ModuleDate { Name = moduleName, Status = ModuleStatus.Connecting });

          IAsyncEnumerator<MessageDump> enumerator = client.StreamCommunication(new Grpc.Core.CallOptions(null, DateTime.UtcNow.AddDays(1), cts.Token)).GetAsyncEnumerator();
          MessageDump result = null;
          bool hasResult = true;
          while (hasResult)
          {
            try
            {
              hasResult = await enumerator.MoveNextAsync().ConfigureAwait(false);
              if (hasResult)
                result = enumerator.Current;
              else
                result = null;
            }
            catch (Exception ex)
            {
              cts.Cancel();
              _eventAggregator.GetEvent<ModuleStatusEvent>().Publish(new ModuleDate { Name = moduleName, Status = ModuleStatus.Disconnected });
              Debug.WriteLine("Error Client disconnected " + ex.ToString());
            }
            if (result != null)
            {
              _eventAggregator.GetEvent<ModuleStatusEvent>().Publish(new ModuleDate { Name = moduleName, Status = ModuleStatus.Connected });
              try
              {
                addMessage(result);
              }
              catch
              {
                Debug.WriteLine("Error Message processing error");
              }
            }
          }
          
          _eventAggregator.GetEvent<ModuleStatusEvent>().Publish(new ModuleDate { Name = moduleName, Status = ModuleStatus.Disconnected });
          Debug.WriteLine("Info Listener terminated");
        }).ContinueWith((t) => { 
          Debug.WriteLine("Listener terminated, unregistering");
          UnregisterListener(moduleName);
        });
    }

    public void UnregisterListener(string moduleName)
    {
      if (!_listeners.TryGetValue(moduleName, out var module))
        return;

      module.Token?.Cancel();
      _listeners.Remove(moduleName, out _);
    }
  }

  class ModuleClient
  {
    public IClient Client { get; set; }
    public CancellationTokenSource Token { get; set; }
  }
}
