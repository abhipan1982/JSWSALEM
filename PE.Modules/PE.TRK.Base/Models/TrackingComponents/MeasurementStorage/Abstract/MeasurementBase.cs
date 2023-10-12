namespace PE.TRK.Base.Models.TrackingComponents.MeasurementStorage.Abstract
{
  public enum MeasurementTypes
  {
    DoubleMeasurement,
    BoolMeasurement,
    ShortMeasurement
  }

  public abstract class MeasurementBase
  {
    public MeasurementBase(int id, MeasurementTypes measurementType, bool shouldStoreLimitedSamplesAmount,
      int samplesAmount)
    {
      Id = id;
      ShouldStoreLimitedSamplesAmount = shouldStoreLimitedSamplesAmount;
      SamplesAmount = samplesAmount;
      MeasurementType = measurementType;
    }

    public int Id { get; }
    public bool ShouldStoreLimitedSamplesAmount { get; }
    public int SamplesAmount { get; }
    public MeasurementTypes MeasurementType { get; }
  }
}
