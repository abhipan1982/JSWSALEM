using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.PRF;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.Interfaces.SendOffices.EVT
{
  public interface IShiftManagerSendOfficeBase
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> CalculcateShiftEndKPIs(DCCalculateKPI dc);
  }
}
