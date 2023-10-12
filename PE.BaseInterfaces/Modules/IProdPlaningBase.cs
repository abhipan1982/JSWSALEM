using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.PPL;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IProdPlaningBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddWorkOrderToScheduleAsync(DCWorkOrderToSchedule data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveItemFromScheduleAsync(DCWorkOrderToSchedule data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    //[System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
    //[System.ServiceModel.ServiceKnownTypeAttribute(typeof(ModuleMessage))]
    Task<DataContractBase> MoveItemInScheduleAsync(DCWorkOrderToSchedule data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCL1L3MaterialConnector> RequestL3MaterialAsync(DCMaterialRelatedOperationData data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveFinishedOrdersFromScheduleAsync(DataContractBase data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveWorkOrderFromScheduleAsync(DCWorkOrderConfirmation data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DCScheduleWorkOrder> GetNextWorkOrder(DataContractBase dc);
  }
}
