using PE.TRK.Base.Models.TrackingComponents.MeasurementStorage.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.MeasurementStorage.Concrete
{
  public class MeasurementValue<T> : MeasurementBase
  {
    public MeasurementValue(int id, MeasurementTypes measurementType, bool shouldStoreLimitedSamplesAmount = false,
      int samplesAmount = 0, T valueInit = default) : base(id, measurementType, shouldStoreLimitedSamplesAmount,
      samplesAmount)
    {
      Value = valueInit;
    }

    public T Value { get; private set; }

    public void SetValue(T value)
    {
      Value = value;
    }
  }
}
