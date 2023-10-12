using System.ServiceModel;
using System.Threading.Tasks;
using PE.BaseModels.DataContracts.Internal.MVH;
using SMF.Core.DC;

namespace PE.BaseInterfaces.Managers.MVH
{
  public interface IMeasurementBaseManager : IMeasurementProcessingBaseManager
  {
    [FaultContract(typeof(ModuleMessage))]
    Task<DCMeasDataAggregated> GetMeasurementsAsync(DCMeasRequest message);
  }
}
