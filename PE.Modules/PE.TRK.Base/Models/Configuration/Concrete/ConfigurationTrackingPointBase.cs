using PE.TRK.Base.Models._Base;

namespace PE.TRK.Base.Models.Configuration.Concrete
{
  public class ConfigurationTrackingPointBase : TrackingPointBase
  {
    public ConfigurationTrackingPointBase(int occupiedFeatureCode, int trackingPointAssetCode, int orderSeq, bool isShear)
    : base(occupiedFeatureCode, trackingPointAssetCode, orderSeq, isShear)
    {
    }
  }
}
