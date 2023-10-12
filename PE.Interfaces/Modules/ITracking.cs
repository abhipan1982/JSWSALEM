using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Modules;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.Interfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface ITracking : ITrackingBase
  {
    //AP on 05072023
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessTrackingEventAsync(DCTrackingEvent message);
  }
}
