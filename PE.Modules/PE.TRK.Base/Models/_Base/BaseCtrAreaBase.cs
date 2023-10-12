using System.Collections.Generic;

namespace PE.TRK.Base.Models._Base
{
  public abstract class BaseCtrAreaBase : TrackingSimpleAreaBase
  {
    public int AreaModeProductionFeatureCode { get; protected set; }
    public int AreaModeAdjustionFeatureCode { get; protected set; }
    public int AreaSimulationFeatureCode { get; protected set; }
    public int AreaAutomaticReleaseFeatureCode { get; protected set; }
    public int AreaEmptyFeatureCodeFeatureCode { get; protected set; }
    public int AreaCobbleDetectedFeatureCode { get; protected set; }
    public int AreaModeLocalFeatureCode { get; protected set; }
    public int AreaCobbleDetectionSelectedFeatureCode { get; protected set; }
    public List<BaseCtrAreaBase> PreviousAreas { get; private set; }
    
    public BaseCtrAreaBase(
      int areaAssetCode,
      int areaModeProductionFeatureCode,
      int areaModeAdjustionFeatureCode,
      int areaSimulationFeatureCode,
      int areaAutomaticReleaseFeatureCode,
      int areaEmptyFeatureCodeFeatureCode,
      int modeModeCobbleDetectedFeatureCode,
      int areaModeLocalFeatureCode,
      int areaCobbleDetectionSelectedFeatureCode,
      List<BaseCtrAreaBase> previousAreas)
      : base(areaAssetCode)
    {
      AreaModeProductionFeatureCode = areaModeProductionFeatureCode;
      AreaModeAdjustionFeatureCode = areaModeAdjustionFeatureCode;
      AreaSimulationFeatureCode = areaSimulationFeatureCode;
      AreaAutomaticReleaseFeatureCode = areaAutomaticReleaseFeatureCode;
      AreaEmptyFeatureCodeFeatureCode = areaEmptyFeatureCodeFeatureCode;
      AreaCobbleDetectedFeatureCode = modeModeCobbleDetectedFeatureCode;
      AreaModeLocalFeatureCode = areaModeLocalFeatureCode;
      AreaCobbleDetectionSelectedFeatureCode = areaCobbleDetectionSelectedFeatureCode;
      PreviousAreas = previousAreas;
    }

    public void SetPreviousAreas(List<BaseCtrAreaBase> previousAreas)
    {
      PreviousAreas = previousAreas;
    }
  }
}
