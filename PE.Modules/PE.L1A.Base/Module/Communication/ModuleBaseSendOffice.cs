using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseInterfaces.SendOffices.EVT;
using PE.BaseInterfaces.SendOffices.L1A;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core;
using SMF.Core.Communication;
using SMF.Core.DC;
using SMF.Core.Interfaces;
using SMF.Module.Core;

namespace PE.L1A.Base.Module.Communication
{
  public class ModuleBaseSendOffice : ModuleSendOfficeBase, IL1SignalSendOfficeBase
  {
    public virtual Task<SendOfficeResult<DataContractBase>> SendL1TrackingPointToTrackingAsync(DcTrackingPointSignal signalToSend)
    {
      string targetModuleName = Core.Constants.SmfAuthorization_Module_Tracking;
      ITrackingBase client = InterfaceHelper.GetFactoryChannel<ITrackingBase>(targetModuleName);
      if (client == null)
      {
        return Task.FromResult(new SendOfficeResult<DataContractBase>());
      }

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessTrackingPointSignalAsync(signalToSend));
    }

    public virtual Task<SendOfficeResult<DataContractBase>> SendAggregatedL1TrackingPointToTrackingAsync(DcAggregatedTrackingPointSignal signalToSend)
    {
      string targetModuleName = Core.Constants.SmfAuthorization_Module_Tracking;
      ITrackingBase client = InterfaceHelper.GetFactoryChannel<ITrackingBase>(targetModuleName);
      if (client == null)
      {
        return Task.FromResult(new SendOfficeResult<DataContractBase>());
      }

      return HandleModuleSendMethod(targetModuleName, () => client.ProcessAggregatedTrackingPointSignalsAsync(signalToSend));
    }
  }
}
