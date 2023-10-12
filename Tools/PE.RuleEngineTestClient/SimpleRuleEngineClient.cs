using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QualitySA.Domain;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace PE.RuleEngineTestClient
{
  internal class SimpleRuleEngineClient
  {
    private readonly TQualitySAService.IAsync _client;

    public SimpleRuleEngineClient(IPAddress ip, int port)
    {
      TTransport transport = new TSocketTransport(ip, port, null);
      TProtocol protocol = new TBinaryProtocol(transport);
      TProtocol multiplexedProtocol = new TMultiplexedProtocol(protocol, "QualitySA");
      _client = new TQualitySAService.Client(multiplexedProtocol);
    }

    public async Task<bool> UpdateSignalDefinitions(List<TSignalDefinition> signalDefinitions)
    {
      await EnsureConnected();
      var result = await _client.updateSignalDefinitions(signalDefinitions);
      return result;
    }

    public async Task<string> GetRevision()
    {
      await EnsureConnected();
      return await _client.getRevision();
    }

    public async Task<List<string>> GetRulesTriggerEvents()
    {
      await EnsureConnected();
      return await _client.getRulesTriggerEvents();
    }

    public async Task<TRuleMapping> GetRuleMappingForTriggerEvent(string triggerName)
    {
      await EnsureConnected();
      return await _client.getRuleMappingForTriggerEvent(triggerName);
    }

    private async Task EnsureConnected()
    {
      await ((TBaseClient)_client).OpenTransportAsync();
    }
  }
}
