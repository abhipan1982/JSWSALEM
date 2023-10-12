using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.PRF;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.PRF
{
  public interface IKPICalculationBaseManager : IManagerBase
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateWorkOrderKPIsAsync(DCCalculateKPI dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateTimerKPIsAsync(DCCalculateKPI dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateShiftEndKPIsAsync(DCCalculateKPI dc);



    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateMetallicYieldKPIAsync(DCCalculateKPI dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateQualityYieldKPIAsync(DCCalculateKPI dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateMeanTimeOfDayKPIAsync(DCCalculateKPI dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateOEEKPIAsync(DCCalculateKPI dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateTestTimePercentageKPIAsync(DCCalculateKPI dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateProductionPlanVarianceAsync(DCCalculateKPI dc);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateScrapPercentageAsync(DCCalculateKPI dc);
    
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> CalculateCorrectivePreventiveMaintenanceKPIAsync(DCCalculateKPI dc);
  }
}
