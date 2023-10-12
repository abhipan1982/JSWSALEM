using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.L1A;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.L1A
{
  public interface IMeasurementStorageManagerBase : IMeasurementProcessingBaseManager
  {
    Task ProcessMeasurementsAsync(DcRelatedToMaterialMeasurementRequest data);
    Task<DcMeasurementResponse> ProcessGetMeasurementValueAsync(DcMeasurementRequest data);
    Task<DcNdrMeasurementResponse> ProcessNdrMeasurementRequestAsync(DcNdrMeasurementRequest data);
    Task<DataContractBase> ProcessAggregatedMeasurementRequestAsync(DcAggregatedMeasurementRequest data);
    Task<DcRawMeasurementResponse> ProcessGetRawMeasurementsAsync(DcAggregatedMeasurementRequest data);
  }
}
