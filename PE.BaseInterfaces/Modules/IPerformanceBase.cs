using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.PRF;
using SMF.Core.DC;
using SMF.Core.Interfaces;

namespace PE.BaseInterfaces.Modules
{
  [ServiceContract(SessionMode = SessionMode.Allowed)]
  public interface IPerformanceBase : IBaseModule
  {
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateWorkOrderKPIsAsync(DCCalculateKPI data);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateTimerKPIsAsync(DCCalculateKPI message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateShiftEndKPIsAsync(DCCalculateKPI message);



    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateMetallicYieldKPIAsync(DCCalculateKPI message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateQualityYieldKPIAsync(DCCalculateKPI message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateMeanTimeOfDayKPIAsync(DCCalculateKPI message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateOEEKPIAsync(DCCalculateKPI message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateTestTimePercentageKPIAsync(DCCalculateKPI message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateProductionPlanVarianceAsync(DCCalculateKPI message);

    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateScrapPercentageAsync(DCCalculateKPI message);
    
    [OperationContract]
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateCorrectivePreventiveMaintenanceKPIAsync(DCCalculateKPI message);
  }
}
