using System.Collections.Generic;
using PE.TRK.Base.Models._Base;

namespace PE.TRK.Base.Models.Configuration.Concrete
{
  public class ConfigurationCtrAreaBase : BaseCtrAreaBase
  {
    public List<ConfigurationTrackingPointBase> TrackingPoints { get; private set; }

    public ConfigurationCtrAreaBase(int areaAssetCode,
      int areaModeProductionFeatureCode,
      int areaModeAdjustionFeatureCode,
      int areaSimulationFeatureCode,
      int areaAutomaticReleaseFeatureCode,
      int areaEmptyFeatureCodeFeatureCode,
      int modeModeCobbleDetectedFeatureCode,
      int areaModeLocalFeatureCode,
      int areaCobbleDetectionSelectedFeatureCode,
      List<BaseCtrAreaBase> previousAreasBase,
      List<ConfigurationTrackingPointBase> trackingPoints)
      : base(areaAssetCode,
          areaModeProductionFeatureCode,
          areaModeAdjustionFeatureCode,
          areaSimulationFeatureCode,
          areaAutomaticReleaseFeatureCode,
          areaEmptyFeatureCodeFeatureCode,
          modeModeCobbleDetectedFeatureCode,
          areaModeLocalFeatureCode,
          areaCobbleDetectionSelectedFeatureCode,
          previousAreasBase)
    {
      TrackingPoints = trackingPoints;
    }
  }
}
