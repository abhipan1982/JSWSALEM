using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseInterfaces.Managers.PRM;
using PE.BaseModels.DataContracts.Internal.DBA;
using PE.BaseModels.DataContracts.Internal.EVT;
using PE.BaseModels.DataContracts.Internal.PRM;
using PE.BaseModels.DataContracts.Internal.TRK;
using SMF.Core.DC;

namespace PE.Interfaces.Managers.PRM
{
  public interface IWorkOrderManager : IWorkOrderBaseManager
  {
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DCWorkOrderStatus> ProcessWorkOrderDataAsync(DCL3L2WorkOrderDefinition message);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CreateWorkOrderAsync(DCWorkOrder workOrder);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateWorkOrderAsync(DCWorkOrder workOrder);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> SendWorkOrderReportAsync(DCWorkOrderConfirmation workOrder);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> SendProductReportAsync(DCRawMaterial coil);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> CheckShiftsWorkOrderStatusses(DCShiftCalendarId arg);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> ProcessWorkOrderStatus(DCRawMaterial arg);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> AddTestWorkOrderToScheduleAsync(DCTestSchedule dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateCanceledWorkOrderAsync(DCWorkOrderCancel dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateUnCanceledWorkOrderAsync(DCWorkOrderCancel dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateBlockedWorkOrderAsync(DCWorkOrderBlock dc);

    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> UpdateUnBlockedWorkOrderAsync(DCWorkOrderBlock dc);
    //[FaultContract(typeof(ModuleMessage))]
    //Task<DataContractBase> EndOfWorkOrderAsync(WorkOrderId dc);
  }
}
