using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.HMI;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IHmiModuleBase : IHmiBase
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> L1MaterialPositionMessageAsync(DCMaterialPosition message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> LastMaterialPositionRequestMessageAsync(DataContractBase message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> SendCurrentLabelPrintingAsync(DCPrintingLabel message);
  }
}
