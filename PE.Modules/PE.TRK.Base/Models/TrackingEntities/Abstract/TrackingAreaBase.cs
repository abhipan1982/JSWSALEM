using System;
using System.Collections.Generic;
using PE.BaseDbEntity.EnumClasses;

namespace PE.BaseModels.AbstractionModels.L1A
{
  public abstract class TrackingAreaBase : AssetBase
  {
    #region ctor

    public TrackingAreaBase(bool isFinishArea, bool isStartArea, TrackingAreaBase previousArea,
      TrackingEventType trackingEventType, bool shouldBeIgnoredForManualOperations)
    {
      IsFinishArea = isFinishArea;
      IsStartArea = isStartArea;
      TrackingPoints = new List<ConfigurationTrackingPointBase>();
      TrackingEventType = trackingEventType;
      PreviousArea = previousArea;
      ShouldBeIgnoredForManualOperations = shouldBeIgnoredForManualOperations;
    }

    #endregion ctor

    #region properties

    public int TrackArea { get; protected set; }
    public DateTime TimeStamp { get; protected set; }
    public List<ConfigurationTrackingPointBase> TrackingPoints { get; protected set; }

    /// <summary>
    ///   When material leave that area - should tracking for this material be ended
    /// </summary>
    public bool IsFinishArea { get; }

    public bool IsStartArea { get; }

    public TrackingAreaBase PreviousArea { get; }
    public bool ShouldBeIgnoredForManualOperations { get; }

    #endregion properties
  }
}
