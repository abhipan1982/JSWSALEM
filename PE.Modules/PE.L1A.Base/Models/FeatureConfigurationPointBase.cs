using PE.BaseDbEntity.EnumClasses;

namespace PE.L1A.Base.Models
{
  public class FeatureConfigurationPointBase : ConfigurationPointBase
  {
    public AggregationStrategy AggregationStrategy { get; set; }
    public bool StoreSamples { get; set; }
    public long SampleOffset { get; set; }
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
    public bool IsVelocity { get; set; }
    public long ExtUnitOfMeasureId { get; set; }
    public bool IsLengthRelated { get; set; }


    public FeatureConfigurationPointBase(ConfigurationPointType configurationPointType,
      int featureCode,
      bool storeSamples,
      long sampleOffet,
      double? minValue,
      double? maxValue,
      bool isVelocity,
      long extUnitOfMeasureId,
      AggregationStrategy aggregationStrategy,
      bool isLengthRelated)
    : base(configurationPointType, featureCode)
    {
      AggregationStrategy = aggregationStrategy;
      StoreSamples = storeSamples;
      SampleOffset = sampleOffet;
      MinValue = minValue;
      MaxValue = maxValue;
      IsVelocity = isVelocity;
      ExtUnitOfMeasureId = extUnitOfMeasureId;
      IsLengthRelated = isLengthRelated;
    }
  }
}
