using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.UaFx.Client;
using PE.BaseModels.DataContracts.Internal.L1A;
using PE.L1A.Base.Models;
using PE.L1A.Base.Providers.Abstract;
using SMF.Core.Notification;

namespace PE.L1A.Base.Providers.Concrete
{
  public class L1OpcMillControlDataProviderBase : OPCSignalProviderBase, IL1MillControlDataProviderBase
  {
    protected readonly IConfigurationStorageProviderBase ConfigurationStorageProvider;

    public L1OpcMillControlDataProviderBase(string opcServerAddress)
       : base(opcServerAddress)
    {

    }

    public virtual void SendMillControlDataMessage(DCMillControlMessage dc, L1MillControlDataBase millControlDataConfiguration, int retryCount = 3)
    {
      try
      {
        Stopwatch watch1 = Stopwatch.StartNew();
        NodeId objectNodeId = new NodeId(millControlDataConfiguration.CommAttr2);

        var nodeInfo = OpcClient.BrowseNode(objectNodeId);
        if (string.IsNullOrEmpty(nodeInfo.Name.Value) || string.IsNullOrEmpty(nodeInfo.DisplayName.Value))
          NotificationController.Warn($"Tag URL {objectNodeId} not found in OPC.");
        else
        {
          var opcResult = OpcClient.WriteNode(objectNodeId, dc.Value);

          if (opcResult.IsGood)
            NotificationController.Debug("Successfully sent MillControlData for code: {@Code} in {@ElapsedMilliseconds} ms. Value: {@Value}. Result: {@OpcResult}.", dc.MillControlDataCode, watch1.ElapsedMilliseconds, dc.Value, opcResult);
          else
            throw new Exception($"MillControlData sending failed for code: {dc.MillControlDataCode} in {watch1.ElapsedMilliseconds} ms. Value: {dc.Value}. Result: {opcResult.Description}.");
        }
      }
      catch (Exception ex)
      {
        NotificationController.LogException(ex, "Something went wrong while sent MillControlData for code: {@Code} in  Value: {@Value}.", dc.MillControlDataCode, dc.Value);

        if (retryCount > 0)
          SendMillControlDataMessage(dc, millControlDataConfiguration, retryCount - 1);
        else
          throw;
      }
    }

    protected override Dictionary<string, Action<object, OpcDataChangeReceivedEventArgs>> GetSubscriptions()
    {
      return new Dictionary<string, Action<object, OpcDataChangeReceivedEventArgs>>();
    }
  }
}
