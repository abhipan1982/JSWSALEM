using System;

namespace PE.TRK.Base.Models.TrackingComponents.Consumption.Abstract
{
  public enum ConsumptionMeasurementType { Temporary, Growing }

  public abstract class ConsumptionBase
  {
    public ConsumptionBase(int id, ConsumptionMeasurementType measurementType)
    {
      MeasurementType = measurementType;
      MeasurementId = id;
      Value = 0;
      TimeStamp = DateTime.Now;
    }

    public ConsumptionBase(ConsumptionBase consumption)
    {
      MeasurementType = consumption.MeasurementType;
      MeasurementId = consumption.MeasurementId;
      Value = consumption.Value;
      TimeStamp = consumption.TimeStamp;
    }

    public int MeasurementId { get; protected set; }
    public double Value { get; protected set; }
    public DateTime TimeStamp { get; protected set; }
    public ConsumptionMeasurementType MeasurementType { get; protected set; }

    public void Update(double value, DateTime timeStamp)
    {
      Value = value;
      TimeStamp = timeStamp;
    }
  }
}
