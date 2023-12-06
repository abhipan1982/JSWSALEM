using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.Models.DataContracts.Internal.PPL;
using PE.BaseModels.DataContracts.Internal.PRM;
using SMF.Core.Communication;
using SMF.Core.DC;

namespace PE.Interfaces.SendOffices.PRM
{
  public interface IProdManagerWorkOrderSendOffice
  {
    //[OperationContract]
    //[FaultContract(typeof(ModuleMessage))]
    //Task<SendOfficeResult<DataContractBase>> ConnectRawMaterialWithL3MaterialAsync(DCL1L3MaterialConnector dataToSend);
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> AutoScheduleWorkOrderAsync(DCWorkOrderToSchedule dataToSend);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> CreateWorkOrderReportAsync(DCL2L3WorkOrderReport report);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> CreateProductReportAsync(DCL2L3ProductReport report);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> SendRemoveWorkOrderFromScheduleAsync(DCWorkOrderConfirmation report);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<SendOfficeResult<DataContractBase>> SendEndOfWorkShop(DataContractBase dc);


 
  }
}
