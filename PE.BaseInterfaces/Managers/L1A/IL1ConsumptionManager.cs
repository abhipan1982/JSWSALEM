using System.Collections.Generic;
using System.Threading.Tasks;
using PE.BaseModels.AbstractionModels.L1A;

namespace PE.BaseInterfaces.Managers.L1A
{
  public interface IL1ConsumptionManager : IManagerBase
  {
    Task InitAsync(List<ConsumptionBase> consumptionConfiguration);
    void UpdateConsumptionMeasurement(ConsumptionBase millConsumptionMeasurement);

    Task SendMeasurements();
    //event L1ConsumptionMeasurementEventHandler OnL1ConsumptionMeasurementEvent;
  }
}
