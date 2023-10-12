using System.Collections.Generic;
using PE.TRK.Base.Models._Base;
using PE.TRK.Base.Models.Configuration.Concrete;

namespace PE.TRK.Base.Models.Configuration.Abstract
{
  public abstract class ConfigurationSimpleAreaBase : TrackingSimpleAreaBase
  {
    public List<ConfigurationTrackingPointBase> TrackingPoints { get; private set; }

    protected ConfigurationSimpleAreaBase(short areaAssetCode, List<ConfigurationTrackingPointBase> trackingPoints)
    : base(areaAssetCode)
    {
      TrackingPoints = trackingPoints;
    }
  }
}
