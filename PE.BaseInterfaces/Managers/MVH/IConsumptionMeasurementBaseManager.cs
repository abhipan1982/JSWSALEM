using System.Collections.Generic;
using System.Threading.Tasks;
using PE.BaseDbEntity.Models;
using PE.BaseModels.DataContracts.Internal.MVH;

namespace PE.BaseInterfaces.Managers.MVH
{
  public interface IConsumptionMeasurementBaseManager
  {
    List<MVHFeature> GetConsumptionMeasurements();
    Task<DcMeasData> CreateNewConsumptionMeasurementAsync(MVHFeature feature);
  }
}
