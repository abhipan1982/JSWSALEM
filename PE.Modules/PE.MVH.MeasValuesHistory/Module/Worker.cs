using PE.BaseInterfaces.Managers.MVH;
using PE.Interfaces.SendOffices.MVH;
using PE.BaseInterfaces.SendOffices.MVH;
using PE.MVH.Base.Module;

namespace PE.MVH.MeasValuesHistory.Module
{
  public class Worker : WorkerBase
  {
    public Worker(IMeasurementBaseManager measurementManager, IConsumptionMeasurementBaseManager consumptionMeasurementManager, IConsumptionMeasurementsSendOfficeBase sendOffice) : base(measurementManager, consumptionMeasurementManager, sendOffice)
    {
    }
  }
}
