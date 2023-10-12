using System.Collections.Generic;
using PE.BaseModels.DataContracts.Internal.MVH;

namespace PE.TRK.Base.Models.TrackingComponents.MeasurementStorage.Concrete
{
  public class MeasurementResult
  {
    #region ctor

    public MeasurementResult(int measurementId, double minValue, double maxValue, double averageValue,
      List<DcSample> samples)
    {
      MeasurementId = measurementId;
      MinValue = minValue;
      MaxValue = maxValue;
      AverageValue = averageValue;
      Samples = samples;
    }

    #endregion

    #region properties

    public int MeasurementId { get; }
    public double MinValue { get; }
    public double MaxValue { get; }
    public double AverageValue { get; }
    public List<DcSample> Samples { get; }

    #endregion
  }
}
