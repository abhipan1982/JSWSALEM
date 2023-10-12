using PE.BaseInterfaces.Managers.MVH;
using PE.MVH.Base.Module.Communication;

namespace PE.MVH.MeasValuesHistory.Communication
{
  public class ExternalAdapterHandler : ModuleBaseExternalAdapterHandler
  {
    private readonly IMeasurementBaseManager _measurementManager;

    public ExternalAdapterHandler(IMeasurementBaseManager measurementManager)
      : base(measurementManager)
    {
    }
  }
}
