using System.ServiceModel;
using System.Threading.Tasks;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.BaseInterfaces.SendOffices.EVT
{
  public interface IDelayManagerSendOfficeBase
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> UpdateActiveDelayStatus(DCIntElementStatusUpdate elementStatus);
  }
}
