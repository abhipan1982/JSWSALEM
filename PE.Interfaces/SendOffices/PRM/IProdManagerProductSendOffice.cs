using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.PRF;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.Interfaces.SendOffices.PRM
{
  public interface IProdManagerProductSendOffice
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> CalculateWorkOrderKPIsAsync(DCCalculateKPI message);
  }
}
