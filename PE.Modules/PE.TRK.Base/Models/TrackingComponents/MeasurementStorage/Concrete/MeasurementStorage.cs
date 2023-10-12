using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PE.BaseModels.DataContracts.Internal.MVH;
using PE.TRK.Base.Models.TrackingComponents.MeasurementStorage.Abstract;

namespace PE.TRK.Base.Models.TrackingComponents.MeasurementStorage.Concrete
{
  public class MeasurementStorage<T>
  {
    #region ctor

    public MeasurementStorage(MeasurementBase measurement)
    {
      _lock = new object();
      _measuredValues = new List<T>();
      _samples = new List<DcSample>();

      MeasurementId = measurement.Id;
      _shouldStoreLimitedSamplesAmount = measurement.ShouldStoreLimitedSamplesAmount;
      _limitedSamplesAmount = measurement.SamplesAmount;
      _measurementType = measurement.MeasurementType;
    }

    #endregion

    #region properties

    public int MeasurementId { get; }

    #endregion

    #region members

    private readonly List<T> _measuredValues;
    private readonly List<DcSample> _samples;
    private readonly bool _shouldStoreLimitedSamplesAmount;
    private readonly int _limitedSamplesAmount;
    private readonly object _lock;
    private readonly MeasurementTypes _measurementType;

    #endregion

    #region public methods

    public void AddValue(T value)
    {
      lock (_lock)
      {
        _measuredValues.Add(value);
      }
    }

    public void AddSample(DcSample value)
    {
      lock (_lock)
      {
        if (!_shouldStoreLimitedSamplesAmount)
        {
          _samples.Add(value);
        }

        if (_shouldStoreLimitedSamplesAmount && _limitedSamplesAmount > _samples.Count)
        {
          _samples.Add(value);
        }
      }
    }

    public List<DcSample> GetSamples()
    {
      return _samples;
    }

    public List<T> GetMeasuredValues()
    {
      return _measuredValues;
    }

    public MeasurementResult GetResultForSending()
    {
      lock (_lock)
      {
        MeasurementResult measurementResult;
        List<T> measuredValuesResult = new List<T>();
        List<DcSample> samplesResult = new List<DcSample>();

        samplesResult.AddRange(GetSamples());
        measuredValuesResult.AddRange(GetMeasuredValues());

        if (_measurementType == MeasurementTypes.BoolMeasurement)
        {
          bool emptyMeasurements = !measuredValuesResult.Any();

          measurementResult = new MeasurementResult(
            MeasurementId,
            emptyMeasurements ? 0.0 : measuredValuesResult.Any(bv => !(bv as bool?).Value) ? 0.0 : 1.0,
            emptyMeasurements ? 0.0 : measuredValuesResult.Any(bv => (bv as bool?).Value) ? 1.0 : 0.0,
            emptyMeasurements ? 0.0 :
            measuredValuesResult.Count(bv => (bv as bool?).Value) >=
            measuredValuesResult.Count(bv => !(bv as bool?).Value) ? 1.0 : 0.0,
            samplesResult);

          _samples.Clear();
          _measuredValues.Clear();

          return measurementResult;
        }

        if (_measurementType == MeasurementTypes.DoubleMeasurement)
        {
          bool emptyMeasurements = !measuredValuesResult.Any();
          List<double> doubleValues = measuredValuesResult.Select(mv => (mv as double?).Value).ToList();

          measurementResult = new MeasurementResult(
            MeasurementId,
            emptyMeasurements ? 0.0 : doubleValues.Min(),
            emptyMeasurements ? 0.0 : doubleValues.Max(),
            emptyMeasurements ? 0.0 : doubleValues.Average(),
            samplesResult);

          _samples.Clear();
          _measuredValues.Clear();

          return measurementResult;
        }

        throw new InvalidEnumArgumentException("enum MeasurementTypes Wrong value");
      }
    }

    #endregion
  }
}
