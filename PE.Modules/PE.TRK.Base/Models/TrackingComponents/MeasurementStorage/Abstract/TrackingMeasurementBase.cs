using System;
using System.Collections.Generic;
using System.Linq;
using PE.TRK.Base.Models.TrackingComponents.MeasurementStorage.Concrete;

namespace PE.TRK.Base.Models.TrackingComponents.MeasurementStorage.Abstract
{
  public abstract class TrackingMeasurementBase
  {
    #region members

    private readonly object _locker;

    #endregion members

    protected TrackingMeasurementBase()
    {
      _locker = new object();
      Measurements = new List<MeasurementStorage<object>>();
    }

    public List<MeasurementStorage<object>> Measurements { get; }

    public void AddMeasurement(MeasurementBase measurement)
    {
      lock (_locker)
      {
        MeasurementStorage<object> measurementObject =
          Measurements.FirstOrDefault(me => me.MeasurementId == measurement.Id);

        if (measurementObject == null)
        {
          Measurements.Add(new MeasurementStorage<object>(measurement));
        }
        else
        {
          if (measurement is MeasurementValue<double> doubleValue)
          {
            measurementObject.AddValue(doubleValue.Value);
          }
          else if (measurement is MeasurementValue<bool> boolValue)
          {
            measurementObject.AddValue(boolValue.Value);
          }
          else if (measurement is MeasurementValue<short> shortValue)
          {
            measurementObject.AddValue(shortValue.Value);
          }
          else
          {
            throw new ArgumentException(
              "Measurement is wrong type - should be SignalValue<double> or SignalValue<bool> (or added new)");
          }
        }
      }
    }
  }
}
