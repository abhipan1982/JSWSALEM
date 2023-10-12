using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MVH;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers
{
  public interface IMeasurementProcessingBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessSingleMeasurementAsync(DcMeasData message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessAggregatedMeasurementsAsync(DCMeasDataAggregated message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessSingleMeasurementAsync(DcMeasDataSample message);

    [FaultContract(typeof(ModuleMessage))]
    Task<DataContractBase> ProcessAggregatedMeasurementsAsync(DCMeasDataAggregatedSample message);
  }
}
