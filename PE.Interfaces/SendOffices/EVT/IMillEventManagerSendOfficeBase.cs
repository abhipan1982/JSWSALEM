using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.EVT;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.Interfaces.SendOffices.EVT
{
  public interface IMillEventManagerSendOfficeBase
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> AddMillEvent(DCMillEvent millEvent);
  }
}
