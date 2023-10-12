using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.EVT
{
  public interface IMillEventManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddMillEvent(DCMillEvent millEvent);
  }
}
