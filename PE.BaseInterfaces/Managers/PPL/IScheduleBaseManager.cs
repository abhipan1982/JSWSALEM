using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.BaseModels.DataContracts.Internal.PPL;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.PPL
{
  public interface IScheduleBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> AddWorkOrderToScheduleAsync(DCWorkOrderToSchedule dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> MoveItemInScheduleAsync(DCWorkOrderToSchedule dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveItemFromScheduleAsync(DCWorkOrderToSchedule dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DCL1L3MaterialConnector> RequestL3MaterialForRawMaterialAsync(DCMaterialRelatedOperationData data);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveFinishedOrdersFromScheduleAsync(DataContractBase message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> RemoveWorkOrderFromScheduleAsync(DCWorkOrderConfirmation message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DCScheduleWorkOrder> GetNextWorkOrder(DataContractBase dc);
  }
}
